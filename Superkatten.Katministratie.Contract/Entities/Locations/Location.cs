namespace Superkatten.Katministratie.Contract.Entities.Locations;

public class Location
{
    public Guid Id { get; init; }
    public LocationType LocationType { get; init; }
    public LocationNaw LocationNaw { get; init; } = null!;
}
