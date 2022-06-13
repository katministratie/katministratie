using System.Text.Json.Serialization;

namespace Superkatten.Katministratie.Domain.Entities;

public class User
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Username { get; init; } = string.Empty;
    public bool IsEnabled { get; init; } = false;

    [JsonIgnore]
    public string? PasswordHash { get; set; }
}
