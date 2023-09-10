namespace Superkatten.Katministratie.Domain.Exceptions;

public class EmptyPasswordException : DomainException
{
    public EmptyPasswordException(string? message) : base(message)
    {

    }
}
