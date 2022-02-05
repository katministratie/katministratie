using Superkatten.Katministratie.Domain.Entities;
using Superkatten.Katministratie.Infrastructure.Entities;

namespace Superkatten.Katministratie.Infrastructure.Mapper
{
    public interface ISuperkatRepositoryMapper
    {
        public SuperkatDto MapDomainToSuperkatDto(Superkat superkat);
        public Superkat MapSuperkatDtoToDomain(SuperkatDto superkatDto);
    }
}
