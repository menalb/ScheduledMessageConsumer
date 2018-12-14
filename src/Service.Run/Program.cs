using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Serilog;

using Service.MessageConsumer;

namespace Service.Run
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var isService = !(Debugger.IsAttached || args.Contains("--console"));

            var configuration = new ConfigurationBuilder()
               // .AddJsonFile("appsettings.json")
               .Build();

            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration).CreateLogger();

            //await HostedService.RunHostedService<ProcessPromotionalCreditJob>(s => s.ConfigureService(configuration), isService);
        }
    }
}
