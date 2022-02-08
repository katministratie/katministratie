using System;

namespace Superkatten.Katministratie.Application.Contracts
{
    public class Superkat
    {
        const int INVALLID_LOCATION = 0;

        public string Picture { get; init; } = string.Empty;
        public int Number { get; init; }
        public string Name { get; init; } = string.Empty;
        public DateTimeOffset FoundDate { get; init; }
        public int Location { get; init; } = INVALLID_LOCATION;
    }
}
