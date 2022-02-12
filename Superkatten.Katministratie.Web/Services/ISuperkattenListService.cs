using Superkatten.Katministratie.Application.Contracts;

namespace Superkatten.Katministratie.Web.Services
{
    public interface ISuperkattenListService
    {
        public Task<List<Superkat>> GetAllSuperkattenAsync();
        public Task<Superkat> GetSuperkatAsync(int superkatNumber);
        public Task CreateSuperkatAsync(CreateSuperkatParameters newSuperkat);
        public Task UpdateSuperkatAsync(int superkatNumber, UpdateSuperkatParameters updateSuperkat);
        public Task DeleteSuperkatAsync(int superkatNumber);
    }
}
