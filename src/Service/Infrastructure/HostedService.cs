using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using System;
using System.Threading.Tasks;

using Service.Infrastructure;

namespace ScheduledMessageConsumer.Service
{
    public class HostedService
    {
        public static async Task RunHostedService<TJobService>(Action<IServiceCollection> registerServices, bool isDebugModeOn = false) where TJobService : IJob
        {
            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    // services.AddHostedService<MessagesProcessorService<TJobService>>();

                    registerServices(services);
                });

            if (isDebugModeOn)
            {
                await builder.RunAsServiceAsync();
            }
            else
            {
                await builder.RunConsoleAsync();
            }
        }
    }
}