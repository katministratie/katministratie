namespace Superkatten.Katministratie.Contract.ApiInterface.Reporting;

public class RequestCatchLocationEmailParameters
{
    public string Email { get; init; } = String.Empty;
    public DateTime From { get; init; }
    public DateTime To { get; init; }
}
