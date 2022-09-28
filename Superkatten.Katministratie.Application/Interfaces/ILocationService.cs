using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Domain.Entities.Locations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Application.Interfaces
{
    public interface ILocationService
    {
        Task<BaseLocation> CreateHostFamilyAsync(CreateUpdateLocationNawParameters parameters);
        Task<IReadOnlyCollection<BaseLocation>> GetLocationsAsync();
        Task<BaseLocation> UpdateLocationAsync(Guid locationId, CreateUpdateLocationNawParameters parameters);
        Task DeleteLocationAsync(Guid id);
    }
}
