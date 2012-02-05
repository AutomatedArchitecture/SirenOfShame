using System;
using System.Collections.Generic;
using System.Linq;
using SirenOfShame.Lib.Exceptions;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Test.Unit.Watcher
{
    public class RulesEngineWrapper
    {
        public const string CURRENT_USER = "User1";
        public const string BUILD1_ID = "Build Def 1";
        public const string BUILD2_ID = "Build Def 2";

        private SosDbFake _sosDbFake = new SosDbFake();

        public RulesEngineWrapper()
        {
            TrayNotificationEvents = new List<TrayNotifyEventArgs>();
            RefreshStatusEvents = new List<RefreshStatusEventArgs>();
            PlayWindowsAudioEvents = new List<PlayWindowsAudioEventArgs>();
            StatusBarUpdateEvents = new List<UpdateStatusBarEventArgs>();
            ModalDialogEvents = new List<ModalDialogEventArgs>();
            SetAudioEvents = new List<SetAudioEventArgs>();
            SetLightsEvents = new List<SetLightsEventArgs>();

            Settings = new SirenOfShameSettingsFake();
            CiEntryPointSetting = new CiEntryPointSettingFake(Settings);
            Settings.CiEntryPointSettings.Add(CiEntryPointSetting);
            Settings.CiEntryPointSettings.First().BuildDefinitionSettings.Add(new BuildDefinitionSetting { Active = true, AffectsTrayIcon = true, Id = BUILD1_ID, Name = "Build Def 1" });
            Settings.CiEntryPointSettings.First().BuildDefinitionSettings.Add(new BuildDefinitionSetting { Active = true, AffectsTrayIcon = true, Id = BUILD2_ID, Name = "Build Def 2" });

            _rulesEngine = new RulesEngine(Settings)
            {
                SosDb = _sosDbFake
            };

            _rulesEngine.TrayNotify += (sender, arg) => TrayNotificationEvents.Add(arg);
            _rulesEngine.RefreshStatus += (sender, arg) => RefreshStatusEvents.Add(arg);
            _rulesEngine.PlayWindowsAudio += (sender, arg) => PlayWindowsAudioEvents.Add(arg);
            _rulesEngine.UpdateStatusBar += (sender, arg) => StatusBarUpdateEvents.Add(arg);
            _rulesEngine.ModalDialog += (sender, arg) => ModalDialogEvents.Add(arg);
            _rulesEngine.SetAudio += (sender, arg) => SetAudioEvents.Add(arg);
            _rulesEngine.SetLights += (sender, arg) => SetLightsEvents.Add(arg);

            _rulesEngine.Start();
        }

        private readonly RulesEngine _rulesEngine;
        
        public SirenOfShameSettingsFake Settings { get; set; }
        public CiEntryPointSettingFake CiEntryPointSetting { get; set; }
        public List<TrayNotifyEventArgs> TrayNotificationEvents { get; set; }
        public List<RefreshStatusEventArgs> RefreshStatusEvents { get; set; }
        public List<PlayWindowsAudioEventArgs> PlayWindowsAudioEvents { get; set; }
        public List<UpdateStatusBarEventArgs> StatusBarUpdateEvents { get; set; }
        public List<ModalDialogEventArgs> ModalDialogEvents { get; set; }
        public List<SetAudioEventArgs> SetAudioEvents { get; set; }
        public List<SetLightsEventArgs> SetLightsEvents { get; set; }

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
                    StartedTime = new DateTime(2010, 1, 1)
                } 
            });
        }
    }
}