using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Lib.Services
{
    public class NewSosOnlineNotificationArgs
    {
        public string Message { get; set; }

        public string DisplayName { get; set; }

        public PersonBase GetSosOnlinePerson()
        {
            return new SosOnlinePerson
            {
                AvatarId = SirenOfShameSettings.GenericSosOnlineAvatarId,
                DisplayName = DisplayName
            };
        }
    }
}