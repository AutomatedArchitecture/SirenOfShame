using System.Collections.Generic;
using System.Linq;

namespace SirenOfShame.Lib.Settings.Upgrades
{
    /// <summary>
    /// aka upgrade to 1.2.0
    /// </summary>
    public class Upgrade0To1 : UpgradeBase
    {
        public override int? FromVersion
        {
            get { return null; }
        }

        public override int ToVersion
        {
            get { return 1; }
        }

        public override void Upgrade(SirenOfShameSettings sirenOfShameSettings)
        {
            var buildDefinitionSettings = sirenOfShameSettings.CiEntryPointSettings.SelectMany(i => i.BuildDefinitionSettings).ToList();
            foreach (var buildDefinitionSetting in buildDefinitionSettings)
            {
                var emptyPerson = buildDefinitionSetting.People.FirstOrDefault(string.IsNullOrEmpty);
                if (emptyPerson != null)
                {
                    buildDefinitionSetting.People.Remove(emptyPerson);
                }
            }

            sirenOfShameSettings.People = new List<PersonSetting>();
            var allPeople = buildDefinitionSettings.SelectMany(i => i.People);
            foreach (var person in allPeople)
            {
                sirenOfShameSettings.FindAddPerson(person);
            }
        }
    }
}
