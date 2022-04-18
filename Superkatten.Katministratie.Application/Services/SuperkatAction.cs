using Microsoft.Extensions.Logging;
using Superkatten.Katministratie.Application.Interfaces;
using Superkatten.Katministratie.Application.Mappers;
using Superkatten.Katministratie.Domain.Interfaces;
using System;
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

        public async Task ToggleRetourAsync(Guid id)
        {
            var superkat = await _repository.GetSuperkatAsync(id);            
            superkat.SetRetour(!superkat.Retour);
            await _repository.UpdateSuperkatAsync(superkat);
        }

        public async Task ToggleReserveAsync(Guid id)
        {
            var superkat = await _repository.GetSuperkatAsync(id);
            superkat.SetReserved(!superkat.Reserved);
            await _repository.UpdateSuperkatAsync(superkat);
        }
    }
}
