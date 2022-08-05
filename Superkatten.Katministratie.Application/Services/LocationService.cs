using Superkatten.Katministratie.Application.Interfaces;
using Superkatten.Katministratie.Domain.Entities;
using Superkatten.Katministratie.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Application.Services;

public class LocationService : ILocationService
{
    private readonly ILocationRepository _locationRepository;

    public LocationService(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public Task<IReadOnlyCollection<Location>> GetLocationsAsync()
    {
        return _locationRepository.GetLocationsAsync();
    }
}
