using System;

namespace Superkatten.Katministratie.Application.Contracts
{
    public class Superkat
    {
        public string Picture { get; set; } = string.Empty;
        public int Number { get; init; }
        public string Name { get; init; } = string.Empty;
        public DateTimeOffset FoundDate { get; init; }
    }
}
