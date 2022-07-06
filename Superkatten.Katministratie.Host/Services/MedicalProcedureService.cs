using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Host.Services.Authentication;
using Superkatten.Katministratie.Host.Services.Interfaces;

namespace Superkatten.Katministratie.Host.Services;

public class MedicalProcedureService : IMedicalProcedureService
{
    private readonly IHttpService _httpService;

    public MedicalProcedureService(IHttpService httpService)
    {
        _httpService = httpService;
    }

    public Task AddMedicalProcedure(Guid superkatId, AddMedicalProcedureParameters parameters)
    {
        var uri = $"api/MedicalProcedure/?superkatId={superkatId}";
        return _httpService.Put(uri, parameters);
    }
}
