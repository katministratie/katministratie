using Microsoft.AspNetCore.Mvc;
using Superkatten.Katministratie.Application.Authorisation;
using Superkatten.Katministratie.Application.Contracts.Interfaces;
using Superkatten.Katministratie.Application.Contracts.Parameters;
using Superkatten.Katministratie.Domain.Shared;

namespace Superkatten.Katministratie.HttpApi.Controllers
{
    // TODO: Tijdelijk uitgezet
    //[AuthorizeRoles(UserPermissions.Administrator)]
    //[ApiController]
    [Route("[controller]")]
    public class SuperkattenController : ControllerBase
    {
        private readonly ILogger<SuperkattenController> _logger;
        private readonly ISuperkatService _superkatService;

        public SuperkattenController(
            ILogger<SuperkattenController> logger,
            ISuperkatService superkatService
        )
        {
            _logger = logger;
            _superkatService = superkatService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var superkatten = await _superkatService.GetSuperkattenAsync();
            return Ok(superkatten);
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewSuperkatParameters parameters)
        {
            var superkat = await _superkatService.CreateSuperkatAsync(parameters);
            
            if(superkat is null)
            {
                return BadRequest("Superkat cannot be created");
            }

            return Ok(superkat);
        }
    }
}