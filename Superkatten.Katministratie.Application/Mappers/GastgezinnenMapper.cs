using Superkatten.Katministratie.Application.Entities;

namespace Superkatten.Katministratie.Application.Mappers
{
    public class GastgezinnenMapper : IGastgezinnenMapper
    {
        public Gastgezin MapFromDomain(Domain.Entities.Gastgezin superkat)
        {
            return new Gastgezin
            {
                Name = superkat.Name,
                Address = superkat?.Address,
                City = superkat?.City,
                Phone = superkat?.Phone,
            };
        }
    }
}
