using System;

namespace Superkatten.Katministratie.Domain.Contracts
{
    public class SuperkatCageCardPrintParameters
    {
        public Guid Id { get; init; }
        public string PrinterName { get; init; } = string.Empty;
    }
}
