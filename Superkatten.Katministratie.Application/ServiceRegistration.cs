using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Superkatten.Katministratie.Application.Authenticate;
using Superkatten.Katministratie.Application.Authorization;
using Superkatten.Katministratie.Application.CageCard;
using Superkatten.Katministratie.Application.Interfaces;
using Superkatten.Katministratie.Application.Mappers;
using Superkatten.Katministratie.Application.Reporting;
using Superkatten.Katministratie.Application.Services;

namespace Superkatten.Katministratie.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ISuperkatAction, SuperkatAction>();
            services.AddTransient<ISuperkattenService, SuperkattenService>();
            services.AddTransient<IGastgezinnenService, GastgezinnenService>();
            services.AddTransient<IMedicalProcedureService, MedicalProcedureService>();
            services.AddTransient<ICageCardProducer, CageCardProducer>();
            services.AddTransient<ISuperkatMapper, SuperkatMapper>();
            services.AddTransient<IGastgezinMapper, GastgezinMapper>();
            services.AddTransient<IUserAuthorisationMapper, UserAuthorisationMapper>();
            services.AddTransient<IMedicalProcedureMapper, MedicalProcedureMapper>();
            services.AddTransient<IReportBuilder, ReportBuilder>();
            services.AddTransient<IMailService, MailService>();
            services.AddTransient<IReportingService, ReportingService>();
            services.AddTransient<ILocationService, LocationService>();
            services.AddTransient<ILocationMapper, LocationMapper>();
            services.AddTransient<ICageCardComposerFactory, CageCardComposerFactory>();
            services.AddTransient<ISettingsService, SettingsService>();
            
            services.AddScoped<IJwtUtils, JwtUtils>();
            services.AddScoped<IUserService, UserService>();

            services.AddTransient<IAuthorizationHandler, AuthorisationHandler>();

            return services;
        }
    }
}
