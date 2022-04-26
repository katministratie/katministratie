using Superkatten.Katministratie.Domain.Contracts;

namespace Superkatten.Katministratie.Host.Services;

public interface ISuperkatActionService
{
    Task ToggleReserveSuperkatAsync(int superkatNumber);
    Task ToggleRetourSuperkatAsync(int superkatNumber);
    Task PrintSuperkatCageCardAsync(SuperkatCageCardPrintParameters parameters);
}
