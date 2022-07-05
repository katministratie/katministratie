using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Host.Services.Authentication;

namespace Superkatten.Katministratie.Host.Services;

public class MedicalProcedureService : IMedicalProcedureService
{
    private readonly IHttpService _httpService;

    public MedicalProcedureService(IHttpService httpService)
    {
        _httpService = httpService;
    }

    public Task AddMedicalProcedureAsync(AddMedicalProcedureParameters parameters)
    {
        var uri = $"api/MedicalProcedure";
        return _httpService.Put(uri, parameters);
    }
}
