using Superkatten.Katministratie.Application.Entities;
using Superkatten.Katministratie.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Application.Interfaces
{
    public interface IGastgezinnenService
    {
        Task<Gastgezin> CreateGastgezinAsync(CreateOrUpdateGastgezinParameters createGastgezinDto);
        Task<Gastgezin> ReadGastgezinAsync(string name);
        Task<Gastgezin> UpdateGastgezinAsync(CreateOrUpdateGastgezinParameters updateGastgezinDto);
        Task DeleteGastgezinAsync(string name);
        Task<IReadOnlyCollection<Gastgezin>> ReadAvailableGastgezinAsync();
    }
}
