namespace Superkatten.Katministratie.Contract.Authenticate;

public class AuthenticateResponse
{
    public int Id { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? Username { get; init; }
    public string? Token { get; set; }
}