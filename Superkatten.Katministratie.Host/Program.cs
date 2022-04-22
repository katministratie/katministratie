using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Superkatten.Katministratie.Host;
using Superkatten.Katministratie.Host.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Add services
builder.Services.AddTransient<ISuperkattenListService, SuperkattenListService>();
builder.Services.AddTransient<ISuperkatActionService, SuperkatActionService>();
builder.Services.AddTransient<IGastgezinService, GastgezinService>();

// When localhost: https://localhost:7171
// When azure: https://katministratie.azurewebsites.net/
// Use configuration appsettings or other config file
// Schemes: https://docs.microsoft.com/en-us/dotnet/framework/wcf/feature-details/understanding-http-authentication


const string uriName = "https://katministratie.azurewebsites.net/";
//const string uriName = "https://localhost:7171";
builder.Services.AddScoped<HttpClient>(s =>
{
    return new HttpClient { BaseAddress = new System.Uri(uriName) };
});

// Add the ANT design from https://antblazor.com/
builder.Services.AddAntDesign();

await builder.Build().RunAsync();
