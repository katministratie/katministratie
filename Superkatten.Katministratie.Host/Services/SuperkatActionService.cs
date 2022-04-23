using System.Net.Http.Headers;
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
            var myContent = JsonSerializer.Serialize(superkatNumber);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            _ = await _client.PutAsync(uri, byteContent);
        }

        public async Task ToggleRetourSuperkatAsync(int superkatNumber)
        {
            var uri = $"api/SuperkatAction/ToggleRetour";
            var myContent = JsonSerializer.Serialize(superkatNumber);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            _ = await _client.PutAsync(uri, byteContent);
        }

        public async Task CreateSuperkatCard(Guid id)
        {
            var uri = $"api/SuperkatAction/CreateSuperkatCard?id={id}";
            _ = await _client.PutAsync(uri, null);
        }
    }
}
