using Superkatten.Katministratie.Application.Entities;
using DomainEntities = Superkatten.Katministratie.Domain.Entities;

namespace Superkatten.Katministratie.Application.Mappers
{
    public interface IGastgezinnenMapper
    {
        public Gastgezin MapFromDomain(DomainEntities.Gastgezin superkat);
    }
}
