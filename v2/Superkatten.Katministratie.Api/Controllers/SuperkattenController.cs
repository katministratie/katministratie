using Microsoft.AspNetCore.Mvc;
using Superkatten.Katministratie.Application.Contracts.Interfaces;
using Superkatten.Katministratie.Application.Contracts.Parameters;
using Superkatten.Katministratie.HttpApi.ExceptionHandling;
using System.Data;

namespace Superkatten.Katministratie.HttpApi.Controllers
{
    [Route("[controller]")]
    [TypeFilter(typeof(ExceptionHandlingFilter))]
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
        [HandleException(typeof(Exception))]
        public async Task<IActionResult> GetAll()
        {
            var superkatten = await _superkatService.GetSuperkattenAsync();
            return Ok(superkatten);
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewSuperkatParameters parameters)
        {
            var superkat = await _superkatService.CreateSuperkatAsync(parameters);
            return Ok(superkat);
        }
    }
}