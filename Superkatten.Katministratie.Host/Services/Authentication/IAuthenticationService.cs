using Superkatten.Katministratie.Host.Entities;

namespace Superkatten.Katministratie.Host.Services.Authentication;

public interface IAuthenticationService
{
    bool IsAuthenticated { get; }
    User? User { get; }
    Task InitializeAsync();
    Task RegisterAsync(string username, string password, string name, string email);
    Task AuthenticateUserAsync(string username, string password);
    Task LogoutAsync();
}
