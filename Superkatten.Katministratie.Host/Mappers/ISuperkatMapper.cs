using Superkatten.Katministratie.Host.Entities;

using contractEntities = Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.Host.Mappers
{
    public interface ISuperkatMapper
    {
        CatBehaviour MapToContract(contractEntities.CatBehaviour behaviour);
        Gender MapToContract(contractEntities.Gender gender);
        CatArea MapToContract(contractEntities.CatArea gender);
    }
}
