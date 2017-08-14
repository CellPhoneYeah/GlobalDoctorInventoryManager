using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Common
{
    public class DALConfig
    {
        private static DALConfig _instance = new DALConfig();

        private string _connectionStr;

        /// <summary>
        /// 在Instance中才能访问的连接字符串
        /// </summary>
        public string ConnectionStr
        {
            get
            {
                return _connectionStr;
            }
        }

        public static DALConfig Instance
        {
            get
            {
                return _instance;
            }
        }

        private DALConfig()
        {
            string tempStr = string.Empty;
            if (ConfigurationManager.AppSettings.AllKeys.Contains("ConnectionString"))
            {
                tempStr = ConfigurationManager.AppSettings["ConnectionString"];
            }
            if (string.IsNullOrEmpty(tempStr))
                LogManager.WriteLog("没有在配置中初始化ConnectionString");
            else
                _connectionStr = tempStr;
        }
    }
}
