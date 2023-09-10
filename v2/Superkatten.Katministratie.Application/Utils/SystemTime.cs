namespace Superkatten.Katministratie.Application.Utils;

public class SystemTime : ISystemTime
{
    public DateTime UtcNow => DateTime.UtcNow;
}
