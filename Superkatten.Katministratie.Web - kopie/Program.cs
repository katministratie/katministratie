using Superkatten.Katministratie.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddTransient<ISuperkattenListService, SuperkattenListService>();
builder.Services.AddTransient<ISuperkatActionService, SuperkatActionService>();
builder.Services.AddScoped<HttpClient>(s => 
    { 
        return new HttpClient { BaseAddress = new System.Uri("https://katministratie.azurewebsites.net/") }; 
    });
builder.Services.AddAntDesign();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
