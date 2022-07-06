using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Superkatten.Katministratie.Application.Authorization;
using Superkatten.Katministratie.Application.Interfaces;
using Superkatten.Katministratie.Application.Mappers;
using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Domain.Entities;

using ContractEntities = Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.SuperkatApi.Controllers;

[AuthorizeRoles(PermissionEnum.Administrator, PermissionEnum.Coordinator, PermissionEnum.Gastgezin)]
[Route("api/[Controller]")]
[ApiController]
public class MedicalProcedureController
{
    private readonly IMedicalProcedureService _medicalProcedureService;

    public MedicalProcedureController(IMedicalProcedureService medicalProcedureService)
    {
        _medicalProcedureService = medicalProcedureService;
    }

    [HttpGet]
    public async Task<IReadOnlyCollection<ContractEntities.MedicalProcedure>> GetAll()
    {
        return new List<ContractEntities.MedicalProcedure>();
    }

    [HttpPut]
    public Task AddMedicalProcedure(AddMedicalProcedureParameters medicalProcedureParameters)
    {
        return _medicalProcedureService.AddMedicalProcedureAsync(medicalProcedureParameters);
    }
}
