﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Superkatten.Katministratie.Infrastructure.Exceptions;
using Superkatten.Katministratie.Infrastructure.Interfaces;
using Superkatten.Katministratie.Infrastructure.Mapper;
using Superkatten.Katministratie.Infrastructure.Persistence;
using System;

namespace Superkatten.Katministratie.Infrastructure
{
    public static class ServiceRegistration
    {
        // See: https://docs.microsoft.com/en-us/azure/app-service/configure-common?tabs=portal
        private const string ENVIRONMENT_VAR_CONNECTION_STRING = "SQLCONNSTR_SuperkattenDatabase";
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Use the Web Api environment variable from azure
            // Goto Azure -> App Service -> Configuration and add the ENVIRONMENT_VAR_CONNECTION_STRING
            var cs = Environment.GetEnvironmentVariable(ENVIRONMENT_VAR_CONNECTION_STRING);
            if (string.IsNullOrEmpty(cs))
            {
                throw new DatabaseException("Database connectionstring is invallid.");
            }
            
            services.AddDbContext<SuperkattenDbContext>(option => 
                option.UseSqlServer(cs ?? string.Empty).EnableDetailedErrors()
            );

            services.AddTransient<ISuperkattenRepository, SuperkattenRepository>();
            services.AddTransient<IGastgezinnenRepository, GastgezinnenRepository>();
            services.AddTransient<IUserAuthorisationRepository, UserAuthorisationRepository>();
            services.AddTransient<IUserMapper, UserMapper>();

            return services;
        }
    }
}
