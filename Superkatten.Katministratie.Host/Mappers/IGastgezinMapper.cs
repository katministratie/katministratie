using Superkatten.Katministratie.Host.Entities;
using ContractEntities = Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.Host.Mappers
{
    public interface IGastgezinMapper
    {
        ContractEntities.Gastgezin MapHostToContract(Gastgezin gastgezin);
        Gastgezin MapContractToHost(ContractEntities.Gastgezin gastgezin);
    }
}
