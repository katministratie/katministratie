using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Superkatten.Katministratie.Domain.Interfaces;
using Superkatten.Katministratie.Infrastructure.Mapper;
using Superkatten.Katministratie.Infrastructure.Persistence;

namespace Superkatten.Katministratie.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var cs = "Server=tcp:katministratiedbserver.database.windows.net,1433;Initial Catalog=KatministratieDb;Persist Security Info=False;User ID=katministrator;Password=Superkatten4143vk.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
              
            services.AddDbContext<SuperkattenDbContext>(option => option
                .UseSqlServer(cs)
                .EnableDetailedErrors()
            );
            services.AddTransient<ISuperkattenRepository, SuperkattenRepository>();
            services.AddTransient<ISuperkatRepositoryMapper, SuperkatRepositoryMapper>();
            services.AddTransient<IGastgezinnenRepository, GastgezinnenRepository>();
            services.AddTransient<IGastgezinRepositoryMapper, GastgezinRepositoryMapper>();

            return services;
        }
    }
}
