using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Superkatten.Katministratie.View.Services;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.View
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateWebHostBuilder(args).Build().RunAsync();
        }

        private static IHostBuilder CreateWebHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddHostedService<SuperkattenService>();
                })
//            .ConfigureAppConfiguration(config =>
//                {
//                    config.AddJsonFile("appsettings.Local.json", optional: true);
//                })
        }
    }
}
