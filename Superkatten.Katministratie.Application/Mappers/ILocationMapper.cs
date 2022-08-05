using Superkatten.Katministratie.Domain.Entities;

using ContractEntities = Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.Application.Mappers;

public interface ILocationMapper
{
    public ContractEntities.Location MapDomainToContract(Location location);
}
