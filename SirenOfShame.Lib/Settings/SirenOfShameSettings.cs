using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using SirenOfShame.Lib.Device;
using log4net;
using SirenOfShame.Lib.Helpers;

namespace SirenOfShame.Lib.Settings
{
    [Serializable]
    public class SirenOfShameSettings
    {
        [Import(typeof(ISirenOfShameDevice))]
        private ISirenOfShameDevice SirenOfShameDevice { get; set; }
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(SirenOfShameSettings));
        private string _updateLocationOther;

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
            IocContainer.Instance.Compose(this);
            Rules = new List<Rule>();
            CiEntryPointSettings = new List<CiEntryPointSetting>();
            AudioPatterns = new List<AudioPatternSetting>();
            LedPatterns = new List<LedPatternSetting>();
        }

        public List<Rule> Rules { get; set; }

        private const string SIRENOFSHAME_CONFIG = @"SirenOfShame.config";

        public int Pattern { get; set; }

        public List<CiEntryPointSetting> CiEntryPointSettings { get; set; }

        public bool SirenEverConnected { get; set; }

        public UpdateLocation UpdateLocation { get; set; }

        public string UpdateLocationOther
        {
            get { return _updateLocationOther; }
            set
            {
                _updateLocationOther = value;

                // file:///c|/temp/

                if (!string.IsNullOrWhiteSpace(_updateLocationOther))
                {
                    _updateLocationOther = _updateLocationOther.Trim();

                    if (_updateLocationOther.Length > 2)
                    {
                        if (char.IsLetter(_updateLocationOther[0]) && _updateLocationOther[1] == ':')
                        {
                            _updateLocationOther = _updateLocationOther.Replace('\\', '/');
                            _updateLocationOther = _updateLocationOther.Substring(0, 1).ToLowerInvariant() + "|" + _updateLocationOther.Substring(2);
                            _updateLocationOther = "file:///" + _updateLocationOther;
                        }
                    }

                    if (!_updateLocationOther.EndsWith("/") && !_updateLocationOther.EndsWith("\\"))
                    {
                        _updateLocationOther += "/";
                    }
                }
            }
        }

        /// <summary>
        /// In seconds
        /// </summary>
        public int PollInterval { get; set; }

        public List<AudioPatternSetting> AudioPatterns { get; set; }
        public List<LedPatternSetting> LedPatterns { get; set; }

        public volatile string _fileName;

        public string FileName { get { return _fileName; } }

        public virtual void Save()
        {
            string fileName = GetConfigFileName();
            Save(fileName);
        }

        public virtual void Save(string fileName)
        {
            StreamWriter myWriter = null;
            try
            {
                XmlSerializer mySerializer = new XmlSerializer(typeof(SirenOfShameSettings));
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

        private static string GetConfigFileName()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Automated Architecture\\SirenOfShame");
            Directory.CreateDirectory(path);
            return Path.Combine(path, SIRENOFSHAME_CONFIG);
        }

        public static SirenOfShameSettings GetAppSettings()
        {
            string fileName = GetConfigFileName();
            return GetAppSettings(fileName);
        }

        public static SirenOfShameSettings GetAppSettings(string fileName)
        {
            FileStream myFileStream = null;
            try
            {
                XmlSerializer mySerializer = new XmlSerializer(typeof(SirenOfShameSettings));
                FileInfo fi = new FileInfo(fileName);
                if (fi.Exists)
                {
                    myFileStream = fi.OpenRead();
                    SirenOfShameSettings settings = (SirenOfShameSettings)mySerializer.Deserialize(myFileStream);
                    settings._fileName = fileName;
                    settings.ErrorIfAnythingLooksBad();
                    return settings;
                }
            }
            catch (Exception ex)
            {
                _log.Error("Unable to deserialize settings file, so reverting", ex);
                ExceptionMessageBox.Show(null, "Drat",
                                         "Hate to tell you this, but there was an error deserializing the settings file so we took the liberty of reverting the settings to the factory defaults to get everything up and running.  Sorry about your luck.",
                                         ex);
            }
            finally
            {
                if (myFileStream != null)
                {
                    myFileStream.Close();
                }
            }
            SirenOfShameSettings defaultSettings = GetDefaultSettings();
            defaultSettings._fileName = fileName;
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

        private void TrySetDefaultRule(TriggerType triggerType, int audioDuration, bool setLed)
        {
            Rule rule = Rules.FirstOrDefault(r => r.TriggerType == triggerType && r.BuildDefinitionId == null && r.TriggerPerson == null);
            if (rule != null)
            {
                rule.InheritAudioSettings = false;
                rule.AudioPattern = SirenOfShameDevice.AudioPatterns.First();
                rule.AudioDuration = audioDuration;
                rule.InheritLedSettings = !setLed;
                rule.LedPattern = setLed ? SirenOfShameDevice.LedPatterns.First() : null;
                rule.LightsDuration = null;
            }
        }

        public void ResetSirenSettings()
        {
            if (!SirenOfShameDevice.IsConnected) return;
            TrySetDefaultRule(TriggerType.BuildTriggered, 1, false);
            TrySetDefaultRule(TriggerType.InitialFailedBuild, 10, true);
            TrySetDefaultRule(TriggerType.SubsequentFailedBuild, 10, true);
        }
    }
}
