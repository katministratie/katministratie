using Microsoft.AspNetCore.Mvc;
using Superkatten.Katministratie.Application.Interfaces;
using Superkatten.Katministratie.Domain.Contracts;

namespace Superkatten.Katministratie.SuperkatApi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class SuperkatActionController
    {
        private readonly ISuperkatAction _actionService;
        private readonly ILogger<SuperkattenController> _logger;

        public SuperkatActionController(
            ISuperkatAction service, 
            ILogger<SuperkattenController> logger)
        {
            _actionService = service;
            _logger = logger;
        }

        [HttpPut]
        [Route("ToggleReserve")]
        public async Task ToggleReserve([FromBody] Guid id)
        {
            await _actionService.ToggleReserveAsync(id);
        }

        [HttpPut]
        [Route("ToggleRetour")]
        public async Task ToggleRetour([FromBody] Guid id)
        {
            await _actionService.ToggleRetourAsync(id);
        }

        [HttpPut]
        [Route("PrintSuperkatCageCard")]
        public async Task PrintSuperkatCageCard(SuperkatCageCardPrintParameters parameters)
        {
            await _actionService.PrintSuperkatCageCardAsync(parameters);
        }
    }
}
