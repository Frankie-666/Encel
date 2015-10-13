using System;
using System.Web;
using Encel.Configuration;
using Encel.Logging.Composite;
using Encel.Logging.Debug;
using Encel.Logging.NLog;
using Encel.Settings;
using Encel.TemplateSite;

[assembly: PreApplicationStartMethod(typeof(EncelConfig), "Startup")]

namespace Encel.TemplateSite
{
    public class EncelConfig
    {
        public static void Startup()
        {
            EncelApplication.Startup(Configure);
        }

        private static void Configure(EncelConfiguration config)
        {
            config.UseLogger(new CompositeLogFactory(new NLogFactory(), new DebugLogFactory()));
            config.UseDefaultServicesAndFeatures();

            config.UseSettings(new EncelAppSettings
            {
                //RootPath = "blog"
            });
        }
    }
}