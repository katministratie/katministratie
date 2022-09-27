using Superkatten.Katministratie.Domain.Exceptions;
using System;

namespace Superkatten.Katministratie.Domain.Entities.Locations;

public class Adoptant : LocationBase
{
    public override LocationType LocationType => LocationType.Adopter;
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string? Address { get; set; }
    public string? Postcode { get; set; }
    public string? City { get; set; }
    public string Email { get; set; }

    public Adoptant(string name, string email)
    {
        Id = Guid.NewGuid();

        if (string.IsNullOrEmpty(name))
        {
            throw new DomainException("Name may not be empty");
        }

        if (string.IsNullOrEmpty(email))
        {
            throw new DomainException("Email address may not be empty");
        }

        Name = name;
        Email = email;
    }

    public Adoptant CreateUpdatedModel(string name, string address, string postcode, string city, string email)
    {
        return new Adoptant(name, email)
        {
            Id = Id
        };
    }
}
