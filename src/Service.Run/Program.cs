using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Serilog;

using Service.Consumer;
using Service.Run.Jobs;

namespace Service.Run
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var isService = !(Debugger.IsAttached || args.Contains("--console"));

            var configuration = BuildConfiguration();

            await HostedService.RunHostedService<ProcessMyJob>(s => s.ConfigureService(configuration), isService);
        }

        private static IConfiguration BuildConfiguration() =>
            new ConfigurationBuilder()
                //.AddJsonFile("appsettings.json")
                .Build();
    }
}
