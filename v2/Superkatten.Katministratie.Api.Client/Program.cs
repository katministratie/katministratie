using Superkatten.Katministratie.Api.Client.Mappers;
using Superkatten.Katministratie.Api.Client.Services;

namespace Superkatten.Katministratie.Api.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            var urlApi = "https://localhost:5000";
            builder.Services.AddTransient(s =>
            {
                return new HttpClient
                {
                    BaseAddress = new Uri(urlApi)
                };
            });

            builder.Services.AddTransient<IHttpService, HttpService>();
            builder.Services.AddTransient<ISuperkatMapper, SuperkatMapper>();
            builder.Services.AddTransient<ISuperkattenService, SuperkattenService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}