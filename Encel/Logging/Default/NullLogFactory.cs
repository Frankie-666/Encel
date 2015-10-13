namespace Encel.Logging.Default
{
    public class NullLogFactory : ILogFactory
    {
        public ILogger GetLogger(string loggerName = null)
        {
            return new NullLogger();
        }
    }
}