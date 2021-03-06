using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Contract.Entities;
using Superkatten.Katministratie.Host.Services.Authentication;

using ContractEntities = Superkatten.Katministratie.Contract.Entities;

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
    
    public async Task<Superkat?> CreateSuperkatAsync(CreateSuperkatParameters newSuperkat)
    {
        var uri = $"api/Superkatten";
        var superkat = await _httpService.Put<Superkat>(uri, newSuperkat);
        return superkat;
    }

    public async Task UpdateSuperkatAsync(Guid id, UpdateSuperkatParameters updateSuperkat)
    {
        var uri = $"api/Superkatten?Id={id}";
        await _httpService.Post<Superkat?>(uri, updateSuperkat);
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
            ? new List<Superkat>() 
            : superkatten;
    }

    public async Task<List<Superkat>> GetAllNotAssignedSuperkattenAsync()
    {
        var uri = "api/Superkatten/NotAssigned";
        var superkatten = await _httpService.Get<List<Superkat>>(uri);

        return superkatten is null
            ? new()
            : superkatten;
    }
}
