using Superkatten.Katministratie.Domain.Shared;

namespace Superkatten.Katministratie.Application.Contracts.Parameters;

public class NewSuperkatParameters
{
    public RefugeArea RefugeArea { get; init; }
    public int CageNumber { get; init; }
}
