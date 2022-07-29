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

    public async Task EmailInventoryDetailsReport(RequestCatchLocationEmailParameters requestPeriod)
    {
        var uri = "api/Reporting/reports/catchlocation";
        await _httpService.Put(uri, requestPeriod);
    }
}
