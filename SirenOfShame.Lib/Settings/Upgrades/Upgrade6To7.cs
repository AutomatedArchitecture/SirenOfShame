namespace SirenOfShame.Lib.Settings.Upgrades
{
    public class Upgrade6To7 : UpgradeBase
    {
        public Upgrade6To7()
        {
        }

        public override int ToVersion
        {
            get { return 7; }
        }

        public override void Upgrade(SirenOfShameSettings settings)
        {
            //SosDb sosDb = new SosDb();
            //List<BuildStatus> allActiveBuildDefinitionsOrderedChronoligically = sosDb
            //    .ReadAll(settings.GetAllActiveBuildDefinitions())
            //    .OrderBy(i => i.StartedTime)
            //    .ToList();

            //List<BuildStatus> currentBuildDefinitionOrderedChronoligically = allActiveBuildDefinitionsOrderedChronoligically
            //    .Where(i => i.BuildDefinitionId == build.BuildDefinitionId)
            //    .ToList();

            //sirenOfShameSettings.People.ForEach(i => i.CalculateNewAchievements());
        }
    }
}
