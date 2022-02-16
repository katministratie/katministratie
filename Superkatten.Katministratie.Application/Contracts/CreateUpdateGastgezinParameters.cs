namespace Superkatten.Katministratie.Application.Contracts
{
    public class CreateUpdateGastgezinParameters
    {
        public string Name { get; init; } = string.Empty;
        public string? Address { get; init; }
        public string? City{ get; init; }
        public string? Phone { get; set; }
    }
}
