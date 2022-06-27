using Superkatten.Katministratie.Host.Entities;
using System.ComponentModel;

using contractEntities = Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.Host.Mappers
{
    public class SuperkatMapper : ISuperkatMapper
    {
        public Superkat MapContractToHost(contractEntities.Superkat superkat)
        {
            return new Superkat
            {
                Id = superkat.Id,
                Name = superkat.Name,
                CageNumber = superkat.CageNumber,
                CatchDate = superkat.CatchDate,
                Birthday = superkat.Birthday,
                Behaviour = superkat.Behaviour,
                CatArea = superkat.CatArea,
                CatchLocation = superkat.CatchLocation,
                Gender = superkat.Gender,
                IsKitten = superkat.IsKitten,
                Number = superkat.Number,
                Reserved = superkat.Reserved,
                Retour = superkat.Retour,
            };
        }

        public contractEntities.Superkat MapHostToContract(Superkat superkat)
        {
            return new contractEntities.Superkat
            {
                Id = superkat.Id,
                Name = superkat.Name,
                CageNumber = superkat.CageNumber,
                CatchDate = superkat.CatchDate,
                Birthday = superkat.Birthday,
                Behaviour = superkat.Behaviour,
                CatArea = superkat.CatArea,
                CatchLocation = superkat.CatchLocation,
                Gender = superkat.Gender,
                IsKitten = superkat.IsKitten,
                Number = superkat.Number,
                Reserved = superkat.Reserved,
                Retour = superkat.Retour
            };
        }
    }
}
