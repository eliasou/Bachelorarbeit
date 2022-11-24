using Vivavis.Softwareplatform.Common;

namespace RawDataFilterCommand;

public class CreatedMeasurementValueEventHandler : MeasurementValueCreatedHandlerV2
{
    public override Task HandleAsync(MeasurementValueCreatedV2 myEvent)
    {
        return Task.Run(() => Console.WriteLine(myEvent.Values.First().Value));
    }
}