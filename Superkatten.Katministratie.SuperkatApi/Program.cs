using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
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
//const string CORS_POLICY_NAME = "DefaultCorsPolicy";

var builder = WebApplication.CreateBuilder(args);

//----------------------------------------------------------------------------------------------------------
builder.Configuration.AddEnvironmentVariables();

{
    // Add services to the container.
    builder.Services.AddControllers();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc(SWAGGER_DOC_VERSION, new OpenApiInfo { Title = "Superkatten", Version = SWAGGER_DOC_VERSION });
    });
    builder.Services.AddApplicationServices(builder.Configuration);
    builder.Services.AddInfrastructure(builder.Configuration);
    /*builder.Services.AddCors(options =>
    {
        options.AddPolicy(
            name: CORS_POLICY_NAME, 
            builder => 
            {
                builder.WithOrigins(
                    "https://localhost:7292"
                );
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });
    });*/

    builder.Services.AddLogging();
}

//----------------------------------------------------------------------------------------------------------
var app = builder.Build();

//----------------------------------------------------------------------------------------------------------
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<SuperkattenDbContext>();
    var isCreated = dataContext.Database.EnsureCreated();
    if (!isCreated)
    {
        await dataContext.Database.MigrateAsync();
    }
}

app.UseSwagger();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Superkatten Api v1"));
}
else
{
    app.UseSwaggerUI();
    app.UseExceptionHandler("/Error");
}

// For routing and sequence
// see: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-6.0#middleware-order
//

//app.UseHttpsRedirection();
app.UseRouting();
app.UseMiddleware<ErrorHandlerMiddleware>();
//app.UseCors(CORS_POLICY_NAME);
//app.UseAuthentication();
//app.UseAuthorization();
app.MapControllers();

app.Run();
