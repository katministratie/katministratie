using Microsoft.AspNetCore.Mvc;
using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Host.Entities;
using Superkatten.Katministratie.Host.Mappers;
using Superkatten.Katministratie.Host.Services.Authentication;

using ContractEntities = Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.Host.Services;

public class SuperkattenListService : ISuperkattenListService
{
    private readonly HttpClient _client;
    private readonly IHttpService _httpService;
    private readonly ISuperkatMapper _mapper;

    public SuperkattenListService(
        HttpClient client,
        IHttpService httpService,
        ISuperkatMapper mapper
    )
    {
        _client = client;
        _httpService = httpService;
        _mapper = mapper;
    }
    
    public async Task<Superkat?> CreateSuperkatAsync([FromBody] CreateSuperkatParameters newSuperkat)
    {
        var uri = $"api/Superkatten";
        var superkat = await _httpService.Put<Superkat>(uri, newSuperkat);return superkat;
    }

    public async Task<Superkat?> UpdateSuperkatAsync(Guid id, [FromBody] UpdateSuperkatParameters updateSuperkat)
    {
        var uri = $"api/Superkatten?Id={id}";
        var superkat = await _httpService.Post<Superkat?>(uri);
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

        Console.WriteLine("Get the contract entities from the API");
        var superkatten = await _httpService.Get<List<ContractEntities.Superkat>>(uri);
        
        return superkatten is null 
            ? new List<Superkat>() 
            : superkatten.Select(_mapper.MapContractToHost).ToList();
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
