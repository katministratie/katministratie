using Microsoft.Extensions.Logging;
using Superkatten.Katministratie.Application.Entities;
using Superkatten.Katministratie.Application.Exceptions;
using Superkatten.Katministratie.Application.Interfaces;
using Superkatten.Katministratie.Domain.Entities;
using Superkatten.Katministratie.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Application.Services
{
    public class GastgezinnenService : IGastgezinnenService
    {
        public readonly ILogger<GastgezinnenService> _logger;
        public readonly IGastgezinnenRepository _repository;
        public GastgezinnenService(
            ILogger<GastgezinnenService> logger,
            IGastgezinnenRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Gastgezin> CreateGastgezinAsync(CreateOrUpdateGastgezinParameters createGastgezinParameters)
        {
            if (string.IsNullOrEmpty(createGastgezinParameters.Name))
            {
                throw new ValidationException("Gastgezin name is invalid");
            }

            var gastgezin= new Gastgezin(
                Guid.NewGuid(),
                createGastgezinParameters.Name,
                createGastgezinParameters.Address,
                createGastgezinParameters.City,
                createGastgezinParameters.Phone
                );

            await _repository.CreateGastgezinAsync(gastgezin);

            return gastgezin;
        }

        public async Task DeleteGastgezinAsync(string name)
        {
            await _repository.DeleteGastgezinAsync(name);
        }

        public async Task<IReadOnlyCollection<Gastgezin>> ReadAvailableGastgezinAsync()
        {
            var gastgezinnen = await _repository.GetGastgezinnenAsync();
            return gastgezinnen.ToList();
        }

        public async Task<Gastgezin> ReadGastgezinAsync(string name)
        {
            var gastgezin = await _repository.GetGastgezinAsync(name);
            return gastgezin;
        }

        public async Task<Gastgezin> UpdateGastgezinAsync(CreateOrUpdateGastgezinParameters updateGastgezinParameters)
        {
            if (string.IsNullOrEmpty(updateGastgezinParameters.Name))
            {
                throw new ValidationException($"Gastgezin name is empty");
            }

            var gastgezin = await _repository.GetGastgezinAsync(updateGastgezinParameters.Name);
            if(gastgezin is null)
            {
                throw new ValidationException($"gastgezin {updateGastgezinParameters.Name} does not exsist");
            }

            var updatedGastgezin = new Gastgezin(
                gastgezin.Id,
                updateGastgezinParameters.Name,
                updateGastgezinParameters.Address,
                updateGastgezinParameters.City,
                updateGastgezinParameters.Phone
            );

            await _repository.UpdateGastgezinAsync(updateGastgezinParameters.Name, updatedGastgezin);

            return updatedGastgezin;
        }
    }
}
