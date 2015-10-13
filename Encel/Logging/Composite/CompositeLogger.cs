using System.Collections.Generic;

namespace Encel.Logging.Composite
{
    public class CompositeLogger : LoggerBase
    {
        private readonly string _loggerName;
        private readonly List<ILogFactory> _logFactories;

        public CompositeLogger(string loggerName, List<ILogFactory> logFactories)
        {
            _loggerName = loggerName;
            _logFactories = logFactories;
        }

        public override void Log(LogItem logItem)
        {
            foreach (var logFactory in _logFactories)
            {
                var logger = logFactory.GetLogger(_loggerName);
                LogWithLogger(logItem, logger);
            }
        }

        private static void LogWithLogger(LogItem logItem, ILogger logger)
        {
            switch (logItem.LogLevel)
            {
                case LogLevel.Trace:
                    logger.Trace(() => logItem.Message, logItem.Exception);
                    break;
                case LogLevel.Info:
                    logger.Info(() => logItem.Message, logItem.Exception);
                    break;
                case LogLevel.Warn:
                    logger.Warn(() => logItem.Message, logItem.Exception);
                    break;
                case LogLevel.Error:
                    logger.Error(() => logItem.Message, logItem.Exception);
                    break;
                case LogLevel.Fatal:
                    logger.Fatal(() => logItem.Message, logItem.Exception);
                    break;
            }
        }
    }
}