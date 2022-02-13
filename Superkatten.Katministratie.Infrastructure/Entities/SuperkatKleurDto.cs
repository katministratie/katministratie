using System.ComponentModel.DataAnnotations;

namespace Superkatten.Katministratie.Infrastructure.Entities
{
    public class SuperkatKleurDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Kleur { get; set; }
    }
}
