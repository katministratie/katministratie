using Superkatten.Katministratie.Domain.Entities;
using Superkatten.Katministratie.Infrastructure.Entities;

namespace Superkatten.Katministratie.Infrastructure.Mapper
{
    public class GastgezinRepositoryMapper : IGastgezinRepositoryMapper
    {
        public GastgezinDto MapDomainToGastgezinDto(Gastgezin gastgezin)
        {
            return new GastgezinDto
            {
                Name = gastgezin.Name,
                Address = gastgezin?.Address,
                City = gastgezin?.City,
                Phone = gastgezin?.Phone,
            };
        }

        public Gastgezin MapGastgezinDtoToDomain(GastgezinDto gastgezinDto)
        {
            return new Gastgezin(
                gastgezinDto.Name,
                gastgezinDto?.Address,
                gastgezinDto?.City,
                gastgezinDto?.Phone);
        }
    }
}
