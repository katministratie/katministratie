using Superkatten.Katministratie.Application.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Application.Interfaces
{
    public interface IGastgezinnenService
    {
        Task<Gastgezin> CreateGastgezinAsync(CreateUpdateGastgezinParameters createGastgezinDto);
        Task<Gastgezin> ReadGastgezinAsync(string name);
        Task<Gastgezin> UpdateGastgezinAsync(string name, CreateUpdateGastgezinParameters updateGastgezinDto);
        Task DeleteGastgezinAsync(string name);
        Task<IReadOnlyCollection<Gastgezin>> ReadAvailableGastgezinAsync();
    }
}
