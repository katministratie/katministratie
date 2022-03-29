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
            try
            {
                return await _service.ReadAvailableGastgezinAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error reading available gastgezinnen; Message: ({ex.Message})");
                return default;
            }
        }

        [HttpPut]
        public async Task PutGastgezin(string name, [FromBody] CreateUpdateGastgezinParameters createGastgezinParameters)
        {
            try
            {
                await _service.CreateGastgezinAsync(name, createGastgezinParameters);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating gastgezin; Message: ({ex.Message})");
            }
        }

        [HttpPost]
        public async Task PostGastgezin(string name, [FromBody] CreateUpdateGastgezinParameters updateGastgezinParameters)
        {
            try
            {
                await _service.UpdateGastgezinAsync(name, updateGastgezinParameters);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating gastgezin {name}; Message: {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task DeleteGastgezin(string name)
        {
            try
            {
                await _service.DeleteGastgezinAsync(name);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error deleting gastgezin {name}; Message: {ex.Message}");
            }
        }

    }
}
