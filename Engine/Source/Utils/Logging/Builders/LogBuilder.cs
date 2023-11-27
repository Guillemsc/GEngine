using System;
using System.Text;
using GEngine.Utils.Logging.Loggables;

namespace GEngine.Utils.Logging.Builders
{
    public sealed class LogBuilder : ILogBuilder
    {
        const int indentationMultiplier = 4;

        readonly StringBuilder _stringBuilder = new();

        int _indentation = 0;

        public ILogBuilder Append(ILoggable loggable)
        {
            loggable.Log(this);

            return this;
        }

        public ILogBuilder AppendWithIndentation(ILoggable loggable)
        {
            IncreaseIndentation();
            Append(loggable);
            DecreaseIndentation();

            return this;
        }

        public ILogBuilder AppendWithNameAndIndentation(string name, ILoggable loggable)
        {
            AppendLine(name);
            AppendWithIndentation(loggable);

            return this;
        }

        public ILogBuilder AppendLine(string line)
        {
            line = line.PadLeft(line.Length + _indentation);

            _stringBuilder.AppendLine(line);

            return this;
        }

        public ILogBuilder IncreaseIndentation()
        {
            _indentation += indentationMultiplier;

            return this;
        }

        public ILogBuilder DecreaseIndentation()
        {
            _indentation = Math.Max(0, _indentation - indentationMultiplier);

            return this;
        }

        public override string ToString()
        {
            return _stringBuilder.ToString();
        }

        public static string FromLoggable(ILoggable loggable)
        {
            LogBuilder builder = new();
            builder.Append(loggable);
            return builder.ToString();
        }
    }
}
