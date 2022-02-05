using Superkatten.Katministratie.Application.Contracts;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Superkatten.Katministratie.Web.Services
{
    public class SuperkattenListService : ISuperkattenListService
    {
        private readonly HttpClient _client;

        public SuperkattenListService(HttpClient client)
        {
            _client = client;
        }

        public async Task CreateSuperkatAsync(CreateSuperkatParameters newSuperkat)
        {
            var uri = $"api/Superkatten?Name={newSuperkat.Name}";
            var request = new HttpRequestMessage(HttpMethod.Put, uri);
            await _client.SendAsync(request);
        }
        public async Task UpdateSuperkatAsync(int superkatNumber, UpdateSuperkatParameters updateSuperkat)
        {
            var uri = $"api/Superkatten?Number={superkatNumber}";
            //var request = new HttpRequestMessage(HttpMethod.Post, uri);
            var myContent = JsonSerializer.Serialize(updateSuperkat); 
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            _ = await _client.PostAsync(uri, byteContent);
        }

        public async Task<Superkat> GetSuperkatAsync(int superkatNumber)
        {
            var superkatten = await GetAllSuperkattenAsync();
            return superkatten
                .Where(s => s.Number == superkatNumber)
                .First();
        }

        public async Task<List<Superkat>> GetAllSuperkattenAsync()
        {
            var stream = await _client.GetStreamAsync($"api/superkatten");
            var mylist = await JsonSerializer.DeserializeAsync<List<Superkat>>(
                stream,
                new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });

            return mylist == null ? new List<Superkat>() : mylist;
        }
    }
}
