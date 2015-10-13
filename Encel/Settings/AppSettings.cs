using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;

namespace Encel.Settings
{
    public class AppSettings : IAppSettings
    {
        private readonly NameValueCollection _appSettings;
        
        public AppSettings()
        {
            _appSettings = ConfigurationManager.AppSettings;
        }

        public string this[string key]
        {
            get
            {
                return _appSettings[key];
            }
        }
        
        protected bool GetAsBool(string key, bool defaultValue = false)
        {
            if (!HasKey(key))
            {
                return defaultValue;
            }

            bool value;
            bool.TryParse(this[key], out value);
            return value;
        }

        protected int GetAsInt(string key, int defaultValue = 0)
        {
            if (!HasKey(key))
            {
                return defaultValue;
            }

            int value;
            int.TryParse(this[key], out value);
            return value;
        }

        protected string[] GetAsStringArray(string key)
        {
            if (!HasKey(key))
            {
                return new string[] {};
            }

            return this[key].Split(new [] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }

        protected bool HasKey(string key)
        {
            return _appSettings.AllKeys.Contains(key);
        }
    }
}