using Microsoft.Extensions.Logging;
using Superkatten.Katministratie.Application.Exceptions;
using Superkatten.Katministratie.Application.Interfaces;
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
        public readonly IGastgezinnenRepository _gastgezinRepository;
        private readonly ISuperkattenRepository _superkattenRepository;

        public GastgezinnenService(
            ILogger<GastgezinnenService> logger,
            IGastgezinnenRepository gastgezinRepository,
            ISuperkattenRepository superkattenRepository
        )
        {
            _logger = logger;
            _gastgezinRepository = gastgezinRepository;
            _superkattenRepository = superkattenRepository;
        }

        public async Task<Gastgezin> CreateGastgezinAsync(CreateUpdateGastgezinParameters createGastgezinParameters)
        {
            if (string.IsNullOrEmpty(createGastgezinParameters.Name))
            {
                throw new ValidationException("Gastgezin name is invalid");
            }

            var gastgezin = new Gastgezin(
                createGastgezinParameters.Name,
                createGastgezinParameters.Address,
                createGastgezinParameters.City,
                createGastgezinParameters.Phone
                );

            await _gastgezinRepository.CreateGastgezinAsync(gastgezin);

            return gastgezin;
        }
       
        public async Task DeleteGastgezinAsync(Guid id)
        {
            await _gastgezinRepository.DeleteGastgezinAsync(id);

            // delete all assignments for the superkatten
            var superkatten = await _superkattenRepository.GetAllSuperkattenAsync();
            var toBeUpdated = superkatten
                .Where(o => o.GastgezinId == id)
                .ToList();

            foreach(var superkat in toBeUpdated)
            {
                var updatedSuperkat = superkat.WithGastgezinId(null);
                await _superkattenRepository.UpdateSuperkatAsync(updatedSuperkat);
            }
        }

        public async Task<IReadOnlyCollection<Gastgezin>> ReadAvailableGastgezinAsync()
        {
            var gastgezinnen = await _gastgezinRepository.GetGastgezinnenAsync();
            return gastgezinnen.ToList();
        }

        public async Task<Gastgezin> UpdateGastgezinAsync(Guid id, CreateUpdateGastgezinParameters updateGastgezinParameters)
        {
            if (string.IsNullOrEmpty(updateGastgezinParameters.Name))
            {
                throw new ValidationException($"Gastgezin name is empty");
            }

            var gastgezin = await _gastgezinRepository.GetGastgezinAsync(id);
            if (gastgezin is null)
            {
                throw new ValidationException($"gastgezin with guid: {id} does not exsist");
            }

            var updatedGastgezin = gastgezin.Update(
                updateGastgezinParameters.Name,
                updateGastgezinParameters.Address,
                updateGastgezinParameters.City,
                updateGastgezinParameters.Phone
            );

            await _gastgezinRepository.UpdateGastgezinAsync(updatedGastgezin);

            return updatedGastgezin;
        }
    }
}
