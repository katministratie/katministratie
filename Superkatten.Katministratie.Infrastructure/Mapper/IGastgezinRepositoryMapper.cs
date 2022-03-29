using Superkatten.Katministratie.Domain.Entities;
using Superkatten.Katministratie.Infrastructure.Entities;

namespace Superkatten.Katministratie.Infrastructure.Mapper
{
    public interface IGastgezinRepositoryMapper
    {
        public GastgezinDto MapDomainToGastgezinDto(Gastgezin superkat);
        public Gastgezin MapGastgezinDtoToDomain(GastgezinDto superkatDto);
    }
}
