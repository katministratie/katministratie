using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Superkatten.Katministratie.Domain.Entities.Locations;

public abstract class BaseLocation
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public abstract LocationType LocationType { get; }

    public LocationNaw LocationNaw { get; private set; }

    public BaseLocation()
    {
        // EF needs an empty Ctor
    }

    public void UpdateNaw(string name, string? address, string? postcode, string? city, string? phone, string? email)
    {
        LocationNaw = LocationNaw.Create(name, address, postcode, city, phone, email);
    }
}
