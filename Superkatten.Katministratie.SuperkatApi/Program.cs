using Superkatten.Katministratie.SuperkatApi;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration(config =>
        {
            config.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true);
        })
        .UseDefaultServiceProvider(options => options.ValidateOnBuild = true)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });
}
//var builder = WebApplication.CreateBuilder(args);
//var app = builder.Build();
//app.Run();








