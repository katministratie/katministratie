using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Application.Interfaces
{
    public interface ISuperkattenService
    {
        Task<Superkat> CreateSuperkatAsync(CreateSuperkatParameters createSuperkatDto);
        Task<Superkat> ReadSuperkatAsync(Guid id);
        Task<Superkat> UpdateSuperkatAsync(Guid id, UpdateSuperkatParameters updateSuperkatDto);
        Task DeleteSuperkatAsync(Guid guid);
        Task<IReadOnlyCollection<Superkat>> ReadAllSuperkattenAsync();
        Task<IReadOnlyCollection<Superkat>> ReadAvailableSuperkattenAsync();
    }
}