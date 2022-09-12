using Microsoft.Extensions.Logging;
using Superkatten.Katministratie.Application.Exceptions;
using Superkatten.Katministratie.Application.Interfaces;
using Superkatten.Katministratie.Application.Mappers;
using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Domain.Entities;
using Superkatten.Katministratie.Infrastructure.Interfaces;
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

        public SuperkattenService(
            ILogger<SuperkattenService> logger,
            ISuperkattenRepository superkattenRepository,
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
        }

        public async Task<Superkat> CreateSuperkatAsync(CreateSuperkatParameters createSuperkatParameters)
        {                        
            var maxSuperkatNumberForYear = await _superkattenRepository.GetMaxSuperkatNumberForYear(DateTimeOffset.Now.Year);

            var catchOriginData = _superkattenMapper.MapContractToDomain(createSuperkatParameters.CatchOrigin);

            var catchOrigin = await _catchOriginRepository.GetCatchOriginAsync(catchOriginData.Type, catchOriginData.Name);
            if (catchOrigin is null)
            {
                catchOrigin = await _catchOriginRepository.CreateCatchOriginAsync(catchOriginData.Type, catchOriginData.Name);
            }
            var superkat = new Superkat(
                maxSuperkatNumberForYear + 1, 
                createSuperkatParameters.CatchDate,
                catchOrigin
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

        private Superkat UpdateSuperkatDetails(Superkat superkat, CreateSuperkatParameters createSuperkatParameters)
        {
            var catchDate = createSuperkatParameters.CatchDate;
            var estimatedBirthday = catchDate.AddDays(-DAY_IN_ONE_WEEK * createSuperkatParameters.EstimatedWeeksOld);

            superkat.SetRetour(createSuperkatParameters.Retour);
            superkat.SetAgeCategory(_superkattenMapper.MapContractToDomain(createSuperkatParameters.AgeCategory));
            superkat.SetBehaviour(_superkattenMapper.MapContractToDomain(createSuperkatParameters.Behaviour));
            superkat.SetGender(_superkattenMapper.MapContractToDomain(createSuperkatParameters.Gender));
            superkat.SetBirthday(estimatedBirthday);
            superkat.SetLitterType(_superkattenMapper.MapContractToDomain(createSuperkatParameters.LitterType));
            superkat.SetWetFoodAllowed(createSuperkatParameters.WetFoodAllowed);
            superkat.SetFoodType(_superkattenMapper.MapContractToDomain(createSuperkatParameters.FoodType));
            superkat.SetColor(createSuperkatParameters.CatColor);

            return superkat
                .WithCageNumber(createSuperkatParameters.CageNumber)
                .WithCatArea(_superkattenMapper.MapContractToDomain(createSuperkatParameters.CatArea));
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
                .Where(s => !s.Retour && !s.Reserved && s.GastgezinId is null)
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

            var updatedSuperkat = superkat
                .WithName(updateSuperkatParameters.Name);

            await _superkattenRepository.UpdateSuperkatAsync(updatedSuperkat);

            return superkat;
        }

        public async Task<Superkat> UpdateSuperkatAsync(Guid guid, ReallocateSuperkatParameters reallocateSuperkatParameters)
        {
            var superkat = await _superkattenRepository.GetSuperkatAsync(guid);
            if (superkat is null)
            {
                throw new ServiceException($"Superkat cannot be found for id {guid}");
            }

            var updatedSuperkat = superkat
                .WithGastgezinId(reallocateSuperkatParameters.GastgezinId)
                .WithCatArea(_superkattenMapper.MapContractToDomain(reallocateSuperkatParameters.CatArea))
                .WithCageNumber(reallocateSuperkatParameters.CageNumber);

            await _superkattenRepository.UpdateSuperkatAsync(updatedSuperkat);

            return superkat;
        }

        public async Task<Superkat> UpdateSuperkatAsync(Guid guid, UpdateSuperkatPhotoParameters updateSuperkatPhotoParameters)
        {
            var superkat = await _superkattenRepository.GetSuperkatAsync(guid);
            if (superkat is null)
            {
                throw new ServiceException($"Superkat cannot be found for id {guid}");
            }

            var updatedSuperkat = superkat
                .WithPhoto(updateSuperkatPhotoParameters.PhotoData ?? Array.Empty<byte>());

            await _superkattenRepository.UpdateSuperkatAsync(updatedSuperkat);

            return updatedSuperkat;
        }


        public Task<IReadOnlyCollection<Superkat>> ReadNotNeutralizedSuperkattenAsync()
        {
            return _reportingRepository.GetNotNeutralizedSuperkatten();
        }
    }
}