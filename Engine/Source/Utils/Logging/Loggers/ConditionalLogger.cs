using System;
using GEngine.Utils.Logging.Enums;
using GEngine.Utils.Logging.Outputs;

namespace GEngine.Utils.Logging.Loggers
{
    public class ConditionalLogger : ILogger
    {
        readonly Func<bool> _condition;
        readonly ILogOutput _logOutput;

        public ConditionalLogger(Func<bool> condition, ILogOutput logOutput)
        {
            _condition = condition;
            _logOutput = logOutput;
        }

        public void Log(LogType logType, string log)
        {
            if (!_condition.Invoke())
            {
                return;
            }

            _logOutput.Output(logType, log);
        }

        public void Log<T1>(LogType logType, string log, T1 arg1)
        {
            if (!_condition.Invoke())
            {
                return;
            }

            _logOutput.Output(logType, string.Format(log, arg1));
        }

        public void Log<T1, T2>(LogType logType, string log, T1 arg1, T2 arg2)
        {
            if (!_condition.Invoke())
            {
                return;
            }

            _logOutput.Output(logType, string.Format(log, arg1, arg2));
        }

        public void Log<T1, T2, T3>(LogType logType, string log, T1 arg1, T2 arg2, T3 arg3)
        {
            if (!_condition.Invoke())
            {
                return;
            }

            _logOutput.Output(logType, string.Format(log, arg1, arg2, arg3));
        }

        public void Log<T1, T2, T3, T4>(LogType logType, string log, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (!_condition.Invoke())
            {
                return;
            }

            _logOutput.Output(logType, string.Format(log, arg1, arg2, arg3, arg4));
        }

        public void Log<T1, T2, T3, T4, T5>(LogType logType, string log, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            if (!_condition.Invoke())
            {
                return;
            }

            _logOutput.Output(logType, string.Format(log, arg1, arg2, arg3, arg4, arg5));
        }

        public void Log<T1, T2, T3, T4, T5, T6>(LogType logType, string log, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            if (!_condition.Invoke())
            {
                return;
            }

            _logOutput.Output(logType, string.Format(log, arg1, arg2, arg3, arg4, arg5, arg6));
        }

        public void Log<T1, T2, T3, T4, T5, T6, T7>(LogType logType, string log, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            if (!_condition.Invoke())
            {
                return;
            }

            _logOutput.Output(logType, string.Format(log, arg1, arg2, arg3, arg4, arg5, arg6, arg7));
        }

        public void Log<T1, T2, T3, T4, T5, T6, T7, T8>(LogType logType, string log, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            if (!_condition.Invoke())
            {
                return;
            }

            _logOutput.Output(logType, string.Format(log, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8));
        }

        public void Log<T1, T2, T3, T4, T5, T6, T7, T8, T9>(LogType logType, string log, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            if (!_condition.Invoke())
            {
                return;
            }

            _logOutput.Output(logType, string.Format(log, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9));
        }

        public void Log<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(LogType logType, string log, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            if (!_condition.Invoke())
            {
                return;
            }

            _logOutput.Output(logType, string.Format(log, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10));
        }

        public void Log<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(LogType logType, string log, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            if (!_condition.Invoke())
            {
                return;
            }

            _logOutput.Output(logType, string.Format(log, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11));
        }
    }
}
