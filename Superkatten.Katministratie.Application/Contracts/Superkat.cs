using System;

namespace Superkatten.Katministratie.Application.Contracts
{
    public class Superkat
    {
        public int Number { get; init; }
        public string? Name { get; init; }
        public DateTimeOffset FoundDate { get; init; }
        public DateTimeOffset Birthday { get; init; }
        public string CatchLocation { get; init; } = string.Empty;
        public string SuperkatColor { get; init; } = string.Empty;
        public bool Reserved { get; init; } = false;
        public bool Retour { get; init; } = false;
        public int HokNumber { get; init; }

        public string DisplayableNumber
        {
            get
            {
                var yearPart = FoundDate.Year % 100;
                return yearPart + "-" + Number.ToString("000");
            }
        }
    }
}
