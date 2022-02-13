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
                Kleur = superkat.Kleur,
            };
        }

        public Domain.Entities.Superkat MapToDomain(Contracts.Superkat superkat)
        {
            return new Domain.Entities.Superkat(
                    number: superkat.Number,
                    kleur: superkat.Kleur,
                    foundDate: superkat.FoundDate,
                    catchLocation: superkat.CatchLocation
                )
                .SetName(superkat.Name is null ? string.Empty : superkat.Name)
                .SetBirthday(superkat.Birthday);
        }
    }
}
