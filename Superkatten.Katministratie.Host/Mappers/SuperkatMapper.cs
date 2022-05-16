using Superkatten.Katministratie.Host.Entities;
using System.ComponentModel;

using contractEntities = Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.Host.Mappers
{
    public class SuperkatMapper : ISuperkatMapper
    {
        private static CatBehaviour MapContractToHost(contractEntities.CatBehaviour behaviour)
        {
            return behaviour switch
            {
                contractEntities.CatBehaviour.Social => CatBehaviour.Social,
                contractEntities.CatBehaviour.Shy => CatBehaviour.Shy,
                contractEntities.CatBehaviour.Unknown => CatBehaviour.Unknown,
                _ => throw new InvalidEnumArgumentException(nameof(behaviour), (int)behaviour, typeof(CatBehaviour))
            };
        }

        private static Gender MapContractToHost(contractEntities.Gender gender)
        {
            return gender switch
            {
                contractEntities.Gender.Molly => Gender.Molly,
                contractEntities.Gender.Tomcat => Gender.Tomcat,
                contractEntities.Gender.Unknown => Gender.Unknown,
                _ => throw new InvalidEnumArgumentException(nameof(gender), (int)gender, typeof(Gender))
            };
        }

        private static CatArea MapContractToHost(contractEntities.CatArea catArea)
        {
            return catArea switch
            {
                contractEntities.CatArea.LargeCage => CatArea.LargeCage,
                contractEntities.CatArea.SmallCage => CatArea.SmallCage,
                contractEntities.CatArea.Unknown => CatArea.Unknown,
                _ => throw new InvalidEnumArgumentException(nameof(catArea), (int)catArea, typeof(CatArea))
            };
        }

        public Superkat MapContractToHost(contractEntities.Superkat superkat)
        {
            return new Superkat
            {
                Id = superkat.Id,
                Name = superkat.Name,
                CageNumber = superkat.CageNumber,
                CatchDate = superkat.CatchDate,
                Birthday = superkat.Birthday,
                Behaviour = MapContractToHost(superkat.Behaviour),
                CatArea = MapContractToHost(superkat.CatArea),
                CatchLocation = superkat.CatchLocation,
                Gender = MapContractToHost(superkat.Gender),
                IsKitten = superkat.IsKitten,
                Number = superkat.Number,
                Reserved = superkat.Reserved,
                Retour = superkat.Retour
            };
        }
        
        private static contractEntities.CatBehaviour MapHostToDomain(CatBehaviour behaviour)
        {
            return behaviour switch
            {
                CatBehaviour.Social => contractEntities.CatBehaviour.Social,
                CatBehaviour.Shy => contractEntities.CatBehaviour.Shy,
                CatBehaviour.Unknown => contractEntities.CatBehaviour.Unknown,
                _ => throw new InvalidEnumArgumentException(nameof(behaviour), (int)behaviour, typeof(CatBehaviour))
            };
        }

        private static contractEntities.Gender MapHostToDomain(Gender gender)
        {
            return gender switch
            {
                Gender.Molly => contractEntities.Gender.Molly,
                Gender.Tomcat => contractEntities.Gender.Tomcat,
                Gender.Unknown => contractEntities.Gender.Unknown,
                _ => throw new InvalidEnumArgumentException(nameof(gender), (int)gender, typeof(Gender))
            };
        }

        private static contractEntities.CatArea MapHostToDomain(CatArea catArea)
        {
            return catArea switch
            {
                CatArea.LargeCage => contractEntities.CatArea.LargeCage,
                CatArea.SmallCage => contractEntities.CatArea.SmallCage,
                CatArea.Unknown => contractEntities.CatArea.Unknown,
                _ => throw new InvalidEnumArgumentException(nameof(catArea), (int)catArea, typeof(CatArea))
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
                Behaviour = MapHostToDomain(superkat.Behaviour),
                CatArea = MapHostToDomain(superkat.CatArea),
                CatchLocation = superkat.CatchLocation,
                Gender = MapHostToDomain(superkat.Gender),
                IsKitten = superkat.IsKitten,
                Number = superkat.Number,
                Reserved = superkat.Reserved,
                Retour = superkat.Retour
            };
        }
    }
}
