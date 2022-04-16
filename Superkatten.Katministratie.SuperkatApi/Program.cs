using Microsoft.OpenApi.Models;
using Superkatten.Katministratie.Application;
using Superkatten.Katministratie.Application.Extentions;
using Superkatten.Katministratie.Application.Services.Authentication;
using Superkatten.Katministratie.Infrastructure;

const string SWAGGER_DOC_VERSION = "v1";
const string SECURITY_DEFINITION_NAME = "ApiKeyAuth";

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = ApiKeyOptions.DEFAULT_SCHEME;
    options.DefaultChallengeScheme = ApiKeyOptions.DEFAULT_SCHEME;
}).AddApiKeyAuthentication<ApiKeyValidator>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
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
builder.Services.AddAuthorization();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructure();

var app = builder.Build();

app.UseCors(cors => cors
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials()
    );
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ch20Ex01 v1"));
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.UseHttpsRedirection();
app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();
app.Run();
