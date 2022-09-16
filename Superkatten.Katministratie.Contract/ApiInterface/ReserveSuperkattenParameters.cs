namespace Superkatten.Katministratie.Contract.ApiInterface;

public class ReserveSuperkattenParameters
{
    public Guid GastgezinId { get; init; }
    public IReadOnlyCollection<Guid> Superkatten { get; init; } = null!;
    public string AdoptantName { get; init; } = null!;
    public string AdoptantEmail { get; init; } = null!;
}
