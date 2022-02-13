using Superkatten.Katministratie.Domain.Exceptions;
using System;

namespace Superkatten.Katministratie.Domain.Entities
{
    public class Superkat
    {
        public int Number { get; private set; }
        public DateTimeOffset FoundDate { get; private set; }
        public string CatchLocation { get; private set; }
        public string Kleur { get; private set; }
        public string? Name { get; private set; }
        public DateTimeOffset Birthday { get; private set; }

        public Superkat(
            int number,
            string kleur,
            DateTimeOffset foundDate,
            string catchLocation
        )
        {
            Number = number;
            Kleur = kleur;
            FoundDate = foundDate;
            CatchLocation = catchLocation;
        }

        public Superkat SetBirthday(DateTimeOffset birthday)
        {
            if (birthday >= FoundDate)
            {
                throw new DomainException($"Birthday '{birthday}' is larger or equal than founddate '{FoundDate}'");
            }

            if (Birthday != birthday)
            {
                Birthday = birthday;
            }

            return this;
        }

        public Superkat SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new DomainException("Empty name not allowed");
            }

            if (Name != name)
            {
                Name = name;
            }

            return this;
        }
    }
}
