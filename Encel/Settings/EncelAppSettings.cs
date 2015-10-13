
namespace Encel.Settings
{
    public class EncelAppSettings : AppSettings, IEncelAppSettings
    {
        private const string KEY_PREFIX = "encel:";

        public string FileExtension { get; set; }
        public bool EnableContentCaching { get; set; } // TODO rename to DisableContentCaching?
        public string RootPath { get; set; }
        
        public EncelAppSettings(bool initializeValues = true)
        {
            FileExtension = "md";

            if (initializeValues)
            {
                FileExtension = this[KEY_PREFIX + "FileExtension"];
                EnableContentCaching = GetAsBool(KEY_PREFIX + "EnableContentCaching");
                RootPath = this[KEY_PREFIX + "RootPath"];
            }
        }
    }
}