using Superkatten.Katministratie.Application.Entities;

namespace Superkatten.Katministratie.Application.Mappers
{
    public class SuperkattenMapper : ISuperkattenMapper
    {
        public Superkat MapFromDomain(Domain.Entities.Superkat superkat)
        {
            return new Superkat
            {
                Id = superkat.Id,
                Number = superkat.Number,
                Name = superkat.Name,
                CatchDate = superkat.CatchDate,
                CatchLocation = superkat.CatchLocation,
                Birthday = superkat.Birthday,
                Reserved = superkat.Reserved,
                Retour = superkat.Retour,
                Area = superkat.Area,
                CageNumber = superkat.CageNumber,
                Behaviour = superkat.Behaviour,
                IsKitten = superkat.IsKitten,
                Gender = superkat.Gender,
            };
        }
    }
}
