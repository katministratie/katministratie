using Superkatten.Katministratie.Application.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Application.Interfaces
{
    public interface ISuperkattenService
    {
        Task<Superkat> CreateSuperkatAsync(CreateSuperkatParameters createSuperkatDto);
        Task<Superkat> ReadSuperkatAsync(Guid id);
        Task<Superkat> UpdateSuperkatAsync(UpdateSuperkatParameters updateSuperkatDto);
        Task DeleteSuperkatAsync(Guid id);
        Task<IReadOnlyCollection<Superkat>> ReadAvailableSuperkattenAsync();
    }
}