using Superkatten.Katministratie.Application.Entities;
using Superkatten.Katministratie.Domain.CRUD;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Application.Interfaces
{
    public interface ISuperkattenService
    {
        Task<Superkat> CreateSuperkatAsync(CreateOrModifySuperkatParameters createSuperkatDto);
        Task<Superkat> ReadSuperkatAsync(int superkatId);
        Task<Superkat> UpdateSuperkatAsync(CreateOrModifySuperkatParameters updateSuperkatDto);
        Task DeleteSuperkatAsync(int superkatId);
        Task<IReadOnlyCollection<Superkat>> ReadAvailableSUperkattenAsync();

    }
}