using System;
using System.Web;
using Encel.Configuration;
using $rootnamespace$;

[assembly: PreApplicationStartMethod(typeof(EncelConfig), "Startup")]

namespace $rootnamespace$
{
    public class EncelConfig
    {
        public static void Startup()
        {
            EncelApplication.Startup(Configure);
        }

        private static void Configure(EncelConfiguration config)
        {
            config.UseDefaultServicesAndFeatures();
        }
    }
}