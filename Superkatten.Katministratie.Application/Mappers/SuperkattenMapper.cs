namespace Superkatten.Katministratie.Application.Mappers
{
    internal class SuperkattenMapper : ISuperkattenMapper
    {
        public Entities.Superkat MapFromDomain(Domain.Entities.Superkat superkat)
        {
            return new Entities.Superkat
            {
                Number = superkat.Number,
                Name = superkat.Name,
            };
        }

        public Domain.Entities.Superkat MapToDomain(Entities.Superkat superkat)
        {
            return new Domain.Entities.Superkat(
                superkat.Number,
                superkat.Name
            );
        }
    }
}
