using Superkatten.Katministratie.Domain.Exceptions;
using Superkatten.Katministratie.Domain.Shared;
using System.Text.Json.Serialization;

namespace Superkatten.Katministratie.Domain;

public class User
{
    public string Username { get; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public IReadOnlyCollection<UserPermissions> Permissions { get; init; } = Array.Empty<UserPermissions>();

    [JsonIgnore]
    public string? PasswordHash { get; }

    public User(string username, string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            throw new EmptyUsernameException($"username can not be empty");
        }

        if (string.IsNullOrWhiteSpace(passwordHash))
        {
            throw new EmptyPasswordException($"password can not be empty");
        }
        Username = username;
        PasswordHash= passwordHash;
        Permissions = new List<UserPermissions>()
        {
            UserPermissions.Guest
        };
    }

    public User Update(string username, string email, string name, string passwordHash)
    {
        return new User(username, passwordHash)
        {
            Permissions = Permissions,
            Name = name,
            Email = email,
        };
    }
}
