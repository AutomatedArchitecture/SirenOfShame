using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using log4net;
using SirenOfShame.Lib.Helpers;

namespace SirenOfShame.Lib.Settings
{
    [Serializable]
    public class SirenOfShameSettings
    {
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(SirenOfShameSettings));

        private static readonly List<Rule> _defaultRules = new List<Rule>{
            new Rule { TriggerType = TriggerType.BuildTriggered, AlertType = AlertType.TrayAlert, BuildDefinitionId = null, TriggerPerson = null, InheritAudioSettings = true, InheritLedSettings = true },
            new Rule { TriggerType = TriggerType.InitialFailedBuild, AlertType = AlertType.ModalDialog, BuildDefinitionId = null, TriggerPerson = null, InheritAudioSettings = true, InheritLedSettings = true  },
            new Rule { TriggerType = TriggerType.SubsequentFailedBuild, AlertType = AlertType.TrayAlert, BuildDefinitionId = null, TriggerPerson = null, InheritAudioSettings = true, InheritLedSettings = true  },
            new Rule { TriggerType = TriggerType.SuccessfulBuild, AlertType = AlertType.TrayAlert, BuildDefinitionId = null, TriggerPerson = null, InheritAudioSettings = true, InheritLedSettings = true  },
        };

        public void ResetRules()
        {
            Rules.Clear();
            Rules.AddRange(_defaultRules);
            Save();
        }

        public SirenOfShameSettings()
        {
            Rules = new List<Rule>();
            CiEntryPointSettings = new List<CiEntryPointSetting>();
            AudioPatterns = new List<string>();
            LedPatterns = new List<string>();
        }

        public List<Rule> Rules { get; set; }

        private const string SIRENOFSHAME_CONFIG = @"SirenOfShame.config";

        public int Pattern { get; set; }

        public List<CiEntryPointSetting> CiEntryPointSettings { get; set; }

        public bool SirenEverConnected { get; set; }

        public UpdateLocation UpdateLocation { get; set; }

        public string UpdateLocationOther { get; set; }

        /// <summary>
        /// In seconds
        /// </summary>
        public int PollInterval { get; set; }

        public List<string> AudioPatterns { get; set; }
        public List<string> LedPatterns { get; set; }

        public virtual void Save()
        {
            StreamWriter myWriter = null;
            try
            {
                XmlSerializer mySerializer = new XmlSerializer(typeof(SirenOfShameSettings));
                myWriter = new StreamWriter(GetConfigFileName(), false);
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

        private static string GetConfigFileName()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Automated Architecture\\SirenOfShame");
            Directory.CreateDirectory(path);
            return Path.Combine(path, SIRENOFSHAME_CONFIG);
        }

        public static SirenOfShameSettings GetAppSettings()
        {
            FileStream myFileStream = null;

            try
            {
                XmlSerializer mySerializer = new XmlSerializer(typeof(SirenOfShameSettings));
                FileInfo fi = new FileInfo(GetConfigFileName());
                if (fi.Exists)
                {
                    myFileStream = fi.OpenRead();
                    SirenOfShameSettings settings = (SirenOfShameSettings)mySerializer.Deserialize(myFileStream);
                    settings.ErrorIfAnythingLooksBad();
                    return settings;
                }
            }
            catch (Exception ex)
            {
                _log.Error("Unable to deserialize settings file, so reverting", ex);
                ExceptionMessageBox.Show(null, "Drat", "Hate to tell you this, but there was an error deserializing the settings file so we took the liberty of reverting the settings to the factory defaults to get everything up and running.  Sorry about your luck.", ex);
            }
            finally
            {
                if (myFileStream != null)
                {
                    myFileStream.Close();
                }
            }
            SirenOfShameSettings defaultSettings = GetDefaultSettings();

            defaultSettings.Save();
            return defaultSettings;
        }

        private void ErrorIfAnythingLooksBad()
        {
            if (CiEntryPointSettings.SelectMany(i => i.BuildDefinitionSettings).Any(bds => string.IsNullOrEmpty(bds.BuildServer)))
                throw new Exception("A BuildDefinitionSetting.BuildServer was null or empty");
        }

        private static SirenOfShameSettings GetDefaultSettings()
        {
            return new SirenOfShameSettings
            {
                Rules = _defaultRules,
                PollInterval = 5
            };
        }

        public Rule FindRule(TriggerType triggerType, string id, string triggerPerson)
        {
            return Rules.Where(r =>
                        (r.BuildDefinitionId == null || r.BuildDefinitionId == id) &&
                        r.TriggerType == triggerType &&
                        (r.TriggerPerson == null || r.TriggerPerson == triggerPerson))
                .OrderByDescending(r => r.PriorityId)
                .FirstOrDefault();
        }

        public IEnumerable<ICiEntryPoint> CiEntryPoints
        {
            get { return IocContainer.Instance.GetExports<ICiEntryPoint>(); }
        }
    }
}
