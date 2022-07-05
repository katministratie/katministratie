using Microsoft.Extensions.Logging;
using Superkatten.Katministratie.Application.Exceptions;
using Superkatten.Katministratie.Application.Interfaces;
using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Infrastructure.Interfaces;
using Superkatten.Katministratie.Infrastructure.Persistence;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Application.Services;

public class MedicalProcedureService : IMedicalProcedureService
{
    private readonly ILogger<MedicalProcedureService> _logger;
    private readonly ISuperkattenRepository _superkattenRepository;
    private readonly MedicalProceduresRepository _medicalProcedureRepository;

    public MedicalProcedureService(
        ILogger<MedicalProcedureService> logger,
        ISuperkattenRepository superkattenRepository,
        MedicalProceduresRepository medicalProcedureRepository
    )
    {
        _logger = logger;
        _superkattenRepository = superkattenRepository;
        _medicalProcedureRepository = medicalProcedureRepository;
    }

    public Task AddMedicalProcedureAsyn(AddMedicalProcedureParameters parameters)
    {
        var superkat = _superkattenRepository.GetSuperkatAsync(parameters.SuperkatId);
        if (superkat is null)
        {
            throw new ServiceException($"Superkat with id '{parameters.SuperkatId}' does not exsist");
        }

        return _medicalProcedureRepository.AddMedicalProcedureAsync(parameters);
    }
}
