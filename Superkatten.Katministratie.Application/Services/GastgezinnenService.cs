using Microsoft.Extensions.Logging;
using Superkatten.Katministratie.Application.Contracts;
using Superkatten.Katministratie.Application.Exceptions;
using Superkatten.Katministratie.Application.Interfaces;
using Superkatten.Katministratie.Application.Mappers;
using Superkatten.Katministratie.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Application.Services
{
    public class GastgezinnenService : IGastgezinnenService
    {
        public readonly ILogger<GastgezinnenService> _logger;
        public readonly IGastgezinnenRepository _repository;
        public readonly IGastgezinnenMapper _mapper;
        public GastgezinnenService(
            ILogger<GastgezinnenService> logger,
            IGastgezinnenRepository repository,
            IGastgezinnenMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Gastgezin> CreateGastgezinAsync(string name, CreateUpdateGastgezinParameters createGastgezinParameters)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ValidationException("Gastgezin name is invalid");
            }

            var gastgezin= new Domain.Entities.Gastgezin(
                name,
                createGastgezinParameters.Address,
                createGastgezinParameters.City,
                createGastgezinParameters.Phone
                );

            await _repository.CreateGastgezinAsync(gastgezin);

            return _mapper.MapFromDomain(gastgezin);
        }

        public async Task DeleteGastgezinAsync(string name)
        {
            await _repository.DeleteGastgezinAsync(name);
        }

        public async Task<IReadOnlyCollection<Gastgezin>> ReadAvailableGastgezinAsync()
        {
            var gastgezinnen = await _repository.GetGastgezinnenAsync();

            return gastgezinnen
                .Select(gastgezin => _mapper.MapFromDomain(gastgezin))
                .ToList();
        }

        public async Task<Gastgezin> ReadGastgezinAsync(string name)
        {
            var gastgezin = await _repository.GetGastgezinAsync(name);

            return _mapper.MapFromDomain(gastgezin);
        }

        public async Task<Gastgezin> UpdateGastgezinAsync(string name, CreateUpdateGastgezinParameters updateGastgezinParameters)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ValidationException($"Gastgezin name is empty");
            }

            var gastgezin = await _repository.GetGastgezinAsync(name);
            if(gastgezin is null)
            {
                throw new ValidationException($"gastgezin {name} does not exsist");
            }

            var updatedGastgezin = new Domain.Entities.Gastgezin(
                name,
                updateGastgezinParameters.Address,
                updateGastgezinParameters.City,
                updateGastgezinParameters.Phone
            );

            await _repository.UpdateGastgezinAsync(name, updatedGastgezin);

            return _mapper.MapFromDomain(updatedGastgezin);
        }
    }
}
