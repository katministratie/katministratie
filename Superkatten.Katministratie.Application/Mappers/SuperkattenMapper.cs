namespace Superkatten.Katministratie.Application.Mappers
{
    internal class SuperkattenMapper : ISuperkattenMapper
    {
        public Contracts.Superkat MapFromDomain(Domain.Entities.Superkat superkat)
        {
            return new Contracts.Superkat
            {
                Number = superkat.Number,
                Name = superkat.Name,
                FoundDate= superkat.FoundDate
            };
        }

        public Domain.Entities.Superkat MapToDomain(Contracts.Superkat superkat)
        {
            return new Domain.Entities.Superkat(
                superkat.Number,
                superkat.Name,
                superkat.FoundDate
            );
        }
    }
}
