namespace Superkatten.Katministratie.SuperkatApi.Authentication;

public interface IJwtAuthenticationManager
{
    string Authenticate(string username, string password);
}
