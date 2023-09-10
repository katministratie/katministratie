namespace Superkatten.Katministratie.Application.Utils;

public interface ISystemTime
{
    DateTime UtcNow { get; }
}