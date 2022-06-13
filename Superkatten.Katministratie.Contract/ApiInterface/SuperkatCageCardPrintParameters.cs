namespace Superkatten.Katministratie.Contract.ApiInterface;

public class SuperkatCageCardPrintParameters
{
    public Guid Id { get; init; }
    public string PrinterName { get; init; } = string.Empty;
}
