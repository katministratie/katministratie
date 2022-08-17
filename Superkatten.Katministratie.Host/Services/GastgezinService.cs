using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Contract.Entities;
using Superkatten.Katministratie.Host.Services.Http;

namespace Superkatten.Katministratie.Host.Services;

public class GastgezinService : IGastgezinService
{
    private readonly IHttpService _httpService;

    public GastgezinService(IHttpService httpService)
    {
        _httpService = httpService;
    }


    public async Task<Gastgezin?> CreateGastgezinAsync(CreateUpdateGastgezinParameters newGastgezinParameters)
    {
        var uri = "api/Gastgezinnen";
        var response = await _httpService.Put<Gastgezin>(uri, newGastgezinParameters);
        return response;
    }

    public async Task<Gastgezin?> UpdateGastgezinAsync(Guid id, CreateUpdateGastgezinParameters updateNawGastgezinParameters)
    {
        var uri = $"api/Gastgezinnen?Id={id}";
        var response = await _httpService.Post<Gastgezin>(uri, updateNawGastgezinParameters);
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
