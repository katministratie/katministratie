using Superkatten.Katministratie.Domain.Entities;
using System.Linq;
using ContractEntities = Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.Application.Mappers;

public class GastgezinMapper : IGastgezinMapper
{
    public ContractEntities.Gastgezin MapDomainToContract(Gastgezin gastgezin)
    {
        return new ContractEntities.Gastgezin
        {
            Id = gastgezin.Id,
            Name = gastgezin.Name,
            Address = gastgezin.Address,
            City = gastgezin.City,
            Phone = gastgezin.Phone
        };
    }
}
