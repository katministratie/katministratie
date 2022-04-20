using Superkatten.Katministratie.Domain.Entities;
using Superkatten.Katministratie.Infrastructure.Entities;
using System;
using System.ComponentModel;

namespace Superkatten.Katministratie.Infrastructure.Mapper
{
    public class SuperkatRepositoryMapper : ISuperkatRepositoryMapper
    {
        public SuperkatDto MapDomainToSuperkatDto(Superkat superkat)
        {
            return new SuperkatDto
            {
                Id = superkat.Id,
                Number = superkat.Number,
                Name = superkat.Name,
                CatchDate = superkat.CatchDate,
                CatchLocation = superkat.CatchLocation,
                Birthday = superkat.Birthday,
                Area = (int)superkat.Area,
                CageNumber = superkat.CageNumber,
                Retour = superkat.Retour,
                Reserved = superkat.Reserved,
                Behaviour = (int)superkat.Behaviour,
                IsKitten = superkat.IsKitten,
            };
        }

        public Superkat MapSuperkatDtoToDomain(SuperkatDto superkatDto)
        {
            var superkat = new Superkat(
                superkatDto.Id,
                superkatDto.CatchDate,
                superkatDto.CatchLocation);
            superkat.SetNumber(superkatDto.Number);
            superkat.SetName(superkatDto.Name ?? string.Empty);
            superkat.SetReserved(superkatDto.Reserved);
            superkat.SetArea(MapToDomainArea(superkatDto.Area));
            superkat.SetCageNumber(superkatDto.CageNumber);
            superkat.SetRetour(superkatDto.Retour);
            superkat.SetBehaviour(MapToDomainBehaviour(superkatDto.Behaviour));
            superkat.SetBirthday(superkatDto.Birthday);
            superkat.SetIsKitten(superkatDto.IsKitten);

            return superkat;
        }

        private CatBehaviour MapToDomainBehaviour(int behaviour)
        {
            if (!Enum.IsDefined(typeof(CatBehaviour), behaviour))
            {
                throw new InvalidEnumArgumentException(nameof(CatBehaviour), behaviour, typeof(CatBehaviour));
            }

            return (CatBehaviour)behaviour;
        }

        private CatArea MapToDomainArea(int area)
        {
            if (!Enum.IsDefined(typeof(CatArea), area)) 
            {
                throw new InvalidEnumArgumentException(nameof(CatArea), area, typeof(CatArea));
            }

            return (CatArea)area;
        }
    }
}
