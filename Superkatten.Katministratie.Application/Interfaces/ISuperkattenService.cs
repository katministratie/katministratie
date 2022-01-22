using Superkatten.Katministratie.Application.Contracts;
using Superkatten.Katministratie.Application.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Application.Interfaces
{
    public interface ISuperkattenService
    {
        Task<Superkat> CreateSuperkatAsync(CreateSuperkatParameters createSuperkatDto);
        Task<Superkat> ReadSuperkatAsync(int superkatId);
        Task<Superkat> UpdateSuperkatAsync(int number, UpdateSuperkatParameters updateSuperkatDto);
        Task DeleteSuperkatAsync(int superkatId);
        Task<IReadOnlyCollection<Superkat>> ReadAvailableSUperkattenAsync();
    }
}