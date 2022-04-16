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
                Birthday = superkat.Birthday,
            };
        }

        public Superkat MapSuperkatDtoToDomain(SuperkatDto superkatDto)
        {
            var superkat = new Superkat(
                superkatDto.Number,
                superkatDto.FoundDate,
                superkatDto.CatchLocation
                )
                .WithName(superkatDto.Name)
                .WithReserved(superkatDto.Reserved)
                .WithRetour(superkatDto.Retour)
                .WithBirthday(superkatDto.Birthday);

            return superkat;
        }
    }
}
