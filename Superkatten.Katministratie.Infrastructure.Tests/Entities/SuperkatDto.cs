using System.ComponentModel.DataAnnotations;

namespace Superkatten.Katministratie.Infrastructure.Entities
{
    public class SuperkatDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Number { get;set; }
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
