using Encel.Commands;
using Encel.Messaging;
using Shortcoder;

namespace Encel.ContentTransformers.Shortcodes
{
    public class ShortcodeStartupConsumer : IConsumer<AppStartupCommand>
    {
        public void Consume(AppStartupCommand command)
        {
            ShortcodeConfiguration.Provider.AddFromAssembly(typeof(ShortcodeStartupConsumer).Assembly);
        }
    }
}
