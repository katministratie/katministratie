using Superkatten.Katministratie.Host.Entities;

using contractEntities = Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.Host.Mappers
{
    public interface IGastgezinMapper
    {
        List<contractEntities.Superkat> MapHostToContract(List<Superkat> superkatten);
    }
}
