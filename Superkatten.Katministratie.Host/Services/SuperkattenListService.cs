using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Contract.ApiInterface.Reporting;
using Superkatten.Katministratie.Contract.Entities;
using Superkatten.Katministratie.Host.Services.Http;

namespace Superkatten.Katministratie.Host.Services;

public class SuperkattenListService : ISuperkattenListService
{
    private readonly IHttpService _httpService;

    public SuperkattenListService(IHttpService httpService)
    {
        _httpService = httpService;
    }
    
    public async Task<Superkat?> CreateSuperkatAsync(CreateSuperkatParameters newSuperkat)
    {
        var uri = $"api/Superkatten";
        try
        {
            var superkat = await _httpService.Put<Superkat>(uri, newSuperkat);
            return superkat;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task UpdateSuperkatAsync(Guid id, UpdateSuperkatParameters updateSuperkat)
    {
        var uri = $"api/Superkatten?Id={id}";
        await _httpService.Post<Superkat?>(uri, updateSuperkat);
    }

    public async Task DeleteSuperkatAsync(Guid id)
    {
        var uri = $"api/Superkatten?Id={id}";
        await _httpService.Delete(uri);
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

    public async Task<List<Superkat>> GetCageCardEmailSuperkattenAsync(RequestCageCardEmailParameters requestParameters)
    {
        var uri = "api/Superkatten/NotAssigned";
        var notAssignedSuperkatten = await _httpService.Get<List<Superkat>>(uri);

        var superkatten = notAssignedSuperkatten
            .Where(s => s.CatArea == requestParameters.CatArea)
            .Where(s => s.CageNumber == requestParameters.CageNumber)
            .ToList();

        return notAssignedSuperkatten is null
            ? new()
            : superkatten;
    }
}
