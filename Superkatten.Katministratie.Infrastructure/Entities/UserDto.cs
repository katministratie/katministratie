using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Superkatten.Katministratie.Infrastructure.Entities;

public class UserDto
{
    [Key]
    [Required]
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Username { get; init; } = string.Empty;
    public bool IsEnabled { get; init; } = true;  //TODO: default to false + way to enable ?
    public string Permissions { get; init; } = string.Empty;

    public string? PasswordHash { get; set; }
}
