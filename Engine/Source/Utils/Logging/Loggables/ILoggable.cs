using GEngine.Utils.Logging.Builders;

namespace GEngine.Utils.Logging.Loggables
{
    public interface ILoggable
    {
        void Log(ILogBuilder logBuilder);
    }
}
