using Microsoft.Extensions.DependencyInjection;
using Superkatten.Katministratie.Application.Interfaces;
using Superkatten.Katministratie.Application.Mappers;
using Superkatten.Katministratie.Application.Services;

namespace Superkatten.Katministratie.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<ISuperkattenService, SuperkattenService>();
            services.AddTransient<ISuperkattenMapper, SuperkattenMapper>();
            services.AddTransient<IGastgezinnenService, GastgezinnenService>();
            services.AddTransient<IGastgezinnenMapper, GastgezinnenMapper>();

            return services;
        }
    }
}
