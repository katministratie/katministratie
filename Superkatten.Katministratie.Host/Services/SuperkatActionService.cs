using Superkatten.Katministratie.Host.Entities;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;


namespace Superkatten.Katministratie.Host.Services
{
    public class SuperkatActionService : ISuperkatActionService
    {
        private readonly HttpClient _client;

        public SuperkatActionService(HttpClient client)
        {
            _client = client;
        }

        public async Task ToggleReserveSuperkatAsync(int superkatNumber)
        {
            var uri = $"api/SuperkatAction/ToggleReserve";
            //var myContent = JsonSerializer.Serialize(superkatNumber);
            //var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            //var byteContent = new ByteArrayContent(buffer);
            //byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            _ = await _client.PutAsJsonAsync(uri, superkatNumber);
        }

        public async Task ToggleRetourSuperkatAsync(int superkatNumber)
        {
            var uri = $"api/SuperkatAction/ToggleRetour";
            //var myContent = JsonSerializer.Serialize(superkatNumber);
            //var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            //var byteContent = new ByteArrayContent(buffer);
            //byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            _ = await _client.PutAsJsonAsync(uri, superkatNumber);
        }

        public async Task PrintSuperkatCageCardAsync(SuperkatCageCardPrintParameters parameters)
        {
            await _client.PutAsJsonAsync($"api/SuperkatAction/PrintSuperkatCageCard", parameters);
        }
    }
}
