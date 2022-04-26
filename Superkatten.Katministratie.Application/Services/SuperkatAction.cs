using Microsoft.Extensions.Logging;
using Superkatten.Katministratie.Application.CageCard;
using Superkatten.Katministratie.Application.Interfaces;
using Superkatten.Katministratie.Application.Mappers;
using Superkatten.Katministratie.Application.Printing;
using Superkatten.Katministratie.Domain.Contracts;
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
        public readonly ISuperkatCageCard _cageCardGenerator;
        private readonly IPrinterService _printerService;

        public SuperkatAction(
            ILogger<SuperkatAction> logger,
            ISuperkattenRepository superkattenRepository,
            ISuperkattenMapper superkattenMapper,
            ISuperkatCageCard cageCardGenerator,
            IPrinterService printerService)
        {
            _logger = logger;
            _repository = superkattenRepository;
            _superkattenMapper = superkattenMapper;
            _cageCardGenerator = cageCardGenerator;
            _printerService = printerService;
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

        public async Task PrintSuperkatCageCardAsync(SuperkatCageCardPrintParameters parameters)
        {
            var filename = await _cageCardGenerator.CreateCageCardAsync(parameters.Id);
            await _printerService.PrintPdfAsync(filename, parameters.PrinterName);
        }
    }
}
