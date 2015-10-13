using Microsoft.SqlServer.Server;

namespace Encel.Logging
{
    public interface ILogFactory
    {
        ILogger GetLogger(string loggerName = null);
    }
}