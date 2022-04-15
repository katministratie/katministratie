using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Superkatten.Katministratie.Application.Configuration;
using Superkatten.Katministratie.Application.Interfaces;
using Superkatten.Katministratie.Application.Mappers;
using Superkatten.Katministratie.Application.Services;
using Superkatten.Katministratie.Application.Services.Authentication;

namespace Superkatten.Katministratie.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApiKeyConfigurationSection>(configuration.GetSection("AuthorisationApiKeys"));

            services.AddTransient<ISuperkatAction, SuperkatAction>();
            services.AddTransient<ISuperkattenService, SuperkattenService>();
            services.AddTransient<ISuperkattenMapper, SuperkattenMapper>();
            services.AddTransient<IGastgezinnenService, GastgezinnenService>();
            services.AddTransient<IGastgezinnenMapper, GastgezinnenMapper>();
            services.AddTransient<IApiKeyRegistry, ApiKeyRegistry>();

            return services;
        }
    }
}
