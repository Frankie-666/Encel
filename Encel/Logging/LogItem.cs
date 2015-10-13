using System;

namespace Encel.Logging
{
    public class LogItem
    {
        public LogLevel LogLevel { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public string Message { get; set; }
        public string LoggerName { get; set; }
        public Exception Exception { get; set; }
        
        public LogItem()
        {
            Message = string.Empty;
            LogLevel = LogLevel.Info;
            LoggerName = string.Empty;

            Timestamp = DateTimeOffset.Now;
        }
    }
}