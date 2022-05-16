using Superkatten.Katministratie.Domain.Entities;

using ContractEntitiess = Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.Application.Mappers
{
    public interface ISuperkatMapper
    {
        Superkat MapContractToDomain(ContractEntitiess.Superkat superkat);

        ContractEntitiess.Superkat MapDomainToContract(Superkat createdSuperkat);


        CatArea MapContractToDomain(ContractEntitiess.CatArea area);

        CatBehaviour MapContractToDomain(ContractEntitiess.CatBehaviour behaviour);

        Gender MapContractToDomain(ContractEntitiess.Gender gender);
    }
}
