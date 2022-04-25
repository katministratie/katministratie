using Superkatten.Katministratie.Infrastructure.Entities;
using System.Collections.Generic;

namespace Superkatten.Katministratie.Infrastructure.Printing
{
    public interface IPrinterRepository
    {
        List<Printer> GetPrinterList();
    }
}
