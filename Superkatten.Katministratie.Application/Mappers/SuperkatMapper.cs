﻿using Superkatten.Katministratie.Domain.Entities;
using System;
using System.ComponentModel;

using contractEntities = Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.Application.Mappers
{
    public class SuperkatMapper : ISuperkatMapper
    {
        public contractEntities.Superkat MapDomainToContract(Superkat superkat)
        {
            return new contractEntities.Superkat
            {
                Id = superkat.Id,
                State = MapToContract(superkat.State),
                Birthday = superkat.Birthday,
                CageNumber = superkat.CageNumber,
                CatchDate = superkat.CatchDate,
                CatchOrigin = MapToContract(superkat.CatchOrigin),
                AgeCategory = MapToContract(superkat.AgeCategory),
                Name = superkat.Name,
                Number = superkat.Number,
                Reserved = superkat.Reserved,
                Retour = superkat.Retour,
                Behaviour = MapToContract(superkat.Behaviour),
                CatArea = MapToContract(superkat.CatArea),
                Gender = MapToContract(superkat.Gender),
                GastgezinId = superkat.GastgezinId,
                Photo = superkat.Photo ?? Array.Empty<byte>(),
                Color = superkat.Color,
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
                SuperkatState.Monitoring => contractEntities.SuperkatState.Monitoring,
                SuperkatState.ReadyForAdoption => contractEntities.SuperkatState.ReadyForAdoption,
                SuperkatState.WaitForPayment => contractEntities.SuperkatState.WaitForPayment,
                SuperkatState.FinalizeChecks => contractEntities.SuperkatState.FinalizeChecks,
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
                CatArea.HostFamily => contractEntities.CatArea.HostFamily,
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
        private static contractEntities.CatchOriginType MapToContract(CatchOriginType catchOriginType)
        {
            return catchOriginType switch
            {
                CatchOriginType.Farm => contractEntities.CatchOriginType.Farm,
                CatchOriginType.PrivateProperty => contractEntities.CatchOriginType.PrivateProperty,
                CatchOriginType.AllotmentGarden => contractEntities.CatchOriginType.AllotmentGarden,
                CatchOriginType.Camping => contractEntities.CatchOriginType.Camping,
                CatchOriginType.Farmhouse => contractEntities.CatchOriginType.Farmhouse,
                CatchOriginType.Stable => contractEntities.CatchOriginType.Stable,
                CatchOriginType.BusinessPark => contractEntities.CatchOriginType.BusinessPark,
                CatchOriginType.UrbanArea => contractEntities.CatchOriginType.UrbanArea,
                CatchOriginType.RuralArea => contractEntities.CatchOriginType.RuralArea,
                CatchOriginType.NatureReserve => contractEntities.CatchOriginType.NatureReserve,
                _ => throw new InvalidEnumArgumentException(nameof(catchOriginType), (int)catchOriginType, typeof(CatchOriginType))
            };
        }

        private static contractEntities.CatchOrigin MapToContract(CatchOrigin catchOrigin)
        {
            return new contractEntities.CatchOrigin
            {
                Id = catchOrigin.Id,
                Name = catchOrigin.Name,
                Type = MapToContract(catchOrigin.Type)
            };
        }

        public Superkat MapContractToDomain(contractEntities.Superkat contractSuperkat)
        {
            var superkat = new Superkat(
                contractSuperkat.Number,
                contractSuperkat.CatchDate,
                MapContractToDomain(contractSuperkat.CatchOrigin)
            )
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

            return superkat
                .WithGastgezinId(contractSuperkat.GastgezinId)
                .WithPhoto(contractSuperkat.Photo)
                .WithColor(contractSuperkat.Color);
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
                contractEntities.CatArea.HostFamily => CatArea.HostFamily,
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

        public static SuperkatState MapContractToDomain(contractEntities.SuperkatState state)
        {
            return state switch
            {
                contractEntities.SuperkatState.Monitoring => SuperkatState.Monitoring,
                contractEntities.SuperkatState.ReadyForAdoption => SuperkatState.ReadyForAdoption,
                contractEntities.SuperkatState.WaitForPayment => SuperkatState.WaitForPayment,
                contractEntities.SuperkatState.FinalizeChecks => SuperkatState.FinalizeChecks,
                contractEntities.SuperkatState.Done => SuperkatState.Done,
                _ => throw new InvalidEnumArgumentException(nameof(state), (int)state, typeof(contractEntities.SuperkatState))
            };
        }

        public CatchOrigin MapContractToDomain(contractEntities.CatchOrigin catchOrigin)
        {
            var catchOriginType = MapContractToDomain(catchOrigin.Type);
            return new CatchOrigin(catchOrigin.Name, catchOriginType);
        }

        private static CatchOriginType MapContractToDomain(contractEntities.CatchOriginType catchOriginType)
        {
            return catchOriginType switch
            {
                contractEntities.CatchOriginType.Farm => CatchOriginType.Farm,
                contractEntities.CatchOriginType.PrivateProperty => CatchOriginType.PrivateProperty,
                contractEntities.CatchOriginType.AllotmentGarden => CatchOriginType.AllotmentGarden,
                contractEntities.CatchOriginType.Camping => CatchOriginType.Camping,
                contractEntities.CatchOriginType.Farmhouse => CatchOriginType.Farmhouse,
                contractEntities.CatchOriginType.Stable => CatchOriginType.Stable,
                contractEntities.CatchOriginType.BusinessPark => CatchOriginType.BusinessPark,
                contractEntities.CatchOriginType.UrbanArea => CatchOriginType.UrbanArea,
                contractEntities.CatchOriginType.RuralArea => CatchOriginType.RuralArea,
                contractEntities.CatchOriginType.NatureReserve => CatchOriginType.NatureReserve,
                _ => throw new InvalidEnumArgumentException(nameof(catchOriginType), (int)catchOriginType, typeof(contractEntities.CatchOriginType))
            };
        }
    }
}
