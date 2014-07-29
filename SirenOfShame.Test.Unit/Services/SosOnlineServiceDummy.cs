using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SirenOfShame.Lib.Dto;
using SirenOfShame.Lib.Services;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Test.Unit.Services
{
    public class SosOnlineServiceDummy : SosOnlineService
    {
        public override void VerifyCredentialsAsync(SirenOfShameSettings settings, Action onSuccess, Action<string, Exception> onFail)
        {
        }

        public override void Synchronize(SirenOfShameSettings settings, string exportedBuilds, string exportedAchievements, Action<DateTime> onSuccess, Action<string, Exception> onFail)
        {
        }

        public override void TryToGetAndSendNewSosOnlineAlerts(SirenOfShameSettings settings, DateTime now, Action<NewAlertEventArgs> invokeNewAlert)
        {
        }

        public override async Task StartRealtimeConnection(SirenOfShameSettings settings)
        {
            await Task.Yield();
        }

        public override void SendMessage(SirenOfShameSettings settings, string message)
        {
        }

        public override void BuildStatusChanged(SirenOfShameSettings settings, IList<BuildStatus> changedBuildStatuses, List<InstanceUserDto> requestedByPeople)
        {
        }
    }
}
