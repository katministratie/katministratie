using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Superkatten.Katministratie.Application;
using Superkatten.Katministratie.Infrastructure;
using Superkatten.Katministratie.Infrastructure.Persistance;

//namespace Superkatten.Katministratie.HttpApi;


//public class Program
//{
    const string SWAGGER_DOC_VERSION = "v1";

//    public static async void Main(string[] args)
//    {
        var builder = WebApplication.CreateBuilder(args);      

        // Add services to the container.
        builder.Services.AddApplicationServices(builder.Configuration);
        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(SWAGGER_DOC_VERSION, new OpenApiInfo
            {
                Title = "Superkatten",
                Version = SWAGGER_DOC_VERSION
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorisation header using the bearer scheme. \r\n\r\n"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        //TODO: Tijdelijk uitgezet
        /*builder.Services.AddAuthentication(options => {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options => {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidAudience = null,
                    ValidIssuer = null,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(UserAuthorisationConfiguration.Secret))
                };
            });*/


        var app = builder.Build();

        // Make sure the database is up-to-date
        using (var scope = app.Services.CreateScope())
        {
            var dataContext = scope.ServiceProvider.GetRequiredService<KatministratieContext>();
            await dataContext.Database.MigrateAsync();
        }

        // Configure the HTTP request pipeline.
        app.UseSwagger();
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Superkatten Api v1"));
        }
        else
        {
            app.UseSwaggerUI();
            app.UseExceptionHandler("/Error");
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        
        // TODO: tijdelijk uitschakelen middleware laag
        //app.UseMiddleware<ErrorHandlerMiddleware>();
        //app.UseMiddleware<JwtMiddleware>();
        //app.UseAuthentication();
        //app.UseAuthorization();
        app.MapControllers();
        app.Run();
 //   }
//}