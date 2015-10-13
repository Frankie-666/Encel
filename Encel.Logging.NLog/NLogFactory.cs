using NL = NLog;

namespace Encel.Logging.NLog
{
    public class NLogFactory : ILogFactory
    {
        public ILogger GetLogger(string loggerName = null)
        {
            if (loggerName != null)
            {
                return new NLogLogger(NL.LogManager.GetLogger(loggerName));
            }

            return new NLogLogger(NL.LogManager.GetCurrentClassLogger());
        }
    }
}
