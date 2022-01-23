using Superkatten.Katministratie.Application.Contracts;

namespace Superkatten.Katministratie.Web.Services
{
    public interface ISuperkattenListService
    {
        public Task<List<Superkat>> GetAllSuperkattenAsync();
        public Task<Superkat> GetSuperkatAsync(int superkatNumber);
        public Task CreateSuperkat(CreateSuperkatParameters newSuperkat);
        public Task UpdateSuperkat(int superkatNumber, UpdateSuperkatParameters updateSuperkat);
    }
}
