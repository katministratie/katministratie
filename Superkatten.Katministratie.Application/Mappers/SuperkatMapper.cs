using Superkatten.Katministratie.Domain.Entities;
using System;
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
                State = MapToContract(createdSuperkat.State),
                Birthday = createdSuperkat.Birthday,
                CageNumber = createdSuperkat.CageNumber,
                CatchDate = createdSuperkat.CatchDate,
                CatchLocation = createdSuperkat.CatchLocation,
                AgeCategory = MapToContract(createdSuperkat.AgeCategory),
                Name = createdSuperkat.Name,
                Number = createdSuperkat.Number,
                Reserved = createdSuperkat.Reserved,
                Retour = createdSuperkat.Retour,
                Behaviour = MapToContract(createdSuperkat.Behaviour),
                CatArea = MapToContract(createdSuperkat.CatArea),
                Gender = MapToContract(createdSuperkat.Gender),
                GastgezinId = createdSuperkat.GastgezinId ?? Guid.Empty
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

        private static contractEntities.SuperkatState MapToContract(SuperkatState state)
        {
            return state switch
            {
                SuperkatState.Trapped => contractEntities.SuperkatState.Trapped,
                SuperkatState.Neutralized => contractEntities.SuperkatState.Neutralized,
                SuperkatState.Returnable => contractEntities.SuperkatState.Returnable,
                SuperkatState.Checked => contractEntities.SuperkatState.Checked,
                SuperkatState.Done => contractEntities.SuperkatState.Done,
                _ => throw new InvalidEnumArgumentException(nameof(state), (int)state, typeof(SuperkatState))
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

        private static contractEntities.AgeCategory MapToContract(AgeCategory ageCategory)
        {
            return ageCategory switch
            {
                AgeCategory.Kitten => contractEntities.AgeCategory.Kitten,
                AgeCategory.Juvenile => contractEntities.AgeCategory.Juvenile,
                AgeCategory.Adult => contractEntities.AgeCategory.Adult,
                _ => throw new InvalidEnumArgumentException(nameof(ageCategory), (int)ageCategory, typeof(AgeCategory))
            };
        }


        public Superkat MapContractToDomain(contractEntities.Superkat contractSuperkat)
        {
            var superkat = new Superkat(
                contractSuperkat.Number,
                contractSuperkat.CatchDate,
                contractSuperkat.CatchLocation)
            { 
                Id = contractSuperkat.Id,
                State = MapContractToDomain(contractSuperkat.State)
            };

            superkat.SetArea(MapContractToDomain(contractSuperkat.CatArea));
            superkat.SetBehaviour(MapContractToDomain(contractSuperkat.Behaviour));
            superkat.SetBirthday(contractSuperkat.Birthday);
            superkat.SetCageNumber(contractSuperkat.CageNumber);
            superkat.SetGender(MapContractToDomain(contractSuperkat.Gender));
            superkat.SetAgeCategory(MapContractToDomain(contractSuperkat.AgeCategory));
            superkat.SetName(contractSuperkat.Name ?? string.Empty);

            return superkat.WithGastgezinId(contractSuperkat.GastgezinId);
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

        public AgeCategory MapContractToDomain(contractEntities.AgeCategory ageCategory)
        {
            return ageCategory switch
            {
                contractEntities.AgeCategory.Kitten => AgeCategory.Kitten,
                contractEntities.AgeCategory.Juvenile => AgeCategory.Juvenile,
                contractEntities.AgeCategory.Adult => AgeCategory.Adult,
                _ => throw new InvalidEnumArgumentException(nameof(ageCategory), (int)ageCategory, typeof(contractEntities.AgeCategory))
            };
        }

        public SuperkatState MapContractToDomain(contractEntities.SuperkatState state)
        {
            return state switch
            {
                contractEntities.SuperkatState.Trapped => SuperkatState.Trapped,
                contractEntities.SuperkatState.Neutralized => SuperkatState.Neutralized,
                contractEntities.SuperkatState.Returnable => SuperkatState.Returnable,
                contractEntities.SuperkatState.Checked => SuperkatState.Checked,
                contractEntities.SuperkatState.Done => SuperkatState.Done,
                _ => throw new InvalidEnumArgumentException(nameof(state), (int)state, typeof(contractEntities.SuperkatState))
            };
        }
    }
}
