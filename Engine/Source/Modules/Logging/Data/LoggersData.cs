using GEngine.Utils.Logging.Loggers;
using GEngine.Utils.Logging.Outputs;

namespace GEngine.Modules.Logging.Data
{
    public sealed class LoggersData
    {
        public ILogger DefaultLogger = new ConditionalLogger(() => true, ConsoleLogOutput.Instance);
    }
}
