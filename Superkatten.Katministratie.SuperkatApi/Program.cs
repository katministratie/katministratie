using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Superkatten.Katministratie.Application;
using Superkatten.Katministratie.Application.Authenticate.Middleware;
using Superkatten.Katministratie.Infrastructure;
using Superkatten.Katministratie.Infrastructure.Persistence;

const string SWAGGER_DOC_VERSION = "v1";
var builder = WebApplication.CreateBuilder(args);

//----------------------------------------------------------------------------------------------------------
builder.Configuration.AddEnvironmentVariables();

{
    // Add services to the container.
    builder.Services.AddCors();
    builder.Services.AddControllers();
//    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc(SWAGGER_DOC_VERSION, new OpenApiInfo { Title = "Superkatten", Version = SWAGGER_DOC_VERSION });
    });
    builder.Services.AddApplicationServices(builder.Configuration);
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
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
//    .SetIsOriginAllowed(origin => true)
    .AllowCredentials()
    );

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));
}
app.UseSwagger();
app.UseSwaggerUI();

// global error handler
app.UseMiddleware<ErrorHandlerMiddleware>();

// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.MapControllers();

app.Run("https://localhost:4000");
