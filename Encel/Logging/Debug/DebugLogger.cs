namespace Encel.Logging.Debug
{
    public class DebugLogger : LoggerBase
    {
        public DebugLogger() { }
        public DebugLogger(string loggerName) : base(loggerName) { }

        public override void Log(LogItem logItem)
        {
            if (LoggerName != null) {  
                System.Diagnostics.Debug.WriteLine("{0} [{1}] [{2}] {3}", logItem.Timestamp, logItem.LogLevel, logItem.LoggerName, logItem.Message);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("{0} [{1}] {2}", logItem.Timestamp, logItem.LogLevel, logItem.Message);
            }

            if (logItem.Exception != null)
            {
                System.Diagnostics.Debug.WriteLine(logItem.Exception);
            }
        }
    }
}