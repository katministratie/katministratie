namespace Superkatten.Katministratie.Host.Entities
{
    public class Gastgezin
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public string? City { get; set; } = string.Empty;
        public string? Phone { get; set; } = string.Empty;
    }
}
