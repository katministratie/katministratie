using System;

namespace Superkatten.Katministratie.Application.Mappers
{
    public class SuperkattenMapper : ISuperkattenMapper
    {
        public Contracts.Superkat MapFromDomain(Domain.Entities.Superkat superkat)
        {
            return new Contracts.Superkat
            {
                Number = superkat.Number,
                Name = superkat.Name,
                FoundDate = superkat.FoundDate,
                CatchLocation = superkat.CatchLocation,
                Birthday = superkat.Birthday,
                Reserved = superkat.Reserved,
                Retour = superkat.Retour
            };
        }

        public Domain.Entities.Superkat MapToDomain(Contracts.Superkat contractSuperkat)
        {
            var superkat = new Domain.Entities.Superkat(
                    number: contractSuperkat.Number,
                    foundDate: contractSuperkat.FoundDate,
                    catchLocation: contractSuperkat.CatchLocation
                )
                .WithName(contractSuperkat.Name)
                .WithReserved(contractSuperkat.Reserved)
                .WithRetour(contractSuperkat.Retour)
                .WithBirthday(contractSuperkat.Birthday);

            return superkat;
        }
    }
}
