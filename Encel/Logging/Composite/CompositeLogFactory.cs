using System.Collections.Generic;
using System.Linq;

namespace Encel.Logging.Composite
{
    public class CompositeLogFactory : ILogFactory
    {
        public CompositeLogFactory(params ILogFactory[] logFactories)
        {
            LogFactories = logFactories.ToList();
        }

        public List<ILogFactory> LogFactories { get; set; }

        public ILogger GetLogger(string loggerName = null)
        {
            return new CompositeLogger(loggerName, LogFactories);
        }
    }
}
