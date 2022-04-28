using Blazored.Modal;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Superkatten.Katministratie.Host;
using Superkatten.Katministratie.Host.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Add the dialogbox
builder.Services.AddBlazoredModal();

//See: https://github.com/Append-IT/Blazor.Printing
//builder.Services.AddScoped<IPrintingService, PrintingService>();

// Add services
builder.Services.AddTransient<ISuperkattenListService, SuperkattenListService>();
builder.Services.AddTransient<ISuperkatActionService, SuperkatActionService>();
builder.Services.AddTransient<IGastgezinService, GastgezinService>();

builder.Services.AddScoped<IPrinterService, PrinterService>();

// When localhost: https://localhost:7171
// When azure: https://katministratie.azurewebsites.net/
// Use configuration appsettings or other config file
// Schemes: https://docs.microsoft.com/en-us/dotnet/framework/wcf/feature-details/understanding-http-authentication

builder.Services.AddScoped<HttpClient>(s =>
{
#if DEBUG
    return new HttpClient { BaseAddress = new System.Uri("https://localhost:7171") };
#else
    return new HttpClient { BaseAddress = new System.Uri("https://katministratie.azurewebsites.net/") };
#endif
    });

// Add the ANT design from https://antblazor.com/
builder.Services.AddAntDesign();

await builder.Build().RunAsync();
