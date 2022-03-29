using Superkatten.Katministratie.Domain.Exceptions;
using System;
using System.ComponentModel.DataAnnotations;

namespace Superkatten.Katministratie.Domain.Entities
{
    public class Gastgezin
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; private set; }
        public string? Address { get; private set; }
        public string? City { get; private set; }
        public string? Phone { get; private set; }


        public Gastgezin(string name, string? address, string? city, string? phone)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new DomainException($"{nameof(Name)} should not be null or empty");
            }

            Name = name;
            Address = address;
            City = city;
            Phone = phone;
        }

        public Gastgezin SetName(string name)
        {
            Name = name;
            return this;
        }

        public Gastgezin SetAddress(string? address)
        {
            Address = address;
            return this;
        }

        public Gastgezin SetCity(string? city)
        {
            City = city;
            return this;
        }

        public Gastgezin SetPhone(string? phone)
        {
            Phone = phone;
            return this;
        }
    }
}
