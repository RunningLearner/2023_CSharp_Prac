using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace DiTimerService;

class Program
{
    static async Task Main()
    {
        using var host = new HostBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<TimerService>();
            })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
            })
            .Build();

        await host.RunAsync();
    }
}
