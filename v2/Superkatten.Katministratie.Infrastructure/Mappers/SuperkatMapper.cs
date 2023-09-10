using Superkatten.Katministratie.Domain;
using Superkatten.Katministratie.Infrastructure.Entities;

namespace Superkatten.Katministratie.Infrastructure.Mappers;

public class SuperkatMapper : ISuperkatMapper
{
    public Superkat MapToDomain(SuperkatDb superkatDb)
    {
        return new Superkat(superkatDb.Number, superkatDb.Entered)
        {     
            
        };
    }

    public SuperkatDb MapFromDomain(Superkat superkat)
    {
        return new SuperkatDb()
        {
            Number = superkat.Number,
            Entered = superkat.Entered,
        };
    }
}
