using Superkatten.Katministratie.Contract.ApiInterface.Reporting;
using Superkatten.Katministratie.Host.Services.Http;
using Superkatten.Katministratie.Host.Services.Interfaces;

namespace Superkatten.Katministratie.Host.Services;

public class ReportingService : IReportingService
{
    private readonly IHttpService _httpService;

    public ReportingService(IHttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task EmailInventoryDetailsReportAsync(RequestCatchLocationEmailParameters requestPeriod)
    {
        var uri = "api/Reporting/reports/catchlocation";
        await _httpService.Put(uri, requestPeriod);
    }

    public async Task EmailCageCardAsync(RequestCageCardEmailParameters cageCardRequestParameters)
    {
        var uri = "api/Reporting/reports/cagecard";
        await _httpService.Put(uri, cageCardRequestParameters);
    }
}
