using System.Net.Http.Headers;
using System.Text.Json;


namespace Superkatten.Katministratie.Web.Services
{
    public class SuperkatActionService : ISuperkatActionService
    {
        private readonly HttpClient _client;
        private readonly string _controllerName;

        public SuperkatActionService(HttpClient client)
        {
            _client = client;
        }

        public async Task ReserveSuperkatAsync(int superkatNumber)
        {
            var uri = $"api/Superkat/{_controllerName}/ reserve";
            var myContent = JsonSerializer.Serialize(superkatNumber);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            await _client.PutAsync(uri, byteContent);
        }

        public async Task RetourSuperkatAsync(int superkatNumber)
        {
            var uri = $"api/Superkat/{_controllerName}/retour";
            var myContent = JsonSerializer.Serialize(superkatNumber);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            await _client.PutAsync(uri, byteContent);
        }
    }
}
