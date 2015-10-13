namespace Encel.Messaging
{
    public interface IChannel
    {
        void Send<TCommand>(TCommand message) where TCommand : ICommand;

        void RegisterConsumer<TCommand>(IConsumer<TCommand> consumer) where TCommand : ICommand;
    }
}