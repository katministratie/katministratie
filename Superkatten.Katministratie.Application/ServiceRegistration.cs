﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Superkatten.Katministratie.Application.CageCard;
using Superkatten.Katministratie.Application.Configuration;
using Superkatten.Katministratie.Application.Interfaces;
using Superkatten.Katministratie.Application.Mappers;
using Superkatten.Katministratie.Application.Printing;
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
            services.AddTransient<IGastgezinnenService, GastgezinnenService>();
            services.AddTransient<IApiKeyRegistry, ApiKeyRegistry>();
            services.AddTransient<ISuperkatCageCard, SuperkatCageCard>();
            services.AddTransient<ICageCardComposer, CageCardComposer>();
            services.AddTransient<ISuperkatMapper, SuperkatMapper>();
            services.AddTransient<IGastgezinMapper, GastgezinMapper>();

            // See: https://github.com/svishnevsky/PDFtoPrinter
            services.AddTransient<IPrinterService, PrinterService>();

            return services;
        }
    }
}
