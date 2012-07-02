using System.Collections.Generic;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Lib.Watcher
{
    public class NewAchievementEventArgs
    {
        public List<AchievementLookup> Achievements;
        public PersonSetting Person { get; set; }
    }
}