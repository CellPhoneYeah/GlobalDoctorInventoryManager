using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;
using System.Collections;

namespace CommonUtility
{
    public class ConfigHelper
    {
        /// <summary>
        /// all settings
        /// </summary>
        public static NameValueCollection allSettings { get; set; }

        /// <summary>
        /// all connection strings
        /// </summary>
        public static ConnectionStringSettingsCollection allConnectionStrings { get; set; }

        static ConfigHelper()
        {
            allSettings = ConfigurationManager.AppSettings;
            allConnectionStrings = ConfigurationManager.ConnectionStrings;
        }

        /// <summary>
        /// get setting by name
        /// </summary>
        /// <param name="settingName"></param>
        /// <returns></returns>
        public static string GetSettingByName(string settingName)
        {
            if (!allSettings.AllKeys.Contains(settingName))
                return "";
            return allSettings[settingName];
        }

        /// <summary>
        /// get connection string by connection name
        /// </summary>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        public static string GetConnectionByName(string connectionName)
        {
            ConnectionStringSettings curConnectionStringSetting = null;
            if (allConnectionStrings.Count <= 0)
                return "";
            IEnumerator enumerator = allConnectionStrings.GetEnumerator();
            while (enumerator.MoveNext())
            {
                curConnectionStringSetting = enumerator.Current as ConnectionStringSettings;
                if (curConnectionStringSetting != null)
                {
                    return curConnectionStringSetting.ConnectionString;
                }
            }
            return "";
        }
    }
}
