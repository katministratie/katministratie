using Microsoft.Extensions.Logging;
using Superkatten.Katministratie.Application.Exceptions;
using Superkatten.Katministratie.Application.Interfaces;
using Superkatten.Katministratie.Application.Mappers;
using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Domain.Entities;
using Superkatten.Katministratie.Infrastructure.Interfaces;
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
        public readonly ISuperkatMapper _superkatMapper;
        public GastgezinnenService(
            ILogger<GastgezinnenService> logger,
            IGastgezinnenRepository repository,
            ISuperkatMapper superkatMapper)
        {
            _logger = logger;
            _repository = repository;
            _superkatMapper = superkatMapper;
        }

        public async Task<Gastgezin> CreateGastgezinAsync(CreateUpdateGastgezinParameters createGastgezinParameters)
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
                createGastgezinParameters.Phone,
                new List<Superkat>()
                );

            await _repository.CreateGastgezinAsync(gastgezin);

            return gastgezin;
        }
        public async Task<Gastgezin> AssignSuperkattenAsync(AssignSuperkattenParameters assignSuperkattenParameters)
        {
            if (assignSuperkattenParameters.Id == Guid.Empty)
            {
                throw new ValidationException($"Gastgezin ID is empty");
            }

            var gastgezin = await _repository.GetGastgezinAsync(assignSuperkattenParameters.Id);
            if (gastgezin is null)
            {
                throw new ValidationException($"gastgezin with guid: {assignSuperkattenParameters.Id} does not exsist");
            }

            var updatedGastgezin = new Gastgezin(
                assignSuperkattenParameters.Id,
                gastgezin.Name,
                gastgezin.Address,
                gastgezin.City,
                gastgezin.Phone,
                assignSuperkattenParameters.AssignedSuperkatten
                    .Select(_superkatMapper.MapContractToDomain)
                    .ToList()
            );

            await _repository.UpdateGastgezinAsync(updatedGastgezin.Id, updatedGastgezin);

            return updatedGastgezin;
        }

        public async Task DeleteGastgezinAsync(Guid id)
        {
            await _repository.DeleteGastgezinAsync(id);
        }

        public async Task<IReadOnlyCollection<Gastgezin>> ReadAvailableGastgezinAsync()
        {
            var gastgezinnen = await _repository.GetGastgezinnenAsync();
            return gastgezinnen.ToList();
        }

        public async Task<Gastgezin> UpdateGastgezinAsync(Guid id, CreateUpdateGastgezinParameters updateGastgezinParameters)
        {
            if (string.IsNullOrEmpty(updateGastgezinParameters.Name))
            {
                throw new ValidationException($"Gastgezin name is empty");
            }

            var gastgezin = await _repository.GetGastgezinAsync(id);
            if (gastgezin is null)
            {
                throw new ValidationException($"gastgezin with guid: {id} does not exsist");
            }

            var updatedGastgezin = new Gastgezin(
                gastgezin.Id,
                updateGastgezinParameters.Name,
                updateGastgezinParameters.Address,
                updateGastgezinParameters.City,
                updateGastgezinParameters.Phone,
                gastgezin.Superkatten
            );

            await _repository.UpdateGastgezinAsync(id, updatedGastgezin);

            return updatedGastgezin;
        }
    }
}
