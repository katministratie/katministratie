using Superkatten.Katministratie.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Infrastructure.Interfaces
{
    public interface IGastgezinnenRepository
    {
        Task<Gastgezin> CreateGastgezinAsync(Gastgezin gastgezin);
        Task<Gastgezin> GetGastgezinAsync(Guid id);
        Task DeleteGastgezinAsync(Guid id);
        Task<Gastgezin> UpdateGastgezinAsync(Guid id, Gastgezin gastgezin);
        Task<IReadOnlyCollection<Gastgezin>> GetGastgezinnenAsync();
    }
}
