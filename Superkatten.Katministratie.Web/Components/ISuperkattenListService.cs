using Superkatten.Katministratie.Application.Contracts;

namespace Superkatten.Katministratie.Web.Components
{
    public interface ISuperkattenListService
    {
        public Task<List<Superkat>> GetAllSuperkattenAsync();
    }
}
