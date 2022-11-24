using Vivavis.Platform.Connector.Interfaces;
using Microsoft.Extensions.Hosting;
using Vivavis.Softwareplatform.Common;
using Microsoft.Extensions.Logging;


namespace RawDataFilter
{
    public class CreatedMeasurementValuePublisher : BackgroundService
    {
        private readonly IMessagingContext _context;
        private readonly ILogger<Worker> _logger;

        public CreatedMeasurementValuePublisher(ILogger<Worker> logger, IMessagingContext context)
        {
            _context = context;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        private async void Start()
        {
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _context.PublishEventAsync(new MeasurementValueCreatedV2(), cancellationToken);
            return Task.Run(Start, cancellationToken);
        }
        
        public override Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Stop application...");

            return Task.Run(Stop, stoppingToken);
        }
        
        public void Stop()
        {
        }
    }
}