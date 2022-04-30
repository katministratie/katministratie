using Superkatten.Katministratie.Host.Entities;
using System.Net.Http.Json;

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

        public async Task<List<Printer>> GetPrintersAsync()
        {
            var uri = "api/Printers";
            var printers = await _client.GetFromJsonAsync< List<Printer>>(uri);

            return printers is null
                ? new()
                : printers;
        }

        public void PrintCageCard(Guid superkatId)
        {
            OnPrintSuperkatCageCard?.Invoke(null, superkatId);
        }
    }
}
