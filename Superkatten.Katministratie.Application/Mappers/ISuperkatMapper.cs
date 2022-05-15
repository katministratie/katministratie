using Superkatten.Katministratie.Domain.Entities;

using contractEntitiess = Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.Application.Mappers
{
    public interface ISuperkatMapper
    {
        Superkat MapToDomain(contractEntitiess.Superkat superkat);
        CatArea MapToDomain(contractEntitiess.CatArea area);
        CatBehaviour MapToDomain(contractEntitiess.CatBehaviour behaviour);
        Gender MapToDomain(contractEntitiess.Gender gender);

        contractEntitiess.Superkat MapToContract(Superkat createdSuperkat);
    }
}
