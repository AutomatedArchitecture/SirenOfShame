using System;
using System.Xml.Linq;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace HudsonServices
{
    public class HudsonBuildStatus : BuildStatus
    {
        public HudsonBuildStatus(XDocument doc, BuildDefinitionSetting buildDefinitionSetting)
        {
            if (doc.Root == null) throw new Exception("Could not get root of xml");
            var changeSet = doc.Root.Element("changeSet");
            if (changeSet == null) throw new Exception("Could not find 'changeSet'");

            var resultStr = doc.Root.ElementValueOrDefault("result");
            if (!string.IsNullOrWhiteSpace(resultStr))
            {
                BuildStatusEnum = ToBuildStatusEnum(resultStr);
            }
            else
            {
                var building = doc.Root.ElementValueOrDefault("building");
                if (string.Equals(building, "true", StringComparison.InvariantCultureIgnoreCase))
                {
                    BuildStatusEnum = BuildStatusEnum.InProgress;
                }
                else
                {
                    BuildStatusEnum = BuildStatusEnum.Unknown;
                }
            }

            Id = buildDefinitionSetting.Id;
            Name = buildDefinitionSetting.Name;

            var changeSetItem = changeSet.Element("item");
            if (changeSetItem == null)
            {
                var actionElem = doc.Root.Element("action");
                if (actionElem == null) throw new Exception("Could not find 'action'");
                var causeElem = actionElem.Element("cause");
                if (causeElem == null) throw new Exception("Could not find action 'cause'");
                RequestedBy = causeElem.ElementValueOrDefault("userName");

                StartedTime = ParseTimestamp(doc.Root.ElementValueOrDefault("timestamp"));
            }
            else
            {
                var changeSetAuthor = changeSetItem.Element("author");
                if (changeSetAuthor == null) throw new Exception("Could not find changeSet item 'author'");
                RequestedBy = changeSetAuthor.ElementValueOrDefault("fullName");

                Comment = changeSetItem.ElementValueOrDefault("msg");

                var date = changeSetItem.ElementValueOrDefault("date");
                if (!string.IsNullOrWhiteSpace(date))
                {
                    StartedTime = ParseUnixTime(date);
                }
                else
                {
                    StartedTime = ParseTimestamp(doc.Root.ElementValueOrDefault("timestamp"));
                }
            }

            var durationStr = doc.Root.ElementValueOrDefault("duration");
            if (!string.IsNullOrWhiteSpace(durationStr))
            {
                var duration = int.Parse(durationStr);
                FinishedTime = StartedTime.AddMilliseconds(duration);
            }
        }

        private DateTime ParseTimestamp(string timestampStr)
        {
            var secs = TimeSpan.FromMilliseconds(double.Parse(timestampStr));
            return new DateTime(1970, 1, 1, 0, 0, 0) + secs;
        }

        private DateTime ParseUnixTime(string date)
        {
            var dateParts = date.Split('-');
            var secs = TimeSpan.FromSeconds(double.Parse(dateParts[0]) + double.Parse(dateParts[1]));
            return new DateTime(1970, 1, 1, 0, 0, 0) + secs;
        }

        private BuildStatusEnum ToBuildStatusEnum(string result)
        {
            if (result == null) return BuildStatusEnum.Unknown;
            result = result.Trim().ToUpperInvariant();
            switch (result)
            {
                case "SUCCESS":
                    return BuildStatusEnum.Working;
                case "FAILURE":
                    return BuildStatusEnum.Broken;
            }
            throw new Exception("Could not parse result '" + result + "'");
        }

    }
}
