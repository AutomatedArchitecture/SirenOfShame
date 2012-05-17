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
        ChronLegend = 10,
        ReputationRebound = 11,
        LightningFast = 12,
        ArribaArribaAndaleAndale = 13,
        SpeedDaemon = 14,
        InTheZone = 15,
        UltraProductive = 16,
        AndGotAwayWithIt = 17,
        Opportunistic = 18,
        Critical = 19,
        Perfectionist = 20,
    }

    [Serializable]
    public class AchievementSetting
    {
        public static List<AchievementLookup> AchievementLookups = new List<AchievementLookup>
        {
            new AchievementLookup { Id = AchievementEnum.Apprentice, Name = "Apprentice", Description = "Achieved 25 points" },
            new AchievementLookup { Id = AchievementEnum.Neophyte, Name = "Neophyte", Description = "Achieved 100 Points" },
            new AchievementLookup { Id = AchievementEnum.Master, Name = "Master", Description = "Achieved 250 Points" },
            new AchievementLookup { Id = AchievementEnum.GrandMaster, Name = "Grand-Master", Description = "Achieved 500 Points" },
            new AchievementLookup { Id = AchievementEnum.Legend, Name = "Legend", Description = "Achieved 1,000 Points" },
            new AchievementLookup { Id = AchievementEnum.CiNinja, Name = "CI Ninja", Description = "Fixed someone else's build" },
            new AchievementLookup { Id = AchievementEnum.Assassin, Name = "Assassin", Description = "Fix another person's build >=10 times" },
            new AchievementLookup { Id = AchievementEnum.TimeWarrior, Name = "Time Warrior", Description = "Responsible for 24 hours of cumulative build time" },
            new AchievementLookup { Id = AchievementEnum.ChronMaster, Name = "Chron Master", Description = "Responsible for 48 hours of cumulative build time" },
            new AchievementLookup { Id = AchievementEnum.ChronLegend, Name = "Chron Legend", Description = "Responseible for 96 hours of cumulative build time" },
            new AchievementLookup { Id = AchievementEnum.ReputationRebound, Name = "Reputation Rebound", Description = "Lost 12 points from 3 consecutive failed builds, but made it back up" },
            new AchievementLookup { Id = AchievementEnum.LightningFast, Name = "Lightning Fast", Description = "Queue 3 builds back to back" },
            new AchievementLookup { Id = AchievementEnum.ArribaArribaAndaleAndale, Name = "Arriba Arriba, Andale, Andale", Description = "Queued a back to back build >5 times" },
            new AchievementLookup { Id = AchievementEnum.SpeedDaemon, Name = "Speed Daemon", Description = "Queued a back to back build >10 times" },
            new AchievementLookup { Id = AchievementEnum.InTheZone, Name = "In The Zone", Description = "Queued 5 builds in 1 day" },
            new AchievementLookup { Id = AchievementEnum.UltraProductive, Name = "Ultra-Productive", Description = "Queued 10 builds in 1 day" },
            new AchievementLookup { Id = AchievementEnum.AndGotAwayWithIt, Name = "And Got Away With It", Description = "Fixed a broken build within 1 minute" },
            new AchievementLookup { Id = AchievementEnum.Opportunistic, Name = "Opportunistic", Description = "10 Contiguous Builds" },
            new AchievementLookup { Id = AchievementEnum.Critical, Name = "Critical", Description = "Achieved <10% failed build ratio and > 50 checkins" },
            new AchievementLookup { Id = AchievementEnum.Perfectionist, Name = "Perfectionist", Description = "Achieved <5% failed build ratio and > 50 checkins" },
        };
        
        public DateTime DateAchieved { get; set; }
        public int AchievementId { get; set; }
    }
}