using Superkatten.Katministratie.Host.Entities;
using System.Drawing.Printing;

namespace Superkatten.Katministratie.Host.Services
{
    public class PrinterService : IPrinterService
    {
        private readonly HttpClient _client;

        public event EventHandler<Guid>? OnPrintSuperkatCageCard;

        public PrinterService(HttpClient client)
        {
            _client = client;
        }

        public List<Printer> Printers { get; set; } = new List<Printer>();

        public List<Printer> GetPrinters()
        {
            Printers.Clear();

#pragma warning disable CA1416 // Validate platform compatibility
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                string printerName = PrinterSettings.InstalledPrinters[i];
                Printers.Add(new Printer(printerName));
            }
#pragma warning restore CA1416 // Validate platform compatibility

            return Printers;
        }

        public void PrintCageCard(Guid superkatId)
        {
            OnPrintSuperkatCageCard?.Invoke(null, superkatId);
        }
    }
}
