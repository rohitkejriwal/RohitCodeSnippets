using System.Configuration;

namespace Sgi.LPA.Common.Helper
{
    public class ConfigHelper
    {
        public static string GetAppSettingValueFromConfig(string key)
        {
            string value = ConfigurationManager.AppSettings[key];

            if (!string.IsNullOrEmpty(value))
                return value;

            return string.Empty;
        }
    }
}
