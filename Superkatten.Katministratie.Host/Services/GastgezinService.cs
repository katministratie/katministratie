using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Host.Entities;
using Superkatten.Katministratie.Host.Mappers;
using Superkatten.Katministratie.Host.Services.Authentication;
using System.Net.Http.Json;
using System.Text.Json;
using ContractEntities = Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.Host.Services;

public class GastgezinService : IGastgezinService
{
    private readonly HttpClient _client;
    private readonly IGastgezinMapper _mapper;
    private readonly IHttpService _httpService;

    public GastgezinService(
        HttpClient client,
        IHttpService httpService,
        IGastgezinMapper mapper
    )
    {
        _client = client;
        _mapper = mapper;
        _httpService = httpService;
    }


    public async Task<Gastgezin?> CreateGastgezinAsync(CreateOrUpdateNawGastgezinParameters newGastgezinParameters)
    {
        var uri = "api/Gastgezinnen";
        var response = await _httpService.Put<Gastgezin>(uri, newGastgezinParameters);
        return response;
    }

    public async Task<Gastgezin?> CreateGastgezinAsync(CreateOrUpdateGastgezinParameters newGastgezinParameters)
    {
        var uri = "api/Gastgezinnen";
        var response = await _httpService.Put<Gastgezin>(uri, newGastgezinParameters);
        return response;
    }

    public async Task<Gastgezin?> UpdateGastgezinAsync(Guid id, CreateOrUpdateNawGastgezinParameters updateNawGastgezinParameters)
    {
        var uri = $"api/Gastgezinnen?Id={id}";
        var response = await _httpService.Post<Gastgezin>(uri, updateNawGastgezinParameters);
        return response;
    }

    public async Task<Gastgezin?> UpdateGastgezinAsync(Guid id, CreateOrUpdateGastgezinParameters updateGastgezinParameters)
    {
        var uri = $"api/Gastgezinnen/AssignSuperkatten?Id={id}";
        var response = await _httpService.Post<Gastgezin>(uri, updateGastgezinParameters); // TODO: change func to Async
        return response;
    }

    public async Task DeleteGastgezinAsync(Guid id)
    {
        var uri = $"api/Gastgezinnen?Id={id}";
        await _httpService.Delete(uri);
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
        var gastgezinnen = await _httpService.Get<List<Gastgezin>>(uri);
        return gastgezinnen is null
            ? new()
            : gastgezinnen;
    }
}
