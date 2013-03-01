using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Xml.Serialization;
using SirenOfShame.Lib.Device;
using SirenOfShame.Lib.Watcher;
using log4net;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.Settings.Upgrades;

namespace SirenOfShame.Lib.Settings
{
    [Serializable]
    public class SirenOfShameSettings
    {
        public const int AVATAR_COUNT = 21;

        [Import(typeof(ISirenOfShameDevice))]
        private ISirenOfShameDevice SirenOfShameDevice { get; set; }
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(SirenOfShameSettings));
        private string _updateLocationOther;
        private bool? _anyDuplicateSettingsCached;

        private static readonly List<Rule> _defaultRules = new List<Rule>{
            new Rule { TriggerType = TriggerType.BuildTriggered, AlertType = AlertType.TrayAlert, BuildDefinitionId = null, TriggerPerson = null, InheritAudioSettings = true, InheritLedSettings = true, WindowsAudioLocation = "SirenOfShame.Resources.Audio-Plunk.wav" },
            new Rule { TriggerType = TriggerType.InitialFailedBuild, AlertType = AlertType.ModalDialog, BuildDefinitionId = null, TriggerPerson = null, InheritAudioSettings = true, InheritLedSettings = true, WindowsAudioLocation  = "SirenOfShame.Resources.Audio-Sad-Trombone.wav" },
            new Rule { TriggerType = TriggerType.SubsequentFailedBuild, AlertType = AlertType.TrayAlert, BuildDefinitionId = null, TriggerPerson = null, InheritAudioSettings = true, InheritLedSettings = true, WindowsAudioLocation = "SirenOfShame.Resources.Audio-Boo-Hiss.wav" },
            new Rule { TriggerType = TriggerType.SuccessfulBuild, AlertType = AlertType.TrayAlert, BuildDefinitionId = null, TriggerPerson = null, InheritAudioSettings = true, InheritLedSettings = true, WindowsAudioLocation = null },
        };

        public void ResetRules()
        {
            Rules.Clear();
            Rules.AddRange(_defaultRules);
            Save();
        }

        public SirenOfShameSettings() : this(true) { }
        
        public SirenOfShameSettings(bool useMef)
        {
            if (useMef)
                IocContainer.Instance.Compose(this);
            Rules = new List<Rule>();
            CiEntryPointSettings = new List<CiEntryPointSetting>();
            AudioPatterns = new List<AudioPatternSetting>();
            LedPatterns = new List<LedPatternSetting>();
            People = new List<PersonSetting>();
            UserMappings = new List<UserMapping>();
        }

        public int? Version { get; set; }
        
        public DateTime? LastCheckedForAlert { get; set; }
        
        public List<Rule> Rules { get; set; }

        private const string SIRENOFSHAME_CONFIG = @"SirenOfShame.config";

        public int Pattern { get; set; }

        public List<CiEntryPointSetting> CiEntryPointSettings { get; set; }

        public List<PersonSetting> People { get; set; }

        public List<UserMapping> UserMappings { get; set; }

        public bool SirenEverConnected { get; set; }

        public bool NeverShowGettingStarted { get; set; }

        public UpdateLocation UpdateLocation { get; set; }

        public int? SortColumn { get; set; }
        
        public bool SortDescending { get; set; }

        public WhatToSyncEnum SosOnlineWhatToSync { get; set; }

        public string SosOnlineUsername { get; set; }
        
        public string SosOnlinePassword { get; set; }

        public string SosOnlineProxyUrl { get; set; }
        
        public string SosOnlineProxyUsername { get; set; }
        
        public string SosOnlineProxyPasswordEncrypted { get; set; }

        public long? SosOnlineHighWaterMark { get; set; }

        public bool SosOnlineAlwaysOffline { get; set; }

        public bool SosOnlineAlwaysSync { get; set; }

        [XmlIgnore]
        public bool AnyDuplicateBuildNames
        {
            get
            {
                if (!_anyDuplicateSettingsCached.HasValue)
                {
                    // caching is a pre-optimization, but this will get called a lot and change ~never
                    _anyDuplicateSettingsCached = CiEntryPointSettings
                        .SelectMany(i => i.BuildDefinitionSettings)
                        .GroupBy(i => i.Name)
                        .Any(i => i.Count() > 1);
                }
                return _anyDuplicateSettingsCached.Value;
            }
        }

        public void ClearDuplicateNameCache()
        {
            _anyDuplicateSettingsCached = null;
        }

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

        public int? SoftwareInstanceId { get; set; }

        public bool Mute { get; set; }

        private string _fileName;

        public DateTime? AlertClosed { get; set; }

        public string FileName { get { return _fileName; } }

        public string MyRawName { get; set; }

        [XmlIgnore]
        public bool TryToFindOldAchievementsAtNextOpportunity { get; set; }

        public virtual void Save()
        {
            string fileName = GetConfigFileName();
            Save(fileName);
        }

        private readonly object _lock = new object();

        public AchievementAlertPreferenceEnum AchievementAlertPreference { get; set; }

        public void Save(string fileName)
        {
            lock (_lock)
            {
                StreamWriter myWriter = null;
                try
                {
                    XmlSerializer mySerializer = new XmlSerializer(typeof (SirenOfShameSettings));
                    myWriter = new StreamWriter(fileName, false);
                    mySerializer.Serialize(myWriter, this);
                } finally
                {
                    if (myWriter != null)
                    {
                        myWriter.Close();
                    }
                }
            }
        }

        public static string GetSosAppDataFolder()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Automated Architecture\\SirenOfShame");
        }
        
        public static string GetDeviceAudioFolder()
        {
            return Path.Combine(GetSosAppDataFolder(), "DeviceAudio");
        }

        private static string GetConfigFileName()
        {
            string path = GetSosAppDataFolder();
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
            SirenOfShameSettings defaultSettings = GetDefaultSettings();
            defaultSettings._fileName = fileName;
            defaultSettings.Save();
            return defaultSettings;
        }

        public void TryUpgrade()
        {
            var upgrades = new UpgradeBase[]
                               {
                                   new Upgrade0To1(),
                                   new Upgrade1To2(),
                                   new Upgrade2To3(),
                                   new Upgrade3To4(),
                                   new Upgrade4To5(), 
                                   new Upgrade5To6(AVATAR_COUNT),
                                   new Upgrade6To7(), 
                               };
            var sortedUpgrades = upgrades.OrderBy(i => i.ToVersion);

            foreach (var upgrade in sortedUpgrades)
            {
                if (Version == upgrade.FromVersion)
                {
                    upgrade.Upgrade(this);
                    Version = upgrade.ToVersion;
                    Save();
                }
            }
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

        public IEnumerable<PersonSetting> VisiblePeople
        {
            get { return People.Where(i => !i.Hidden); }
        }

        public static int GenericSosOnlineAvatarId
        {
            get { return AVATAR_COUNT; }
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

        public PersonSetting FindAddPerson(string requestedBy, int avatarCount = AVATAR_COUNT)
        {
            if (string.IsNullOrEmpty(requestedBy))
            {
                _log.Warn("Tried to add a person with a null RawName");
                return null;
            }
            var person = FindPersonByRawName(requestedBy);
            if (person != null) return person;
            person = new PersonSetting
            {
                DisplayName = requestedBy,
                RawName = requestedBy,
                FailedBuilds = 0,
                TotalBuilds = 0,
                AvatarId = People.Count % avatarCount
            };
            People.Add(person);
            Save();
            return person;
        }

        public PersonSetting FindPersonByRawName(string rawName)
        {
            if (People == null) People = new List<PersonSetting>();
            var person = People.FirstOrDefault(i => i.RawName == rawName);
            return person;
        }

        public string TryGetDisplayName(string userName)
        {
            if (string.IsNullOrEmpty(userName)) return userName;
            var person = People.FirstOrDefault(i => i.RawName != null && i.RawName.EndsWith(userName));
            return person == null ? userName : person.DisplayName;
        }

        public void UpdateNameIfChanged(BuildStatus changedBuildStatus)
        {
            var changedName = CiEntryPointSettings
                .SelectMany(i => i.BuildDefinitionSettings)
                .FirstOrDefault(i => i.Id == changedBuildStatus.BuildDefinitionId && i.Name != changedBuildStatus.Name);
            if (changedName == null) return;
            changedName.Name = changedBuildStatus.Name;
            Save();
        }

        public bool BuildExistsAndIsActive(string ciEntryPointName, string buildName)
        {
            var ciEntryPoint = CiEntryPointSettings.FirstOrDefault(i => i.Name == ciEntryPointName);
            if (ciEntryPoint != null)
            {
                return ciEntryPoint.BuildDefinitionSettings.Any(i => i.Name == buildName && i.Active);
            }
            return false;
        }

        public bool IsMeOrDefault(PersonSetting person, bool defaultValue)
        {
            if (string.IsNullOrEmpty(MyRawName)) return defaultValue;
            return person.RawName == MyRawName;
        }

        public IEnumerable<BuildDefinitionSetting> GetAllActiveBuildDefinitions()
        {
            return CiEntryPointSettings.SelectMany(i => i.BuildDefinitionSettings).Where(i => i.Active);
        }

        public void SetSosOnlineProxyPassword(string rawPassword)
        {
            SosOnlineProxyPasswordEncrypted = new TripleDesStringEncryptor().EncryptString(rawPassword);
        }

        public string GetSosOnlineProxyPassword()
        {
            return new TripleDesStringEncryptor().DecryptString(SosOnlineProxyPasswordEncrypted);
        }

        public void SetSosOnlinePassword(string rawPassword)
        {
            SosOnlinePassword = new TripleDesStringEncryptor().EncryptString(rawPassword);
        }

        public string GetSosOnlinePassword()
        {
            return new TripleDesStringEncryptor().DecryptString(SosOnlinePassword);
        }

        public string ExportNewAchievements()
        {
            if (string.IsNullOrEmpty(MyRawName)) return null;
            DateTime? highWaterMark = GetHighWaterMark();
            var initialExport = highWaterMark == null;
            var currentUser = GetCurrentUser();
            if (currentUser == null) return null;
            var currentUsersAchievements = currentUser.Achievements;
            var achievementsAfterHighWaterMark = initialExport ? currentUsersAchievements : currentUsersAchievements.Where(i => i.DateAchieved > highWaterMark);
            var buildsAsExport = achievementsAfterHighWaterMark.Select(i => i.AsSosOnlineExport());
            var result = string.Join("\r\n", buildsAsExport);
            return string.IsNullOrEmpty(result) ? null : result;
        }

        private PersonSetting GetCurrentUser()
        {
            return People.FirstOrDefault(i => i.RawName == MyRawName);
        }

        public DateTime? GetHighWaterMark()
        {
            return SosOnlineHighWaterMark == null ? (DateTime?)null : new DateTime(SosOnlineHighWaterMark.Value);
        }

        public void InitializeUserIAm(ComboBox userIAm)
        {
            userIAm.Items.Add("");
            foreach (var personInProject in People)
            {
                userIAm.Items.Add(personInProject);
            }
            if (!string.IsNullOrEmpty(MyRawName))
            {
                foreach (var item in userIAm.Items)
                {
                    var personSetting = item as PersonSetting;
                    if (personSetting != null && personSetting.RawName == MyRawName)
                    {
                        userIAm.SelectedItem = item;
                    }
                }
            }
        }

        public bool GetSosOnlineContent()
        {
            if (SosOnlineAlwaysOffline) return false;
            // if someone doesn't want to check for the lastest software, they probably are on a private network and don't want random connections to SoS Online
            if (UpdateLocation != UpdateLocation.Auto) return false;
            return true;
        }
        
        public IWebProxy GetSosOnlineProxy()
        {
            if (string.IsNullOrEmpty(SosOnlineProxyUrl)) return null;
            if (string.IsNullOrEmpty(SosOnlineProxyUsername))
            {
                return new WebProxy(SosOnlineProxyUrl);
            }
            return new WebProxy(
                SosOnlineProxyUrl, 
                false, 
                new string[] { },
                new NetworkCredential(SosOnlineProxyUsername, GetSosOnlineProxyPassword())
                );
        }

        public BuildDefinitionSetting FindBuildDefinitionById(string buildId)
        {
            return CiEntryPointSettings
                .SelectMany(i => i.BuildDefinitionSettings)
                .FirstOrDefault(bds => bds.Id == buildId);
        }

        public bool IsGettingStarted()
        {
            if (NeverShowGettingStarted) return false;
            bool anyServers = CiEntryPointSettings.Any();
            if (!anyServers) return true;
            bool connected = !string.IsNullOrEmpty(SosOnlineUsername);
            bool alwaysOffline = SosOnlineAlwaysOffline;
            return !connected && !alwaysOffline;
        }
    }
}
