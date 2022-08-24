using Superkatten.Katministratie.Contract.Entities;
using System;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Application.Interfaces;

public interface IReportingService
{
    Task EmailCatchLocationReport(string email, DateTime from, DateTime to);
    Task EmailCageCard(string email, CatArea catArea, int? CageNumber);
    Task EmailNotNeutralizedAdopteesReport(string email);
    Task EmailNotNeutralizedRefugeReport(string email);
}
