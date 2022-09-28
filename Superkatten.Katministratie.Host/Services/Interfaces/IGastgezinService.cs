using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.Host.Services;

public interface IGastgezinService
{
    Task<List<Gastgezin>> GetAllGastgezinAsync();
    Task<Gastgezin?> GetGastgezinAsync(Guid id);
    Task<Gastgezin?> CreateGastgezinAsync(CreateUpdateLocationNawParameters newGastgezinParameters);
    Task<Gastgezin?> UpdateGastgezinAsync(Guid id, CreateUpdateLocationNawParameters updateNawGastgezinParameters);
    Task DeleteGastgezinAsync(Guid id);
}

