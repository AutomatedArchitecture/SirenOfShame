using System;
using System.IO;
using log4net;
using log4net.Config;

namespace SirenOfShame.Extruder.Services
{
    public class MyLogManager
    {
        static MyLogManager()
        {
            var fileInfo = new FileInfo("ShameExtruder.exe.config");
            if (fileInfo.Exists)
            {
                XmlConfigurator.Configure(fileInfo);
            }
            var logger = GetLogger(typeof(MyLogManager));
            logger.Info("Logging configured");
        }

        public static ILog GetLogger(Type type)
        {
            return LogManager.GetLogger(type);
        }

        public static ILog GetLog(Type type)
        {
            return LogManager.GetLogger(type);
        }
    }
}
