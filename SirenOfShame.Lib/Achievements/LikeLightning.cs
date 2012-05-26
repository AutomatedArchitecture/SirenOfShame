using System;
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

    protected override bool MeetsAchievementCriteria()
    {
        if (currentBuildDefinitionOrderedChronoligically.Count < 3) return false;
        var lastThree = currentBuildDefinitionOrderedChronoligically.Skip(currentBuildDefinitionOrderedChronoligically.Count - 3).ToList();
        if (!lastThree.All(i => i.RequestedBy == PersonSetting.RawName)) return false;
        if (!lastThree.All(i => i.StartedTime.HasValue && i.FinishedTime.HasValue)) return false;
        if (!lastThree.All(i => i.BuildStatusEnum == BuildStatusEnum.Working)) return false;
        BuildStatus oldestBuild = lastThree[0];
        BuildStatus middleBuild = lastThree[1];
        BuildStatus mostRecentBuild = lastThree[2];
        var wereLastBuildsBackToBack = oldestBuild.IsBackToBackWithNextBuild(middleBuild);
        var wereMostRecentBuidlsBackToBack = middleBuild.IsBackToBackWithNextBuild(mostRecentBuild);
        return wereLastBuildsBackToBack && wereMostRecentBuidlsBackToBack;
    }
}
