using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Superkatten.Katministratie.Infrastructure.Entities
{
    public class SuperkatDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        public DateTimeOffset CatchDate { get; set; }


        [Required]
        public string CatchLocation { get; set; } = string.Empty;

        public DateTimeOffset Birthday { get; set; }

        public string? Name { get; set; }

        public bool Reserved { get; set; }

        public bool Retour { get; set; }

        public int Area { get; set; }

        public int? CageNumber { get; set; }

        public int Behaviour { get; set; }

        public bool IsKitten { get; set; }
    }
}
