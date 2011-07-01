using System;
using System.Collections.Generic;
using System.IO;
using log4net;
using log4net.Appender;
using log4net.Config;

namespace SirenOfShame.Lib
{
    public class MyLogManager
    {
        static MyLogManager()
        {
            var fileInfo = new FileInfo("SirenOfShame.exe.config");
            if (fileInfo.Exists)
            {
                XmlConfigurator.Configure(fileInfo);
            }
            ILog logger = GetLogger(typeof(MyLogManager));
            logger.Info("Logging configured");
        }

        public static ILog GetLogger(Type type)
        {
            return LogManager.GetLogger(type);
        }

        public static string GetLogFilename()
        {
            foreach (var appender in GetAppenders())
            {
                FileAppender fileAppender = appender as FileAppender;
                if (fileAppender != null)
                {
                    return fileAppender.File;
                }
            }
            throw new Exception("No file appenders found");
        }

        private static IEnumerable<IAppender> GetAppenders()
        {
            var repos = LogManager.GetAllRepositories();
            foreach (var repo in repos)
            {
                foreach (var appender in repo.GetAppenders())
                {
                    yield return appender;
                }
            }
        }
    }
}
