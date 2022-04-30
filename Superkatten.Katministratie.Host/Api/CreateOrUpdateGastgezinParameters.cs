namespace Superkatten.Katministratie.Host.Api
{
    public class CreateOrUpdateGastgezinParameters
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}
