using Superkatten.Katministratie.Host.Entities;

namespace Superkatten.Katministratie.Host.Services;

public interface IGastgezinService
{
    public Task<List<Gastgezin>> GetAllGastgezinAsync();
    public Task<Gastgezin?> GetGastgezinAsync(Guid id);
    public Task<Gastgezin?> CreateGastgezinAsync(CreateOrUpdateGastgezinParameters newGastgezinParameters);
    public Task UpdateGastgezinAsync(Guid id, CreateOrUpdateGastgezinParameters updateGastgezinParameters);
    public Task DeleteGastgezinAsync(Guid id);
}

