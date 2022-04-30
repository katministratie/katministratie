using Microsoft.AspNetCore.Mvc;
using Superkatten.Katministratie.Host.Api;
using Superkatten.Katministratie.Host.Entities;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Superkatten.Katministratie.Host.Services;

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
        /*var myContent = JsonSerializer.Serialize(newSuperkat);
        var buffer = Encoding.UTF8.GetBytes(myContent);
        var byteContent = new ByteArrayContent(buffer);
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        var Responce = await _client.PutAsync(uri, byteContent);*/
        var Responce = await _client.PutAsJsonAsync(uri, newSuperkat);

        var stream = await Responce.Content.ReadAsStreamAsync();

        return stream is null
            ? null
            : await JsonSerializer.DeserializeAsync<Superkat>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
    }

    public async Task UpdateSuperkatAsync(Guid id, [FromBody] UpdateSuperkatParameters updateSuperkat)
    {
       /* var uri = $"api/Superkatten?Id={superkatNumber}";
        var myContent = JsonSerializer.Serialize(updateSuperkat); 
        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
        var byteContent = new ByteArrayContent(buffer);
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        _ = await _client.PostAsync(uri, byteContent);*/
    }

    public async Task DeleteSuperkatAsync(Guid id)
    {
        var uri = $"api/Superkatten?Id={id}";
        _ = await _client.DeleteAsync(uri);
    }

    public async Task<Superkat> GetSuperkatAsync(Guid id)
    {
        var superkatten = await GetAllSuperkattenAsync();
        return superkatten
            .Where(s => s.Id == id)
            .First();
    }

    public async Task<List<Superkat>> GetAllSuperkattenAsync()
    {
        var stream = await _client.GetStreamAsync($"api/superkatten");

        var mylist = await JsonSerializer.DeserializeAsync<List<Superkat>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        return mylist is null 
            ? new() 
            : mylist;
    }
}
