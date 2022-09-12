using Microsoft.AspNetCore.Mvc;
using Superkatten.Katministratie.Application.Authorization;
using Superkatten.Katministratie.Application.Interfaces;
using Superkatten.Katministratie.Application.Mappers;
using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Domain.Entities;

namespace Superkatten.Katministratie.SuperkatApi.Controllers
{
    [AuthorizeRoles(PermissionEnum.Administrator, PermissionEnum.Coordinator)]
    [Route("api/[controller]")]
    [ApiController]
    public class SuperkattenController : ControllerBase
    {
        private readonly ISuperkattenService _service;
        private readonly ISuperkatMapper _mapper;

        public SuperkattenController(ISuperkattenService service, ISuperkatMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSuperkatten()
        {
            var superkatten = await _service.ReadAllSuperkattenAsync();
            return Ok(superkatten
                .Select(_mapper.MapDomainToContract)
                .ToList());
        }

        [HttpGet]
        [Route("NotAssigned")]
        public async Task<IActionResult> GetAllNotAssignedSuperkatten()
        {
            var superkatten = await _service.ReadAvailableSuperkattenAsync();

            return Ok(superkatten
                .Select(_mapper.MapDomainToContract)
                .ToList());
        }

        [HttpPut]
        public async Task<IActionResult> PutSuperkat([FromBody] CreateSuperkatParameters newSuperkatParameters)
        {
            var superkat = await _service.CreateSuperkatAsync(newSuperkatParameters);

            return Ok(_mapper.MapDomainToContract(superkat));
        }

        [HttpPost]
        [Route("Reallocate")]
        public async Task<IActionResult> PostSuperkat(Guid id, [FromBody] ReallocateSuperkatParameters reallocateSuperkatParameters)
        {
            var superkat = await _service.UpdateSuperkatAsync(id, reallocateSuperkatParameters);

            return Ok(_mapper.MapDomainToContract(superkat));
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> PostSuperkat(Guid id, [FromBody] UpdateSuperkatParameters updateSuperkatParameters)
        {
            var superkat = await _service.UpdateSuperkatAsync(id, updateSuperkatParameters);

            return Ok(_mapper.MapDomainToContract(superkat));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSuperkat(Guid id)
        {
            await _service.DeleteSuperkatAsync(id);
            return Ok();
        }

        [HttpPost]
        [Route("Photo")]
        public async Task<IActionResult> PostSuperkat(Guid id, [FromBody] UpdateSuperkatPhotoParameters updateSuperkatPhotoParameters)
        {
            var superkat = await _service.UpdateSuperkatAsync(id, updateSuperkatPhotoParameters);

            return Ok(_mapper.MapDomainToContract(superkat));
        }

        [HttpGet]
        [Route("NotNeutralized")]
        public async Task<IActionResult> GetNotNeutralizedSuperkatten()
        {
            try
            {
                var superkatten = await _service.ReadNotNeutralizedSuperkattenAsync();

                return Ok(superkatten
                    .Select(_mapper.MapDomainToContract)
                    .ToList());
            }
            catch(Exception ex)
            {
                return Problem(ex.Message, null, null, "Error bij ophalen data");
            }
        }
    }
}
