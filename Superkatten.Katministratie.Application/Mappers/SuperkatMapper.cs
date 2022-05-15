﻿using Superkatten.Katministratie.Domain.Entities;
using System.ComponentModel;

using contractEntities = Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.Application.Mappers
{
    public class SuperkatMapper : ISuperkatMapper
    {
        public contractEntities.Superkat MapToContract(Superkat createdSuperkat)
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

        public contractEntities.CatBehaviour MapToContract(CatBehaviour behaviour)
        {
            return behaviour switch
            {
                CatBehaviour.Shy => contractEntities.CatBehaviour.Shy,
                CatBehaviour.Social => contractEntities.CatBehaviour.Social,
                CatBehaviour.Unknown => contractEntities.CatBehaviour.Unknown,
                _ => throw new InvalidEnumArgumentException(nameof(behaviour), (int)behaviour, typeof(CatBehaviour))
            };
        }

        public contractEntities.CatArea MapToContract(CatArea catArea)
        {
            return catArea switch
            {
                CatArea.LargeCage => contractEntities.CatArea.LargeCage,
                CatArea.SmallCage => contractEntities.CatArea.SmallCage,
                CatArea.Unknown => contractEntities.CatArea.Unknown,
                _ => throw new InvalidEnumArgumentException(nameof(catArea), (int)catArea, typeof(CatArea))
            };
        }
        
        public contractEntities.Gender MapToContract(Gender gender)
        {
            return gender switch
            {
                Gender.Tomcat => contractEntities.Gender.Tomcat,
                Gender.Molly => contractEntities.Gender.Molly,
                Gender.Unknown => contractEntities.Gender.Unknown,
                _ => throw new InvalidEnumArgumentException(nameof(gender), (int)gender, typeof(Gender))
            };
        }


        public Superkat MapToDomain(contractEntities.Superkat contractSuperkat)
        {
            var superkat = new Superkat(contractSuperkat.Number, contractSuperkat.CatchDate, contractSuperkat.CatchLocation);
            superkat.SetArea(MapToDomain(contractSuperkat.CatArea));
            superkat.SetBehaviour(MapToDomain(contractSuperkat.Behaviour));
            superkat.SetBirthday(contractSuperkat.Birthday);
            superkat.SetCageNumber(contractSuperkat.CageNumber);
            superkat.SetGender(MapToDomain(contractSuperkat.Gender));
            superkat.SetIsKitten(contractSuperkat.IsKitten);
            superkat.SetName(contractSuperkat.Name ?? string.Empty);

            return superkat;
        }

        public CatArea MapToDomain(contractEntities.CatArea area)
        {
            return area switch
            {
                contractEntities.CatArea.LargeCage => CatArea.LargeCage,
                contractEntities.CatArea.SmallCage => CatArea.SmallCage,
                contractEntities.CatArea.Unknown => CatArea.Unknown,
                _ => throw new InvalidEnumArgumentException(nameof(area), (int)area, typeof(contractEntities.CatArea))
            };
        }

        public CatBehaviour MapToDomain(contractEntities.CatBehaviour behaviour)
        {
            return behaviour switch
            {
                contractEntities.CatBehaviour.Social => CatBehaviour.Social,
                contractEntities.CatBehaviour.Shy => CatBehaviour.Shy,
                contractEntities.CatBehaviour.Unknown => CatBehaviour.Unknown,
                _ => throw new InvalidEnumArgumentException(nameof(behaviour), (int)behaviour, typeof(contractEntities.CatBehaviour))
            };
        }

        public Gender MapToDomain(contractEntities.Gender gender)
        {
            return gender switch
            {
                contractEntities.Gender.Molly => Gender.Molly,
                contractEntities.Gender.Tomcat => Gender.Tomcat,
                contractEntities.Gender.Unknown => Gender.Unknown,
                _ => throw new InvalidEnumArgumentException(nameof(gender), (int)gender, typeof(contractEntities.Gender))
            };
        }
    }
}
