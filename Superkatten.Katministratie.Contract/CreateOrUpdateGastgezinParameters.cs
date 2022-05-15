using Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.Contract
{
    public class CreateOrUpdateGastgezinParameters
    {
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public string? City { get; set; } = string.Empty;
        public string? Phone { get; set; } = string.Empty;

        public List<Superkat> Superkatten { get; set; } = new();
    }
}
