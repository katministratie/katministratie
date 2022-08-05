using Superkatten.Katministratie.Contract.Entities;
using Superkatten.Katministratie.Host.Services.Http;
using Superkatten.Katministratie.Host.Services.Interfaces;

namespace Superkatten.Katministratie.Host.Services;

public class LocationService : ILocationService
{

    private readonly IHttpService _httpService;

    public LocationService(IHttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task<IReadOnlyCollection<Location>> GetLocationsAsync()
    {
        var uri = "api/Locations";

        var locations = await _httpService.Get<List<Location>>(uri);

        return locations is null
            ? new List<Location>() { 
                new Location { Name="Rhenoy", Type=LocationType.Farm},
                new Location { Name="Tiel", Type=LocationType.Farm},
                new Location { Name="Leerdam", Type=LocationType.Farm},
                new Location { Name="Tuilen", Type=LocationType.Farm},
                new Location { Name="Aquoiy", Type=LocationType.Farm}
            }
            : locations;
    }
}
