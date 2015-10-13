using System.IO;
using System.Runtime.CompilerServices;
using Encel.Logging.Default;

namespace Encel.Logging
{
    public static class LogManager
    {
        private static ILogFactory _logFactory;

        static LogManager()
        {
            _logFactory = new NullLogFactory();
        }

        public static void SetFactory(ILogFactory logFactory)
        {
            _logFactory = logFactory;
        }

        public static ILogger GetLogger(string loggerName = null, [CallerFilePath]string callerFilePath = null)
        {
            if (loggerName == null)
                loggerName = Path.GetFileNameWithoutExtension(callerFilePath);

            return _logFactory.GetLogger(loggerName);
        }
    }
}
