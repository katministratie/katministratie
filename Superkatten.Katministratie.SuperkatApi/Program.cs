using Superkatten.Katministratie.Application;
using Superkatten.Katministratie.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

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
app.UseHttpsRedirection();
//app.UseAuthorization();
app.MapControllers();
app.Run();
