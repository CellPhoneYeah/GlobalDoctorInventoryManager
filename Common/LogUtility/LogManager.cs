using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Common
{
    public class LogManager
    {
        private static string _LogPath;

        public static string LogPath
        {
            get
            {
                if (string.IsNullOrEmpty(_LogPath))
                    return LogConfig.Instance.FilePath;
                else
                    return _LogPath;
            }
            set
            {
                _LogPath = value;
            }
        }

        private static string _LogPrefix;
        /// <summary>
        /// 日志文件前缀
        /// </summary>
        public static string LogPrefix
        {
            get { return _LogPrefix; }
            set { _LogPrefix = value; }
        }

        static LogManager()
        {
            _LogPath = LogConfig.Instance.FilePath;
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="logFile">文件名</param>
        /// <param name="logMessage">要写的日志信息</param>
        private static void WriteLog(string logFile, string logMessage)
        {
            try
            {
                StreamWriter sw = File.AppendText(_LogPath + _LogPrefix + logFile + ".log");
                string LogMsg = "{0}:{1}";
                LogMsg = string.Format(LogMsg,DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), logMessage);
                sw.WriteLine(LogMsg);
                sw.Close();
            }
            catch
            {
            }
        }

        /// <summary>
        /// 按照类型写日志
        /// </summary>
        /// <param name="logFile"></param>
        /// <param name="logMessage"></param>
        public static void WriteLog(LogFile logFile,string logMessage)
        {
            WriteLog(logFile.ToString(), logMessage);
        }

        /// <summary>
        /// 默认写异常日志
        /// </summary>
        /// <param name="logMessage"></param>
        public static void WriteLog(string logMessage)
        {
            WriteLog(LogFile.Exception, logMessage);
        }

        /// <summary>
        /// 日志类型
        /// </summary>
        public enum LogFile
        { 
            Exception,
            Information,
            SQL
        }
    }
}
