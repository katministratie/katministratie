using System;

namespace Superkatten.Katministratie.Application.Contracts
{
    public class Superkat
    {
        public int Number { get; init; }
        public string Name { get; init; }
        public DateTimeOffset FoundDate { get; init; }
    }
}
