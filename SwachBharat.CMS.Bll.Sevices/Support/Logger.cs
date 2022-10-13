using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwachBharat.CMS.Bll.Services.Support
{
  public static class Logger
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger("LogFileAppender");

        public static void WriteDebugMessage(string message)
        {
            log.Debug(message);
        }
        public static void WriteInfoMessage(string message)
        {
            log.Info(message);
        }
        public static void WriteWarnMessage(string message)
        {
            log.Warn(message);
        }
        public static void WriteErrorMessage(string message)
        {
            log.Error(message);
        }

        public static void WriteFatalMessage(string message)
        {
            log.Fatal(message);
        }
    }
}
