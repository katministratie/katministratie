using Superkatten.Katministratie.Domain.Entities;
using System.ComponentModel;

using contractEntities = Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.Application.Mappers
{
    public class SuperkatMapper : ISuperkatMapper
    {
        public contractEntities.Superkat MapDomainToContract(Superkat createdSuperkat)
        {
            return new contractEntities.Superkat
            {
                Id = createdSuperkat.Id,
                Birthday = createdSuperkat.Birthday,
                CageNumber = createdSuperkat.CageNumber,
                CatchDate = createdSuperkat.CatchDate,
                CatchLocation = createdSuperkat.CatchLocation,
                IsKitten = createdSuperkat.IsKitten,
                Name = createdSuperkat.Name,
                Number = createdSuperkat.Number,
                Reserved = createdSuperkat.Reserved,
                Retour = createdSuperkat.Retour,
                Behaviour = MapToContract(createdSuperkat.Behaviour),
                CatArea = MapToContract(createdSuperkat.CatArea),
                Gender = MapToContract(createdSuperkat.Gender)
            };
        }

        private static contractEntities.CatBehaviour MapToContract(CatBehaviour behaviour)
        {
            return behaviour switch
            {
                CatBehaviour.Shy => contractEntities.CatBehaviour.Shy,
                CatBehaviour.Social => contractEntities.CatBehaviour.Social,
                CatBehaviour.Unknown => contractEntities.CatBehaviour.Unknown,
                _ => throw new InvalidEnumArgumentException(nameof(behaviour), (int)behaviour, typeof(CatBehaviour))
            };
        }

        private static contractEntities.CatArea MapToContract(CatArea catArea)
        {
            return catArea switch
            {
                CatArea.Quarantine => contractEntities.CatArea.Quarantine,
                CatArea.Infirmary => contractEntities.CatArea.Infirmary,
                CatArea.SmallEnclosure => contractEntities.CatArea.SmallEnclosure,
                CatArea.BigEnclosure => contractEntities.CatArea.BigEnclosure,
                CatArea.Room2 => contractEntities.CatArea.Room2,
                _ => throw new InvalidEnumArgumentException(nameof(catArea), (int)catArea, typeof(CatArea))
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


        public Superkat MapContractToDomain(contractEntities.Superkat contractSuperkat)
        {
            var superkat = new Superkat(
                contractSuperkat.Number,
                contractSuperkat.CatchDate,
                contractSuperkat.CatchLocation)
            { Id = contractSuperkat.Id };

            superkat.SetArea(MapContractToDomain(contractSuperkat.CatArea));
            superkat.SetBehaviour(MapContractToDomain(contractSuperkat.Behaviour));
            superkat.SetBirthday(contractSuperkat.Birthday);
            superkat.SetCageNumber(contractSuperkat.CageNumber);
            superkat.SetGender(MapContractToDomain(contractSuperkat.Gender));
            superkat.SetIsKitten(contractSuperkat.IsKitten);
            superkat.SetName(contractSuperkat.Name ?? string.Empty);

            return superkat;
        }

        public CatArea MapContractToDomain(contractEntities.CatArea area)
        {
            return area switch
            {
                contractEntities.CatArea.Quarantine => CatArea.Quarantine,
                contractEntities.CatArea.Infirmary => CatArea.Infirmary,
                contractEntities.CatArea.SmallEnclosure => CatArea.SmallEnclosure,
                contractEntities.CatArea.BigEnclosure => CatArea.BigEnclosure,
                contractEntities.CatArea.Room2 => CatArea.Room2,
                _ => throw new InvalidEnumArgumentException(nameof(area), (int)area, typeof(contractEntities.CatArea))
            };
        }

        public CatBehaviour MapContractToDomain(contractEntities.CatBehaviour behaviour)
        {
            return behaviour switch
            {
                contractEntities.CatBehaviour.Social => CatBehaviour.Social,
                contractEntities.CatBehaviour.Shy => CatBehaviour.Shy,
                contractEntities.CatBehaviour.Unknown => CatBehaviour.Unknown,
                _ => throw new InvalidEnumArgumentException(nameof(behaviour), (int)behaviour, typeof(contractEntities.CatBehaviour))
            };
        }

        public Gender MapContractToDomain(contractEntities.Gender gender)
        {
            return gender switch
            {
                contractEntities.Gender.Molly => Gender.Molly,
                contractEntities.Gender.Tomcat => Gender.Tomcat,
                contractEntities.Gender.Unknown => Gender.Unknown,
                _ => throw new InvalidEnumArgumentException(nameof(gender), (int)gender, typeof(contractEntities.Gender))
            };
        }

        public LitterGranuleType MapContractToDomain(contractEntities.LitterGranuleType litterType)
        {
            return litterType switch
            {
                contractEntities.LitterGranuleType.Normal => LitterGranuleType.Normal,
                contractEntities.LitterGranuleType.Clumping => LitterGranuleType.Clumping,
                contractEntities.LitterGranuleType.Wood => LitterGranuleType.Wood,
                _ => throw new InvalidEnumArgumentException(nameof(litterType), (int)litterType, typeof(contractEntities.LitterGranuleType))
            };
        }

        public FoodType MapContractToDomain(contractEntities.FoodType foodType)
        {
            return foodType switch
            {
                contractEntities.FoodType.FirstPhase => FoodType.FirstPhase,
                contractEntities.FoodType.SecondPhase => FoodType.SecondPhase,
                contractEntities.FoodType.Rc365 => FoodType.Rc365,
                _ => throw new InvalidEnumArgumentException(nameof(foodType), (int)foodType, typeof(contractEntities.FoodType))
            };
        }
    }
}
