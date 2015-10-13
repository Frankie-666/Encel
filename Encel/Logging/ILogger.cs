using System;

namespace Encel.Logging
{
    public interface ILogger
    {
        bool IsTraceEnabled { get; }
        bool IsDebugEnabled { get; }
        bool IsInfoEnabled { get; }
        bool IsWarnEnabled { get; }
        bool IsErrorEnabled { get; }
        bool IsFatalEnabled { get; }

        void Trace(Func<string> message, Exception exception = null);
        void Debug(Func<string> message, Exception exception = null);
        void Info(Func<string> message, Exception exception = null);
        void Warn(Func<string> message, Exception exception = null);
        void Error(Func<string> message, Exception exception = null);
        void Fatal(Func<string> message, Exception exception = null);
    }
}