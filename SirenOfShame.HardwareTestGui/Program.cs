using System;
using System.IO;
using System.Windows.Forms;
using log4net;
using log4net.Config;
using SirenOfShame.Lib;

namespace SirenOfShame.HardwareTestGui
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var fileInfo = new FileInfo("SirenOfShame.HardwareTestGui.exe.config");
            XmlConfigurator.Configure(fileInfo);
            LogManager.GetLogger(typeof(Program)).Debug("Starting");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new HardwareTest());
        }
    }
}
