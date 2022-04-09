using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Superkatten.Katministratie.Application.Contracts;
using Superkatten.Katministratie.Application.Interfaces;

namespace Superkatten.Katministratie.SuperkatApi.Controllers
{
    [Route("api/[controller]")]
    [Controller]
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
        public async Task<IReadOnlyCollection<Superkat>?> GetAllSuperkatten()
        {
            try
            {
                return await _service.ReadAvailableSUperkattenAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error reading available superkatten; Message: ({ex.Message})");
                return default;
            }
        }

        [HttpPut]
        public async Task PutSuperkat([FromBody] CreateSuperkatParameters newSuperkatParameters)
        {
            try
            {
                await _service.CreateSuperkatAsync(newSuperkatParameters);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating superkat; Message: ({ex.Message})");
            }
        }

        [HttpPost]
        public async Task PostSuperkat(int number, [FromBody] UpdateSuperkatParameters updateSuperkatParameters)
        {
            try
            {
                await _service.UpdateSuperkatAsync(number, updateSuperkatParameters);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating superkat; Message: {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task DeleteSuperkat(int number)
        {
            try
            {
                await _service.DeleteSuperkatAsync(number);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error deleting superkat number {number}; {ex.Message}");
            }
        }

    }
}
