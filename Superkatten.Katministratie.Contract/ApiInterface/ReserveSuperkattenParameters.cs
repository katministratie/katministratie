using Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.Contract.ApiInterface;

public class ReserveSuperkattenParameters
{
    public Guid GastgezinId { get; init; }
    public IReadOnlyCollection<Superkat> Superkatten { get; init; }
}
