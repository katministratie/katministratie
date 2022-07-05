using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Superkatten.Katministratie.Application.Interfaces;
using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Domain.Entities;

namespace Superkatten.Katministratie.SuperkatApi.Controllers
{
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

        [HttpPut]
        public async Task AddMedicalProcedure(AddMedicalProcedureParameters parameters)
        {
            await _actionService.AddMedicalProcedureAsync(parameters);
        }
    }
}
