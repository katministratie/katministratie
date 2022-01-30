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

        public async Task CreateSuperkat(CreateSuperkatParameters newSuperkat)
        {
            var uri = $"api/Superkatten?Name={newSuperkat.Name}";
            var request = new HttpRequestMessage(HttpMethod.Put, $"api/Superkatten?Name=" + newSuperkat.Name);
            await _client.SendAsync(request);
        }
        public async Task UpdateSuperkat(int superkatNumber, UpdateSuperkatParameters updateSuperkat)
        {
            var request = $"api/Superkatten?Number={superkatNumber}";
            var myContent = JsonSerializer.Serialize<UpdateSuperkatParameters>(updateSuperkat);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            await _client.PostAsync(request, byteContent);
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
            try
            {
                var stream = await _client.GetStreamAsync("/api/superkatten");
                var mylist = await JsonSerializer.DeserializeAsync<List<Superkat>>(
                    stream,
                    new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    });
                return mylist == null ? new List<Superkat>() : mylist;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); 
            }

            return new List<Superkat>();
        }
    }
}
