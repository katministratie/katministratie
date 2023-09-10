namespace Superkatten.Katministratie.Domain.Exceptions;

public class EmptyUsernameException : DomainException
{
    public EmptyUsernameException(string? message) : base(message)
    {

    }
}
