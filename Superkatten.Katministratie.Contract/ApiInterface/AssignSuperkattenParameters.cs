using Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.Contract.ApiInterface;

public class AssignSuperkattenParameters
{
    public Guid Id { get; init; } = Guid.Empty;

    public IReadOnlyCollection<Superkat> AssignedSuperkatten { get; init; } = new List<Superkat>();
}
