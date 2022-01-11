using System.Collections.Generic;
using System.Threading.Tasks;
using Superkatten.Application.View.Models;

namespace Superkatten.Application.View.Services
{
    public interface ISuperkattenService
    {
        Task<IReadOnlyCollection<Superkat>> GetSuperkattenAsync();
    }
}