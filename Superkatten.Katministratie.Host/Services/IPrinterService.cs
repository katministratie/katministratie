using Superkatten.Katministratie.Host.Entities;

namespace Superkatten.Katministratie.Host.Services;

public interface IPrinterService
{
    Task<List<Printer>> GetPrintersAsync();
    event EventHandler<Guid> OnPrintSuperkatCageCard;
    void PrintCageCard(Guid superkatId);
}
