using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Superkatten.Katministratie.Application.Interfaces;

namespace Superkatten.Katministratie.SuperkatApi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class SuperkatActionController
    {
        private readonly ISuperkatAction _service;
        private readonly ILogger<SuperkattenController> _logger;

        public SuperkatActionController(ISuperkatAction service, ILogger<SuperkattenController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPut]
        [Route("ToggleReserve")]
        public async Task ToggleReserve([FromBody] int superkatNumber)
        {
            await _service.ToggleReserveAsync(superkatNumber);
        }

        [HttpPut]
        [Route("ToggleRetour")]
        public async Task ToggleRetour([FromBody] int superkatNumber)
        {
            await _service.ToggleRetourAsync(superkatNumber);
        }
    }
}
