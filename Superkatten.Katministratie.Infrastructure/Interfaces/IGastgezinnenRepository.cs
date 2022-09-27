using Superkatten.Katministratie.Domain.Entities.Locations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Infrastructure.Interfaces
{
    public interface IGastgezinnenRepository
    {
        Task CreateGastgezinAsync(Gastgezin gastgezin);
        Task<Gastgezin> GetGastgezinAsync(Guid id);
        Task DeleteGastgezinAsync(Guid id);
        Task UpdateGastgezinAsync(Gastgezin gastgezin);
        Task<IReadOnlyCollection<Gastgezin>> GetGastgezinnenAsync();
    }
}
