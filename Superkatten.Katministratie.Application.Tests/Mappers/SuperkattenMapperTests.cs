using FluentAssertions;
using Moq;
using Superkatten.Katministratie.Application.Mappers;
using Superkatten.Katministratie.Domain.Entities;
using Xunit;

namespace Superkatten.Katministratie.Application.Tests.Mappers
{
    public class SuperkattenMapperTests
    {
        [Fact]
        public void SuperkattenMapper_MapToDomain_Success()
        {
            //arrange
            const string KAT_NAAM = "kat_under_test";
            var sut = new SuperkattenMapper();
            var contractSuperkat = new Contracts.Superkat() { Number = 1, Name = KAT_NAAM };

            // act
            var result = sut.MapToDomain(contractSuperkat);

            // assert
            result.Should().BeEquivalentTo(new Superkat(
                1, 
                KAT_NAAM, 
                It.IsAny<DateTimeOffset>()
            ));
        }

        [Fact]
        public void SupperkattenMapper_MapFromDomain_Success()
        {
            // arrange
            const string KAT_NAAM = "kat_under_test";
            var sut = new SuperkattenMapper();
            var superkat = new Superkat(1, KAT_NAAM, It.IsAny<DateTimeOffset>());

            // act
            var result = sut.MapFromDomain(superkat);

            // assert
            result.Should().BeEquivalentTo(new Contracts.Superkat {
                Number = 1,
                Name = KAT_NAAM,
                FoundDate = It.IsAny<DateTimeOffset>()
            });
        }

    }
}
