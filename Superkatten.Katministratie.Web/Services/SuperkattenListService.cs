using Superkatten.Katministratie.Application.Contracts;
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

        public async Task CreateSuperkat(CreateSuperkatParameters newSuperkat)
        {
            var uri = $"api/Superkatten?Name={newSuperkat.Name}";
            var request = new HttpRequestMessage(HttpMethod.Put, $"api/Superkatten?Name=" + newSuperkat.Name);
            await _client.SendAsync(request);
        }
        public async Task UpdateSuperkat(int superkatNumber, UpdateSuperkatParameters updateSuperkat)
        {
            var uri = $"api/Superkatten?Number={superkatNumber}&Name={updateSuperkat.Name}";
            var request = new HttpRequestMessage(HttpMethod.Put, uri);
            await _client.SendAsync(request);
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
