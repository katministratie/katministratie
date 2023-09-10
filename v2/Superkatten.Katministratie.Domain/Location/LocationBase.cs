using Superkatten.Katministratie.Domain.Shared;

namespace Superkatten.Katministratie.Domain.Location;

public abstract class LocationBase
{
    public abstract LocationType Type { get; }
}