using GEngine.Utils.Logging.Enums;

namespace GEngine.Utils.Logging.Outputs
{
    public sealed class NopLogOutput : ILogOutput
    {
        public static readonly NopLogOutput Instance = new();

        NopLogOutput() { }

        public void Output(LogType logType, string log) { }
    }
}
