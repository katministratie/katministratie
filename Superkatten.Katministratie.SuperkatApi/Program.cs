using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Superkatten.Katministratie.Application;
using Superkatten.Katministratie.Infrastructure;
using Superkatten.Katministratie.SuperkatApi.Authentication;
using System.Text;
using static Superkatten.Katministratie.SuperkatApi.Authentication.AuthenticationManager;

var builder = WebApplication.CreateBuilder(args);


// Add authentication
// zie: https://dotnetcorecentral.com/blog/authentication-handler-in-asp-net-core/#:~:text=In%20ASP.Net%20Core%2C%20the%20authentication%20middleware%20is%20added,the%20Startup%20class%20inside%20of%20the%20ConfigureServices%20method.
var tokenKey = builder.Configuration.GetValue<string>("TokenKey");
var key = Encoding.ASCII.GetBytes(tokenKey);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
builder.Services.AddSingleton<IJwtAuthenticationManager>(new JwtAuthenticationManager(tokenKey));

//JDK: also check
// https://docs.microsoft.com/en-us/aspnet/core/security/authorization/limitingidentitybyscheme?view=aspnetcore-6.0

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Superkatten.Katministratie.SuperkatApi",
        Version = "v1"
    });
});
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructure();

var app = builder.Build();

//TODO: Tijdelijk alles open zetten....
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
