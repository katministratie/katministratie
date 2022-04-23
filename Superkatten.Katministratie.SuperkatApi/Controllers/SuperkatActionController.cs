using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Superkatten.Katministratie.Application.Interfaces;

namespace Superkatten.Katministratie.SuperkatApi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
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
        public async Task ToggleReserve([FromBody] Guid id)
        {
            await _service.ToggleReserveAsync(id);
        }

        [HttpPut]
        [Route("ToggleRetour")]
        public async Task ToggleRetour([FromBody] Guid id)
        {
            await _service.ToggleRetourAsync(id);
        }

        [HttpPut]
        [Route("CreateSuperkatCard")]
        public async Task CreateSuperkatCard(Guid id)
        {
            await _service.CreateSuperkatCard(id);
        }
    }
}
