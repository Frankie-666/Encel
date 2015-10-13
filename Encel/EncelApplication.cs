using System;
using Encel.Commands;
using Encel.Configuration;
using Encel.Logging;

namespace Encel
{
    public class EncelApplication
    {
        public static EncelConfiguration Configuration { get; private set; }

        public static void Startup()
        {
            var config = new EncelConfiguration().UseDefaultServicesAndFeatures();
            Startup(config);
        }

        public static void Startup(Action<EncelConfiguration> configuration)
        {
            var config = new EncelConfiguration();
            configuration(config);

            Startup(config);
        }

        public static void Startup(EncelConfiguration configuration)
        {
            LogManager.GetLogger().Trace(() => "Encel application is starting up...");

            Configuration = configuration;

            Configuration.Channel.Send(new AppStartupCommand());

            LogManager.GetLogger().Trace(() => "Encel application has started.");
        }
    }
}
