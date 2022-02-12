using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Superkatten.Katministratie.Application.Services;
using Moq;
using Microsoft.Extensions.Logging;
using Superkatten.Katministratie.Domain.Interfaces;
using Superkatten.Katministratie.Application.Mappers;
using Superkatten.Katministratie.Application.Contracts;
using FluentAssertions;
using Superkatten.Katministratie.Application.Exceptions;

namespace Superkatten.Katministratie.Application.Tests.Service
{
    public class SuperkatServiceTests
    {
        [Fact]
        public async void SuperkatService_CreateSuperkat_HasValidNumber()
        {
            // arrange
            const int SUPERKATTEN_COUNT = 10;
            const string SUPERKAT_NAME = "SuperkatName";
            const int SUPERKAT_LOCATION = 10;

            var superkattenRepository = new Mock<ISuperkattenRepository>();
            superkattenRepository
                .Setup(s => s.GetSuperkatCountForGivenYearAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(SUPERKATTEN_COUNT));

            var superkattenMapper = new SuperkattenMapper();

            var superkattenService = new SuperkattenService(Mock.Of<ILogger<SuperkattenService>>(), superkattenRepository.Object, superkattenMapper);

            var createSuperkatParameters = new CreateSuperkatParameters
            {
                Name = SUPERKAT_NAME,
                Location = SUPERKAT_LOCATION
            };

            // act
            var superkat = await superkattenService.CreateSuperkatAsync(createSuperkatParameters);

            //assert
            superkat.Number.Should().Be(SUPERKATTEN_COUNT + 1);
        }

        [Fact]
        public async void SuperkatService_CreateSuperkatWithEmptyName_ThrowsException()
        {
            // arrange
            const int SUPERKATTEN_COUNT = 10;
            const int SUPERKAT_LOCATION = 10;

            var superkattenRepository = new Mock<ISuperkattenRepository>();
            superkattenRepository
                .Setup(s => s.GetSuperkatCountForGivenYearAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(SUPERKATTEN_COUNT));

            var superkattenMapper = new SuperkattenMapper();

            var superkattenService = new SuperkattenService(Mock.Of<ILogger<SuperkattenService>>(), superkattenRepository.Object, superkattenMapper);

            var createSuperkatParameters = new CreateSuperkatParameters
            {
                Name = String.Empty,
                Location = SUPERKAT_LOCATION
            };

            // act
            var act = () => superkattenService.CreateSuperkatAsync(createSuperkatParameters);

            //assert
            await act
                .Should()
                .ThrowExactlyAsync<ValidationException>()
                .WithMessage("Superkat name is empty");
        }

        [Fact]
        public async void SuperkatService_CreateSuperkatWithInvallidLocation_ThrowsException()
        {
            // arrange
            const int SUPERKATTEN_COUNT = 10;
            const string SUPERKAT_NAME = "SuperkatName";
            const int SUPERKAT_INVALLID_LOCATION = -5;

            var superkattenRepository = new Mock<ISuperkattenRepository>();
            superkattenRepository
                .Setup(s => s.GetSuperkatCountForGivenYearAsync(DateTime.Now.Year))
                .Returns(Task.FromResult(SUPERKATTEN_COUNT));

            var superkattenMapper = new SuperkattenMapper();

            var superkattenService = new SuperkattenService(Mock.Of<ILogger<SuperkattenService>>(), superkattenRepository.Object, superkattenMapper);

            var createSuperkatParameters = new CreateSuperkatParameters
            {
                Name = SUPERKAT_NAME,
                Location = SUPERKAT_INVALLID_LOCATION
            };

            // act
            var act = () => superkattenService.CreateSuperkatAsync(createSuperkatParameters);

            //assert
            await act
                .Should()
                .ThrowExactlyAsync<ValidationException>()
                .WithMessage("Superkat location * is invallid");
        }
    }
}
