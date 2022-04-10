using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Superkatten.Katministratie.Host;
using Superkatten.Katministratie.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Add services
builder.Services.AddTransient<ISuperkattenListService, SuperkattenListService>();
builder.Services.AddTransient<ISuperkatActionService, SuperkatActionService>();

//const string uriName = "https://katministratie.azurewebsites.net/";
builder.Services.AddScoped<HttpClient>(s =>
{
    return new HttpClient { BaseAddress = new System.Uri("https://localhost:7171") };
});
// Add the ANT design from https://antblazor.com/
builder.Services.AddAntDesign();

await builder.Build().RunAsync();
