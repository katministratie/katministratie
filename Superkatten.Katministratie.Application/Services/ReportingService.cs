
using Superkatten.Katministratie.Application.Helpers;
using Superkatten.Katministratie.Application.Interfaces;
using Superkatten.Katministratie.Application.Reporting;
using Superkatten.Katministratie.Infrastructure.Interfaces;
using System;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Application.Services;

public class ReportingService : IReportingService
{
    private readonly IReportingRepository _reportingRepository;
    private readonly IReportBuilder _reportBuilder;
    private readonly IMailService _mailService;

    public ReportingService(
        IReportingRepository reportingRepository,
        IReportBuilder reportBuilder,
        IMailService mailService
    )
    {
        _reportingRepository = reportingRepository;
        _reportBuilder = reportBuilder;
        _mailService = mailService;
    }

    public async Task EmailCatchLocationReport(string email, DateTime from, DateTime to)
    {
        if (EmailValidator.IsValidEmail(email))
        {
            throw new ApplicationException($"'{email}' is not a valid email address.");
        }

        var superkatten = await _reportingRepository.GetSuperkattenBetweenFromToAsync(from, to);

        var reportCsvData = _reportBuilder.BuildSuperkattenInventory(superkatten);

        await _mailService.MailToAsync(email, reportCsvData);
    }
}
