using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace BambooServices
{
    public class BambooBuildStatus : BuildStatus
    {
        private static readonly Dictionary<string, DateTime> _startTimeLookup = new Dictionary<string, DateTime>();

        public static BambooBuildStatus CreateIsBuilding(BuildDefinitionSetting buildDefinitionSetting)
        {
            BambooBuildStatus result = new BambooBuildStatus
            {
                BuildDefinitionId = buildDefinitionSetting.Id,
                Name = buildDefinitionSetting.Name,
                CurrentBuildStatus = BuildStatusEnum.InProgress
            };

            DateTime startTime;
            if (_startTimeLookup.TryGetValue(buildDefinitionSetting.Id, out startTime))
            {
                result.StartedTime = startTime;
            }
            else
            {
                result.StartedTime = DateTime.Now;
                _startTimeLookup.Add(buildDefinitionSetting.Id, result.StartedTime.Value);
            }

            return result;
        }

        /// <param name="rootUrl">Must not end with /</param>
        public static BambooBuildStatus CreateBuildResult(XDocument doc, BuildDefinitionSetting buildDefinitionSetting, string rootUrl)
        {
            BambooBuildStatus result = new BambooBuildStatus
            {
                BuildDefinitionId = buildDefinitionSetting.Id,
                Name = buildDefinitionSetting.Name
            };

            if (doc.Root == null) throw new Exception("Invalid root element");

            var stateStr = doc.Root.AttributeValueOrDefault("state");

            result.StartedTime = ParseDateTime(doc.Root.ElementValueOrDefault("buildStartedTime"));
            result.FinishedTime = ParseDateTime(doc.Root.ElementValueOrDefault("buildCompletedTime"));

            result.CurrentBuildStatus = ToBuildStatusEnum(stateStr);

            var changesElem = doc.Root.Element("changes");
            if (changesElem != null)
            {
                var changeElem = changesElem.Element("change");
                if (changeElem != null)
                {
                    result.RequestedBy = changeElem.AttributeValueOrDefault("author");
                    result.Comment = changeElem.ElementValueOrDefault("comment");
                }
            }

            result.BuildId = doc.Root.AttributeValueOrDefault("number");
            string buildKey = doc.Root.AttributeValueOrDefault("key"); // i.e. the "plan key"
            result.Url = rootUrl + "/browse/" + buildKey;

            if (string.IsNullOrWhiteSpace(result.RequestedBy) && string.IsNullOrWhiteSpace(result.Comment))
            {
                var buildReason = doc.Root.ElementValueOrDefault("buildReason");
                result.Comment = GetCommentFromBuildReason(buildReason);
                result.RequestedBy = GetUserFromBuildReason(buildReason);
            }

            return result;
        }

        private static string GetCommentFromBuildReason(string buildReason)
        {
            buildReason = Regex.Replace(buildReason, "<a .*?>", "");
            buildReason = buildReason.Replace("</a>", "");
            return buildReason;
        }

        private static string GetUserFromBuildReason(string buildReason)
        {
            // <a href="http://192.168.117.128:8080/bamboo/browse/user/jferner">Joe Ferner</a>
            var match = Regex.Match(buildReason, "browse/user/(.*)\"");
            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            return null;
        }

        private static DateTime ParseDateTime(string elemVal)
        {
            if (string.IsNullOrWhiteSpace(elemVal))
            {
                throw new Exception("Could not parse date time");
            }
            return DateTime.Parse(elemVal);
        }


        private static BuildStatusEnum ToBuildStatusEnum(string result)
        {
            if (result == null) return BuildStatusEnum.Unknown;
            result = result.Trim().ToUpperInvariant();
            switch (result)
            {
                case "SUCCESSFUL":
                    return BuildStatusEnum.Working;
                case "FAILED":
                    return BuildStatusEnum.Broken;
            }
            throw new Exception("Could not parse result '" + result + "'");
        }
    }
}
