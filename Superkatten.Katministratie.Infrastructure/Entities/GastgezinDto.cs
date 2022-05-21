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

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? Phone { get; set; }

        [Required]
        public List<SuperkatDto> Superkatten { get; set; } = new List<SuperkatDto>();
    }
}
