using Microsoft.Extensions.Logging;
using Superkatten.Katministratie.Application.Entities;
using Superkatten.Katministratie.Application.Exceptions;
using Superkatten.Katministratie.Application.Interfaces;
using Superkatten.Katministratie.Application.Mappers;
using Superkatten.Katministratie.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Application.Services
{
    public class SuperkattenService : ISuperkattenService
    {
        public readonly ILogger<SuperkattenService> _logger;
        public readonly ISuperkattenRepository _superkattenRepository;
        public readonly ISuperkattenMapper _superkattenMapper;

        public SuperkattenService(
            ILogger<SuperkattenService> logger,
            ISuperkattenRepository superkattenRepository,
            ISuperkattenMapper superkattenMapper)
        {
            _logger = logger;
            _superkattenRepository = superkattenRepository;
            _superkattenMapper = superkattenMapper;
        }

        public async Task<Superkat> CreateSuperkatAsync(CreateSuperkatParameters createSuperkatParameters)
        {
            if (string.IsNullOrEmpty(createSuperkatParameters.CatchLocation))
            {
                throw new ValidationException($"Superkat location is empty");
            }

            //if (createSuperkatParameters.CatchDate < createSuperkatParameters.Birthday)
            //{
            //    throw new ValidationException($"Not possible to catch the cat before it is born");
            //}

            var superkat = new Domain.Entities.Superkat(
                Guid.NewGuid(),
                createSuperkatParameters.CatchDate,
                createSuperkatParameters.CatchLocation);
            superkat.SetBirthday(createSuperkatParameters.Birthday);
            superkat.SetArea(createSuperkatParameters.Area);
            superkat.SetCageNumber(createSuperkatParameters.CageNumber);
            superkat.SetBehaviour(createSuperkatParameters.Behaviour);
            superkat.SetRetour(createSuperkatParameters.Retour);
            
            var uniqueCatNumber = await _superkattenRepository.GetSuperkatMaxNumberForGivenYearAsync(DateTimeOffset.Now.Year);
            superkat.SetNumber(uniqueCatNumber);

            await _superkattenRepository.CreateSuperkatAsync(superkat);

            return _superkattenMapper.MapFromDomain(superkat);
        }

        public async Task DeleteSuperkatAsync(Guid id)
        {
            await _superkattenRepository.DeleteSuperkatAsync(id);
        }

        public async Task<IReadOnlyCollection<Superkat>> ReadAvailableSuperkattenAsync()
        {
            var superkatten = await _superkattenRepository.GetAvailableSuperkattenAsync();
            
            return superkatten
                .Select(superkat => _superkattenMapper.MapFromDomain(superkat))
                .ToList();
        }

        public async Task<Superkat> ReadSuperkatAsync(Guid id)
        {
            var superkat = await _superkattenRepository.GetSuperkatAsync(id);

            return _superkattenMapper.MapFromDomain(superkat);
        }

        public async Task<Superkat> UpdateSuperkatAsync(UpdateSuperkatParameters updateSuperkatParameters)
        {
            var superkat = await _superkattenRepository.GetSuperkatAsync(updateSuperkatParameters.Id);
            if (superkat is null)
            {
                throw new ApplicationException($"Superkat cannot be found for id {updateSuperkatParameters.Id}");
            }

            superkat.SetName(updateSuperkatParameters.Name);
            superkat.SetBirthday(updateSuperkatParameters.Birthday);

            await _superkattenRepository.UpdateSuperkatAsync(superkat);

            return _superkattenMapper.MapFromDomain(superkat);
        }
    }
}