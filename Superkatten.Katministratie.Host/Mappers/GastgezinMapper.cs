using Superkatten.Katministratie.Host.Entities;
using System.ComponentModel;

using contractEntities = Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.Host.Mappers
{
    public class GastgezinMapper : IGastgezinMapper
    {
        private static contractEntities.Superkat MapToContract(Superkat superkat)
        {
            return new contractEntities.Superkat
            {
                Name = superkat.Name,
                Birthday = superkat.Birthday,
                CageNumber = superkat.CageNumber,
                CatchDate = superkat.CatchDate,
                CatchLocation = superkat.CatchLocation,
                IsKitten = superkat.IsKitten,
                Number = superkat.Number,
                Reserved = superkat.Reserved,
                Retour = superkat.Retour,
                Gender = MapToContract(superkat.Gender),
                CatArea = MapToContract(superkat.CatArea),
                Behaviour = MapToContract(superkat.Behaviour)
            };
        }
        private static contractEntities.CatBehaviour MapToContract(CatBehaviour behaviour)
        {
            return behaviour switch
            {
                CatBehaviour.Social => contractEntities.CatBehaviour.Social,
                CatBehaviour.Shy => contractEntities.CatBehaviour.Shy,
                CatBehaviour.Unknown => contractEntities.CatBehaviour.Unknown,
                _ => throw new InvalidEnumArgumentException(nameof(behaviour), (int)behaviour, typeof(CatBehaviour))
            };
        }
        private static contractEntities.Gender MapToContract(Gender gender)
        {
            return gender switch
            {
                Gender.Tomcat => contractEntities.Gender.Tomcat,
                Gender.Molly => contractEntities.Gender.Molly,
                Gender.Unknown => contractEntities.Gender.Unknown,
                _ => throw new InvalidEnumArgumentException(nameof(gender), (int)gender, typeof(Gender))
            };
        }
        private static contractEntities.CatArea MapToContract(CatArea catArea)
        {
            return catArea switch
            {
                CatArea.LargeCage => contractEntities.CatArea.LargeCage,
                CatArea.SmallCage => contractEntities.CatArea.SmallCage,
                CatArea.Unknown => contractEntities.CatArea.Unknown,
                _ => throw new InvalidEnumArgumentException(nameof(catArea), (int)catArea, typeof(CatArea))
            };
        }

        public List<contractEntities.Superkat> MapHostToContract(List<Superkat> superkatten)
        {
            return superkatten
                .Select(s => MapToContract(s))
                .ToList();
        }
    }
}
