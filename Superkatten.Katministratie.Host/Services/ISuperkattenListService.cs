using Superkatten.Katministratie.Host.Entities;

namespace Superkatten.Katministratie.Host.Services
{
    public interface ISuperkattenListService
    {
        public Task<List<Superkat>> GetAllSuperkattenAsync();
        public Task<Superkat> GetSuperkatAsync(Guid id);
        public Task<Superkat?> CreateSuperkatAsync(CreateSuperkatParameters newSuperkat);
        public Task UpdateSuperkatAsync(Guid id, UpdateSuperkatParameters updateSuperkat);
        public Task DeleteSuperkatAsync(Guid id);
    }
}
