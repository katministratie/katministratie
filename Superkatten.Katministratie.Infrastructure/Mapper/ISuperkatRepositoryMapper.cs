using Superkatten.Katministratie.Domain.Entities;
using Superkatten.Katministratie.Infrastructure.Entities;

namespace Superkatten.Katministratie.Infrastructure.Mapper;

public interface ISuperkatRepositoryMapper
{
    public SuperkatDto MapDomainToRepository(Superkat superkat);
    public Superkat MapRepositoryToDomain(SuperkatDto superkatDto);
}
