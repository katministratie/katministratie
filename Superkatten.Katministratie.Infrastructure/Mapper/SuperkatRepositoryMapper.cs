using Superkatten.Katministratie.Domain.Entities;
using Superkatten.Katministratie.Infrastructure.Entities;

namespace Superkatten.Katministratie.Infrastructure.Mapper
{
    public class SuperkatRepositoryMapper : ISuperkatRepositoryMapper
    {
        public SuperkatDto MapDomainToSuperkatDto(Superkat superkat)
        {
            return new SuperkatDto
            {
                Number = superkat.Number,
                Name = superkat.Name,
                FoundDate = superkat.FoundDate
            };
        }

        public Superkat MapSuperkatDtoToDomain(SuperkatDto superkatDto)
        {
            return new Superkat(
                superkatDto.Number,
                superkatDto.Name,
                superkatDto.FoundDate
            );
        }
    }
}
