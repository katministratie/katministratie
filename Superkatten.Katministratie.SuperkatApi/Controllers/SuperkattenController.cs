using Microsoft.AspNetCore.Mvc;
using Superkatten.Katministratie.Application.Entities;
using Superkatten.Katministratie.Application.Interfaces;

namespace Superkatten.Katministratie.SuperkatApi.Controllers
{
    [Route("api/[controller]")]
    [Controller]
    public class SuperkattenController
    {
        private readonly ISuperkattenService _service;
        public SuperkattenController(ISuperkattenService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IReadOnlyCollection<Superkat>> GetAllSuperkatten()
        {
            return await _service.ReadAvailableSUperkattenAsync();
        }
    }
}
