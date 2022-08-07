using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Contract.ApiInterface.Reporting;
using Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.Host.Services
{
    public interface ISuperkattenListService
    {
        public Task<List<Superkat>> GetAllSuperkattenAsync();
        public Task<List<Superkat>> GetAllNotAssignedSuperkattenAsync();
        public Task<List<Superkat>> GetCageCardEmailSuperkattenAsync(RequestCageCardEmailParameters requestParameters);
        public Task<Superkat> GetSuperkatAsync(Guid id);
        public Task<Superkat?> CreateSuperkatAsync(CreateSuperkatParameters newSuperkat);
        public Task UpdateSuperkatAsync(Guid id, UpdateSuperkatParameters updateSuperkat);
        public Task DeleteSuperkatAsync(Guid id);
    }
}
