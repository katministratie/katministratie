namespace Superkatten.Katministratie.Application.Configuration;

public class UserAuthorisationConfiguration : AppSettings { }

public class AppSettings
{
    public string Secret { get; set; }
}
