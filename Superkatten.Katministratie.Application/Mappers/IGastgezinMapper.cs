using Superkatten.Katministratie.Domain.Entities;
using ContractEntities = Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.Application.Mappers;

public interface IGastgezinMapper
{
    public ContractEntities.Gastgezin MapDomainToContract(Gastgezin gastgezin);
}
