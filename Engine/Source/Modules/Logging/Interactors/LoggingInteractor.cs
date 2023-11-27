using GEngine.Modules.Logging.UseCases;
using GEngine.Utils.Logging.Loggers;

namespace GEngine.Modules.Logging.Interactors
{
    public sealed class LoggingInteractor : ILoggingInteractor
    {
        readonly GetLoggerUseCase _getLoggerUseCase;

        public LoggingInteractor(GetLoggerUseCase getLoggerUseCase)
        {
            _getLoggerUseCase = getLoggerUseCase;
        }

        public ILogger GetLogger() => _getLoggerUseCase.Execute();
    }
}
