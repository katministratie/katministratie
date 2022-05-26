namespace Superkatten.Katministratie.Application.Authenticate;

public interface IJwtUtils
{
    public string GenerateToken(User user);
    public int? ValidateToken(string token);
}