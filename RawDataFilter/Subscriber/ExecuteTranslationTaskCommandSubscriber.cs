using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Vivavis.Platform.Connector.Handling.Interfaces.Commands;
using Vivavis.Platform.Connector.Interfaces.PubSub;
using Vivavis.Softwareplatform.Messaging;

namespace RawDataFilter.Subscriber
{
    public class ExecuteTranslationTaskCommandSubscriber : BackgroundService
    {
        private readonly ISubscriber<CommandV2> _subscriber;
        private readonly ICommandReceiver<ICommandV2> _receiver;

        public ExecuteTranslationTaskCommandSubscriber(ISubscriber<CommandV2> subscriber, ICommandReceiver<ICommandV2> receiver)
        {
            _subscriber = subscriber;
            _receiver = receiver;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return _subscriber.SubscribeDurableAsync("rawDataFilter.notification", "rawDataFilter",
                _receiver.ReceiveCommand, cancellationToken: stoppingToken);
        }
    }
}