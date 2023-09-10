using Moq;
using Superkatten.Katministratie.Application.Contracts.Entities;
using Superkatten.Katministratie.Application.Mappers;
using Superkatten.Katministratie.Domain;
using Xunit;

namespace Superkatten.Katministratie.Application.Tests.Mappers
{
    public class SuperkatMapperTests
    {
        [Fact]
        public void Map_Superkat_ReturnsSuperkatDto()
        {
            // arrange
            const int YEAR = 2023;
            const int MONTH = 10;
            const int DAY = 23;

            var entered = new DateTime(YEAR, MONTH, DAY);

            var superkat = new Superkat(11, entered);
            var expectedSuperkatDto = new SuperkatDto 
            { 
                Number = 11,
                Entered = entered,
            };

            var sut = new SuperkatMapper();

            // act
            var superkatDto = sut.MapFromDomain(superkat);

            // assert
            Assert.Equivalent(expectedSuperkatDto, superkatDto);
        }
    }
}