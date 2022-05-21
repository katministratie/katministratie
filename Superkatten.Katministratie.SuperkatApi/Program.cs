using Microsoft.OpenApi.Models;
using Superkatten.Katministratie.Application;
using Superkatten.Katministratie.Infrastructure;
using Superkatten.Katministratie.Infrastructure.Persistence;

const string SWAGGER_DOC_VERSION = "v1";
var builder = WebApplication.CreateBuilder(args);

builder
    .Configuration
    .AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(SWAGGER_DOC_VERSION, new OpenApiInfo { Title = "Superkatten", Version = SWAGGER_DOC_VERSION });
});

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<SuperkattenDbContext>();
    var result = context.Database.EnsureCreated();
    Console.WriteLine($"Database created: {result}");
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
app.UseRouting();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
