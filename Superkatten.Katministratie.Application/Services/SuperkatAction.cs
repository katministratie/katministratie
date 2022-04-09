using Microsoft.Extensions.Logging;
using Superkatten.Katministratie.Application.Interfaces;
using Superkatten.Katministratie.Application.Mappers;
using Superkatten.Katministratie.Domain.Interfaces;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Application.Services
{
    internal class SuperkatAction : ISuperkatAction
    {
        public readonly ILogger<SuperkatAction> _logger;
        public readonly ISuperkattenRepository _repository;
        public readonly ISuperkattenMapper _superkattenMapper;
        public SuperkatAction(
            ILogger<SuperkatAction> logger,
            ISuperkattenRepository superkattenRepository,
            ISuperkattenMapper superkattenMapper)
        {
            _logger = logger;
            _repository = superkattenRepository;
            _superkattenMapper = superkattenMapper;
        }

        public Task GoingRetourAsync(int superkatNumber, bool retour)
        {
            throw new System.NotImplementedException();
        }

        public async Task ReserveAsync(int superkatNumber, bool reserve)
        {
            throw new System.NotImplementedException();
        }
    }
}
