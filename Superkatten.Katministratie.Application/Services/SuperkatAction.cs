using Microsoft.Extensions.Logging;
using Superkatten.Katministratie.Application.Interfaces;
using Superkatten.Katministratie.Application.Mappers;
using Superkatten.Katministratie.Application.PdfGenerator;
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
        public readonly ISuperkatCardPdfGenerator _pdfGenerator;

        public SuperkatAction(
            ILogger<SuperkatAction> logger,
            ISuperkattenRepository superkattenRepository,
            ISuperkattenMapper superkattenMapper,
            ISuperkatCardPdfGenerator pdfGenerator)
        {
            _logger = logger;
            _repository = superkattenRepository;
            _superkattenMapper = superkattenMapper;
            _pdfGenerator = pdfGenerator;
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

        public async Task CreateSuperkatCard(Guid id)
        {
            await _pdfGenerator.CreateSuperkatCardAsync(id);
        }
    }
}
