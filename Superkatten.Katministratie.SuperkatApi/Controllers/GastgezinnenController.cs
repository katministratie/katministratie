using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Superkatten.Katministratie.Application.Interfaces;
using Superkatten.Katministratie.Application.Mappers;
using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Domain.Entities;
using ContractEntities = Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.SuperkatApi.Controllers
{
    [Authorize(Policy = SuperkattenPolicies.POLICY_GASTGEZIN)]
    [Route("api/[controller]")]
    [ApiController]
    public class GastgezinnenController
    {
        private readonly IGastgezinnenService _service;
        private readonly IGastgezinMapper _mapper;

        public GastgezinnenController(IGastgezinnenService service, IGastgezinMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IReadOnlyCollection<ContractEntities.Gastgezin>?> GetAllGastgezinnen()
        {
            var gastgezinnen = await _service.ReadAvailableGastgezinAsync();

            return gastgezinnen
                .Select(s => _mapper.MapDomainToContract(s))
                .ToList();
        }

        [Authorize(Policy = SuperkattenPolicies.POLICY_ADMINISTRATOR)]
        [HttpPut]
        public async Task<ContractEntities.Gastgezin> PutGastgezin([FromBody] CreateOrUpdateNawGastgezinParameters createGastgezinParameters)
        {
            var gastgezin = await _service.CreateGastgezinAsync(createGastgezinParameters);
            return _mapper.MapDomainToContract(gastgezin);
        }

        [Authorize(Policy = SuperkattenPolicies.POLICY_ADMINISTRATOR)]
        [HttpPost]
        [Route("AssignSuperkatten")]
        public async Task<ContractEntities.Gastgezin> PostGastgezin(Guid id, [FromBody] CreateOrUpdateGastgezinParameters updateGastgezinParameters)
        {
            var gastgezin =  await _service.UpdateGastgezinAsync(id, updateGastgezinParameters);
            return _mapper.MapDomainToContract(gastgezin);
        }

        [Authorize(Policy = SuperkattenPolicies.POLICY_ADMINISTRATOR)]
        [HttpPost]
        public async Task<ContractEntities.Gastgezin> PostGastgezin(Guid id, [FromBody] CreateOrUpdateNawGastgezinParameters updateGastgezinParameters)
        {
            var gastgezin = await _service.UpdateGastgezinAsync(id, updateGastgezinParameters);
            return _mapper.MapDomainToContract(gastgezin);
        }

        [Authorize(Policy = SuperkattenPolicies.POLICY_ADMINISTRATOR)]
        [HttpDelete]
        public async Task DeleteGastgezin(Guid id)
        {
            await _service.DeleteGastgezinAsync(id);
        }
    }
}
