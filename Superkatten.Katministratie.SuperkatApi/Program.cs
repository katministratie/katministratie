using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Superkatten.Katministratie.Application;
using Superkatten.Katministratie.Application.Authenticate.Middleware;
using Superkatten.Katministratie.Application.Configuration;
using Superkatten.Katministratie.Domain.Entities;
using Superkatten.Katministratie.Infrastructure;
using Superkatten.Katministratie.Infrastructure.Persistence;
using System.Text;

const string SWAGGER_DOC_VERSION = "v1";
var builder = WebApplication.CreateBuilder(args);

//----------------------------------------------------------------------------------------------------------
builder.Configuration.AddEnvironmentVariables();

{
    // Add services to the container.
    builder.Services.AddCors();
    builder.Services.AddControllers();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc(SWAGGER_DOC_VERSION, new OpenApiInfo { Title = "Superkatten", Version = SWAGGER_DOC_VERSION });
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
                new string[] {}
            }
        });
    });
    builder.Services.AddApplicationServices(builder.Configuration);
    builder.Services.AddInfrastructure(builder.Configuration);

    builder.Services.AddAuthentication(options => {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
        // Adding Jwt Bearer
        .AddJwtBearer(options => {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidAudience = null,
                ValidIssuer = null,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(UserAuthorisationConfiguration.Secret))
            };
        });

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy(SuperkattenPolicies.POLICY_ADMINISTRATOR, policy =>
        {
            var allPermissionValues = (PermissionEnum[])Enum.GetValues(typeof(PermissionEnum));
            var array = allPermissionValues
                .Select(value => value.ToString())
                .ToArray();
            foreach (var item in array)
            {
                policy.RequireRole(item);
            }
        });

        options.AddPolicy(SuperkattenPolicies.POLICY_VIEW_ONLY, policy =>
            policy.RequireRole(
                PermissionEnum.ViewOnly.ToString()
            ));

        options.AddPolicy(SuperkattenPolicies.POLICY_GASTGEZIN, policy =>
            policy.RequireRole(
                PermissionEnum.CreateEditDeleteGastgezin.ToString(),
                PermissionEnum.AssignSuperkatten.ToString()
            ));
    });
}

//----------------------------------------------------------------------------------------------------------
var app = builder.Build();


//----------------------------------------------------------------------------------------------------------
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<SuperkattenDbContext>();
    dataContext.Database.EnsureCreated();
}

app.UseCors(cors => cors
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials()
    );

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));
}
app.UseSwagger();
app.UseSwaggerUI();

// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();

// global error handler
app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
