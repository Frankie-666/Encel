using System;

namespace Encel.Logging.Default
{
    public class NullLogger : ILogger
    {
        public bool IsTraceEnabled { get; }
        public bool IsDebugEnabled { get; }
        public bool IsInfoEnabled { get; }
        public bool IsWarnEnabled { get; }
        public bool IsErrorEnabled { get; }
        public bool IsFatalEnabled { get; }

        public void Trace(Func<string> message, Exception exception = null) { }
        public void Debug(Func<string> message, Exception exception = null) { }
        public void Info(Func<string> message, Exception exception = null) { }
        public void Warn(Func<string> message, Exception exception = null) { }
        public void Error(Func<string> message, Exception exception = null) { }
        public void Fatal(Func<string> message, Exception exception = null) { }
        public void Log(LogItem logItem) { }
    }
}