using Microsoft.Extensions.Logging;
using Superkatten.Katministratie.Application.Entities;
using Superkatten.Katministratie.Application.Interfaces;
using Superkatten.Katministratie.Application.Mappers;
using Superkatten.Katministratie.Domain.CRUD;
using Superkatten.Katministratie.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Superkatten.Katministratie.Application.Exceptions;

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

        public async Task<Superkat> CreateSuperkatAsync(CreateOrModifySuperkatParameters createSuperkatDto)
        {
            if (string.IsNullOrEmpty(createSuperkatDto.Name))
            {
                throw new ValidationException($"Superkat name is empty");
            }

            var superkatNumber = 1; //TODO: GetUniqueSuperkatNumber();
            var superkat = new Domain.Entities.Superkat(superkatNumber, createSuperkatDto.Name);

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

        public async Task<Superkat> ReadSuperkatAsync(int superkatId)
        {
            var superkat = await _superkattenRepository.GetSuperkatAsync(superkatId);

            return _superkattenMapper.MapFromDomain(superkat);
        }

        public async Task<Superkat> UpdateSuperkatAsync(CreateOrModifySuperkatParameters updateSuperkatDto)
        {
            var superkat = await _superkattenRepository.GetSuperkatAsync(updateSuperkatDto.Number);

            var newSuperkat = superkat.CreateUpdatedModel(
                updateSuperkatDto.Name
            );

            await _superkattenRepository.UpdateSuperkatAsync(newSuperkat);

            return _superkattenMapper.MapFromDomain(newSuperkat);
        }
    }
}