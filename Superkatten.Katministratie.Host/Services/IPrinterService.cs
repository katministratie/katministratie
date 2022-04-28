using Superkatten.Katministratie.Host.Entities;

namespace Superkatten.Katministratie.Host.Services;

public interface IPrinterService
{
    Task<List<Printer>> GetPrintersAsync();
}
