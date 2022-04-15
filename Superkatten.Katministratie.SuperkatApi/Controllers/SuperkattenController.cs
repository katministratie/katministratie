using Microsoft.AspNetCore.Mvc;
using Superkatten.Katministratie.Application.Contracts;
using Superkatten.Katministratie.Application.Interfaces;

namespace Superkatten.Katministratie.SuperkatApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperkattenController
    {
        private readonly ISuperkattenService _service;
        private readonly ILogger<SuperkattenController> _logger;

        public SuperkattenController(ISuperkattenService service, ILogger<SuperkattenController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IReadOnlyCollection<Superkat>> GetAllSuperkatten()
        {
            return await _service.ReadAvailableSUperkattenAsync();
        }

        [HttpPut]
        public async Task PutSuperkat([FromBody] CreateSuperkatParameters newSuperkatParameters)
        {
            await _service.CreateSuperkatAsync(newSuperkatParameters);
        }

        [HttpPost]
        public async Task PostSuperkat(int number, [FromBody] UpdateSuperkatParameters updateSuperkatParameters)
        {
            await _service.UpdateSuperkatAsync(number, updateSuperkatParameters);
        }

        [HttpDelete]
        public async Task DeleteSuperkat(int number)
        {
            await _service.DeleteSuperkatAsync(number);
        }
    }
}
