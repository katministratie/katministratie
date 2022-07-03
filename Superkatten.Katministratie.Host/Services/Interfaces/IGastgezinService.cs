using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.Host.Services;

public interface IGastgezinService
{
    public Task<List<Gastgezin>> GetAllGastgezinAsync();
    public Task<Gastgezin?> GetGastgezinAsync(Guid id);
    public Task<Gastgezin?> CreateGastgezinAsync(CreateUpdateGastgezinParameters newGastgezinParameters);
    public Task<Gastgezin?> UpdateGastgezinAsync(Guid id, CreateUpdateGastgezinParameters updateNawGastgezinParameters);
    public Task DeleteGastgezinAsync(Guid id);
}

