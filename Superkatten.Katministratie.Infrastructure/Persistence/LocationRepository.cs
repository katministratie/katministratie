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
    private readonly SuperkattenDbContext _context;

    public LocationRepository(SuperkattenDbContext context)
    {
        _context = context;
    }

    public async Task<Location?> GetLocationAsync(CatchOriginType type, string name)
    {
        return await _context
            .Locations
            .Where(l => l.Type == type && l.Name.ToLower().Equals(name.ToLower()))
            .FirstOrDefaultAsync();
    }

    public async Task<Location> CreateLocationAsync(CatchOriginType type, string name)
    {
        var location = new Location(name, type);

        await _context.Locations.AddAsync(location);
        await _context.SaveChangesAsync();

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
