using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Contract.Entities.Locations;

namespace Superkatten.Katministratie.Host.Services;

public interface IGastgezinService
{
    Task<List<Location>> GetAllGastgezinAsync();
    Task<Location?> GetGastgezinAsync(Guid id);
    Task<Location?> CreateGastgezinAsync(LocationNawParameters newGastgezinParameters);
    Task<Location?> UpdateGastgezinAsync(Guid id, LocationNawParameters updateNawGastgezinParameters);
    Task DeleteGastgezinAsync(Guid id);
}

