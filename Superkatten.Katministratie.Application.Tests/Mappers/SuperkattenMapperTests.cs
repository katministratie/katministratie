using FluentAssertions;
using Moq;
using Superkatten.Katministratie.Application.Mappers;
using Superkatten.Katministratie.Domain.Entities;
using Xunit;

namespace Superkatten.Katministratie.Application.Tests.Mappers
{
    public class SuperkattenMapperTests
    {
        private readonly string SUPERKAT_COLOR = "red";
        private readonly string SUPERKAT_CATCH_LOCATION = "rhenoy";
        private readonly int SUPERKAT_NUMBER = 1;
        [Fact]
        public void SuperkattenMapper_MapToDomain_Success()
        {
            //arrange
            var foundDate = DateTime.Now;
            var sut = new SuperkattenMapper();
            var contractSuperkat = new Contracts.Superkat()
            {
                Number = SUPERKAT_NUMBER,
                Kleur = SUPERKAT_COLOR,
                FoundDate = foundDate,
                CatchLocation = SUPERKAT_CATCH_LOCATION,
                Birthday = foundDate.AddDays(-1),
                Name = string.Empty
            };

            // act
            var result = sut.MapToDomain(contractSuperkat);

            // assert
            result.Should().BeEquivalentTo(
                new Superkat(
                    number: SUPERKAT_NUMBER,
                    kleur: SUPERKAT_COLOR,
                    foundDate: foundDate,
                    catchLocation: SUPERKAT_CATCH_LOCATION
                ));
        }

        [Fact]
        public void SupperkattenMapper_MapFromDomain_Success()
        {
            // arrange
            var foundDate = DateTime.Now;
            var sut = new SuperkattenMapper();
            var superkat = new Superkat(
                SUPERKAT_NUMBER,
                SUPERKAT_COLOR,
                foundDate,
                SUPERKAT_CATCH_LOCATION
            );

            // act
            var result = sut.MapFromDomain(superkat);

            // assert
            result.Should().BeEquivalentTo(
                new Contracts.Superkat 
                {
                    Number = SUPERKAT_NUMBER,
                    Name = null,
                    FoundDate = foundDate,
                    CatchLocation = SUPERKAT_CATCH_LOCATION
                });
        }

    }
}
