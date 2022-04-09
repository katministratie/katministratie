using Microsoft.AspNetCore.Mvc;
using Superkatten.Katministratie.Application.Interfaces;

namespace Superkatten.Katministratie.SuperkatApi.Controllers
{
    [Route("api/[Controller]")]
    [Controller]
    public class SuperkatActionControler
    {
        private readonly ISuperkatAction _service;
        private readonly ILogger<SuperkattenController> _logger;

        public SuperkatActionControler(ISuperkatAction service, ILogger<SuperkattenController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPut]
        [Route("reserve")]
        public async Task Reserve([FromBody] int superkatNumber)
        {
            try
            {
                await _service.ReserveAsync(superkatNumber);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error setting reserved; Message: ({ex.Message})");
            }
        }

        [HttpPut]
        [Route("retour")]
        public async Task GoRetour([FromBody] int superkatNumber)
        {
            try
            {
                await _service.GoingRetourAsync(superkatNumber);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error setting retour; Message: ({ex.Message})");
            }
        }
    }
}
