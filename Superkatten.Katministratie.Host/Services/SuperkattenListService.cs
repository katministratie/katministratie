using Microsoft.AspNetCore.Mvc;
using Superkatten.Katministratie.Host.Api;
using Superkatten.Katministratie.Host.Entities;
using System.Net.Http.Json;
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
        var Response = await _client.PutAsJsonAsync(uri, newSuperkat);

        var stream = await Response.Content.ReadAsStreamAsync();

        return stream is null
            ? null
            : await JsonSerializer.DeserializeAsync<Superkat>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
    }

    public async Task<Superkat?> UpdateSuperkatAsync(Guid id, [FromBody] UpdateSuperkatParameters updateSuperkat)
    {
        var uri = $"api/Superkatten?Id={id}";
        var response = await _client.PostAsJsonAsync(uri, updateSuperkat);

        var stream = await response.Content.ReadAsStreamAsync();

        return stream is null
                ? null
                : await JsonSerializer.DeserializeAsync<Superkat>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
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
        var uri = "api/Superkatten";
        var superkatten = await _client.GetFromJsonAsync<List<Superkat>>(uri);

        return superkatten is null 
            ? new() 
            : superkatten;
    }

    public async Task<List<Superkat>> GetAllNotAssignedSuperkattenAsync()
    {
        var uri = "api/Superkatten/NotAssigned";
        var superkatten = await _client.GetFromJsonAsync<List<Superkat>>(uri);

        return superkatten is null
            ? new()
            : superkatten;
    }
}
