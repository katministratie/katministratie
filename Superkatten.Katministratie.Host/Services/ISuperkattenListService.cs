using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Host.Api;
using Superkatten.Katministratie.Host.Entities;

namespace Superkatten.Katministratie.Host.Services
{
    public interface ISuperkattenListService
    {
        public Task<List<Superkat>> GetAllSuperkattenAsync();
        public Task<List<Superkat>> GetAllNotAssignedSuperkattenAsync();
        public Task<Superkat> GetSuperkatAsync(Guid id);
        public Task<Superkat?> CreateSuperkatAsync(CreateSuperkatParameters newSuperkat);
        public Task<Superkat?> UpdateSuperkatAsync(Guid id, UpdateSuperkatParameters updateSuperkat);
        public Task DeleteSuperkatAsync(Guid id);
    }
}
