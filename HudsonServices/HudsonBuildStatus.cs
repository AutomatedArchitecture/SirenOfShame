using System;
using System.Xml.Linq;
using SirenOfShame.Lib;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;
using log4net;

namespace HudsonServices
{
    using System.Xml.XPath;

    public class HudsonBuildStatus : BuildStatus
    {
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(HudsonService));

        public HudsonBuildStatus(XDocument doc, BuildDefinitionSetting buildDefinitionSetting)
        {
            try
            {
                BuildDefinitionId = buildDefinitionSetting.Id;
                Name = buildDefinitionSetting.Name;
                BuildStatusEnum = BuildStatusEnum.Unknown;
                StartedTime = DateTime.MinValue;
                FinishedTime = DateTime.MinValue;
                RequestedBy = "No builds found";

                if (doc == null) return;

                if (doc.Root == null) throw new Exception("Could not get root of xml");
                var changeSet = doc.Root.Element("changeSet");
                if (changeSet == null) throw new Exception("Could not find 'changeSet'");

                var resultStr = doc.Root.ElementValueOrDefault("result");
                if (!string.IsNullOrWhiteSpace(resultStr))
                {
                    BuildStatusEnum = ToBuildStatusEnum(resultStr);
                } else
                {
                    var building = doc.Root.ElementValueOrDefault("building");
                    if (string.Equals(building, "true", StringComparison.InvariantCultureIgnoreCase))
                    {
                        BuildStatusEnum = BuildStatusEnum.InProgress;
                    } else
                    {
                        BuildStatusEnum = BuildStatusEnum.Unknown;
                    }
                }

                var changeSetItem = changeSet.Element("item");
                string timestamp = doc.Root.ElementValueOrDefault("timestamp");
                if (changeSetItem == null)
                {
                    var userName = GetRequestedByFromCause(doc);
                    RequestedBy = userName;

                    StartedTime = ParseTimestamp(timestamp);
                } else
                {
                    var changeSetAuthor = changeSetItem.Element("author");
                    if (changeSetAuthor != null)
                    {
                        RequestedBy = changeSetAuthor.ElementValueOrDefault("fullName");
                    } else
                    {
                        var userElem = changeSetItem.Element("user");
                        if (userElem == null) throw new Exception("Could not find author or user on changeset");
                        RequestedBy = userElem.Value;
                    }

                    Comment = changeSetItem.ElementValueOrDefault("msg");

                    if (string.IsNullOrWhiteSpace(timestamp))
                    {
                        var date = changeSetItem.ElementValueOrDefault("date");
                        if (string.IsNullOrWhiteSpace(date))
                        {
                            throw new Exception("Could find neither a 'date' nor a 'timestamp' in order to calculate StartedTime.");
                        }
                        StartedTime = ParseUnixTime(date);
                    } else
                    {
                        StartedTime = ParseTimestamp(timestamp);
                    }
                }

                var durationStr = doc.Root.ElementValueOrDefault("duration");
                if (!string.IsNullOrWhiteSpace(durationStr))
                {
                    var duration = int.Parse(durationStr);
                    FinishedTime = StartedTime == null ? (DateTime?) null : StartedTime.Value.AddMilliseconds(duration);
                }

                Url = doc.Root.ElementValueOrDefault("url");
                if (Url != null)
                    Url = Url.Trim();

                BuildId = doc.Root.ElementValueOrDefault("number");
            } 
            catch (Exception)
            {
                _log.Error("Error parsing the following xml: " + doc);
                throw;
            }
        }

        private static string GetRequestedByFromCause(XDocument doc)
        {
            if (doc.Root == null) return null;
            var causeElem = doc.Root.XPathSelectElement("//action/cause");
            if (causeElem == null)
            {
                _log.Warn("Could not find 'cause'");
                return null;
            }

            string userName = causeElem.ElementValueOrDefault("userName");
            return userName;
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
                case "UNSTABLE":
                    return BuildStatusEnum.Broken;
                default:
                    return BuildStatusEnum.Unknown;
            }
        }

    }
}
