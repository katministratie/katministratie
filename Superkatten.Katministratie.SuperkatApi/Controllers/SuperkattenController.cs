using Microsoft.AspNetCore.Mvc;
using Superkatten.Katministratie.Application.Interfaces;
using Superkatten.Katministratie.Application.Mappers;
using Superkatten.Katministratie.Contract;
using ContractEntities = Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.SuperkatApi.Controllers
{
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

        /*[HttpGet]
        public async Task<IReadOnlyCollection<ContractEntities.Superkat>> GetAllSuperkatten()
        {
            var superkatten = await _service.ReadAvailableSuperkattenAsync();
            return superkatten
                .Select(_mapper.MapDomainToContract)
                .ToList();
        }*/

        [HttpPut]
        public async Task<ContractEntities.Superkat> PutSuperkat([FromBody] CreateSuperkatParameters newSuperkatParameters)
        {
            var superkat = await _service.CreateSuperkatAsync(newSuperkatParameters);
            return _mapper.MapDomainToContract(superkat);
        }

        [HttpPost]
        public async Task<ContractEntities.Superkat> PostSuperkat([FromBody] UpdateSuperkatParameters updateSuperkatParameters)
        {
            var superkat = await _service.UpdateSuperkatAsync(updateSuperkatParameters);
            return _mapper.MapDomainToContract(superkat);
        }

        [HttpDelete]
        public async Task DeleteSuperkat(Guid id)
        {
            await _service.DeleteSuperkatAsync(id);
        }
    }
}
