using GEngine.Utils.Logging.Enums;

namespace GEngine.Utils.Logging.Outputs
{
    public interface ILogOutput
    {
        void Output(LogType logType, string log);
    }
}
