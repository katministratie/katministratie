using System;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Application.Interfaces;

public interface IReportingService
{
    Task EmailCatchLocationReport(string email, DateTime from, DateTime to);
}
