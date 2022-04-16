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
        public readonly ILogger<SuperkatAction> Logger;
        public readonly ISuperkattenRepository Repository;
        public readonly ISuperkattenMapper SuperkattenMapper;        public SuperkatAction(
            ILogger<SuperkatAction> logger,
            ISuperkattenRepository superkattenRepository,
            ISuperkattenMapper superkattenMapper)
        {
            Logger = logger;
            Repository = superkattenRepository;
            SuperkattenMapper = superkattenMapper;
        }

        public async Task ToggleRetourAsync(int superkatNumber)
        {
            var superkat = await Repository.GetSuperkatAsync(superkatNumber);

            if (superkat is null)
            {
                throw new ApplicationException($"Superkat with number {superkatNumber} is unknown.");
            }

            var updatedSuperkat = new Domain.Entities.Superkat(
                superkatNumber, 
                superkat.FoundDate, 
                superkat.CatchLocation
                ).WithName(superkat.Name)
                .WithBirthday(superkat.Birthday)
                .WithReserved(superkat.Reserved)
                .WithRetour(!superkat.Retour);

            await Repository.UpdateSuperkatAsync(updatedSuperkat);
        }

        public async Task ToggleReserveAsync(int superkatNumber)
        {
            var superkat = await Repository.GetSuperkatAsync(superkatNumber);

            if (superkat is null)
            {
                throw new ApplicationException($"Superkat with number {superkatNumber} is unknown.");
            }

            var updatedSuperkat = new Domain.Entities.Superkat(
                superkatNumber, 
                superkat.FoundDate, 
                superkat.CatchLocation
                ).WithName(superkat.Name)
                .WithBirthday(superkat.Birthday)
                .WithRetour(superkat.Retour)
                .WithReserved(!superkat.Reserved);

            await Repository.UpdateSuperkatAsync(updatedSuperkat);
        }
    }
}
