namespace Superkatten.Katministratie.Contract.ApiInterface;

public class CreateUpdateGastgezinParameters
{
    public string Name { get; init; } = string.Empty;
    public string? Address { get; init; } = string.Empty;
    public string? City { get; init; } = string.Empty;
    public string? Phone { get; init; } = string.Empty;
}
