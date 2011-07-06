using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using log4net;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Lib.Settings
{
    [Serializable]
    public class SirenOfShameSettings
    {
        private static readonly ILog Log = MyLogManager.GetLogger(typeof(SirenOfShameSettings));

        private static List<Rule> _defaultRules = new List<Rule>{
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
            BuildDefinitionSettings = new List<BuildDefinitionSetting>();
            CiEntryPointSettings = new List<CiEntryPointSettingContainer>();
            AudioPatterns = new List<string>();
            LedPatterns = new List<string>();
        }

        public List<Rule> Rules { get; set; }

        private const string _sirenofshameConfig = @"SirenOfShame.config";

        public List<BuildDefinitionSetting> BuildDefinitionSettings { get; set; }

        public int Pattern { get; set; }

        public string ServerType { get; set; }

        public List<CiEntryPointSettingContainer> CiEntryPointSettings { get; set; }

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
            XmlSerializer mySerializer;
            try
            {
                mySerializer = new XmlSerializer(typeof(SirenOfShameSettings));
                myWriter = new StreamWriter(Path.Combine(Application.LocalUserAppDataPath, _sirenofshameConfig), false);
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

        public static SirenOfShameSettings GetAppSettings()
        {
            XmlSerializer mySerializer;
            FileStream myFileStream = null;

            try
            {
                mySerializer = new XmlSerializer(typeof(SirenOfShameSettings));
                FileInfo fi = new FileInfo(Path.Combine(Application.LocalUserAppDataPath, _sirenofshameConfig));
                if (fi.Exists)
                {
                    myFileStream = fi.OpenRead();
                    SirenOfShameSettings settings = (SirenOfShameSettings)mySerializer.Deserialize(myFileStream);
                    settings.InitializeCiEntryPointSettings();
                    return settings;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Unable to deserialize settings file, so reverting", ex);
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

        private static SirenOfShameSettings GetDefaultSettings()
        {
            return new SirenOfShameSettings
            {
                Rules = _defaultRules,
                PollInterval = 5
            };
        }

        public virtual WatcherBase GetWatcher()
        {
            IEnumerable<ICiEntryPoint> ciEntryPoints = IocContainer.Instance.GetExports<ICiEntryPoint>();
            ICiEntryPoint entryPoint = ciEntryPoints.FirstOrDefault(ep => ep.Name == ServerType);
            if (entryPoint == null)
            {
                return null;
            }
            return entryPoint.GetWatcher(this);
        }

        public BuildDefinitionSetting FindAddBuildDefinition(MyBuildDefinition buildDefinition)
        {
            BuildDefinitionSetting buildDefinitionSetting = BuildDefinitionSettings.FirstOrDefault(bd => bd.Id == buildDefinition.Id);
            if (buildDefinitionSetting != null) return buildDefinitionSetting;
            buildDefinitionSetting = new BuildDefinitionSetting(buildDefinition);
            BuildDefinitionSettings.Add(buildDefinitionSetting);
            return buildDefinitionSetting;
        }

        public BuildDefinitionSetting GetBuildDefinition(string buildDefinitionId)
        {
            return BuildDefinitionSettings.First(bd => bd.Id == buildDefinitionId);
        }

        private void InitializeCiEntryPointSettings()
        {
            IEnumerable<ICiEntryPoint> ciEntryPoints = IocContainer.Instance.GetExports<ICiEntryPoint>();
            foreach (var ciEntryPoint in ciEntryPoints)
            {
                FindAddSettings(ciEntryPoint.Name);
            }
        }

        public CiEntryPointSettings FindAddSettings(string name)
        {
            var result = CiEntryPointSettings.FirstOrDefault(s => s.Name == name);
            if (result != null)
            {
                return result.Settings;
            }
            var settings = new CiEntryPointSettings();
            CiEntryPointSettings.Add(new CiEntryPointSettingContainer
            {
                Name = name,
                Settings = settings
            });
            return settings;
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
    }

    [Serializable]
    public class CiEntryPointSettingContainer
    {
        public string Name { get; set; }
        public CiEntryPointSettings Settings { get; set; }
    }
}
