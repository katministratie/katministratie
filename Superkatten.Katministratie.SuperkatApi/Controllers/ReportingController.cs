using Microsoft.AspNetCore.Mvc;
using Superkatten.Katministratie.Application.Authorization;
using Superkatten.Katministratie.Application.Interfaces;
using Superkatten.Katministratie.Contract.ApiInterface.Reporting;
using Superkatten.Katministratie.Domain.Entities;

namespace Superkatten.Katministratie.SuperkatApi.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class ReportingController : ControllerBase
{
    private readonly IReportingService _reportingService;

    public ReportingController(IReportingService reportingService)
    {
        _reportingService = reportingService;
    }

    [HttpPut]
    [Route("reports/catchlocation")]
    public async Task<IActionResult> EmailCatchLocationReport([FromBody] RequestCatchLocationEmailParameters requestCatchLocationParameters)
    {
        await _reportingService.EmailCatchLocationReport(
            requestCatchLocationParameters.Email,
            requestCatchLocationParameters.From,
            requestCatchLocationParameters.To
        );

        return Ok();
    }

    [HttpPut]
    [Route("reports/cagecard")]
    public async Task<IActionResult> EmailCageCard([FromBody] RequestCageCardEmailParameters requestCageCardParameters)
    {
        await _reportingService.EmailCageCard(
            requestCageCardParameters.Email,
            requestCageCardParameters.CatArea,
            requestCageCardParameters.CageNumber
        );

        return Ok();
    }

    [HttpPut]
    [Route("reports/cagecard/notNeutralizedAdoptees")]
    public async Task<IActionResult> EmailNotNeutralizedAdoptees([FromBody] string email)
    {
        await _reportingService.EmailNotNeutralizedAdoptees(email);

        return Ok();
    }

    [HttpPut]
    [Route("reports/cagecard/notNeutralizedRefuge")]
    public async Task<IActionResult> EmailNotNeutralizedRefuge([FromBody] string email)
    {
        await _reportingService.EmailNotNeutralizedRefuge(email);

        return Ok();
    }
}
