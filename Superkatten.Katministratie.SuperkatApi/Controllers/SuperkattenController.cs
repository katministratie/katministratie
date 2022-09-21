using Microsoft.AspNetCore.Mvc;
using Superkatten.Katministratie.Application.Authorization;
using Superkatten.Katministratie.Application.Interfaces;
using Superkatten.Katministratie.Application.Mappers;
using Superkatten.Katministratie.Application.Services;
using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Domain.Entities;
using System.Linq;

namespace Superkatten.Katministratie.SuperkatApi.Controllers
{
    [AuthorizeRoles(PermissionEnum.Administrator, PermissionEnum.Coordinator)]
    [Route("api/[controller]")]
    [ApiController]
    public class SuperkattenController : ControllerBase
    {
        private readonly ISuperkattenService _superkattenService;
        private readonly IAdoptionService _adoptionService;
        private readonly ISuperkatMapper _superkatMapper;

        public SuperkattenController(
            ISuperkattenService superkattenService,
            IAdoptionService adoptionService,
            ISuperkatMapper mapper
        )
        {
            _superkattenService = superkattenService;
            _adoptionService = adoptionService;
            _superkatMapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSuperkatten()
        {
            var superkatten = await _superkattenService.ReadAllSuperkattenAsync();
            return Ok(superkatten
                .Select(_superkatMapper.MapDomainToContract)
                .ToList());
        }

        [HttpGet]
        [Route("NotAssigned")]
        public async Task<IActionResult> GetAllNotAssignedSuperkatten()
        {
            var superkatten = await _superkattenService.ReadAvailableSuperkattenAsync();

            return Ok(superkatten
                .Select(_superkatMapper.MapDomainToContract)
                .ToList());
        }

        [HttpPut]
        public async Task<IActionResult> PutSuperkat([FromBody] CreateSuperkatParameters newSuperkatParameters)
        {
            var superkat = await _superkattenService.CreateSuperkatAsync(newSuperkatParameters);

            return Ok(_superkatMapper.MapDomainToContract(superkat));
        }

        [HttpPut]
        [Route("Adopting")]
        public async Task<IActionResult> PutSuperkatten(StartAdoptionSuperkattenParameters reserveSuperkattenParameters)
        {
            await _adoptionService.StartSuperkattenAdoptionAsync(reserveSuperkattenParameters);

            return Ok();
        }

        [HttpPost]
        [Route("Reallocate")]
        public async Task<IActionResult> PostSuperkat(Guid id, [FromBody] ReallocateSuperkatParameters reallocateSuperkatParameters)
        {
            var superkat = await _superkattenService.UpdateSuperkatAsync(id, reallocateSuperkatParameters);

            return Ok(_superkatMapper.MapDomainToContract(superkat));
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> PostSuperkat(Guid id, [FromBody] UpdateSuperkatParameters updateSuperkatParameters)
        {
            var superkat = await _superkattenService.UpdateSuperkatAsync(id, updateSuperkatParameters);

            return Ok(_superkatMapper.MapDomainToContract(superkat));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSuperkat(Guid id)
        {
            await _superkattenService.DeleteSuperkatAsync(id);
            return Ok();
        }

        [HttpPost]
        [Route("Photo")]
        public async Task<IActionResult> PostSuperkat(Guid id, [FromBody] UpdateSuperkatPhotoParameters updateSuperkatPhotoParameters)
        {
            var superkat = await _superkattenService.UpdateSuperkatAsync(id, updateSuperkatPhotoParameters);

            return Ok(_superkatMapper.MapDomainToContract(superkat));
        }

        [HttpGet]
        [Route("NotNeutralized")]
        public async Task<IActionResult> GetNotNeutralizedSuperkatten()
        {
            try
            {
                var superkatten = await _superkattenService.ReadNotNeutralizedSuperkattenAsync();

                return Ok(superkatten
                    .Select(_superkatMapper.MapDomainToContract)
                    .ToList());
            }
            catch(Exception ex)
            {
                return Problem(ex.Message, null, null, "Error bij ophalen data");
            }
        }
    }
}