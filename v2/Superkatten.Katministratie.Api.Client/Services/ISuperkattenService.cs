using Superkatten.Katministratie.Api.Client.Entities;

namespace Superkatten.Katministratie.Api.Client.Services;

public interface ISuperkattenService
{
    Task<IReadOnlyList<SuperkatView>> GetSuperkattenAsync();
}