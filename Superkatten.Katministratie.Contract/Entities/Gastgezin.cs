namespace Superkatten.Katministratie.Contract.Entities;

public class Gastgezin
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Address { get; init; }
    public string? City { get; init; }
    public string? Phone { get; init; }

    public IList<Superkat> Superkatten { get; init; } = Array.Empty<Superkat>();
}
