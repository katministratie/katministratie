using Superkatten.Katministratie.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Domain.Interfaces
{
    public interface ISuperkattenRepository
    {
        Task CreateSuperkatAsync(Superkat superkat);
        Task<Superkat> GetSuperkatAsync(Guid id);
        Task DeleteSuperkatAsync(Guid id);
        Task UpdateSuperkatAsync(Superkat superkat);
        Task<IReadOnlyCollection<Superkat>> GetAvailableSuperkattenAsync();
        Task<int> GetSuperkatMaxNumberForGivenYearAsync(int year);
    }
}
