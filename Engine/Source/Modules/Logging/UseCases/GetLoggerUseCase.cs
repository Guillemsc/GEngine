using GEngine.Modules.Logging.Data;
using GEngine.Utils.Logging.Loggers;

namespace GEngine.Modules.Logging.UseCases
{
    public sealed class GetLoggerUseCase
    {
        readonly LoggersData _loggersData;

        public GetLoggerUseCase(LoggersData loggersData)
        {
            _loggersData = loggersData;
        }

        public ILogger Execute() => _loggersData.DefaultLogger;
    }
}
