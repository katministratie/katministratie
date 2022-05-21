using Superkatten.Katministratie.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Superkatten.Katministratie.Domain.Entities;

public class Gastgezin
{
    [Key]
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string? Address { get; private set; }
    public string? City { get; private set; }
    public string? Phone { get; private set; }

    public List<Superkat> Superkatten { get; private set; } = new();

    public Gastgezin(Guid id, string name, string? address, string? city, string? phone, List<Superkat> superkatten)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new DomainException($"{nameof(Name)} should not be null or empty");
        }

        Id = id;
        Name = name;
        Address = address;
        City = city;
        Phone = phone;
        Superkatten = superkatten;
    }
}
