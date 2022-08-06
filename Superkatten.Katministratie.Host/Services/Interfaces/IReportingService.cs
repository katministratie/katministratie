using Superkatten.Katministratie.Contract.ApiInterface.Reporting;

namespace Superkatten.Katministratie.Host.Services.Interfaces;

public interface IReportingService
{
    Task EmailInventoryDetailsReportAsync(RequestCatchLocationEmailParameters requestPeriod);
    Task EmailCageCardAsync(RequestCageCardEmailParameters cageCardRequestParameters);
}
