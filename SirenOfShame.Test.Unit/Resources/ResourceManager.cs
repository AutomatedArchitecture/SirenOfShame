using System;
using System.IO;
using System.Reflection;
using System.Xml.Linq;

namespace SirenOfShame.Test.Unit.Resources
{
    public static class ResourceManager
    {
        private static string GetResource(string resourceName)
        {
            string fullResourceName = "SirenOfShame.Test.Unit.Resources." + resourceName;

            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fullResourceName))
            {
                try
                {
                    if (stream == null) throw new Exception("Stream was null");
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Error retrieving Resource: '" + resourceName + "'. " + e);
                }

            }
        }

        public static string TravisCiWorkingBuild
        {
            get { return GetResource("TravisCiWorkingBuild.json"); }
        }

        public static string TravisFunkyDate
        {
            get { return GetResource("TravisFunkyDate.json"); }
        }

        public static XDocument JenkinsBuildStatusForIssue10
        {
            get { return XDocument.Parse(GetResource("JenkinsBuildStatusForIssue10.xml")); }
        }

        public static XDocument JenkinsPassingBuild
        {
            get { return XDocument.Parse(GetResource("JenkinsPassingBuild.xml")); }
        }

        public static XDocument Bug152HudsonDuration
        {
            get { return XDocument.Parse(GetResource("Bug152HudsonDuration.xml")); }
        }

        public static XDocument TeamCityFailingBuild
        {
            get { return XDocument.Parse(GetResource("TeamCityFailingBuild.xml")); }
        }

        public static XDocument TeamCityFailingChange
        {
            get { return XDocument.Parse(GetResource("TeamCityFailingChange.xml")); }
        }

        public static XDocument CruiseControlNetBrokenWithAuthor
        {
            get { return XDocument.Parse(GetResource("CruiseControlNetBrokenWithAuthor.xml")); }
        }
        
        public static XDocument CruiseControlNetJoesProject1
        {
            get { return XDocument.Parse(GetResource("CruiseControlNetJoesProject1.xml")); }
        }
        
        public static XDocument CruiseControlNetJoesProject2
        {
            get { return XDocument.Parse(GetResource("CruiseControlNetJoesProject2.xml")); }
        }

        public static XDocument BambooFailingBuild
        {
            get { return XDocument.Parse(GetResource("BambooFailingBuild.xml")); }
        }

        public static XDocument TeamCityServerCleanup
        {
            get { return XDocument.Parse(GetResource("TeamCityServerCleanup.xml")); }
        }

        public static XDocument TeamCityFailureDueToCleanup
        {
            get { return XDocument.Parse(GetResource("TeamCityFailureDueToCleanup.xml")); }
        }
    }
}
