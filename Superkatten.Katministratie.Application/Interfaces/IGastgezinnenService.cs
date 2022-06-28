using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Application.Interfaces
{
    public interface IGastgezinnenService
    {
        Task<Gastgezin> CreateGastgezinAsync(CreateUpdateGastgezinParameters createGastgezinDtoParameters);
        Task<Gastgezin> AssignSuperkattenAsync(AssignSuperkattenParameters updateGastgezinParameters);
        Task<Gastgezin> UpdateGastgezinAsync(Guid id, CreateUpdateGastgezinParameters updateGastgezinParameters);
        Task DeleteGastgezinAsync(Guid id);
        Task<IReadOnlyCollection<Gastgezin>> ReadAvailableGastgezinAsync();
    }
}
