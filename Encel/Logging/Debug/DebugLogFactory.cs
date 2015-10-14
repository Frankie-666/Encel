namespace Encel.Logging.Debug
{
    public class DebugLogFactory : ILogFactory
    {
        public ILogger GetLogger(string loggerName = null)
        {
            return new DebugLogger(loggerName);
        }
    }
}
