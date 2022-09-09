using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Contract.Entities;
using Superkatten.Katministratie.Host.Entities;

namespace Superkatten.Katministratie.Host.Services;

public interface ISuperkatActionService
{
    Task ToggleReserveSuperkatAsync(Guid superkatId);
    Task ToggleRetourSuperkatAsync(Guid superkatId);
    Task CreateSuperkatCageCardAsync(SuperkatCageCardPrintParameters parameters);
    Task ReserveSuperkatten(Guid superkatId, IReadOnlyCollection<Superkat> reservedSuperkattenParameters);
}
