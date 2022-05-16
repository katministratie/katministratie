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

        public SuperkatActionController(ISuperkatAction service)
        {
            _actionService = service;
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
