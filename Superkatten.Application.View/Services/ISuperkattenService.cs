using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.View.Services
{
    public interface ISuperkattenService : IHostedService
    {
        Task<IReadOnlyCollection<Superkat>> GetSuperkattenAsync();
    }
}