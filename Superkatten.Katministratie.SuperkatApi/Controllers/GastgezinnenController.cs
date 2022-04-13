using Microsoft.AspNetCore.Mvc;
using Superkatten.Katministratie.Application.Contracts;
using Superkatten.Katministratie.Application.Interfaces;

namespace Superkatten.Katministratie.SuperkatApi.Controllers
{
    [Route("api/[controller]")]
    [Controller]
    public class GastgezinnenController
    {
        private readonly IGastgezinnenService _service;
        private readonly ILogger<GastgezinnenController> _logger;

        public GastgezinnenController(IGastgezinnenService service, ILogger<GastgezinnenController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IReadOnlyCollection<Gastgezin>?> GetAllGastgezinnen()
        {
            return await _service.ReadAvailableGastgezinAsync();
        }

        [HttpPut]
        public async Task PutGastgezin(string name, [FromBody] CreateUpdateGastgezinParameters createGastgezinParameters)
        {
            await _service.CreateGastgezinAsync(name, createGastgezinParameters);
        }

        [HttpPost]
        public async Task PostGastgezin(string name, [FromBody] CreateUpdateGastgezinParameters updateGastgezinParameters)
        {
            await _service.UpdateGastgezinAsync(name, updateGastgezinParameters);
        }

        [HttpDelete]
        public async Task DeleteGastgezin(string name)
        {
            await _service.DeleteGastgezinAsync(name);
        }
    }
}
