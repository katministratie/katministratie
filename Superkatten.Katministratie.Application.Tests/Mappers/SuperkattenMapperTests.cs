using FluentAssertions;
using Moq;
using Superkatten.Katministratie.Application.Mappers;
using Superkatten.Katministratie.Domain.Entities;
using Xunit;

namespace Superkatten.Katministratie.Application.Tests.Mappers
{
    public class SuperkattenMapperTests
    {
        private readonly string KAT_NAME = "katName";
        private readonly int KAT_LOCATION = 1;
        private readonly int KAT_NUMBER = 1;
        [Fact]
        public void SuperkattenMapper_MapToDomain_Success()
        {
            //arrange
            var foundDate = DateTime.Now;
            var sut = new SuperkattenMapper();
            var contractSuperkat = new Contracts.Superkat()
            {
                Number = KAT_NUMBER,
                Name = KAT_NAME,
                FoundDate = foundDate,
                Location = KAT_LOCATION,
                Picture = It.IsAny<string>()
            };

            // act
            var result = sut.MapToDomain(contractSuperkat);

            // assert
            result.Should().BeEquivalentTo(new Superkat(
                KAT_NUMBER,
                KAT_NAME,
                foundDate,
                KAT_LOCATION
            ));
        }

        [Fact]
        public void SupperkattenMapper_MapFromDomain_Success()
        {
            // arrange
            var foundDate = DateTime.Now;
            var sut = new SuperkattenMapper();
            var superkat = new Superkat(
                KAT_NUMBER,
                KAT_NAME,
                foundDate,
                KAT_LOCATION
            );

            // act
            var result = sut.MapFromDomain(superkat);

            // assert
            result.Should().BeEquivalentTo(new Contracts.Superkat {
                Number = KAT_NUMBER,
                Name = KAT_NAME,
                FoundDate = foundDate,
                Location = KAT_LOCATION,
                Picture = String.Empty
            });
        }

    }
}
