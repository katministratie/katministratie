using Superkatten.Katministratie.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Domain.Interfaces
{
    public interface ISuperkattenRepository
    {
        Task<Superkat> CreateSuperkatAsync(Superkat superkat);
        Task<Superkat> GetSuperkatAsync(int superkatNumber);
        Task DeleteSuperkatAsync(int superkatNumber);
        Task<Superkat> UpdateSuperkatAsync(Superkat superkat);
        Task<IReadOnlyCollection<Superkat>> GetAvailableSuperkattenAsync();
        Task<int> GetSuperkatCountForGivenYearAsync(int year);
    }
}
