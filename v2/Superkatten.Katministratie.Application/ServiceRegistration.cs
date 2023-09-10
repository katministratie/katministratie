using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Superkatten.Katministratie.Application.Authorisation;
using Superkatten.Katministratie.Application.Contracts.Interfaces;
using Superkatten.Katministratie.Application.Mappers;
using Superkatten.Katministratie.Application.Services;
using Superkatten.Katministratie.Application.Utils;
using Superkatten.Katministratie.Domain.Shared;

namespace Superkatten.Katministratie.Application;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ISuperkatService, SuperkatService>();
        services.AddTransient<ISuperkatMapper, SuperkatMapper>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IJwtUtils, JwtUtils>();
        services.AddTransient<IAuthorizationHandler, AuthorisationHandler>();
        services.AddTransient<ISystemTime, SystemTime>();

        return services;
    }
}