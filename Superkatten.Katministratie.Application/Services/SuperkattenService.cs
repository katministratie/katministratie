using Microsoft.Extensions.Logging;
using Superkatten.Katministratie.Application.Contracts;
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
            if (string.IsNullOrEmpty(createSuperkatParameters.Name))
            {
                throw new ValidationException($"Superkat name is empty");
            }

            int superkatCountForYear = await _superkattenRepository.GetSuperkatCountForGivenYear(DateTime.Now.Year);
            var superkat = new Domain.Entities.Superkat(
                superkatCountForYear + 1, 
                createSuperkatParameters.Name, 
                DateTimeOffset.Now,
                createSuperkatParameters.Location
            );

            await _superkattenRepository.CreateSuperkatAsync(superkat);

            return _superkattenMapper.MapFromDomain(superkat);
        }

        public async Task DeleteSuperkatAsync(int superkatId)
        {
            await _superkattenRepository.DeleteSuperkatAsync(superkatId);
        }

        public async Task<IReadOnlyCollection<Superkat>> ReadAvailableSUperkattenAsync()
        {
            var superkatten = await _superkattenRepository.GetAvailableSuperkattenAsync();
            
            return superkatten
                .Select(superkat => _superkattenMapper.MapFromDomain(superkat))
                .ToList();
        }

        public async Task<Superkat> ReadSuperkatAsync(int superkatNumber)
        {
            var superkat = await _superkattenRepository.GetSuperkatAsync(superkatNumber);

            return _superkattenMapper.MapFromDomain(superkat);
        }

        public async Task<Superkat> UpdateSuperkatAsync(int number, UpdateSuperkatParameters updateSuperkatParameters)
        {
            if (number <=  0)
            {
                throw new ValidationException($"Superkat number ({number}) is invallid");
            }

            if (string.IsNullOrEmpty(updateSuperkatParameters.Name))
            {
                throw new ValidationException("Superkat name is empty");
            }

            var superkat = await _superkattenRepository.GetSuperkatAsync(number);

            var newSuperkat = superkat.CreateUpdatedModel(
                updateSuperkatParameters.Name,
                updateSuperkatParameters.Location
            );

            await _superkattenRepository.UpdateSuperkatAsync(newSuperkat);

            return _superkattenMapper.MapFromDomain(newSuperkat);
        }
    }
}