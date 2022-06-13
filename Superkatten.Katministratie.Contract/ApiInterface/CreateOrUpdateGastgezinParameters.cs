using Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.Contract.ApiInterface;

public class CreateOrUpdateGastgezinParameters
{
    public string Name { get; init; } = string.Empty;
    public string? Address { get; init; } = string.Empty;
    public string? City { get; init; } = string.Empty;
    public string? Phone { get; init; } = string.Empty;

    public List<Superkat> Superkatten { get; init; } = new List<Superkat>();
}
