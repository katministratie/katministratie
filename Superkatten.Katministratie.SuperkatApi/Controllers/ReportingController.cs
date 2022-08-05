using Microsoft.AspNetCore.Mvc;
using Superkatten.Katministratie.Application.Authorization;
using Superkatten.Katministratie.Application.Interfaces;
using Superkatten.Katministratie.Application.Mappers;
using Superkatten.Katministratie.Contract.ApiInterface.Reporting;
using Superkatten.Katministratie.Domain.Entities;

namespace Superkatten.Katministratie.SuperkatApi.Controllers;

[AuthorizeRoles(PermissionEnum.Administrator, PermissionEnum.Coordinator)]
[Route("api/[Controller]")]
[ApiController]
public class ReportingController
{
    private readonly IReportingService _reportingService;

    public ReportingController(IReportingService reportingService)
    {
        _reportingService = reportingService;
    }

    [HttpPut]
    [Route("reports/catchlocation")]
    public async Task EmailCatchLocationReport([FromBody] RequestCatchLocationEmailParameters requestParameters)
    {
        await _reportingService.EmailCatchLocationReport(
            requestParameters.Email,
            requestParameters.From,
            requestParameters.To
        );
    }
}
