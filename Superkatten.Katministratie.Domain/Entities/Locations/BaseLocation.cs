using System;

namespace Superkatten.Katministratie.Domain.Entities.Locations;

public abstract class BaseLocation
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public abstract LocationType LocationType { get; }
    public LocationNaw Naw { get; private set; } = new LocationNaw();

    public BaseLocation(string name, string? address, string? postcode, string? city, string? phone, string? email)
    {
        Naw = LocationNaw.Create(name, address, postcode, city, phone, email);
    }

    public void UpdateNaw(string name, string? address, string? postcode, string? city, string? phone, string? email)
    {
        Naw = LocationNaw.Create(name, address, postcode, city, phone, email);
    }
}
