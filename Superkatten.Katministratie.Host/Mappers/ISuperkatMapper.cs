using Superkatten.Katministratie.Host.Entities;

using ContractEntities = Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.Host.Mappers;

public interface ISuperkatMapper
{
    Superkat MapContractToHost(ContractEntities.Superkat contract);

    ContractEntities.Superkat MapHostToContract(Superkat contract);
}
