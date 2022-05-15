using Superkatten.Katministratie.Contract;
using Superkatten.Katministratie.Host.Entities;
using System.Net.Http.Json;
using System.Text.Json;

namespace Superkatten.Katministratie.Host.Services;

public class GastgezinService : IGastgezinService
{
    private readonly HttpClient _client;

    public GastgezinService(HttpClient client)
    {
        _client = client;
    }
    public async Task<Gastgezin?> CreateGastgezinAsync(CreateOrUpdateGastgezinParameters newGastgezinParameters)
    {
        var uri = "api/Gastgezinnen";
        var Responce = await _client.PutAsJsonAsync(uri, newGastgezinParameters);

        var stream = await Responce.Content.ReadAsStreamAsync();

        return stream is null
            ? null
            : await JsonSerializer.DeserializeAsync<Gastgezin>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
    }
    public async Task UpdateGastgezinAsync(Guid id, CreateOrUpdateGastgezinParameters updateGastgezinParameters)
    {
        var uri = $"api/Gastgezinnen?Id={id}";
        _ = await _client.PostAsJsonAsync(uri, updateGastgezinParameters);
    }

    public async Task DeleteGastgezinAsync(Guid id)
    {
        var uri = $"api/Gastgezinnen?Id={id}";
        _ = await _client.DeleteAsync(uri);
    }

    public async Task<Gastgezin?> GetGastgezinAsync(Guid id)
    {
        var gastgezinnen = await GetAllGastgezinAsync();
        return gastgezinnen
            .Where(s => s.Id == id)
            .First();
    }
        
    public async Task<List<Gastgezin>> GetAllGastgezinAsync()
    {
        var uri = "api/Gastgezinnen";
        var gastgezinnen = await _client.GetFromJsonAsync<List<Gastgezin>>(uri);

        return gastgezinnen is null 
            ? new() 
            : gastgezinnen;
    }
}
