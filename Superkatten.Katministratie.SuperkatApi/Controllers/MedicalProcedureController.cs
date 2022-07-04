using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Superkatten.Katministratie.Application.Interfaces;
using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Domain.Entities;

using ContractEntities = Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.SuperkatApi.Controllers;

[Authorize(Roles = nameof(PermissionEnum.Viewer))]
[Route("api/[Controller]")]
[ApiController]
public class MedicalProcedureController
{
    private readonly ISuperkatAction _actionService;

    public MedicalProcedureController(ISuperkatAction service)
    {
        _actionService = service;
    }

    [HttpGet]
    public Task<IReadOnlyCollection<ContractEntities.MedicalProcedure>> GetAll()
    {
        return _actionService.GetAllMedicalProceduresAsync();
    }

    [HttpPut]
    [Route("AddMedicalProcedure")]
    public Task AddMedicalProcedure(AddMedicalProcedureParameters parameters)
    {
        return _actionService.AddMedicalProcedureAsync(parameters);
    }
}
