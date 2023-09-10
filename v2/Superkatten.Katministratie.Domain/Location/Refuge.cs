using Superkatten.Katministratie.Domain.Shared;

namespace Superkatten.Katministratie.Domain.Location;

public class Refuge : LocationBase
{
    public override LocationType Type => LocationType.Refuge;
}
