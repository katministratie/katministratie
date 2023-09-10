using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Superkatten.Katministratie.Infrastructure.Mappers;
using Superkatten.Katministratie.Infrastructure.Persistance;
using Superkatten.Katministratie.Infrastructure.Services;

namespace Superkatten.Katministratie.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        ConfigureDatabaseContext(services);
        services.AddTransient<ISuperkattenRepository, SuperkattenRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<ISuperkatMapper, SuperkatMapper>();

        return services;
    }

    private static void ConfigureDatabaseContext(IServiceCollection services)
    {
        var connectionString = "Server=localhost;User ID=katministrator;Password=4143vk.;Database=katministratie";

        // Replace with your server version and type.
        // Use 'MariaDbServerVersion' for MariaDB.
        // Alternatively, use 'ServerVersion.AutoDetect(connectionString)'.
        // For common usages, see pull request #1233.
        //        var serverVersion = new MySqlServerVersion(new Version(8, 0, 34));
        var serverVersion = ServerVersion.AutoDetect(connectionString);

        services.AddDbContext<KatministratieContext>(dbContextOptions => 
            dbContextOptions.UseMySql(connectionString, serverVersion)
//#if DEBUG
                // The following three options help with debugging, but should
                // be changed or removed for production.
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
//#endif
        );
    }
}