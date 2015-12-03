using System.Collections.Generic;
using System.Linq;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Lib.Dto
{
    public class InstanceUserDto
    {
        public InstanceUserDto(PersonSetting personSetting)
        {
            RawName = personSetting.RawName;
            Reputation = personSetting.GetReputation();
            DisplayName = personSetting.DisplayName;
            Achievements = personSetting.Achievements.Select(i => new OfflineUserAchievementDto(i)).ToList();
            AvatarId = personSetting.AvatarId;
            AvatarImageName = personSetting.AvatarImageName;
            Email = personSetting.Email;
            Hidden = personSetting.Hidden;
            FailPercent = (int)(personSetting.CurrentBuildRatio * 1000);
            Csb = personSetting.CurrentSuccessInARow;
            TotalBuilds = personSetting.TotalBuilds;
            Fseb = personSetting.NumberOfTimesFixedSomeoneElsesBuild;
        }

        public string AvatarImageName { get; set; }

        public string Email { get; set; }

        public int? AvatarId { get; set; }

        public string DisplayName { get; set; }

        public int Reputation { get; set; }

        public int FailPercent { get; set; }
        
        public int Csb { get; set; }
        
        public int TotalBuilds { get; set; }
        
        public int Fseb { get; set; }

        public string RawName { get; set; }

        public bool Hidden { get; set; }

        public List<OfflineUserAchievementDto> Achievements { get; set; }
    }
}
