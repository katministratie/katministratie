namespace Superkatten.Katministratie.Contract.Entities;

public class Location
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public CatchOriginType Type { get; init; }
}
