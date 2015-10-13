using System.Web;
using Encel.TemplateSite.App_Start;
using NLog;
using NLog.Config;
using NLog.Targets;

[assembly: PreApplicationStartMethod(typeof(NLogConfig), "Startup")]

namespace Encel.TemplateSite.App_Start
{
    public class NLogConfig
    {
        public static void Startup()
        {
            var fileTarget = new FileTarget
            {
                FileName = "${basedir}/file.txt",
                Layout = "${message}"
            };

            var loggingConfig = new LoggingConfiguration();
            loggingConfig.AddTarget("file", fileTarget);

            var rule = new LoggingRule("*", LogLevel.Trace, fileTarget);
            loggingConfig.LoggingRules.Add(rule);

            //LogManager.Configuration = loggingConfig;
        }
    }
}