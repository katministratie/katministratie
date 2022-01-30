using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Superkatten.Katministratie.Web.Services;
using Superkatten.Katministratie.WebApp;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient<ISuperkattenListService, SuperkattenListService>();
builder.Services.AddScoped<HttpClient>(s => { return new HttpClient { BaseAddress = new System.Uri("https://localhost:7171/") }; });
builder.Services.AddAntDesign();

var app = builder.Build();

await app.RunAsync();