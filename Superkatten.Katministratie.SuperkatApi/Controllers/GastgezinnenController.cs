using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Superkatten.Katministratie.Application.Authorization;
using Superkatten.Katministratie.Application.Interfaces;
using Superkatten.Katministratie.Application.Mappers;
using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Domain.Entities;
using ContractEntities = Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.SuperkatApi.Controllers
{
    [AuthorizeRoles(PermissionEnum.Administrator, PermissionEnum.Coordinator)]
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

        [HttpPut]
        public async Task<ContractEntities.Gastgezin> PutGastgezin([FromBody] CreateUpdateGastgezinParameters createGastgezinParameters)
        {
            var gastgezin = await _service.CreateGastgezinAsync(createGastgezinParameters);
            return _mapper.MapDomainToContract(gastgezin);
        }

        [HttpPost]
        public async Task<ContractEntities.Gastgezin> PostGastgezin(Guid id, [FromBody] CreateUpdateGastgezinParameters updateGastgezinParameters)
        {
            var gastgezin = await _service.UpdateGastgezinAsync(id, updateGastgezinParameters);
            return _mapper.MapDomainToContract(gastgezin);
        }

        [HttpDelete]
        public async Task DeleteGastgezin(Guid id)
        {
            await _service.DeleteGastgezinAsync(id);
        }
    }
}
