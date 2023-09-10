using Superkatten.Katministratie.Application.Contracts.Entities;
using Superkatten.Katministratie.Domain;

namespace Superkatten.Katministratie.Application.Mappers;

public class SuperkatMapper : ISuperkatMapper
{
    public SuperkatDto MapFromDomain(Superkat superkat)
    {
        return new SuperkatDto
        {
            Number = superkat.Number,
            Entered = superkat.Entered,
        };
    }
}
