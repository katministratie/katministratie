using Microsoft.Extensions.Logging;
using Superkatten.Katministratie.Application.Exceptions;
using Superkatten.Katministratie.Application.Interfaces;
using Superkatten.Katministratie.Application.Mappers;
using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Contract.ApiInterface.Reallocate;
using Superkatten.Katministratie.Domain.Entities;
using Superkatten.Katministratie.Domain.Entities.Locations;
using Superkatten.Katministratie.Infrastructure.Interfaces;
using Superkatten.Katministratie.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Application.Services
{
    public class SuperkattenService : ISuperkattenService
    {
        public const int DAY_IN_ONE_WEEK = 7;

        private readonly ILogger<SuperkattenService> _logger;
        private readonly ISuperkattenRepository _superkattenRepository;
        private readonly IReportingRepository _reportingRepository;
        private readonly IMedicalProceduresRepository _medicalProceduresRepository;
        private readonly ISuperkatMapper _superkattenMapper;
        private readonly ICatchOriginRepository _catchOriginRepository;
        private readonly ILocationRepository _locationRepository;

        public SuperkattenService(
            ILogger<SuperkattenService> logger,
            ISuperkattenRepository superkattenRepository,
            ILocationRepository locationRepository,
            IReportingRepository reportingRepository,
            IMedicalProceduresRepository medicalProceduresRepository,
            ICatchOriginRepository catchOriginRepository,
            ISuperkatMapper superkattenMapper)
        {
            _logger = logger;
            _superkattenRepository = superkattenRepository;
            _reportingRepository = reportingRepository;
            _medicalProceduresRepository = medicalProceduresRepository;
            _superkattenMapper = superkattenMapper;
            _catchOriginRepository = catchOriginRepository;
            _locationRepository = locationRepository;
        }

        public async Task<Superkat> CreateSuperkatAsync(CreateSuperkatParameters createSuperkatParameters)
        {                        
            var maxSuperkatNumberForYear = await _superkattenRepository.GetMaxSuperkatNumberForYear(DateTimeOffset.Now.Year);
            var catchOrigin = await CreateCatchOriginFromParametersAsync(createSuperkatParameters);
            var location = CreateLocationFromParametersAsync(createSuperkatParameters);

            var superkat = new Superkat(
                maxSuperkatNumberForYear + 1, 
                createSuperkatParameters.CatchDate,
                catchOrigin,
                location
            );
            
            superkat = UpdateSuperkatDetails(superkat, createSuperkatParameters);

            await _superkattenRepository.CreateSuperkatAsync(superkat);

            if (createSuperkatParameters.StrongholdGiven)
            {
                var medicalProcedure = new MedicalProcedure(
                    MedicalProcedureType.Stronghold,
                    superkat.Id,
                    DateTime.UtcNow,
                    "tijdens vangen");
                await _medicalProceduresRepository.AddMedicalProcedureAsync(medicalProcedure);
            }            

            return superkat;
        }

        private BaseLocation CreateLocationFromParametersAsync(CreateSuperkatParameters createSuperkatParameters)
        {
            var catArea = _superkattenMapper.MapContractToDomain(createSuperkatParameters.CatArea);
            return new Refuge(catArea, createSuperkatParameters.CageNumber);
        }

        private async Task<CatchOrigin> CreateCatchOriginFromParametersAsync(CreateSuperkatParameters createSuperkatParameters)
        {
            var catchOriginDetails = _superkattenMapper.MapContractToDomain(createSuperkatParameters.CatchOrigin);
            var catchOrigin = await _catchOriginRepository.GetCatchOriginAsync(catchOriginDetails.Type, catchOriginDetails.Name);
            return catchOrigin ?? await _catchOriginRepository.CreateCatchOriginAsync(catchOriginDetails.Type, catchOriginDetails.Name);
        }

        private Superkat UpdateSuperkatDetails(Superkat superkat, CreateSuperkatParameters createSuperkatParameters)
        {
            var catchDate = createSuperkatParameters.CatchDate;
            var estimatedBirthday = catchDate.AddDays(-DAY_IN_ONE_WEEK * createSuperkatParameters.EstimatedWeeksOld);

            return superkat.CreateUpdatedModel(
                estimatedBirthday,
                _superkattenMapper.MapContractToDomain(createSuperkatParameters.Behaviour),
                createSuperkatParameters.Retour,
                _superkattenMapper.MapContractToDomain(createSuperkatParameters.AgeCategory),
                _superkattenMapper.MapContractToDomain(createSuperkatParameters.Gender),
                _superkattenMapper.MapContractToDomain(createSuperkatParameters.LitterType),
                createSuperkatParameters.WetFoodAllowed,
                _superkattenMapper.MapContractToDomain(createSuperkatParameters.FoodType),
                createSuperkatParameters.CatColor);
        }

        public async Task DeleteSuperkatAsync(Guid guid)
        {
            await _superkattenRepository.DeleteSuperkatAsync(guid);
        }

        public async Task<IReadOnlyCollection<Superkat>> ReadAllSuperkattenAsync()
        {
            return await _superkattenRepository.GetSuperkattenAsync();
        }

        public async Task<IReadOnlyCollection<Superkat>> ReadAvailableSuperkattenAsync()
        {
            var superkatten = await _superkattenRepository.GetSuperkattenAsync();

            return superkatten
                .Where(s => !s.Retour && !s.Reserved && s.Location.LocationType == LocationType.Refuge)
                .ToList();
        }

        public async Task<Superkat> ReadSuperkatAsync(Guid guid)
        {
            var superkat = await _superkattenRepository.GetSuperkatAsync(guid);
            return superkat;
        }

        public async Task<Superkat> UpdateSuperkatAsync(Guid guid, UpdateSuperkatParameters updateSuperkatParameters)
        {
            var superkat = await _superkattenRepository.GetSuperkatAsync(guid);
            if (superkat is null)
            {
                throw new ServiceException($"Superkat cannot be found for id {guid}");
            }

            superkat.SetName(updateSuperkatParameters.Name);

            await _superkattenRepository.UpdateSuperkatAsync(superkat);

            return superkat;
        }

        public async Task<Superkat> ReallocateToGastgezinAsync(Guid superkatId, ReallocateToGastgezinParameters parameters)
        {
            var superkat = await _superkattenRepository.GetSuperkatAsync(superkatId);
            if (superkat is null)
            {
                throw new ServiceException($"Superkat cannot be found for id {superkatId}");
            }

            var location = await _locationRepository.GetLocationAsync(parameters.LocationId);
            if (location is null)
            {
                throw new ServiceException($"Hostfamily cannot be found for id {parameters.LocationId}");
            }

            superkat.Relocate(location);

            await _superkattenRepository.UpdateSuperkatAsync(superkat);

            return superkat;
        }

        public async Task<Superkat> UpdateSuperkatAsync(Guid guid, PhotoParameters parameters)
        {
            var superkat = await _superkattenRepository.GetSuperkatAsync(guid);
            if (superkat is null)
            {
                throw new ServiceException($"Superkat cannot be found for id {guid}");
            }

            superkat.SetPhoto(parameters.Photo ?? Array.Empty<byte>());

            await _superkattenRepository.UpdateSuperkatAsync(superkat);

            return superkat;
        }


        public Task<IReadOnlyCollection<Superkat>> ReadNotNeutralizedSuperkattenAsync()
        {
            return _reportingRepository.GetNotNeutralizedSuperkatten();
        }
    }
}