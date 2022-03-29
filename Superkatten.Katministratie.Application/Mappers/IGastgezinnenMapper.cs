using Superkatten.Katministratie.Application.Contracts;

using DomainEntities = Superkatten.Katministratie.Domain.Entities;

namespace Superkatten.Katministratie.Application.Mappers
{
    public interface IGastgezinnenMapper
    {
        public DomainEntities.Gastgezin MapToDomain(Gastgezin superkat);
        public Gastgezin MapFromDomain(DomainEntities.Gastgezin superkat);
    }
}
