using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Encel.Logging;

namespace Encel.Messaging
{
    public class Channel : IChannel
    {
        private readonly ICollection<object> _consumers = new Collection<object>();

        public void Send<TCommand>(TCommand message) where TCommand : ICommand
        {
            var logger = LogManager.GetLogger();

            logger.Trace(() => "Channel received command " + message.GetType().Name);

            foreach (var consumer in _consumers.OfType<IConsumer<TCommand>>())
            {
                consumer.Consume(message);

                logger.Trace(() => "Channel ran consumer " + consumer.GetType().Name);
            }
        }

        public void RegisterConsumer<TCommand>(IConsumer<TCommand> consumer) where TCommand : ICommand
        {
            if (!_consumers.Contains(consumer))
            {
                _consumers.Add(consumer);
            }
        }
    }
}