namespace Superkatten.Katministratie.Contract
{
    public class UpdateSuperkatParameters
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public DateTimeOffset Birthday { get; init; }
    }
}