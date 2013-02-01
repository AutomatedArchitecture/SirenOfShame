using System;
using System.Collections.Generic;

namespace SirenOfShame.Lib.Settings
{
    public enum AchievementEnum
    {
        Apprentice = 1,
        Neophyte = 2,
        Master = 3,
        GrandMaster = 4,
        Legend = 5,
        CiNinja = 6,
        Assassin = 7,
        TimeWarrior = 8,
        ChronMaster = 9,
        ChronGrandMaster = 10,
        ReputationRebound = 11,
        LikeLightning = 12,
        ArribaArribaAndaleAndale = 13,
        SpeedDaemon = 14,
        InTheZone = 15,
        Terminator = 16,
        AndGotAwayWithIt = 17,
        Critical = 19,
        Perfectionist = 20,
        JonSkeet = 21,
        Macgyver = 22,
        Napoleon = 23,
        ShamePusher = 24,
    }

    [Serializable]
    public class AchievementSetting
    {
        public static List<AchievementLookup> AchievementLookups = new List<AchievementLookup>
        {
            new AchievementLookup { Id = AchievementEnum.Apprentice, Name = "Apprentice", Description = "Achieved 25 reputation" },
            new AchievementLookup { Id = AchievementEnum.Neophyte, Name = "Neophyte", Description = "Achieved 100 reputation" },
            new AchievementLookup { Id = AchievementEnum.Master, Name = "Master", Description = "Achieved 250 reputation" },
            new AchievementLookup { Id = AchievementEnum.GrandMaster, Name = "Grand-Master", Description = "Achieved 500 reputation" },
            new AchievementLookup { Id = AchievementEnum.Legend, Name = "Legend", Description = "Achieved 1,000 reputation" },
            new AchievementLookup { Id = AchievementEnum.JonSkeet, Name = "Jon Skeet", Description = "Achieved 2,500 reputation" },
            new AchievementLookup { Id = AchievementEnum.CiNinja, Name = "CI Ninja", Description = "Fixed someone else's build" },
            new AchievementLookup { Id = AchievementEnum.Assassin, Name = "Assassin", Description = "Fixed another person's build 10+ times" },
            new AchievementLookup { Id = AchievementEnum.TimeWarrior, Name = "Time Warrior", Description = "Was responsible for 24 hours of cumulative build time" },
            new AchievementLookup { Id = AchievementEnum.ChronMaster, Name = "Chron Master", Description = "Was responsible for 48 hours of cumulative build time" },
            new AchievementLookup { Id = AchievementEnum.ChronGrandMaster, Name = "Chron Grand-Master", Description = "Was responsible for 96 hours of cumulative build time" },
            new AchievementLookup { Id = AchievementEnum.ReputationRebound, Name = "Reputation Rebound", Description = "Lost 12 points from 3 consecutive failed builds, but made it back up" },
            new AchievementLookup { Id = AchievementEnum.LikeLightning, Name = "Like Lightning", Description = "Queued 3 successful builds back to back" },
            new AchievementLookup { Id = AchievementEnum.ArribaArribaAndaleAndale, Name = "ArribaArribaAndaleAndale", Description = "Queued a back to back build 5+ times" },
            new AchievementLookup { Id = AchievementEnum.SpeedDaemon, Name = "Speed Daemon", Description = "Queued a back to back build 10+ times" },
            new AchievementLookup { Id = AchievementEnum.InTheZone, Name = "In The Zone", Description = "Queued 5+ builds in 1 day" },
            new AchievementLookup { Id = AchievementEnum.Terminator, Name = "Terminator", Description = "Queued 10+ builds in 1 day" },
            new AchievementLookup { Id = AchievementEnum.AndGotAwayWithIt, Name = "And Got Away With It", Description = "Fixed a broken build within 60 seconds" },
            new AchievementLookup { Id = AchievementEnum.Critical, Name = "Critical", Description = "Achieved under 10% failed build ratio after 50 checkins" },
            new AchievementLookup { Id = AchievementEnum.Perfectionist, Name = "Perfectionist", Description = "Achieved under 5% failed build ratio after 50 checkins" },
            new AchievementLookup { Id = AchievementEnum.Macgyver, Name = "Macgyver", Description = "Reduced build time by 15%" },
            new AchievementLookup { Id = AchievementEnum.Napoleon, Name = "Napoleon", Description = "Achieved 100 more reputation than anyone else on team" },
            new AchievementLookup { Id = AchievementEnum.ShamePusher, Name = "Shame Pusher", Description = "Own a siren of shame device" },
        };
        
        public DateTime DateAchieved { get; set; }
        public int AchievementId { get; set; }

        public string AsSosOnlineExport()
        {
            return string.Format("{0},{1}", AchievementId, DateAchieved.Ticks);
        }
    }
}