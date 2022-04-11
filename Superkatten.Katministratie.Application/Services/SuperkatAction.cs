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

        public async Task ToggleRetourAsync(int superkatNumber)
        {
            var superkat = await _repository.GetSuperkatAsync(superkatNumber);

            if (superkat is null)
            {
                throw new ApplicationException($"Superkat with number {superkatNumber} is unknown.");
            }

            var updatedSuperkat = new Domain.Entities.Superkat(superkatNumber, superkat.FoundDate, superkat.CatchLocation);
            updatedSuperkat.SetName(superkat.Name);
            updatedSuperkat.SetReserved(superkat.Reserved);
            var today = DateTimeOffset.Now;
            var weeksOld = (int)((today - superkat.Birthday).TotalDays / 7);
            updatedSuperkat.SetWeeksOld(weeksOld);

            updatedSuperkat.SetRetour(!superkat.Retour);

            await _repository.UpdateSuperkatAsync(updatedSuperkat);
        }

        public async Task ToggleReserveAsync(int superkatNumber)
        {
            var superkat = await _repository.GetSuperkatAsync(superkatNumber);

            if (superkat is null)
            {
                throw new ApplicationException($"Superkat with number {superkatNumber} is unknown.");
            }

            var updatedSuperkat = new Domain.Entities.Superkat(superkatNumber, superkat.FoundDate, superkat.CatchLocation);
            updatedSuperkat.SetName(superkat.Name);
            var today = DateTimeOffset.Now;
            var weeksOld = (int)((today - superkat.Birthday).TotalDays / 7);  
            updatedSuperkat.SetWeeksOld(weeksOld);
            
            updatedSuperkat.SetReserved(!superkat.Reserved);

            await _repository.UpdateSuperkatAsync(updatedSuperkat);
        }
    }
}
