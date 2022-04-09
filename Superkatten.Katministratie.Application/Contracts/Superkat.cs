using System;

namespace Superkatten.Katministratie.Application.Contracts
{
    public class Superkat
    {
        public int Number { get; init; }
        public string? Name { get; init; }
        public DateTimeOffset FoundDate { get; init; }
        public DateTimeOffset Birthday { get;init; }
        public string CatchLocation { get; init; } = string.Empty;
        public string SuperkatColor { get; init; } = string.Empty;
        public bool Reserved { get; init; } = false;
        public bool IsGoingRetour { get; init; } = false;
    }
}
