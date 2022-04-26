using Superkatten.Katministratie.Infrastructure.Entities;
using System.Collections.Generic;
using System.Management;

namespace Superkatten.Katministratie.Infrastructure.Printing;

public class PrinterRepository : IPrinterRepository
{
    public List<Printer> Printers { get; } = new();

#pragma warning disable CA1416 // Validate platform compatibility
    public List<Printer> GetPrinterList()
    {
        ManagementScope objScope = new(ManagementPath.DefaultPath); //For the local Access
        objScope.Connect();

        SelectQuery selectQuery = new();
        selectQuery.QueryString = "Select * from win32_Printer";
        ManagementObjectSearcher searcher = new(objScope, selectQuery);
        ManagementObjectCollection collection = searcher.Get();
        foreach (ManagementObject managementObject in collection)
        {
            var printerName = managementObject["name"];
            if (managementObject["name"] is null)
            {
                continue;
            }
            if (printerName is not null)
            {
                var printer = new Printer(printerName.ToString());
                Printers.Add(printer);
            }
        }

        return Printers;

    }
#pragma warning restore CA1416 // Validate platform compatibility
}
