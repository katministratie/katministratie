using Superkatten.Katministratie.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Domain.Interfaces
{
    public interface IGastgezinnenRepository
    {
        Task<Gastgezin> CreateGastgezinAsync(Gastgezin gastgezin);
        Task<Gastgezin> GetGastgezinAsync(string name);
        Task DeleteGastgezinAsync(string name);
        Task<Gastgezin> UpdateGastgezinAsync(string name, Gastgezin gastgezin);
        Task<IReadOnlyCollection<Gastgezin>> GetGastgezinnenAsync();
    }
}
