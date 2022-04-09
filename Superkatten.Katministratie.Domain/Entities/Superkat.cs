using Superkatten.Katministratie.Domain.Exceptions;
using System;

namespace Superkatten.Katministratie.Domain.Entities
{
    public class Superkat
    {
        public Guid Id { get; private set; }
        public int Number { get; private set; }
        public DateTimeOffset FoundDate { get; private set; }
        public string CatchLocation { get; private set; }
        public string? Name { get; private set; } = string.Empty;
        public DateTimeOffset Birthday { get; private set; }
        public bool Reserved { get; private set; }

        public Superkat(
            int number,
            string catchLocation
        )
        {
            Number = number;
            FoundDate = DateTimeOffset.Now;
            CatchLocation = catchLocation;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new DomainException("Name cannot be null or empty");
            }

            Name = name;
        }

        public void SetWeeksOld(int weeksOld)
        {
            if (weeksOld <= 0)
            {
                throw new DomainException($"Value {weeksOld} for parameter {nameof(weeksOld)} cannot be negative");
            }

            var today = DateTimeOffset.Now;
            var daysOld = 7 * weeksOld;
            var birthDay = today.AddDays(-daysOld);

            Birthday = birthDay;
        }

        public void SetReserved(bool isReserved)
        {
            Reserved = isReserved;
        }
    }
}
