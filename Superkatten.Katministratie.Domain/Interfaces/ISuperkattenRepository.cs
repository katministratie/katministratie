using Superkatten.Katministratie.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Domain.Interfaces
{
    public interface ISuperkattenRepository
    {
        Task<Superkat> CreateSuperkatAsync(Superkat createSuperkat);
        Task<Superkat> GetSuperkatAsync(int superkatId);
        Task DeleteSuperkatAsync(int superkatId);
        Task<Superkat> UpdateSuperkatAsync(Superkat updateSuperkat);
        Task<IReadOnlyCollection<Superkat>> GetAvailableSuperkattenAsync();
    }
}
