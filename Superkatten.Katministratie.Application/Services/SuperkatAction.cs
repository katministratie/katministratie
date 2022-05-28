using Microsoft.Extensions.Logging;
using Superkatten.Katministratie.Application.CageCard;
using Superkatten.Katministratie.Application.Interfaces;
using Superkatten.Katministratie.Contract;
using Superkatten.Katministratie.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Application.Services
{
    internal class SuperkatAction : ISuperkatAction
    {
        public readonly ILogger<SuperkatAction> _logger;
        public readonly ISuperkattenRepository _repository;
        public readonly ISuperkatCageCard _cageCardGenerator;

        public SuperkatAction(
            ILogger<SuperkatAction> logger,
            ISuperkattenRepository superkattenRepository,
            ISuperkatCageCard cageCardGenerator)
        {
            _logger = logger;
            _repository = superkattenRepository;
            _cageCardGenerator = cageCardGenerator;
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

        public async Task CreateSuperkatCageCardAsync(SuperkatCageCardPrintParameters parameters)
        {
            _ = await _cageCardGenerator.CreateCageCardAsync(parameters.Id);
        }
    }
}
