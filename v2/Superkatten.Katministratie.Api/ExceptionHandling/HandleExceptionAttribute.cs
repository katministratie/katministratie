namespace Superkatten.Katministratie.HttpApi.ExceptionHandling;

public class HandleExceptionAttribute : Attribute
{
    public Type ExceptionType { get; } = null!;

    public HandleExceptionAttribute(Type exceptionType)
    {
        ExceptionType = exceptionType;
    }
}
