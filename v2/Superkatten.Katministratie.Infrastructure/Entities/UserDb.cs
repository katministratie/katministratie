using System.ComponentModel.DataAnnotations;

namespace Superkatten.Katministratie.Infrastructure.Entities;

public class UserDb
{
    [Key]
    [Required]
    public int Id { get; set; }
    
    [Required]
    public string UserName { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Permissions { get; set; } = string.Empty;
}
