using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Common
{
    public class LogConfig
    {
        private string _FilePath;

        public string FilePath
        {
            get { return _FilePath; }
        }

        private static LogConfig _Instance = new LogConfig();

        public static LogConfig Instance
        {
            get { return _Instance; }
        }

        private LogConfig()
        {
            string tempStr = null;
            if (ConfigurationManager.AppSettings.AllKeys.Contains("LogPathString"))
            {
                tempStr = ConfigurationManager.AppSettings["LogPathString"];
            }

            if (!string.IsNullOrEmpty(tempStr))
                _FilePath = tempStr;
            else
            {
                if (System.Web.HttpContext.Current == null)
                    //winform程序
                    _FilePath = AppDomain.CurrentDomain.BaseDirectory;
                else
                    //web程序
                    _FilePath = AppDomain.CurrentDomain.BaseDirectory + @"bin\";
            }
        }
    }
}
