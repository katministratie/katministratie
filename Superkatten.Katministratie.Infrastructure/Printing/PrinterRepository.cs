using Superkatten.Katministratie.Infrastructure.Entities;
using System.Collections.Generic;
using System.Drawing.Printing;

namespace Superkatten.Katministratie.Infrastructure.Printing;

public class PrinterRepository : IPrinterRepository
{
    public List<Printer> Printers { get; } = new();

    public List<Printer> GetPrinterList()
    {
#pragma warning disable CA1416 // Validate platform compatibility
        for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
        {
            string printerName = PrinterSettings.InstalledPrinters[i];
            Printers.Add(new Printer(printerName));
        }
#pragma warning restore CA1416 // Validate platform compatibility

        return Printers;
    }
}
