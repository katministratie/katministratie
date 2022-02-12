using Superkatten.Katministratie.Domain.Exceptions;
using System;

namespace Superkatten.Katministratie.Domain.Entities
{
    public class Superkat
    {
        public int Number { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public DateTimeOffset FoundDate { get; private set; }
        public int Location { get; private set; }
        public string? ChipNumber { get; private set; }

        public Superkat(
            int number,
            string name,
            DateTimeOffset foundDate,
            int location
        )
        {
            Number = number;
            Name = name;
            FoundDate = foundDate;
            Location = location;
        }

        public Superkat CreateUpdatedModel(string name, int location)
        {
            if (name == string.Empty)
            {
                throw new DomainException("An empty name is not allowed.");
            }

            if (location <= 0)
            {
                throw new DomainException($"location {location} must be higher than 0.");
            }

            return new Superkat(
                Number, 
                name, 
                FoundDate,
                Location
            );
        }
    }
}
