using Microsoft.AspNetCore.Mvc;
using Superkatten.Katministratie.Host.Api;
using Superkatten.Katministratie.Host.Entities;
using Superkatten.Katministratie.Host.Services.Authentication;
using System.Net.Http.Json;
using System.Text.Json;

namespace Superkatten.Katministratie.Host.Services;

public class SuperkattenListService : ISuperkattenListService
{
    private readonly HttpClient _client;
    private readonly IHttpService _httpService;

    public SuperkattenListService(
        HttpClient client,
        IHttpService httpService
    )
    {
        _client = client;
        _httpService = httpService;
    }
    
    public async Task<Superkat?> CreateSuperkatAsync([FromBody] CreateSuperkatParameters newSuperkat)
    {
        var uri = $"api/Superkatten";
        var superkat = await _httpService.Put<Superkat>(uri, newSuperkat);
//        var Response = await _client.PutAsJsonAsync(uri, newSuperkat);

//        var superkat = await response.Content.ReadAsStreamAsync();

//        return stream is null
//            ? null
//            : await JsonSerializer.DeserializeAsync<Superkat>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        return superkat;
    }

    public async Task<Superkat?> UpdateSuperkatAsync(Guid id, [FromBody] UpdateSuperkatParameters updateSuperkat)
    {
        var uri = $"api/Superkatten?Id={id}";
        var superkat = await _httpService.Post<Superkat?>(uri);
        //        var response = await _client.PostAsJsonAsync(uri, updateSuperkat);

        //        var stream = await response.Content.ReadAsStreamAsync();

        //        return stream is null
        //                ? null
        //                : await JsonSerializer.DeserializeAsync<Superkat>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        return superkat;
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

        var superkatten = await _httpService.Get<List<Superkat>>(uri);

        return superkatten is null 
            ? new() 
            : superkatten;
    }

    public async Task<List<Superkat>> GetAllNotAssignedSuperkattenAsync()
    {
        var uri = "api/Superkatten/NotAssigned";
        var superkatten = await _httpService.Get<List<Superkat>>(uri);
        //var superkatten = await _client.GetFromJsonAsync<List<Superkat>>(uri);

        return superkatten is null
            ? new()
            : superkatten;
    }
}
