using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Vivavis.Softwareplatform.Metering;

namespace RawDataFilter.Handler
{
    public class ExecuteTranslationTaskHandler : ExecuteTranslationTaskHandlerV1
    {
        private readonly ILogger<ExecuteTranslationTaskHandler> logger;

        public ExecuteTranslationTaskHandler(ILogger<ExecuteTranslationTaskHandler> logger)
        {
            this.logger = logger;
        }

        public override Task<ExecuteTranslationTaskResultV1> HandleAsync(ExecuteTranslationTaskV1 command, ExecuteTranslationTaskResultV1 result)
        {
            logger.LogInformation("Handle command received.");

            // read command informationen
            logger.LogInformation($"Command payload: {command.TranslationTaskModel.DeviceIdentifier}");

            // set result information
            result.ResultModel = new ResponseResultModelV1
            {
                ResponseResult = ResponseResultV1.Success
            };

            return Task.Run(() => result);
        }
    }

}