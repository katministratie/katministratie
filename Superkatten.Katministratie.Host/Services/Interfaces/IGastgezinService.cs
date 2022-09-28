using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Contract.Entities.Locations;

namespace Superkatten.Katministratie.Host.Services;

public interface IGastgezinService
{
    Task<List<Location>> GetAllGastgezinAsync();
    Task<Location?> GetGastgezinAsync(Guid id);
    Task<Location?> CreateGastgezinAsync(CreateUpdateLocationNawParameters newGastgezinParameters);
    Task<Location?> UpdateGastgezinAsync(Guid id, CreateUpdateLocationNawParameters updateNawGastgezinParameters);
    Task DeleteGastgezinAsync(Guid id);
}

