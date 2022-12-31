using Vivavis.Platform.Connector.Interfaces;
using Microsoft.Extensions.Hosting;
using Vivavis.Softwareplatform.Common;
using Microsoft.Extensions.Logging;


namespace RawDataFilter
{
    public class CreatedMeasurementValuePublisher
    {
        private readonly IMessagingContext _context;
        private readonly ILogger<Worker> _logger;

        public CreatedMeasurementValuePublisher(ILogger<Worker> logger, IMessagingContext context)
        {
            _context = context;
            _logger = logger;
        }
        
        public void Publish(string inputModel)
        {
            var swpModel = Map(inputModel);
            _context.PublishEventAsync(swpModel);
        }

        private MeasurementValueCreatedV2 Map(string inputModel)
        {
            return new MeasurementValueCreatedV2(Constants.ModuleKey)
            {
                DomainSpecificLogicalNames = new List<KeyValuePairV1>
                {
                    new KeyValuePairV1
                    {
                        Key = "Test",
                        Value = inputModel
                    }
                }
            };
        }
    }
}