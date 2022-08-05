using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Superkatten.Katministratie.Domain.Entities;
using Superkatten.Katministratie.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Infrastructure.Persistence;

public class LocationRepository : ILocationRepository
{
    private readonly ILogger<LocationRepository> _logger;
    private readonly SuperkattenDbContext _context;

    public LocationRepository(
        ILogger<LocationRepository> logger,
        SuperkattenDbContext context
    )
    {
        _logger = logger;
        _context = context;
    }

    public async Task<Location> CreateOrGetLocationAsync(LocationType type, string name)
    {
        var location = await _context
            .Locations
            .Where(l => l.Type == type && l.Name.Equals(name))
            .FirstOrDefaultAsync();

        if (location is null)
        {
            location = new Location(name, type);

            await _context.Locations.AddAsync(location);
            await _context.SaveChangesAsync();
        }

        return location;
    }

    public async Task<IReadOnlyCollection<Location>> GetLocationsAsync()
    {
        return await _context
            .Locations
            .AsNoTracking()
            .ToListAsync();
    }
}
