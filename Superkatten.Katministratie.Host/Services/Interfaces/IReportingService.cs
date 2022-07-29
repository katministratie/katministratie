using Superkatten.Katministratie.Contract.ApiInterface.Reporting;

namespace Superkatten.Katministratie.Host.Services.Interfaces;

public interface IReportingService
{
    Task EmailInventoryDetailsReport(RequestCatchLocationEmailParameters requestPeriod);
}
