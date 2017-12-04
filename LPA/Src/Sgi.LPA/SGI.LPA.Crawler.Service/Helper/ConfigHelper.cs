using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGI.LPA.Crawler.Service.Helper
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
