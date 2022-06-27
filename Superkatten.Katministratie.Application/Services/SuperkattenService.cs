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
    public class SuperkattenService : ISuperkattenService
    {
        private const int WEEKS_IN_ONE_YEAR = 52;
        public const int DAY_IN_ONE_WEEK = 7;

        private readonly ILogger<SuperkattenService> _logger;
        private readonly ISuperkattenRepository _superkattenRepository;
        private readonly ISuperkatMapper _mapper;

        public SuperkattenService(
            ILogger<SuperkattenService> logger,
            ISuperkattenRepository superkattenRepository,
            ISuperkatMapper mapper)
        {
            _logger = logger;
            _superkattenRepository = superkattenRepository;
            _mapper = mapper;
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

            var uniqueCatNumber = await _superkattenRepository.GetSuperkatMaxNumberForGivenYearAsync(DateTimeOffset.Now.Year);
            
            var superkat = new Superkat(
                uniqueCatNumber,
                createSuperkatParameters.CatchDate,
                createSuperkatParameters.CatchLocation);

            superkat.SetCageNumber(createSuperkatParameters.CageNumber);
            superkat.SetRetour(createSuperkatParameters.Retour);
            superkat.SetIsKitten(createSuperkatParameters.IsKitten);
            superkat.SetBehaviour(_mapper.MapContractToDomain(createSuperkatParameters.Behaviour));
            superkat.SetArea(_mapper.MapContractToDomain(createSuperkatParameters.CatArea));
            superkat.SetGender(_mapper.MapContractToDomain(createSuperkatParameters.Gender));

            var estimatedWeeksOld = createSuperkatParameters.IsKitten
                ? createSuperkatParameters.EstimatedWeeksOld
                : WEEKS_IN_ONE_YEAR * createSuperkatParameters.EstimatedWeeksOld;

            var catchDate = createSuperkatParameters.CatchDate;
            var estimatedBirthday = catchDate.AddDays(-DAY_IN_ONE_WEEK * estimatedWeeksOld);
            superkat.SetBirthday(estimatedBirthday);

            superkat.SetLitterType(_mapper.MapContractToDomain(createSuperkatParameters.LitterType));
            superkat.SetWetFoodAllowed(createSuperkatParameters.WetFoodAllowed);
            superkat.SetFoodType(_mapper.MapContractToDomain(createSuperkatParameters.FoodType));
            superkat.SetColor(createSuperkatParameters.Color);

            var createdSuperkat = await _superkattenRepository.CreateSuperkatAsync(superkat);

            return createdSuperkat;
        }

        public async Task DeleteSuperkatAsync(Guid id)
        {
            await _superkattenRepository.DeleteSuperkatAsync(id);
        }

        public async Task<IReadOnlyCollection<Superkat>> ReadAvailableSuperkattenAsync()
        {
            var superkatten = await _superkattenRepository.GetAvailableSuperkattenAsync();            
            return superkatten.ToList();
        }

        public async Task<IReadOnlyCollection<Superkat>> ReadNotAssignedSuperkattenAsync()
        {
            var superkatten = await _superkattenRepository.GetNotAssignedSuperkattenAsync();
            return superkatten.ToList();
        }

        public async Task<Superkat> ReadSuperkatAsync(Guid id)
        {
            var superkat = await _superkattenRepository.GetSuperkatAsync(id);
            return superkat;
        }

        public async Task<Superkat> UpdateSuperkatAsync(UpdateSuperkatParameters updateSuperkatParameters)
        {
            var superkat = await _superkattenRepository.GetSuperkatAsync(updateSuperkatParameters.Id);
            if (superkat is null)
            {
                throw new ServiceException($"Superkat cannot be found for id {updateSuperkatParameters.Id}");
            }

            superkat.SetName(updateSuperkatParameters.Name);
            superkat.SetBirthday(updateSuperkatParameters.Birthday);

            await _superkattenRepository.UpdateSuperkatAsync(superkat);

            return superkat;
        }
    }
}