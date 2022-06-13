namespace Superkatten.Katministratie.Contract.ApiInterface;

public class UpdateSuperkatParameters
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public DateTime Birthday { get; init; }
}
