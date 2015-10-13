using System;
using NLog;

namespace Encel.Logging.NLog
{
    public class NLogLogger : ILogger
    {
        private readonly Logger _inner;

        public NLogLogger(Logger inner)
        {
            _inner = inner;
        }

        public bool IsTraceEnabled { get { return _inner.IsTraceEnabled; } }
        public bool IsDebugEnabled { get { return _inner.IsDebugEnabled; } }
        public bool IsInfoEnabled { get { return _inner.IsInfoEnabled; } }
        public bool IsWarnEnabled { get { return _inner.IsWarnEnabled; } }
        public bool IsErrorEnabled { get { return _inner.IsErrorEnabled; } }
        public bool IsFatalEnabled { get { return _inner.IsFatalEnabled; } }

        public void Trace(Func<string> message, Exception exception = null)
        {
            if (_inner.IsTraceEnabled)
            {
                _inner.Trace(exception, message());
            }
        }
        public void Debug(Func<string> message, Exception exception = null)
        {
            if (_inner.IsDebugEnabled)
            {
                _inner.Debug(exception, message());
            }
        }
        
        public void Info(Func<string> message, Exception exception = null)
        {
            if (_inner.IsInfoEnabled)
            {
                _inner.Info(exception, message());
            }
        }
        

        public void Warn(Func<string> message, Exception exception = null)
        {
            if (_inner.IsWarnEnabled)
            {
                _inner.Warn(exception, message());
            }
        }

        public void Error(Func<string> message, Exception exception = null)
        {
            if (_inner.IsErrorEnabled)
            {
                _inner.Error(exception, message());
            }
        }

        public void Fatal(Func<string> message, Exception exception = null)
        {
            if (_inner.IsFatalEnabled)
            {
                _inner.Fatal(exception, message());
            }
        }
    }
}
