using Superkatten.Katministratie.Domain.Exceptions;
using System;

namespace Superkatten.Katministratie.Domain.Entities.Locations;

public class Gastgezin : BaseLocation
{
    public override LocationType LocationType => LocationType.HostFamily;

    public Gastgezin(string name, string? address, string? postcode, string? city, string? phone, string? email)
        : base(name, address, postcode, city, phone, email)
    {
    
    }
}
