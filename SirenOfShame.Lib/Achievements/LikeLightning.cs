using System.Collections.Generic;
using System.Linq;
using SirenOfShame.Lib.Achievements;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

public class LikeLightning : AchievementBase
{
    private readonly List<BuildStatus> currentBuildDefinitionOrderedChronoligically;

    public LikeLightning(PersonSetting personSetting, List<BuildStatus> currentBuildDefinitionOrderedChronoligically) : base(personSetting)
    {
        this.currentBuildDefinitionOrderedChronoligically = currentBuildDefinitionOrderedChronoligically;
    }

    public override AchievementEnum AchievementEnum
    {
        get { return AchievementEnum.LikeLightning; }
    }

    protected override bool MeetsAchievementCriteria(PersonSetting personSetting)
    {
        if (currentBuildDefinitionOrderedChronoligically.Count < 3) return false;
        var lastThree = currentBuildDefinitionOrderedChronoligically.Skip(currentBuildDefinitionOrderedChronoligically.Count - 3).ToList();
        if (!lastThree.All(i => i.RequestedBy == PersonSetting.RawName)) return false;
        if (!lastThree.All(i => i.StartedTime.HasValue && i.FinishedTime.HasValue)) return false;
        var timeBetweenOneAndTwo = lastThree[1].StartedTime.Value - lastThree[0].FinishedTime.Value;
        var timeBetweenTwoAndThree = lastThree[2].StartedTime.Value - lastThree[1].FinishedTime.Value;
        return timeBetweenOneAndTwo.TotalSeconds <= 10 && timeBetweenTwoAndThree.TotalSeconds < 10;
    }
}
