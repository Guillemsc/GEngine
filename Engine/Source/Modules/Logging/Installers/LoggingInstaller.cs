using GEngine.Modules.Logging.Data;
using GEngine.Modules.Logging.Interactors;
using GEngine.Modules.Logging.UseCases;
using GEngine.Utils.Di.Builder;

namespace GEngine.Modules.Logging.Installers
{
    public static class LoggingInstaller
    {
        public static void InstallLogging(this IDiContainerBuilder builder)
        {
            builder.Bind<ILoggingInteractor>()
                .FromFunction(c => new LoggingInteractor(
                    c.Resolve<GetLoggerUseCase>()
                ));

            builder.Bind<LoggersData>().FromNew();

            builder.Bind<GetLoggerUseCase>()
                .FromFunction(c => new GetLoggerUseCase(
                    c.Resolve<LoggersData>()
                ));
        }
    }
}
