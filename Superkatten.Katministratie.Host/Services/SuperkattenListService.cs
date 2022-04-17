using Microsoft.AspNetCore.Mvc;
using Superkatten.Katministratie.Application.Contracts;
using System.Net.Http.Headers;
using System.Text;
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

        public async Task<Superkat?> CreateSuperkatAsync([FromBody] CreateSuperkatParameters newSuperkat)
        {
            var uri = $"api/Superkatten";
            var myContent = JsonSerializer.Serialize(newSuperkat);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var Responce = await _client.PutAsync(uri, byteContent);
            var test = await Responce.Content.ReadAsStringAsync();
            var superkat = JsonSerializer.Deserialize<Superkat>(test);
            return superkat;
        }

        public async Task UpdateSuperkatAsync(int superkatNumber, [FromBody] UpdateSuperkatParameters updateSuperkat)
        {
            var uri = $"api/Superkatten?Number={superkatNumber}";
            var myContent = JsonSerializer.Serialize(updateSuperkat); 
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            _ = await _client.PostAsync(uri, byteContent);
        }

        public async Task DeleteSuperkatAsync(int superkatNumber)
        {
            var uri = $"api/Superkatten?Number={superkatNumber}";
            _ = await _client.DeleteAsync(uri);
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

            var mylist = await JsonSerializer.DeserializeAsync<List<Superkat>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return mylist is null ? new() : mylist;
        }
    }
}
