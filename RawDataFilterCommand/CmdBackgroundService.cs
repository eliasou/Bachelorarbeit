using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Vivavis.Platform.Connector.Interfaces;
using Vivavis.Softwareplatform.Common;
using Vivavis.Softwareplatform.Metering;

namespace RawDataFilterCommand
{
    public class CmdBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<CmdBackgroundService> _logger;
        private readonly IMessagingContext _context;
        private bool _quit = false;

        public CmdBackgroundService(IServiceProvider serviceProvider, ILogger<CmdBackgroundService> logger, IMessagingContext context)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _context = context;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(() => { Execute(); });
        }

        private void Execute()
        {
            while (!_quit)
            {
                MenuItems();
            }
        }

        public void MenuItems()
        {
            Thread.Sleep(4000);
            Console.Clear();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("s) Send Command");
            Console.WriteLine("e) Exit");
            Console.Write("\r\nSelect an option: ");

            var key = Console.ReadKey();

            try
            {
                switch (key.Key)
                {
                    case ConsoleKey.S:
                        SendCommand();
                        break;
                    case ConsoleKey.E:
                        Exit();
                        break;
                    default:
                        Console.WriteLine("\r\nIncorrect input!");
                        MenuItems();
                        break;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            
        }

        private void SendCommand()
        {
            var command = new ExecuteTranslationTaskV1
            {
                TranslationTaskModel = new TranslationTaskModelV1
                {
                    DeviceIdentifier = "Device1",
                    Data = new List<byte>
                    {
                        0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20
                    },
                    CorrelationId = new Guid(),
                    Arguments = new List<KeyValuePairV1>
                    {
                        new ()
                        {
                            Key = "Format", 
                            Value = "HEX"
                        },
                        new ()
                        {
                            Key = "Rawdatatype",
                            Value = "MBus"
                        }
                    }
                },
                ModuleKey = "CommandSender",
                CallbackAddress = "CommandSender.callback",
                CorrelationId = Guid.NewGuid()
            };

            var result = _context.InvokeCommand<ExecuteTranslationTaskResultV1>(command);
            
            _logger.LogDebug(result?.ResultModel.ResponseResult.ToString());
        }
        private void Exit()
        {
            _logger.LogInformation("Quit");

            _quit = true;
            
            Environment.Exit(0);
        }
    }
}
