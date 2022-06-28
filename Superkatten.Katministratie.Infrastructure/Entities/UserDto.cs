using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Superkatten.Katministratie.Infrastructure.Entities;

public class UserDto
{
    [Key]
    [Required]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public bool IsEnabled { get; set; } = false;
    public string Permissions { get; set; } = string.Empty;
    public string? PasswordHash { get; set; }
}
