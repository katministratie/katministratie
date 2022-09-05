using Superkatten.Katministratie.Application.Interfaces;
using Superkatten.Katministratie.Application.Mappers;
using Superkatten.Katministratie.Contract.Entities;
using Superkatten.Katministratie.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Application.Services;

public class LocationService : ILocationService
{
    private readonly ILocationRepository _locationRepository;
    private readonly ILocationMapper _locationMapper;

    public LocationService(
        ILocationRepository locationRepository,
        ILocationMapper locationMapper
    )
    {
        _locationRepository = locationRepository;
        _locationMapper = locationMapper;
    }

    public async Task<IReadOnlyCollection<Location>> GetLocationsAsync()
    {
        var locations = await _locationRepository.GetLocationsAsync();

        return locations
            .Select(_locationMapper.MapDomainToContract)
            .ToList();
    }
}
