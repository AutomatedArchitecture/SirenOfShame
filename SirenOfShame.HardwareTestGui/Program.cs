using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using SirenOfShame.Lib.Device;
using SirenOfShame.Lib.Helpers;
using log4net;
using log4net.Config;

namespace SirenOfShame.HardwareTestGui
{
    static class Program
    {
        private static ISirenOfShameDevice _sirenOfShameDevice;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var fileInfo = new FileInfo("SirenOfShame.HardwareTestGui.exe.config");
            XmlConfigurator.Configure(fileInfo);
            LogManager.GetLogger(typeof(Program)).Debug("Starting");

            _sirenOfShameDevice = new SirenOfShameDevice();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new HardwareTest());
        }

        public static ISirenOfShameDevice SirenOfShameDevice
        {
            get { return _sirenOfShameDevice; }
        }

        public static string GetDeviceInfoAsString()
        {
            SirenOfShameInfo deviceInfo = _sirenOfShameDevice.ReadDeviceInfo();
            return GetDeviceInfoAsString(deviceInfo);
        }

        public static string GetDeviceInfoAsString(SirenOfShameInfo deviceInfo)
        {
            StringBuilder info = new StringBuilder();
            info.AppendLine("FirmwareVersion: " + deviceInfo.FirmwareVersion + "\n");
            info.AppendLine("HardwareType: " + deviceInfo.HardwareType + "\n");
            info.AppendLine("HardwareVersion: " + deviceInfo.HardwareVersion + "\n");
            info.AppendLine("AudioMode: " + deviceInfo.AudioMode + "\n");
            info.AppendLine("AudioPlayDuration: " + deviceInfo.AudioPlayDuration + "\n");
            info.AppendLine("LedMode: " + deviceInfo.LedMode + "\n");
            info.AppendLine("LedPlayDuration: " + deviceInfo.LedPlayDuration + "\n");
            info.AppendLine("External Memory Size: " + SiUnitHelpers.ToBinaryString(deviceInfo.ExternalMemorySize) + "B\n");
            return info.ToString();
        }
    }
}
