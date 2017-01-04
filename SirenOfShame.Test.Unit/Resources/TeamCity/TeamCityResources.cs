using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SirenOfShame.Test.Unit.Resources.TeamCity
{
    public static class TeamCityResources
    {
        public static XDocument TeamCityFailingBuild
        {
            get { return XDocument.Parse(ResourceManager.GetResource("TeamCity.TeamCityFailingBuild.xml")); }
        }

        public static XDocument TeamCityFailingChange
        {
            get { return XDocument.Parse(ResourceManager.GetResource("TeamCity.TeamCityFailingChange.xml")); }
        }

        public static XDocument TeamCityServerCleanup
        {
            get { return XDocument.Parse(ResourceManager.GetResource("TeamCity.TeamCityServerCleanup.xml")); }
        }

        public static XDocument TeamCityFailureDueToCleanup
        {
            get { return XDocument.Parse(ResourceManager.GetResource("TeamCity.TeamCityFailureDueToCleanup.xml")); }
        }

        public static XDocument TeamCity_10004_BuildInfo
        {
            get { return XDocument.Parse(ResourceManager.GetResource("TeamCity._10._0._0._4.BuildInfo.xml")); }
        }

        public static XDocument TeamCity_10004_changeInfo
        {
            get { return XDocument.Parse(ResourceManager.GetResource("TeamCity._10._0._0._4.ChangeInfo.xml")); }
        }
    }
}
