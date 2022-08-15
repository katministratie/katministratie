using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.Host.Services;

public interface IGastgezinService
{
    Task<List<Gastgezin>> GetAllGastgezinAsync();
    Task<Gastgezin?> GetGastgezinAsync(Guid id);
    Task<Gastgezin?> CreateGastgezinAsync(CreateUpdateGastgezinParameters newGastgezinParameters);
    Task<Gastgezin?> UpdateGastgezinAsync(Guid id, CreateUpdateGastgezinParameters updateNawGastgezinParameters);
    Task DeleteGastgezinAsync(Guid id);
}

