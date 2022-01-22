using Superkatten.Katministratie.Domain.Exceptions;
using System;

namespace Superkatten.Katministratie.Domain.Entities
{
    public class Superkat
    {
        public int Number { get; private set; }
        public string Name { get; private set; }
        public DateTimeOffset FoundDate { get; private set; }

        public Superkat(
            int number,
            string name,
            DateTimeOffset foundDate
        )
        {
            Number = number;
            Name = name;
            FoundDate = foundDate;
        }

        public Superkat CreateUpdatedModel(string name)
        {
            return new Superkat(
                Number, 
                name, 
                FoundDate
            );
        }
    }
}
