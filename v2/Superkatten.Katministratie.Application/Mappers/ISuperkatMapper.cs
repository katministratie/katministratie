using Superkatten.Katministratie.Application.Contracts.Entities;
using Superkatten.Katministratie.Domain;

namespace Superkatten.Katministratie.Application.Mappers;

public interface ISuperkatMapper
{
    SuperkatDto MapFromDomain(Superkat superkat);
}