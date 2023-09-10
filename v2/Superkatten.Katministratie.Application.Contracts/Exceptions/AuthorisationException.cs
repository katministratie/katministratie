namespace Superkatten.Katministratie.Application.Contracts.Exceptions;

public class AuthorisationException : Exception
{
    public AuthorisationException(string message) :
        base(message)
    {

    }
}
