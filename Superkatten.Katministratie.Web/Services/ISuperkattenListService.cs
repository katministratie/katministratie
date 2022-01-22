using Superkatten.Katministratie.Application.Contracts;

namespace Superkatten.Katministratie.Web.Services
{
    public interface ISuperkattenListService
    {
        public Task<List<Superkat>> GetAllSuperkattenAsync();
    }
}
