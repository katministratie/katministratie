using Microsoft.Extensions.Logging;
using Superkatten.Katministratie.Application.Exceptions;
using Superkatten.Katministratie.Application.Interfaces;
using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Contract.Entities;
using Superkatten.Katministratie.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Application.Services;

public class MedicalProcedureService : IMedicalProcedureService
{
    private readonly ILogger<MedicalProcedureService> _logger;
    private readonly ISuperkattenRepository _superkattenRepository;
    private readonly IMedicalProceduresRepository _medicalProceduresRepository;

    public MedicalProcedureService(
        ILogger<MedicalProcedureService> logger,
        ISuperkattenRepository superkattenRepository,
        IMedicalProceduresRepository medicalProceduresRepository
    )
    {
        _logger = logger;
        _superkattenRepository = superkattenRepository;
        _medicalProceduresRepository = medicalProceduresRepository;
    }

    public Task AddMedicalProcedureAsync(AddMedicalProcedureParameters parameters)
    {
        var superkat = _superkattenRepository.GetSuperkatAsync(parameters.SuperkatId);
        if (superkat is null)
        {
            throw new ServiceException($"Superkat with id '{parameters.SuperkatId}' does not exist");
        }

        return _medicalProceduresRepository.AddMedicalProcedureAsync(parameters);
    }

    public async Task<IReadOnlyCollection<MedicalProcedure>> GetAllMedicalProceduresAsync()
    {
        var medicalProcedures = await _medicalProceduresRepository.GetAllMedicalProcedureAsync();

        return medicalProcedures
            .Select(_medicalProcedureMapper.MapToContracy)
            .ToList();
    }
}
