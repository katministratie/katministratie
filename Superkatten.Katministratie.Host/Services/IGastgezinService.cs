using Superkatten.Katministratie.Contract;
using Superkatten.Katministratie.Host.Entities;

namespace Superkatten.Katministratie.Host.Services;

public interface IGastgezinService
{
    public Task<List<Gastgezin>> GetAllGastgezinAsync();
    public Task<Gastgezin?> GetGastgezinAsync(Guid id);
    public Task<Gastgezin?> CreateGastgezinAsync(CreateOrUpdateNawGastgezinParameters newGastgezinParameters);
    public Task<Gastgezin?> UpdateGastgezinAsync(Guid id, CreateOrUpdateGastgezinParameters updateGastgezinParameters);
    public Task<Gastgezin?> UpdateGastgezinAsync(Guid id, CreateOrUpdateNawGastgezinParameters updateNawGastgezinParameters);
    public Task DeleteGastgezinAsync(Guid id);
}

