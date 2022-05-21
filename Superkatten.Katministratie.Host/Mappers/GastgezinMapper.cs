
using Superkatten.Katministratie.Host.Entities;
using ContractEntities = Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.Host.Mappers
{
    public class GastgezinMapper : IGastgezinMapper
    {
        private readonly ISuperkatMapper _superkatMapper;

        public GastgezinMapper(ISuperkatMapper superkatMapper)
        {
            _superkatMapper = superkatMapper;
        }

        public Gastgezin MapContractToHost(ContractEntities.Gastgezin gastgezin)
        {
            return new Gastgezin
            {
                Id = gastgezin.Id,
                Name = gastgezin.Name,
                Address = gastgezin.Address,
                City = gastgezin.City,
                Phone = gastgezin.Phone,
                Superkatten = gastgezin
                    .Superkatten
                    .Select(_superkatMapper.MapContractToHost)
                    .ToList()
            };
        }

        public ContractEntities.Gastgezin MapHostToContract(Gastgezin gastgezin)
        {
            return new ContractEntities.Gastgezin
            {
                Id = gastgezin.Id,
                Name = gastgezin.Name,
                Address = gastgezin.Address,
                City = gastgezin.City,
                Phone = gastgezin.Phone,
                Superkatten = gastgezin
                    .Superkatten
                    .Select(_superkatMapper.MapHostToContract)
                    .ToList()
            };
        }
    }
}
