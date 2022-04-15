using System;

namespace Superkatten.Katministratie.Application.Contracts
{
    public class UpdateSuperkatParameters
    {
        public string Name { get; init; } = string.Empty;
        public DateTimeOffset Birthday { get; init; }
    }
}
