using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Angular2Mvc5Application3.Helpers
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