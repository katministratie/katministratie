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
                Id = gastgezin.Id,
                Name = gastgezin.Name,
                Address = gastgezin.Address,
                City = gastgezin.City,
                Phone = gastgezin.Phone,
            };
        }

        public Gastgezin MapGastgezinDtoToDomain(GastgezinDto gastgezinDto)
        {
            return new Gastgezin(
                gastgezinDto.Id,
                gastgezinDto.Name,
                gastgezinDto.Address,
                gastgezinDto.City,
                gastgezinDto.Phone);
        }
    }
}
