using Superkatten.Katministratie.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Domain.Interfaces
{
    public interface ISuperkattenRepository
    {
        Task<Superkat> CreateSuperkatAsync(Superkat superkat);
        Task<Superkat> GetSuperkatAsync(Guid id);
        Task DeleteSuperkatAsync(Guid id);
        Task UpdateSuperkatAsync(Superkat superkat);
        Task<IReadOnlyCollection<Superkat>> GetAvailableSuperkattenAsync();
        Task<IReadOnlyCollection<Superkat>> GetNotAssignedSuperkattenAsync();
        Task<int> GetSuperkatMaxNumberForGivenYearAsync(int year);
    }
}
