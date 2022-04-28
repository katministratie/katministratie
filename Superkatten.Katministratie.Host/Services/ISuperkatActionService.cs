using Superkatten.Katministratie.Host.Entities;

namespace Superkatten.Katministratie.Host.Services;

public interface ISuperkatActionService
{
    Task ToggleReserveSuperkatAsync(int superkatNumber);
    Task ToggleRetourSuperkatAsync(int superkatNumber);
    Task PrintSuperkatCageCardAsync(SuperkatCageCardPrintParameters parameters);
}
