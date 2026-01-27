using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System;
using System.Threading.Tasks;

namespace CustomerInvoiceApp.DbMigrator;

class Program
{
    static async Task Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Volo.Abp", LogEventLevel.Warning)
#if DEBUG
                .MinimumLevel.Override("CustomerInvoiceApp", LogEventLevel.Debug)
#else
                .MinimumLevel.Override("CustomerInvoiceApp", LogEventLevel.Information)
#endif
                .Enrich.FromLogContext()
            .WriteTo.Async(c => c.File("Logs/logs.txt"))
            .WriteTo.Async(c => c.Console())
            .CreateLogger();

        await CreateHostBuilder(args).RunConsoleAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
		    .UseContentRoot(AppContext.BaseDirectory)
			.AddAppSettingsSecretsJson()
            .ConfigureLogging((context, logging) => logging.ClearProviders())
            .ConfigureServices((hostContext, services) =>
            {
				var config = hostContext.Configuration;
				foreach (var kv in config.GetSection("ConnectionStrings").GetChildren())
				{
					Console.WriteLine($"[HostBuilder] ConnectionString: {kv.Key} = {kv.Value}");
				}

				services.AddSingleton(hostContext.Configuration);
				services.AddHostedService<DbMigratorHostedService>();
            });
}
