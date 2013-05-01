using System;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Lib.Dto
{
    public class OfflineUserAchievementDto
    {
        public OfflineUserAchievementDto(AchievementSetting achievementSetting)
        {
            AchievementId = achievementSetting.AchievementId;
            DateAchieved = achievementSetting.DateAchieved;
        }

        public int AchievementId { get; set; }
        
        public DateTime DateAchieved { get; set; }
    }
}