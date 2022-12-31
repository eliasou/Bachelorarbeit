using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Vivavis.Softwareplatform.Metering;

namespace RawDataFilter.Handler
{
    public class ExecuteTranslationTaskHandler : ExecuteTranslationTaskHandlerV1
    {
        private readonly ILogger<ExecuteTranslationTaskHandler> logger;
        private readonly CreatedMeasurementValuePublisher _publisher;

        public ExecuteTranslationTaskHandler(ILogger<ExecuteTranslationTaskHandler> logger, CreatedMeasurementValuePublisher publisher)
        {
            this.logger = logger;
            _publisher = publisher;
        }

        public override Task<ExecuteTranslationTaskResultV1> HandleAsync(ExecuteTranslationTaskV1 command, ExecuteTranslationTaskResultV1 result)
        {
            logger.LogInformation("Handle command received.");

            // read command informationen
            logger.LogInformation($"Command payload: {command.TranslationTaskModel.DeviceIdentifier}");
            
            //Die Daten müssen interpretiert werden. TODO
            var filterResult = ResponseResultV1.Success; 
            
            // set result information
            result.ResultModel = new ResponseResultModelV1
            {
                ResponseResult = filterResult
            };

            if (filterResult == ResponseResultV1.Success)
            {
                _publisher.Publish(command.TranslationTaskModel.DeviceIdentifier);
            }
            
            return Task.Run(() => result);
        }
    }

}