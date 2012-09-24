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

	    public BuildStatus AsUnknownBuildStatus(SosDb sosDb)
	    {
	        var lastKnownBuild = sosDb.ReadAll(this).LastOrDefault();
            var comment = lastKnownBuild == null ? null : lastKnownBuild.Comment;
            var startedTime = lastKnownBuild == null ? null : lastKnownBuild.StartedTime;
            // SosDb doesn't store local start time, so use the server's start time
            var localStartTime = lastKnownBuild == null ? DateTime.MinValue : lastKnownBuild.StartedTime ?? DateTime.MinValue;
	        var buildId = lastKnownBuild == null ? null : lastKnownBuild.BuildId;
            var requestedBy = lastKnownBuild == null ? null : lastKnownBuild.RequestedBy;
            var finishedTime = lastKnownBuild == null ? null : lastKnownBuild.FinishedTime;
	        var url = lastKnownBuild == null ? null : lastKnownBuild.Url;
	        
            return new BuildStatus
	        {
	            BuildStatusEnum = BuildStatusEnum.Unknown,
	            BuildDefinitionId = Id,
	            Name = Name,
                StartedTime = startedTime,
                Comment = comment,
                LocalStartTime = localStartTime,
                BuildId = buildId,
                RequestedBy = requestedBy,
                FinishedTime = finishedTime,
                Url = url
	        };
	    }

	    public bool ContainsPerson(BuildStatus buildStatus)
        {
            if (buildStatus == null) return false;
            return People.Any(p => p == buildStatus.RequestedBy);
        }
	}
}
