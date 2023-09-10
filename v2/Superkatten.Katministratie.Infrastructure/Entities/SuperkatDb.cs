using System.ComponentModel.DataAnnotations;

namespace Superkatten.Katministratie.Infrastructure.Entities;

public class SuperkatDb
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public int Number { get; set; }

    [Required]
    public DateTime Entered { get; set; }
}
