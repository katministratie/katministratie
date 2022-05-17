using Superkatten.Katministratie.Domain.Entities;
using Superkatten.Katministratie.Infrastructure.Entities;

namespace Superkatten.Katministratie.Infrastructure.Mapper;

public interface IGastgezinRepositoryMapper
{
    public GastgezinDto MapDomainToRepository(Gastgezin superkat);
    public Gastgezin MapRepositoryToDomain(GastgezinDto superkatDto);
}
