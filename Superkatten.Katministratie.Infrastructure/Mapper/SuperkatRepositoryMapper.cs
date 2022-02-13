using Superkatten.Katministratie.Domain.Entities;
using Superkatten.Katministratie.Infrastructure.Entities;
using System;

namespace Superkatten.Katministratie.Infrastructure.Mapper
{
    public class SuperkatRepositoryMapper : ISuperkatRepositoryMapper
    {
        public SuperkatDto MapDomainToSuperkatDto(Superkat superkat)
        {
            return new SuperkatDto
            {
                Number = superkat.Number,
                Name = superkat.Name,
                FoundDate = superkat.FoundDate,
                CatchLocation = superkat.CatchLocation,
                Kleur = MapFromDomainKleur(superkat.Kleur),
                Birthday = superkat.Birthday
            };
        }

        private string MapFromDomainKleur(string kleur)
        {
            return kleur;
        }

        public Superkat MapSuperkatDtoToDomain(SuperkatDto superkatDto)
        {
            return new Superkat(
                superkatDto.Number,
                MapToDomainKleur(superkatDto.Kleur),
                superkatDto.FoundDate,
                superkatDto.CatchLocation
                )
                .SetBirthday(superkatDto.Birthday)
                .SetName(superkatDto.Name);
        }

        private string MapToDomainKleur(string kleur)
        {
            return kleur;
        }
    }
}
