using System.Reflection;
using Encel.ContentTransformers.Markdown;
using Encel.ContentTransformers.Shortcodes;
using Encel.Logging;
using Encel.Mvc;
using Encel.Settings;
using Shortcoder;

namespace Encel.Configuration
{
    public static class EncelConfigurationExtensions
    {
        public static EncelConfiguration UseDefaultServices(this EncelConfiguration config)
        {
            return config.InitializeDefaults();
        }

        public static EncelConfiguration UseDefaultServicesAndFeatures(this EncelConfiguration config)
        {
            return config.UseDefaultServices()
                    .UseMvc()
                    .UseShortcodes(registerAssemblies: Assembly.GetCallingAssembly())
                    .UseMarkdown();
        }

        public static EncelConfiguration UseMvc(this EncelConfiguration config)
        {
            config.Channel.RegisterConsumer(new MvcStartupConsumer());

            return config;
        }

        public static EncelConfiguration UseShortcodes(this EncelConfiguration config, params Assembly[] registerAssemblies)
        {
            config.Channel.RegisterConsumer(new ShortcodeStartupConsumer());
            config.ContentTransformers.Add(new ShortcodeContentTransformer());

            if (registerAssemblies != null)
            {
                foreach (var assembly in registerAssemblies)
                {
                    ShortcodeConfiguration.Provider.AddFromAssembly(assembly);
                }
            }

            return config;
        }

        public static EncelConfiguration UseMarkdown(this EncelConfiguration config)
        {
            config.ContentTransformers.Add(new MarkdownContentTransformer());

            return config;
        }

        public static EncelConfiguration UseSettings(this EncelConfiguration config, EncelAppSettings settings)
        {
            config.AppSettings = settings;

            return config;
        }

        public static EncelConfiguration UseLogger(this EncelConfiguration config, ILogFactory logFactory)
        {
            LogManager.SetFactory(logFactory);

            return config;
        }
    }
}