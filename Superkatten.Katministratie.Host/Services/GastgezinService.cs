using Superkatten.Katministratie.Host.Api;
using Superkatten.Katministratie.Host.Entities;
using System.Net.Http.Headers;
using System.Text;
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
        var uri = $"api/Gastgezinnen";
        var myContent = JsonSerializer.Serialize(newGastgezinParameters);
        var buffer = Encoding.UTF8.GetBytes(myContent);
        var byteContent = new ByteArrayContent(buffer);
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var Responce = await _client.PutAsync(uri, byteContent);

        var stream = await Responce.Content.ReadAsStreamAsync();

        return stream is null
            ? null
            : await JsonSerializer.DeserializeAsync<Gastgezin>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
    }
    public Task UpdateGastgezinAsync(Guid id, CreateOrUpdateGastgezinParameters updateGastgezinParameters)
    {
        throw new NotImplementedException();
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
        var stream = await _client.GetStreamAsync($"api/gastgezinnen");

        var mylist = await JsonSerializer.DeserializeAsync<List<Gastgezin>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        return mylist is null 
            ? new() 
            : mylist;
    }
}
