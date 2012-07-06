using System;
using System.Xml.Linq;
using SirenOfShame.Lib;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;
using log4net;

namespace TravisCiServices
{
    public class TravisCiBuildStatus : BuildStatus
    {
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(TravisCiService));

        public TravisCiBuildStatus(TravisCiBuildDefinition travisCiBuildDefinition, string jsonDoc, BuildDefinitionSetting buildDefinitionSetting)
        {
            try
            {
                BuildStatusEnum = ToBuildStatusEnum(TravisCiService.GetJsonValue(jsonDoc, "result"));
                BuildDefinitionId = buildDefinitionSetting.Id;
                Name = buildDefinitionSetting.Name;
                RequestedBy = TravisCiService.GetJsonValue(jsonDoc, "author_name");
                StartedTime = TravisCiService.GetJsonDate(jsonDoc, "started_at");
                Comment = TravisCiService.GetJsonValue(jsonDoc, "message");
                FinishedTime = TravisCiService.GetJsonDate(jsonDoc, "finished_at");
                Url = "http://travis-ci.org/" + travisCiBuildDefinition.OwnerName + "/" + travisCiBuildDefinition.ProjectName + "/builds/" + TravisCiService.GetJsonValue(jsonDoc, "id");
                BuildId = TravisCiService.GetJsonValue(jsonDoc, "id");
            } 
            catch (Exception)
            {
                _log.Error("Error parsing the following json: " + jsonDoc);
                throw;
            }
        }

        private BuildStatusEnum ToBuildStatusEnum(string result)
        {
            if (result == null) return BuildStatusEnum.Unknown;
            result = result.Trim().ToUpperInvariant();
            switch (result)
            {
                case "0":
                    return BuildStatusEnum.Working;
                case "1":
                    return BuildStatusEnum.Broken;
                default:
                    return BuildStatusEnum.Unknown;
            }
        }

    }
}
