﻿using Superkatten.Katministratie.Domain.Exceptions;
using System;
using System.Net;
using System.Numerics;

namespace Superkatten.Katministratie.Domain.Entities.Locations;

public class Adoptant : BaseLocation
{
    public override LocationType LocationType => LocationType.Adopter;

    public Adoptant() : base(string.Empty, null, null, null, null, null)
    {
        // Mandatory for EF
    }

    public Adoptant(string name, string? address, string? postcode, string? city, string? phone, string email) 
        : base(name, address, postcode, city, phone, email)
    {
        if (string.IsNullOrEmpty(email))
        {
            throw new DomainException("Email address may not be empty");
        }
    }
}
