using Superkatten.Katministratie.Application.Contracts.Entities;
using Superkatten.Katministratie.Application.Contracts.Parameters;

namespace Superkatten.Katministratie.Application.Contracts.Interfaces;

public interface ISuperkatService
{
    Task<List<SuperkatDto>> GetSuperkattenAsync();

    Task<SuperkatDto> CreateSuperkatAsync(NewSuperkatParameters newSuperkatParameters);
}
