using System;
using System.Collections.Generic;
using System.Linq;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Lib.Settings {
	[Serializable]
	public class BuildDefinitionSetting {
		public BuildDefinitionSetting()
		{
		    People = new List<string>();
		}

        public BuildDefinitionSetting(MyBuildDefinition buildDefinition, string buildServer) : this()
        {
			Active = false;
			Id = buildDefinition.Id;
			Name = buildDefinition.Name;
            AffectsTrayIcon = true;
            BuildServer = buildServer;
        }

		public bool Active { get; set; }
		public string Id { get; set; }
		public string Name { get; set; }
        public bool AffectsTrayIcon { get; set; }
        public List<string> People { get; set; }
	    public string BuildServer { get; set; }

	    public BuildStatus AsUnknownBuildStatus()
	    {
	        return new BuildStatus
	        {
	            BuildStatusEnum = BuildStatusEnum.Unknown,
	            Id = Id,
	            Name = Name,
                StartedTime = DateTime.MinValue
	        };
	    }

        public bool ContainsPerson(BuildStatus buildStatus)
        {
            if (buildStatus == null) return false;
            return People.Any(p => p == buildStatus.RequestedBy);
        }
	}
}
