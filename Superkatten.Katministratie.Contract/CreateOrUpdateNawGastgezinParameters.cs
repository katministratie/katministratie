namespace Superkatten.Katministratie.Contract;

public class CreateOrUpdateNawGastgezinParameters
{
    public string Name { get; init; } = string.Empty;
    public string? Address { get; init; } = string.Empty;
    public string? City { get; init; } = string.Empty;
    public string? Phone { get; init; } = string.Empty;
}
