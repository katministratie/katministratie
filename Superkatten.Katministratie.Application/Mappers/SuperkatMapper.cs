using Superkatten.Katministratie.Domain.Entities;
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
                CatchLocation = MapToContract(superkat.CatchLocation),
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
        private static contractEntities.LocationType MapToContract(LocationType locationType)
        {
            return locationType switch
            {
                LocationType.Farm => contractEntities.LocationType.Farm,
                LocationType.PrivateProperty => contractEntities.LocationType.PrivateProperty,
                LocationType.AllotmentGarden => contractEntities.LocationType.AllotmentGarden,
                LocationType.Camping => contractEntities.LocationType.Camping,
                LocationType.Farmhouse => contractEntities.LocationType.Farmhouse,
                LocationType.Stable => contractEntities.LocationType.Stable,
                LocationType.BusinessPark => contractEntities.LocationType.BusinessPark,
                LocationType.UrbanArea => contractEntities.LocationType.UrbanArea,
                LocationType.RuralArea => contractEntities.LocationType.RuralArea,
                LocationType.NatureReserve => contractEntities.LocationType.NatureReserve,
                _ => throw new InvalidEnumArgumentException(nameof(locationType), (int)locationType, typeof(LocationType))
            };
        }

        private static contractEntities.Location MapToContract(Location location)
        {
            return new contractEntities.Location
            {
                Id = location.Id,
                Name = location.Name,
                Type = MapToContract(location.Type)
            };
        }

        public Superkat MapContractToDomain(contractEntities.Superkat contractSuperkat)
        {
            var superkat = new Superkat(
                contractSuperkat.Number,
                contractSuperkat.CatchDate,
                MapContractToDomain(contractSuperkat.CatchLocation)
            )
            {
                Id = contractSuperkat.Id,
                State = MapContractToDomain(contractSuperkat.State)
            };

            superkat.SetBehaviour(MapContractToDomain(contractSuperkat.Behaviour));
            superkat.SetBirthday(contractSuperkat.Birthday);
            superkat.SetGender(MapContractToDomain(contractSuperkat.Gender));
            superkat.SetAgeCategory(MapContractToDomain(contractSuperkat.AgeCategory));

            return superkat
                .WithGastgezinId(contractSuperkat.GastgezinId)
                .WithPhoto(contractSuperkat.Photo)
                .WithCatArea(MapContractToDomain(contractSuperkat.CatArea))
                .WithCageNumber(contractSuperkat.CageNumber)
                .WithName(contractSuperkat.Name ?? string.Empty);
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

        public Location MapContractToDomain(contractEntities.Location location)
        {
            var locationType = MapContractToDomain(location.Type);
            return new Location(location.Name, locationType);
        }

        private static LocationType MapContractToDomain(contractEntities.LocationType locationType)
        {
            return locationType switch
            {
                contractEntities.LocationType.Farm => LocationType.Farm,
                contractEntities.LocationType.PrivateProperty => LocationType.PrivateProperty,
                contractEntities.LocationType.AllotmentGarden => LocationType.AllotmentGarden,
                contractEntities.LocationType.Camping => LocationType.Camping,
                contractEntities.LocationType.Farmhouse => LocationType.Farmhouse,
                contractEntities.LocationType.Stable => LocationType.Stable,
                contractEntities.LocationType.BusinessPark => LocationType.BusinessPark,
                contractEntities.LocationType.UrbanArea => LocationType.UrbanArea,
                contractEntities.LocationType.RuralArea => LocationType.RuralArea,
                contractEntities.LocationType.NatureReserve => LocationType.NatureReserve,
                _ => throw new InvalidEnumArgumentException(nameof(locationType), (int)locationType, typeof(contractEntities.LocationType))
            };
        }
    }
}
