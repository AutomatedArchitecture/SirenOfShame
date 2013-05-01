using System.Collections.Generic;
using System.Linq;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Lib.Dto
{
    public class OfflineUserDto
    {
        public OfflineUserDto(PersonSetting personSetting)
        {
            RawName = personSetting.RawName;
            Reputation = personSetting.GetReputation();
            DisplayName = personSetting.DisplayName;
            Achievements = personSetting.Achievements.Select(i => new OfflineUserAchievementDto(i)).ToList();
            AvatarId = personSetting.AvatarId;
            Hidden = personSetting.Hidden;
        }

        public int AvatarId { get; set; }

        public string DisplayName { get; set; }

        public int Reputation { get; set; }

        public string RawName { get; set; }

        public bool Hidden { get; set; }

        public List<OfflineUserAchievementDto> Achievements { get; set; }
    }
}
