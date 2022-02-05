using FluentAssertions;
using Moq;
using Superkatten.Katministratie.Domain.Entities;
using Superkatten.Katministratie.Infrastructure.Entities;
using Superkatten.Katministratie.Infrastructure.Mapper;
using Xunit;

namespace Superkatten.Katministratie.Infrastructure.Tests.Mapper
{
    public class SuperkatRepositoryMapperTests
    {
        private readonly string KAT_NAME = "katnaam";
        private readonly int KAT_LOCATION = 2;
        private readonly int KAT_NUMBER = 1;
        [Fact]
        public void SuperkatRepositoryMapper_MapDomainToSuperkatDto_Success()
        {
            // arrange
            var sut = new SuperkatRepositoryMapper();
            var superkat = new Superkat(
                KAT_NUMBER,
                KAT_NAME, 
                It.IsAny<DateTimeOffset>(),
                KAT_LOCATION
            );

            // act
            var superkatDto = sut.MapDomainToSuperkatDto(superkat);

            // assert
            superkatDto
                .Should()
                .BeEquivalentTo(
                new SuperkatDto
                {
                    Id = It.IsAny<int>(),
                    Number = KAT_NUMBER,
                    Name = KAT_NAME,
                    Location = KAT_LOCATION,
                    FoundDate = It.IsAny<DateTimeOffset>(),
                    Details = new List<SuperkatDetailsDto>()
                });

        }

        [Fact]
        public void SuperkatRepositoryMapperTests_MapSuperkatDtoToDomain_Success()
        {
            // arrange
            var sut = new SuperkatRepositoryMapper();
            var superkatDto = new SuperkatDto
            {
                Id = It.IsAny<int>(),
                Number = KAT_NUMBER,
                Name = KAT_NAME,
                Location = KAT_LOCATION,
                FoundDate = It.IsAny<DateTimeOffset>(),
                Details = new List<SuperkatDetailsDto>()
            };

            // act
            var superkat = sut.MapSuperkatDtoToDomain(superkatDto);

            // assert
            superkat
                .Should()
                .BeEquivalentTo(
                new Superkat(
                    KAT_NUMBER,
                    KAT_NAME,
                    It.IsAny<DateTimeOffset>(),
                    KAT_LOCATION
                ));
        }
    }
}
