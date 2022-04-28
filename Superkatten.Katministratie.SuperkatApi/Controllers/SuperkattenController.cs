using Microsoft.AspNetCore.Mvc;
using Superkatten.Katministratie.Application.Entities;
using Superkatten.Katministratie.Application.Interfaces;
using Superkatten.Katministratie.Domain.Entities;

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
            return await _service.ReadAvailableSuperkattenAsync();
        }

        [HttpPut]
        public async Task<Superkat> PutSuperkat([FromBody] CreateSuperkatParameters newSuperkatParameters)
        {
            return await _service.CreateSuperkatAsync(newSuperkatParameters);
        }

        [HttpPost]
        public async Task PostSuperkat([FromBody] UpdateSuperkatParameters updateSuperkatParameters)
        {
            await _service.UpdateSuperkatAsync(updateSuperkatParameters);
        }

        [HttpDelete]
        public async Task DeleteSuperkat(Guid id)
        {
            await _service.DeleteSuperkatAsync(id);
        }
    }
}
