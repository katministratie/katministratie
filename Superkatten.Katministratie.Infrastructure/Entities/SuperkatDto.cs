using System;
using System.Collections.Generic;
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
        public string Name { get; set; } = string.Empty;
        [Required]
        public DateTimeOffset FoundDate { get; set; }
        [Required]
        public List<SuperkatDetailsDto> Details { get; set; } = new List<SuperkatDetailsDto>();
    }
}
