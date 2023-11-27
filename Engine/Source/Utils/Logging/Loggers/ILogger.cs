using GEngine.Utils.Logging.Enums;

namespace GEngine.Utils.Logging.Loggers
{
    /// <summary>
    /// High performance logger.
    /// The advantage of using generics is that we can skip the string formatting on release builds.
    /// More arguments can be added if needed :)
    /// </summary>
    public interface ILogger
    {
        void Log(LogType logType, string log);
        void Log<T1>(LogType logType, string log, T1 arg1);
        void Log<T1, T2>(LogType logType, string log, T1 arg1, T2 arg2);
        void Log<T1, T2, T3>(LogType logType, string log, T1 arg1, T2 arg2, T3 arg3);
        void Log<T1, T2, T3, T4>(LogType logType, string log, T1 arg1, T2 arg2, T3 arg3, T4 arg4);
        void Log<T1, T2, T3, T4, T5>(LogType logType, string log, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
        void Log<T1, T2, T3, T4, T5, T6>(LogType logType, string log, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);
        void Log<T1, T2, T3, T4, T5, T6, T7>(LogType logType, string log, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7);
        void Log<T1, T2, T3, T4, T5, T6, T7, T8>(LogType logType, string log, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8);
        void Log<T1, T2, T3, T4, T5, T6, T7, T8, T9>(LogType logType, string log, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9);
        void Log<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(LogType logType, string log, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10);
        void Log<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(LogType logType, string log, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11);
    }
}
