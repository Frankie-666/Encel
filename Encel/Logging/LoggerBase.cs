using System;

namespace Encel.Logging
{
    public abstract class LoggerBase : ILogger
    {
        public string LoggerName { get; }

        public virtual bool IsTraceEnabled { get { return true; } }
        public virtual bool IsDebugEnabled { get { return true; } }
        public virtual bool IsInfoEnabled { get { return true; } }
        public virtual bool IsWarnEnabled { get { return true; } }
        public virtual bool IsErrorEnabled { get { return true; } }
        public virtual bool IsFatalEnabled { get { return true; } }

        protected LoggerBase(string loggerName)
        {
            LoggerName = loggerName;
        }

        protected LoggerBase() { }

        public virtual void Trace(Func<string> message, Exception exception = null)
        {
            if (IsTraceEnabled)
            {
                Log(LogLevel.Trace, message(), exception);
            }
        }
        public virtual void Debug(Func<string> message, Exception exception = null)
        {
            if (IsDebugEnabled)
            {
                Log(LogLevel.Debug, message(), exception);
            }
        }

        public virtual void Info(Func<string> message, Exception exception = null)
        {
            if (IsInfoEnabled)
            {
                Log(LogLevel.Info, message(), exception);
            }
        }

        public void Warn(Func<string> message, Exception exception = null)
        {
            if (IsWarnEnabled)
            {
                Log(LogLevel.Warn, message(), exception);
            }
        }

        public virtual void Error(Func<string> message, Exception exception = null)
        {
            if (IsErrorEnabled)
            {
                Log(LogLevel.Error, message(), exception);
            }
        }

        public virtual void Fatal(Func<string> message, Exception exception = null)
        {
            if (IsFatalEnabled)
            {
                Log(LogLevel.Fatal, message(), exception);
            }
        }

        public virtual void Log(LogLevel logLevel, string message, Exception exception = null)
        {
            Log(new LogItem
            {
                Message = message,
                Exception = exception,
                LogLevel = logLevel,
                LoggerName = LoggerName
            });
        }

        public abstract void Log(LogItem logItem);
    }
}