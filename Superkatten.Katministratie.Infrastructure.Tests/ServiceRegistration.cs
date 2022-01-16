using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Superkatten.Katministratie.Domain.Interfaces;
using Superkatten.Katministratie.Infrastructure.Persistence;

namespace Superkatten.Katministratie.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<SuperkattenDbContext>(opt => opt.UseInMemoryDatabase("Superkatten"));
            services.AddTransient<ISuperkattenRepository, SuperkattenRepository>();

            return services;
        }
    }
}
