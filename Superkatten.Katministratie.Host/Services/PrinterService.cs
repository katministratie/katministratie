using Superkatten.Katministratie.Host.Entities;
using System.Text.Json;

namespace Superkatten.Katministratie.Host.Services
{
    public class PrinterService : IPrinterService
    {
        private readonly HttpClient _client;

        public PrinterService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<Printer>> GetPrintersAsync()
        {
            var stream = await _client.GetStreamAsync($"api/Printers");

            var printers = await JsonSerializer.DeserializeAsync<List<Printer>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return printers is null
                ? new()
                : printers;
        }
    }
}
