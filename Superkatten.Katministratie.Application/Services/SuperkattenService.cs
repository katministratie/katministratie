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
            if (string.IsNullOrEmpty(createSuperkatParameters.Kleur))
            {
                throw new ValidationException("Superkat kleur is invalid");
            }

            if (string.IsNullOrEmpty(createSuperkatParameters.CatchLocation))
            {
                throw new ValidationException($"Superkat location is empty");
            }

            var today = DateTimeOffset.Now;
            int lastUsedSuperkatNumber = await _superkattenRepository.GetSuperkatMaxNumberForGivenYearAsync(today.Year);
            var superkatNumber = lastUsedSuperkatNumber + 1;

            var superkat = new Domain.Entities.Superkat(superkatNumber, createSuperkatParameters.Kleur, today, createSuperkatParameters.CatchLocation);

            if (createSuperkatParameters.WeeksOld > 0)
            {
                var daysOld = 7 * createSuperkatParameters.WeeksOld;
                var birthDay = today.AddDays(-daysOld);
                superkat = superkat.SetBirthday(birthDay);
            }

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

            var superkat = await _superkattenRepository.GetSuperkatAsync(number);

            var birthday = superkat.FoundDate.AddDays(-updateSuperkatParameters.DaysOld);
            var updatedSuperkat = superkat
                .SetName(updateSuperkatParameters.Name)
                .SetBirthday(birthday);

            await _superkattenRepository.UpdateSuperkatAsync(updatedSuperkat);

            return _superkattenMapper.MapFromDomain(updatedSuperkat);
        }
    }
}