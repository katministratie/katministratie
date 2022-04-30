
using Microsoft.AspNetCore.Mvc;
using Superkatten.Katministratie.Application.Entities;
using Superkatten.Katministratie.Application.Interfaces;
using Superkatten.Katministratie.Domain.Entities;

namespace Superkatten.Katministratie.SuperkatApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<Gastgezin> PutGastgezin([FromBody] CreateOrUpdateGastgezinParameters createGastgezinParameters)
        {
            return await _service.CreateGastgezinAsync(createGastgezinParameters);
        }

        [HttpPost]
        public async Task PostGastgezin(Guid id, [FromBody] CreateOrUpdateGastgezinParameters updateGastgezinParameters)
        {
            await _service.UpdateGastgezinAsync(id, updateGastgezinParameters);
        }

        [HttpDelete]
        public async Task DeleteGastgezin(Guid id)
        {
            await _service.DeleteGastgezinAsync(id);
        }
    }
}
