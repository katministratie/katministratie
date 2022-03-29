using System;
using System.ComponentModel.DataAnnotations;

namespace Superkatten.Katministratie.Infrastructure.Entities
{
    public class SuperkatDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        public DateTimeOffset FoundDate { get; set; }
        [Required]
        public DateTimeOffset Birthday { get; set; }

        [Required]
        public string CatchLocation { get; set; } = string.Empty;

        [Required]
        public string Kleur { get; set; } = string.Empty;

        public string? Name { get; set; } = String.Empty;
    }
}
