namespace Encel.Messaging
{
    public interface IConsumer<in TCommand> where TCommand : ICommand
    {
        void Consume(TCommand command);
    }
}