using Superkatten.Katministratie.Host.Entities;

namespace Superkatten.Katministratie.Host.Services;

public interface IPrinterService
{
    List<Printer> GetPrinters();
    event EventHandler<Guid> OnPrintSuperkatCageCard;
    void PrintCageCard(Guid superkatId);
}
