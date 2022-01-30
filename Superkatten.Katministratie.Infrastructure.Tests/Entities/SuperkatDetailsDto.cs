using System;
using System.ComponentModel.DataAnnotations;

namespace Superkatten.Katministratie.Infrastructure.Entities
{
    public class SuperkatDetailsDto
    {
        [Key]
        public int Id { get; set; }
        [Required, DataType(DataType.DateTime)]
        public DateTime Entered { get; set; }

        [Required, StringLength(50)]
        public string Title { get; set; }

        public string Description { get; set; }
    }
}
