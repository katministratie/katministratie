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
