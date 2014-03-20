using System;
using System.Collections.Generic;
using System.Linq;
using SirenOfShame.Lib.Exceptions;
using SirenOfShame.Lib.Services;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Test.Unit.Watcher
{
    public class RulesEngineWrapper
    {
        public const string CURRENT_USER = "User1";
        public const string BUILD1_ID = "Build Def 1";
        public const string BUILD2_ID = "Build Def 2";

        private readonly SosDbFake _sosDbFake = new SosDbFake();

        public RulesEngineWrapper()
        {
            TrayNotificationEvents = new List<TrayNotifyEventArgs>();
            SetTrayIconEvents = new List<SetTrayIconEventArgs>();
            RefreshStatusEvents = new List<RefreshStatusEventArgs>();
            PlayWindowsAudioEvents = new List<PlayWindowsAudioEventArgs>();
            StatusBarUpdateEvents = new List<UpdateStatusBarEventArgs>();
            ModalDialogEvents = new List<ModalDialogEventArgs>();
            SetAudioEvents = new List<SetAudioEventArgs>();
            SetLightsEvents = new List<SetLightsEventArgs>();
            NewAlertEvents = new List<NewAlertEventArgs>();
            NewAchievementEvents = new List<NewAchievementEventArgs>();
            NewNewsItemEvents = new List<NewNewsItemEventArgs>();
            NewUserEvents = new List<NewUserEventArgs>();

            Settings = new SirenOfShameSettingsFake();
            CiEntryPointSetting = new CiEntryPointSettingFake(Settings);
            Settings.CiEntryPointSettings.Add(CiEntryPointSetting);
            Settings.CiEntryPointSettings.First().BuildDefinitionSettings.Add(new BuildDefinitionSetting { Active = true, AffectsTrayIcon = true, Id = BUILD1_ID, Name = "Build Def 1" });
            Settings.CiEntryPointSettings.First().BuildDefinitionSettings.Add(new BuildDefinitionSetting { Active = true, AffectsTrayIcon = true, Id = BUILD2_ID, Name = "Build Def 2" });

            _rulesEngine = new FakeRulesEngine(Settings)
            {
                SosDb = _sosDbFake
            };

            _rulesEngine.TrayNotify += (sender, arg) => TrayNotificationEvents.Add(arg);
            _rulesEngine.SetTrayIcon += (sender, arg) => SetTrayIconEvents.Add(arg);
            _rulesEngine.RefreshStatus += (sender, arg) => RefreshStatusEvents.Add(arg);
            _rulesEngine.PlayWindowsAudio += (sender, arg) => PlayWindowsAudioEvents.Add(arg);
            _rulesEngine.UpdateStatusBar += (sender, arg) => StatusBarUpdateEvents.Add(arg);
            _rulesEngine.ModalDialog += (sender, arg) => ModalDialogEvents.Add(arg);
            _rulesEngine.SetAudio += (sender, arg) => SetAudioEvents.Add(arg);
            _rulesEngine.SetLights += (sender, arg) => SetLightsEvents.Add(arg);
            _rulesEngine.NewAlert += (sender, arg) => NewAlertEvents.Add(arg);
            _rulesEngine.NewAchievement += (sender, arg) => NewAchievementEvents.Add(arg);
            _rulesEngine.NewNewsItem += (sender, arg) => NewNewsItemEvents.Add(arg);
            _rulesEngine.NewUser += (sender, arg) => NewUserEvents.Add(arg);

            _rulesEngine.Start(initialStart: true);
        }


        private readonly FakeRulesEngine _rulesEngine;

        public SirenOfShameSettingsFake Settings { get; private set; }
        private CiEntryPointSettingFake CiEntryPointSetting { get; set; }
        public List<TrayNotifyEventArgs> TrayNotificationEvents { get; private set; }
        public List<SetTrayIconEventArgs> SetTrayIconEvents { get; private set; }
        public List<RefreshStatusEventArgs> RefreshStatusEvents { get; private set; }
        public List<PlayWindowsAudioEventArgs> PlayWindowsAudioEvents { get; private set; }
        public List<UpdateStatusBarEventArgs> StatusBarUpdateEvents { get; private set; }
        public List<ModalDialogEventArgs> ModalDialogEvents { get; private set; }
        public List<SetAudioEventArgs> SetAudioEvents { get; private set; }
        public List<SetLightsEventArgs> SetLightsEvents { get; private set; }
        private List<NewAlertEventArgs> NewAlertEvents { get; set; }
        public List<NewAchievementEventArgs> NewAchievementEvents { get; private set; }
        public List<NewNewsItemEventArgs> NewNewsItemEvents { get; private set; }
        public List<NewUserEventArgs> NewUserEvents { get; private set; }

        public SosDbFake SosDb
        {
            get { return _sosDbFake; }
        }

        public List<Rule> Rules
        {
            get { return Settings.Rules; }
        }

        public BuildDefinitionSetting GetBuildDefinitionSetting(string buildDefinitionSetting)
        {
            return Settings.CiEntryPointSettings.SelectMany(i => i.BuildDefinitionSettings).SingleOrDefault(bds => bds.Id == buildDefinitionSetting);
        }

        public void InvokeServerUnavailable(ServerUnavailableException serverUnavailableException)
        {
            ((WatcherFake)CiEntryPointSetting.GetWatcher(Settings)).InvokeServerUnavailable(serverUnavailableException);
        }

        public void InvokeStatusChecked(BuildStatus[] args)
        {
            ((WatcherFake)CiEntryPointSetting.GetWatcher(Settings)).InvokeStatusChecked(args);
        }

        public void InvokeStatusChecked(BuildStatus args)
        {
            ((WatcherFake)CiEntryPointSetting.GetWatcher(Settings)).InvokeStatusChecked(new[] { args });
        }

        public void InvokeStatusChecked(BuildStatusEnum status)
        {
            InvokeStatusChecked(new[]
            {
                new BuildStatus
                {
                    BuildStatusEnum = status,
                    Name = BUILD1_ID, 
                    RequestedBy = CURRENT_USER, 
                    BuildDefinitionId = BUILD1_ID, 
                    StartedTime = new DateTime(2010, 1, 1, 1, 1, 1),
                    FinishedTime = status == BuildStatusEnum.InProgress ? (DateTime?)null : new DateTime(2010, 1, 1, 1, 10, 10),
                    Comment = "Fixing a typo"
                } 
            });
        }

        public SosOnlineService SosOnlineService
        {
            set { _rulesEngine.MockSosOnlineService = value; }
        }
    }
}