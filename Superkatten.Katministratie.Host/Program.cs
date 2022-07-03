using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Superkatten.Katministratie.Host;
using Superkatten.Katministratie.Host.Services;
using Superkatten.Katministratie.Host.Services.Authentication;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Add Transient services
builder.Services.AddTransient<ISuperkattenListService, SuperkattenListService>();
builder.Services.AddTransient<ISuperkatActionService, SuperkatActionService>();
builder.Services.AddTransient<IGastgezinService, GastgezinService>();
builder.Services.AddTransient(s =>
{
    // When localhost: https://localhost:7171
    // When azure: https://katministratie.azurewebsites.net/

    //return new HttpClient { BaseAddress = new System.Uri("https://localhost:7171/") };
    // ook naar environment var
    return new HttpClient { BaseAddress = new Uri("https://superkatten.azurewebsites.net/") };
});

// Add singleton services
builder.Services.AddSingleton<IHttpService, HttpService>();
builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();
builder.Services.AddSingleton<ILocalStorageService, LocalStorageService>();

builder.Services.AddLogging(configure => configure.SetMinimumLevel(LogLevel.Debug));

// Add the ANT design from https://antblazor.com/
builder.Services.AddAntDesign();

await builder.Build().RunAsync();

//
// Use configuration appsettings or other config file
// Schemes: https://docs.microsoft.com/en-us/dotnet/framework/wcf/feature-details/understanding-http-authentication
