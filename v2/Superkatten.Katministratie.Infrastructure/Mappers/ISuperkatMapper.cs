using Superkatten.Katministratie.Domain;
using Superkatten.Katministratie.Infrastructure.Entities;

namespace Superkatten.Katministratie.Infrastructure.Mappers;

public interface ISuperkatMapper
{
    SuperkatDb MapFromDomain(Superkat superkat);
    Superkat MapToDomain(SuperkatDb superkatDb);
}