using GEngine.Utils.Logging.Loggables;

namespace GEngine.Utils.Logging.Builders
{
    public interface ILogBuilder
    {
        ILogBuilder AppendLine(string line);
        ILogBuilder Append(ILoggable loggable);
        ILogBuilder AppendWithIndentation(ILoggable loggable);
        ILogBuilder AppendWithNameAndIndentation(string name, ILoggable loggable);
        ILogBuilder IncreaseIndentation();
        ILogBuilder DecreaseIndentation();
    }
}
