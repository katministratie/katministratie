using Moq;
using Superkatten.Katministratie.Domain;
using Superkatten.Katministratie.Infrastructure.Entities;
using Superkatten.Katministratie.Infrastructure.Mappers;
using Xunit;

namespace Superkatten.Katministratie.Infrastructure.Tests.Mappers;

public class SuperkatMapperTests
{
    [Fact]
    public void MapFromDomain_SuppliedSuperkatDomainInstance_CreatedSuperkatDatabaseInstance()
    {
        // arrange
        const int YEAR = 2023;
        const int MONTH = 10;
        const int DAY = 23;

        var entered = new DateTime(YEAR, MONTH, DAY);

        const int SUPERKAT_NUMBER = 55;
        var domainSuperkat = new Superkat(SUPERKAT_NUMBER, entered);
        var expectedSuperkatDb = new SuperkatDb()
        {
            Id = It.IsAny<int>(),
            Number = SUPERKAT_NUMBER,
            Entered = new DateTime(YEAR, MONTH, DAY),
        };

        var sut = new SuperkatMapper();

        // act
        var superkatDb = sut.MapFromDomain(domainSuperkat);

        // assert
        Assert.Equivalent(expectedSuperkatDb, superkatDb);
    }


    [Fact]
    public void MapToDomain_SuppliedSuperkatDbInstance_CreatedSuperkatDomainInstance()
    {
        // arrange
        const int YEAR = 2023;
        const int MONTH = 10;
        const int DAY = 23;

        var entered = new DateTime(YEAR, MONTH, DAY);

        const int SUPERKAT_NUMBER = 55;
        var superkatDb = new SuperkatDb() {
            Id = 1,
            Number = SUPERKAT_NUMBER,
            Entered = new DateTime(YEAR, MONTH, DAY),
        };
        var expectedSuperkat = new Superkat(SUPERKAT_NUMBER, entered);

        var sut = new SuperkatMapper();

        // act
        var superkat = sut.MapToDomain(superkatDb);

        // assert
        Assert.Equivalent(expectedSuperkat, superkat);
    }
}
