using Microsoft.Extensions.Hosting;
using Vivavis.Platform.Connector.Handling.Interfaces.Events;
using Vivavis.Platform.Connector.Interfaces.PubSub;
using Vivavis.Softwareplatform.Common;
using Vivavis.Softwareplatform.Messaging;

namespace RawDataFilterCommand;
public class CreatedMeasurementValueEventSubscriber : BackgroundService
{
    private readonly ISubscriber<EventV2> _subscriber;
    private readonly IEventReceiver<IEventV2> _receiver;

    public CreatedMeasurementValueEventSubscriber(ISubscriber<EventV2> subscriber, IEventReceiver<IEventV2> receiver)
    {
        _subscriber = subscriber;
        _receiver = receiver;
    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return _subscriber.SubscribeDurableAsync(new MeasurementValueCreatedV2().EventAddress, "EventSubscriber",
            _receiver.Receive, cancellationToken: stoppingToken);
    }
}
