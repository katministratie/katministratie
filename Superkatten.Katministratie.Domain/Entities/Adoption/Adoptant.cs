using Superkatten.Katministratie.Domain.Exceptions;
using System;

namespace Superkatten.Katministratie.Domain.Entities.Adoption;

public class Adoptant
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string? Address { get; set; }
    public string? Postcode { get; set; }
    public string? City { get; set; }
    public string Email { get; set; }

    public AdoptionStage Stage { get; private set; } = AdoptionStage.Waiting;

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

    public void StartAdoption()
    {
        Stage = AdoptionStage.Starting;
    }

    public void PayAdoption()
    {
        Stage = AdoptionStage.Paying;
    }

    public void RetreiveAdoptees()
    {
        Stage = AdoptionStage.Retrieving;
    }

    public void RemoveAdopter()
    {
        Stage = AdoptionStage.Done;
    }
}
