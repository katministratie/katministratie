using Superkatten.Katministratie.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Infrastructure.Interfaces;

public interface ILocationRepository
{
    Task<Location> CreateLocationAsync(CatchOriginType type, string name);
    Task<Location?> GetLocationAsync(CatchOriginType type, string name);
    Task<IReadOnlyCollection<Location>> GetLocationsAsync();
}
