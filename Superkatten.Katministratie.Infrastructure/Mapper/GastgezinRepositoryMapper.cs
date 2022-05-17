using Superkatten.Katministratie.Domain.Entities;
using Superkatten.Katministratie.Infrastructure.Entities;
using System.Linq;

namespace Superkatten.Katministratie.Infrastructure.Mapper;

public class GastgezinRepositoryMapper : IGastgezinRepositoryMapper
{
    public GastgezinDto MapDomainToRepository(Gastgezin gastgezin)
    {
        var superkatMapper = new SuperkatRepositoryMapper();
        var superkatten = gastgezin
            .Superkatten
            .Select(superkatMapper.MapDomainToRepository)
            .ToList();

        var gastgezinDto = new GastgezinDto
        {
            Id = gastgezin.Id,
            Name = gastgezin.Name,
            Address = gastgezin.Address,
            City = gastgezin.City,
            Phone = gastgezin.Phone,
            Superkatten = superkatten
        };

        return gastgezinDto;
    }

    public Gastgezin MapRepositoryToDomain(GastgezinDto gastgezinDto)
    {
        var superkatMapper = new SuperkatRepositoryMapper();
        var superkatten = gastgezinDto
            .Superkatten
            .Select(superkatMapper.MapRepositoryToDomain)
            .ToList();

        var gastgezin = new Gastgezin(
            gastgezinDto.Id,
            gastgezinDto.Name,
            gastgezinDto.Address,
            gastgezinDto.City,
            gastgezinDto.Phone);
        gastgezin.Superkatten.AddRange(superkatten);

        return gastgezin;
    }
}
