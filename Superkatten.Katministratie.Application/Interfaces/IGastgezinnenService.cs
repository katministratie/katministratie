using Superkatten.Katministratie.Application.Entities;
using Superkatten.Katministratie.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Application.Interfaces
{
    public interface IGastgezinnenService
    {
        Task<Gastgezin> CreateGastgezinAsync(CreateOrUpdateGastgezinParameters createGastgezinDto);
        Task<Gastgezin> UpdateGastgezinAsync(Guid id, CreateOrUpdateGastgezinParameters updateGastgezinDto);
        Task DeleteGastgezinAsync(Guid id);
        Task<IReadOnlyCollection<Gastgezin>> ReadAvailableGastgezinAsync();
    }
}
