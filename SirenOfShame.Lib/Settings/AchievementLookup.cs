using System;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Lib.Settings
{
    public class AchievementLookup
    {
        public AchievementEnum Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public NewNewsItemEventArgs AsNewNewsItem(PersonSetting person)
        {
            return new NewNewsItemEventArgs
            {
                Person = person,
                EventDate = DateTime.Now,
                NewsItemType = NewsItemTypeEnum.NewAchievement,
                BuildDefinitionId = null,
                ReputationChange = null,
                Title = "Achieved " + Name,
            };
        }
    }
}