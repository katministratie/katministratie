using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Superkatten.Katministratie.Infrastructure.Entities
{
    public class GastgezinDto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string? Address { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string? City { get; set; } = string.Empty;

        [Required, StringLength(15)]
        public string? Phone { get; set; } = string.Empty;

        [Required]
        public List<SuperkatDto> Superkatten { get; set; } = new List<SuperkatDto>();
    }
}
