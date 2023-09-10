using Moq;
using Superkatten.Katministratie.Application.Contracts.Entities;
using Superkatten.Katministratie.Application.Contracts.Parameters;
using Superkatten.Katministratie.Application.Mappers;
using Superkatten.Katministratie.Application.Services;
using Superkatten.Katministratie.Application.Utils;
using Superkatten.Katministratie.Domain;
using Superkatten.Katministratie.Domain.Shared;
using Superkatten.Katministratie.Infrastructure.Services;
using System.Collections.ObjectModel;
using Xunit;

namespace Superkatten.Katministratie.Application.Tests.Services;

public class SuperkatServiceTests
{
    [Fact]
    public void GetSuperkattenAsync_ValidSuperkatAvailable_returnsSuperkatDto()
    {
        // arrange
        const int DAY = 15;
        const int MONTH = 8;
        const int YEAR = 2023;

        var entered = new DateTime(YEAR, MONTH, DAY);

        var superkattenCollection = new List<Superkat>()
        {
            new Superkat(1, entered),
            new Superkat(2, entered),
        };

        var repository = new Mock<ISuperkattenRepository>();
        repository.Setup(x => x.GetSuperkattenAsync())
            .Returns(() => Task.FromResult(superkattenCollection));

        var systemTime = new Mock<ISystemTime>();
        systemTime
            .Setup(x => x.UtcNow)
            .Returns(entered);

        var expectedSuperkatDtoCollection = new Collection<SuperkatDto>()
        {
            new SuperkatDto() { Number = 1, Entered = new DateTime(YEAR, MONTH, DAY), },
            new SuperkatDto() { Number = 2, Entered = new DateTime(YEAR, MONTH, DAY), },
        };

        var mapper = new SuperkatMapper();

        var sut = new SuperkatService(repository.Object, mapper, systemTime.Object);

        // act
        var task = sut.GetSuperkattenAsync();

        // assert
        Assert.Equivalent(expectedSuperkatDtoCollection, task.Result);
    }

    [Fact]
    public void CreateSuperkat_ValidData_SuperkatCreated()
    {
        // arrange
        const int DAY = 15;
        const int MONTH = 8;
        const int YEAR = 2023;
        const int CURRENT_MAX_CAT_NUMBER = 34;
        const int CAGE_NUMBER = 10;
        const int EXPECTED_SUPERKAT_NUMBER = 35;

        var entered = new DateTime(YEAR, MONTH, DAY);

        var expectedSuperkat = new Superkat(EXPECTED_SUPERKAT_NUMBER, entered);

        var mapper = new SuperkatMapper();

        var repository = new Mock<ISuperkattenRepository>();
        repository
            .Setup(x => x.GetMaxSuperkatNumberForYear(YEAR))
            .Returns(() => Task.FromResult(CURRENT_MAX_CAT_NUMBER));

        var systemTime = new Mock<ISystemTime>();
        systemTime
            .Setup(x => x.UtcNow)
            .Returns(new DateTime(YEAR, MONTH, DAY));
        var newSuperkatParameters = new NewSuperkatParameters()
        {
            RefugeArea = RefugeArea.Quarantine,
            CageNumber = CAGE_NUMBER
        };

        var sut = new SuperkatService(repository.Object, mapper, systemTime.Object);

        // act
        var superkat = sut.CreateSuperkatAsync(newSuperkatParameters);

        // assert
        Assert.Equal(expectedSuperkat.Number, superkat.Result?.Number);
    }
}
