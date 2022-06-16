using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Superkatten.Katministratie.Host;
using Superkatten.Katministratie.Host.Mappers;
using Superkatten.Katministratie.Host.Services;
using Superkatten.Katministratie.Host.Services.Authentication;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Add Transient services
builder.Services.AddTransient<ISuperkattenListService, SuperkattenListService>();
builder.Services.AddTransient<ISuperkatActionService, SuperkatActionService>();
builder.Services.AddTransient<IGastgezinService, GastgezinService>();
builder.Services.AddTransient<IGastgezinMapper, GastgezinMapper>();
builder.Services.AddTransient<ISuperkatMapper, SuperkatMapper>();
builder.Services.AddTransient<HttpClient>(s =>
{
    // When localhost: https://localhost:4000
    // When azure: https://katministratie.azurewebsites.net/

    //return new HttpClient { BaseAddress = new System.Uri("https://localhost:7171/") };
    return new HttpClient { BaseAddress = new System.Uri("https://katministratie.azurewebsites.net/") };
});


// Add Scoped services
builder.Services.AddSingleton<IHttpService, HttpService>();
builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();
builder.Services.AddSingleton<ILocalStorageService, LocalStorageService>();

// Add the ANT design from https://antblazor.com/
builder.Services.AddAntDesign();

await builder.Build().RunAsync();


//
// Use configuration appsettings or other config file
// Schemes: https://docs.microsoft.com/en-us/dotnet/framework/wcf/feature-details/understanding-http-authentication
