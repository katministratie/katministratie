﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Superkatten.Katministratie.Application.Interfaces;
using Superkatten.Katministratie.Contract;

namespace Superkatten.Katministratie.SuperkatApi.Controllers
{
    [Authorize]
    [Route("api/[Controller]")]
    [ApiController]
    public class SuperkatActionController
    {
        private readonly ISuperkatAction _actionService;

        public SuperkatActionController(ISuperkatAction service)
        {
            _actionService = service;
        }

        [HttpPut]
        [Route("ToggleReserve")]
        public async Task ToggleReserve([FromBody] Guid id)
        {
            await _actionService.ToggleReserveAsync(id);
        }

        [HttpPut]
        [Route("ToggleRetour")]
        public async Task ToggleRetour([FromBody] Guid id)
        {
            await _actionService.ToggleRetourAsync(id);
        }

        [HttpPut]
        [Route("CreateSuperkatCageCard")]
        public async Task CreateSuperkatCageCard(SuperkatCageCardPrintParameters parameters)
        {
            await _actionService.CreateSuperkatCageCardAsync(parameters);
        }
    }
}
