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
    public class SuperkattenController
    {
        private readonly ISuperkattenService _service;
        private readonly ISuperkatMapper _mapper;

        public SuperkattenController(ISuperkattenService service, ISuperkatMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IReadOnlyCollection<ContractEntities.Superkat>> GetAllSuperkatten()
        {
            var superkatten = await _service.ReadAllSuperkattenAsync();
            return superkatten
                .Select(_mapper.MapDomainToContract)
                .ToList();
        }

        [HttpGet]
        [Route("NotAssigned")]
        public async Task<IReadOnlyCollection<ContractEntities.Superkat>> GetAllNotAssignedSuperkatten()
        {
            var superkatten = await _service.ReadAvailableSuperkattenAsync();
            return superkatten
                .Select(_mapper.MapDomainToContract)
                .ToList();
        }

        [HttpPut]
        public async Task<ContractEntities.Superkat> PutSuperkat([FromBody] CreateSuperkatParameters newSuperkatParameters)
        {
            var superkat = await _service.CreateSuperkatAsync(newSuperkatParameters);
            return _mapper.MapDomainToContract(superkat);
        }

        [HttpPost]
        public async Task<ContractEntities.Superkat> PostSuperkat(Guid id, [FromBody] UpdateSuperkatParameters updateSuperkatParameters)
        {
            var superkat = await _service.UpdateSuperkatAsync(id, updateSuperkatParameters);
            return _mapper.MapDomainToContract(superkat);
        }

        [HttpDelete]
        public async Task DeleteSuperkat(Guid id)
        {
            await _service.DeleteSuperkatAsync(id);
        }
    }
}
