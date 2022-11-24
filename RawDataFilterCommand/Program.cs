using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Vivavis.Platform.Connector.Connection;
using Vivavis.Platform.Connector.Handling;
using Vivavis.Platform.Connector.Handling.Interfaces.Events;
using Vivavis.Softwareplatform.Messaging;

namespace RawDataFilterCommand
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddOptions<AmqpConnectionOptions>()
                        .Bind(hostContext.Configuration.GetSection(nameof(AmqpConnectionOptions)));
                    
                    services.AddLogging((logging) =>
                    {
                        logging.AddConsole();
                        logging.AddDebug();
                        logging.SetMinimumLevel(LogLevel.Trace);
                    });

                    //Vivavis connector
                    StartUpService.AddVivavisConnectorAndHandler(services);

                    services.AddTransient<IEventHandler<IEventV2>, CreatedMeasurementValueEventHandler>();
                    
                    services.AddHostedService<CreatedMeasurementValueEventSubscriber>();
                    services.AddHostedService<CmdBackgroundService>();
                })
                .ConfigureAppConfiguration((hostContext, configBuilder) =>
                {
                    configBuilder.AddCommandLine(args);
                    configBuilder.SetBasePath(Directory.GetCurrentDirectory());
                    configBuilder.AddJsonFile($"appsettings.json", optional: false);
                    configBuilder.AddEnvironmentVariables();
                })
                .ConfigureLogging((hostContext, logging) =>
                {
                    logging.AddConfiguration(hostContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                    logging.AddDebug();
                    logging.SetMinimumLevel(LogLevel.Trace);
                });

            Console.WriteLine("DONE !!!!!");

            return builder;
        }
    }
}