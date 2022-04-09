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
                FoundDate= superkat.FoundDate,
                CatchLocation = superkat.CatchLocation,
                Birthday = superkat.Birthday,
                Reserved = superkat.Reserved
            };
        }

        public Domain.Entities.Superkat MapToDomain(Contracts.Superkat contractSuperkat)
        {
            var superkat = new Domain.Entities.Superkat(
                    number: contractSuperkat.Number,
                    catchLocation: contractSuperkat.CatchLocation
                );

            superkat.SetName(superkat.Name is null ? string.Empty : superkat.Name);
            superkat.SetReserved(contractSuperkat.Reserved);

            var weeksOld = ConvertBirthdayToWeeksOld(contractSuperkat.Birthday);
            superkat.SetWeeksOld(weeksOld);

            return superkat;
        }

        private int ConvertBirthdayToWeeksOld(DateTimeOffset birthday)
        {
            var today = DateTimeOffset.Now;
            return (int)(today - birthday).TotalDays / 7;
        }
    }
}
