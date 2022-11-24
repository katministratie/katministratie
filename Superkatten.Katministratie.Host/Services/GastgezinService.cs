using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Contract.Entities.Locations;
using Superkatten.Katministratie.Host.Services.Http;

namespace Superkatten.Katministratie.Host.Services;

public class GastgezinService : IGastgezinService
{
    private readonly IHttpService _httpService;

    public GastgezinService(IHttpService httpService)
    {
        _httpService = httpService;
    }


    public async Task<Location?> CreateGastgezinAsync(LocationNawParameters newGastgezinParameters)
    {
        var uri = "api/Location";
        var response = await _httpService.Put<Location>(uri, newGastgezinParameters);
        return response;
    }

    public async Task<Location?> UpdateGastgezinAsync(Guid id, LocationNawParameters updateNawGastgezinParameters)
    {
        var uri = $"api/Location?Id={id}";
        var response = await _httpService.Post<Location>(uri, updateNawGastgezinParameters);
        return response;
    }

    public async Task DeleteGastgezinAsync(Guid id)
    {
        var uri = $"api/Location?Id={id}";
        await _httpService.Delete(uri);
    }

    public async Task<Location?> GetGastgezinAsync(Guid id)
    {
        var gastgezinnen = await GetAllGastgezinAsync();

        return gastgezinnen
            .Where(s => s.Id == id)
            .First();
    }
        
    public async Task<List<Location>> GetAllGastgezinAsync()
    {
        var uri = "api/Location";
        var gastgezinnen = await _httpService.Get<List<Location>>(uri);

        return gastgezinnen is null
            ? new()
            : gastgezinnen;
    }
}
