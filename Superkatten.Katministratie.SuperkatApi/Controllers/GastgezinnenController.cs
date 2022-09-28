﻿using Microsoft.AspNetCore.Mvc;
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
    public class GastgezinnenController : ControllerBase
    {
        private readonly ILocationService _service;
        private readonly ILocationMapper _mapper;

        public GastgezinnenController(ILocationService service, ILocationMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGastgezinnen()
        {
            var gastgezinnen = await _service.GetLocationsAsync();

            return Ok(
                gastgezinnen
                .Select(s => _mapper.MapDomainToContract(s))
                .ToList()
            );
        }

        [HttpPut]
        public async Task<IActionResult> PutGastgezin([FromBody] CreateUpdateLocationNawParameters createGastgezinParameters)
        {
            var gastgezin = await _service.CreateLocationAsync(createGastgezinParameters);
            
            return Ok(
                _mapper.MapDomainToContract(gastgezin)
            );
        }

        [HttpPost]
        public async Task<IActionResult> PostGastgezin(Guid id, [FromBody] CreateUpdateLocationNawParameters updateGastgezinParameters)
        {
            var gastgezin = await _service.UpdateGastgezinAsync(id, updateGastgezinParameters);

            return Ok(
                _mapper.MapDomainToContract(gastgezin)
            );
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteGastgezin(Guid id)
        {
            await _service.DeleteLocationAsync(id);

            return Ok();
        }
    }
}
