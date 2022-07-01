using Superkatten.Katministratie.Domain.Exceptions;
using System;

namespace Superkatten.Katministratie.Domain.Entities;

public class Gastgezin
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string? Address { get; private set; }
    public string? City { get; private set; }
    public string? Phone { get; private set; }

    public Gastgezin(
        string name, 
        string? address, 
        string? city, 
        string? phone
    )
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new DomainException($"{nameof(Name)} should not be null or empty");
        }

        Id = Guid.NewGuid();

        Name = name;
        Address = address;
        City = city;
        Phone = phone;
    }

    public Gastgezin Update(string name, string? address, string? city, string? phone)
    {
        return new Gastgezin(name, address, city, phone)
        {
            Id = Id
        };
    }
}
