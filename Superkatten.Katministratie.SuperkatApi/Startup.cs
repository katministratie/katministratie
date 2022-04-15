using Microsoft.OpenApi.Models;
using Superkatten.Katministratie.Application;
using Superkatten.Katministratie.Application.Extentions;
using Superkatten.Katministratie.Application.Services.Authentication;
using Superkatten.Katministratie.Infrastructure;
using System.Text.Json.Serialization;

namespace Superkatten.Katministratie.SuperkatApi
{
    public partial class Startup
    {
        const string SWAGGER_DOC_VERSION = "v1";
        const string SECURITY_DEFINITION_NAME = "ApiKeyAuth";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                });
            services.AddEndpointsApiExplorer();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = ApiKeyOptions.DEFAULT_SCHEME;
                options.DefaultChallengeScheme = ApiKeyOptions.DEFAULT_SCHEME;
            }).AddApiKeyAuthentication<ApiKeyValidator>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Superkatten", Version = SWAGGER_DOC_VERSION });
                options.AddSecurityDefinition(SECURITY_DEFINITION_NAME, new()
                {
                    In = ParameterLocation.Header,
                    Name = ApiKeyOptions.DEFAULT_HEADER,
                    Type = SecuritySchemeType.ApiKey
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = SECURITY_DEFINITION_NAME }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            services.AddAuthorization();
            services.AddInfrastructure();
            services.AddApplicationServices(Configuration);
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));
            }
            app.UseSwagger();
            //app.UseSwaggerUI();
            app.UseRouting();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
