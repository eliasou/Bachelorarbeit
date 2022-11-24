using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Vivavis.Platform.Connector.Handling.Interfaces.Commands;
using Vivavis.Platform.Connector.Interfaces;
using Vivavis.Platform.Connector.Interfaces.PubSub;
using Vivavis.Softwareplatform.Messaging;
using Vivavis.Softwareplatform.Platform;
using Vivavis.Softwareplatform.Platform.Registry;


namespace RawDataFilter
{
    public class Worker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<Worker> _logger;
        private readonly IMessagingContext _context;
        private readonly ISubscriber<CommandV2> _subscriber;
        private readonly ICommandReceiver<ICommandV2> _receiver;

        public Worker(IServiceProvider serviceProvider, ILogger<Worker> logger, IMessagingContext context, ISubscriber<CommandV2> subscriber, ICommandReceiver<ICommandV2> receiver)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _context = context;
            _subscriber = subscriber;
            _receiver = receiver;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.Run(Start, cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Stop application...");

            return Task.Run(Stop, stoppingToken);
        }

        private async void Start()
        {
            var module = new RegisterModuleV1(Guid.NewGuid(), "RawDataFilter")
            {
                Module = Module()
            };

            var result =
                await _context.InvokeCommandAsync<RegisterModuleResultV1>(module, "VirtualTopic.vivavis.commands");
        }

        public void Stop()
        {
            
        }

        private ModuleV1 Module()
        {
            var commands = _serviceProvider.GetServices<ICommandHandler<ICommandV2, IResultV2>>()
                .Select(x => new ModuleCommandV1
            {
                Noun = x.Noun,
                Verb = x.Verb,
                Version = x.Version.ToString(),
                RequestAddress = "rawDataFilter.notification"
            });

            return new ModuleV1
            {
                Commands = commands.ToList(),
                Key = "RawDataFilter"
            };
        }

        public override void Dispose()
        {
            _logger.LogInformation("Dispose");

            base.Dispose();
        }
    }
}
