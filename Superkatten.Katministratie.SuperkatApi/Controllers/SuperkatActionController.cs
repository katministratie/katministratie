using Microsoft.AspNetCore.Mvc;
using Superkatten.Katministratie.Application.Interfaces;

namespace Superkatten.Katministratie.SuperkatApi.Controllers
{
    [Route("api/[Controller]")]
    [Controller]
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
        [Route("Reserve")]
        public async Task Reserve(int superkatNumber, [FromBody] bool reserve)
        {
            await _service.ReserveAsync(superkatNumber, reserve);
        }

        [HttpPut]
        [Route("Retour")]
        public async Task GoRetour(int superkatNumber, [FromBody] bool retour)
        {
            await _service.GoingRetourAsync(superkatNumber, retour);
        }
    }
}
