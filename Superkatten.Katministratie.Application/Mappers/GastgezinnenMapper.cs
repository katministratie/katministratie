namespace Superkatten.Katministratie.Application.Mappers
{
    internal class GastgezinnenMapper : IGastgezinnenMapper
    {
        public Contracts.Gastgezin MapFromDomain(Domain.Entities.Gastgezin superkat)
        {
            return new Contracts.Gastgezin
            {
                Name = superkat.Name,
                Address = superkat?.Address,
                City = superkat?.City,
                Phone = superkat?.Phone,
            };
        }

        public Domain.Entities.Gastgezin MapToDomain(Contracts.Gastgezin superkat)
        {
            return new Domain.Entities.Gastgezin(
                superkat.Name,
                superkat.Address,
                superkat.City,
                superkat.Phone
                );
        }
    }
}
