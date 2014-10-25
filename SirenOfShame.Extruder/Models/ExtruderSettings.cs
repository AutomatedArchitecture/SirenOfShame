using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using log4net;
using SirenOfShame.Extruder.Services;

namespace SirenOfShame.Extruder.Models
{
    [Serializable]
    public class ExtruderSettings
    {
        public string UserName { get; set; }
        public string MyName { get; set; }
        public string EncryptedPassword { get; set; }
        public string LedPatterns { get; set; }
        public string AudioPatterns { get; set; }

        private readonly object _lock = new object();
        private const string EXTRUDER_CONFIG = @"ShameExtruder.config";
        private static readonly ILog _log = MyLogManager.GetLog(typeof (ExtruderSettings));
        
        private static string GetConfigFileName()
        {
            string path = GetExtruderAppDataFolder();
            Directory.CreateDirectory(path);
            return Path.Combine(path, EXTRUDER_CONFIG);
        }

        private static string GetExtruderAppDataFolder()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Automated Architecture\\ShameExtruder");
        }

        public static ExtruderSettings GetAppSettings()
        {
            string fileName = GetConfigFileName();
            return GetAppSettings(fileName);
        }

        private static ExtruderSettings GetAppSettings(string fileName)
        {
            FileStream myFileStream = null;
            try
            {
                XmlSerializer mySerializer = new XmlSerializer(typeof(ExtruderSettings));
                FileInfo fi = new FileInfo(fileName);
                if (fi.Exists)
                {
                    myFileStream = fi.OpenRead();
                    var settings = (ExtruderSettings)mySerializer.Deserialize(myFileStream);
                    settings.ErrorIfAnythingLooksBad();
                    return settings;
                }
            }
            catch (Exception ex)
            {
                _log.Error("Unable to deserialize settings file, so reverting", ex);
                var dialogResult = MessageBox.Show("There was an error deserializing the settings file.  Click OK to revert the file and start over or cancel to fix the problem yourself (we'll start the app so you can view logs, etc, just close the app quickly and your old settings will remain).  Here's the error: " + ex, "Drat!", MessageBoxButtons.OKCancel);
                if (dialogResult == DialogResult.Cancel)
                {
                    Application.Exit();
                    return null;
                }
            }
            finally
            {
                if (myFileStream != null)
                {
                    myFileStream.Close();
                }
            }
            var defaultSettings = GetDefaultSettings();
            defaultSettings.Save();
            return defaultSettings;
        }

        public void Save()
        {
            string fileName = GetConfigFileName();
            Save(fileName);
        }

        private void Save(string fileName)
        {
            lock (_lock)
            {
                StreamWriter myWriter = null;
                try
                {
                    var mySerializer = new XmlSerializer(typeof(ExtruderSettings));
                    myWriter = new StreamWriter(fileName, false);
                    mySerializer.Serialize(myWriter, this);
                }
                finally
                {
                    if (myWriter != null)
                    {
                        myWriter.Close();
                    }
                }
            }
        }

        private static ExtruderSettings GetDefaultSettings()
        {
            return new ExtruderSettings();
        }

        private void ErrorIfAnythingLooksBad()
        {
            
        }

        public void InitializeConnectExtruderModel(ConnectExtruderModel connectExtruderModel)
        {
            connectExtruderModel.LedPatterns = LedPatterns;
            connectExtruderModel.AudioPatterns = AudioPatterns;
        }
    }
}
