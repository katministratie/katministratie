using System;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Application.Printing
{
    public class PrinterService : IPrinterService
    {
        public Task PrintPdfAsync(string filename, string printerName)
        {
            var printTimeout = new TimeSpan(0, 30, 0);
            //var printer = new PDFtoPrinterPrinter();
            //var printOptions = new PrintingOptions(printerName, filename);
            //await printer.Print(printOptions, printTimeout);
            return Task.CompletedTask;
        }
    }
}
