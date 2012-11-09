using System;
using System.Globalization;
using System.Xml.Linq;
using System.Xml.XPath;

using SirenOfShame.Lib;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

using log4net;

namespace HudsonServices
{
    public class HudsonBuildStatus : BuildStatus
    {
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(HudsonService));

        public HudsonBuildStatus(XDocument doc, BuildDefinitionSetting buildDefinitionSetting, string defaultBuildStatusMessage = null)
        {
            try
            {
                InitialiseBuildStatus(buildDefinitionSetting, defaultBuildStatusMessage);
                if (doc == null) return;

                var docRoot = LocateDocumentRoot(doc);
                if (docRoot == null)
                {
                    _log.Error("Error parsing the following xml:" + Environment.NewLine + doc);
                    return;
                }

                DetermineBuildUrl(docRoot);
                DetermineBuildId(docRoot);
                DetermineBuildStatus(docRoot);

                var changeSet = GetElementByName(docRoot, "changeSet");
                if (changeSet == null)
                {
                    _log.Error("ChangeSet not found in the following xml:" + Environment.NewLine + doc);
                    return;
                }

                var changeSetItem = GetElementByName(changeSet, "item");

                DetermineBuildTimes(docRoot, changeSetItem);
                DetermineChangeDetails(docRoot, changeSetItem);
            }
            catch (Exception)
            {
                _log.Error("Error parsing the following xml:" + Environment.NewLine + doc);
                throw;
            }
        }

        private void DetermineBuildId(XElement docRoot)
        {
            BuildId = docRoot.ElementValueOrDefault("number");
        }

        private void DetermineBuildStatus(XElement docRoot)
        {
            var resultStr = docRoot.ElementValueOrDefault("result");
            if (!string.IsNullOrWhiteSpace(resultStr))
            {
                BuildStatusEnum = ToBuildStatusEnum(resultStr);
            }
            else
            {
                var building = docRoot.ElementValueOrDefault("building");
                BuildStatusEnum = string.Equals(building, "true", StringComparison.InvariantCultureIgnoreCase)
                                      ? BuildStatusEnum.InProgress
                                      : BuildStatusEnum.Unknown;
            }
        }

        private void DetermineBuildTimes(XElement docRoot, XElement changeSetItem)
        {
            StartedTime = null;
            FinishedTime = null;

            var timestamp = docRoot.ElementValueOrDefault("timestamp");
            if (string.IsNullOrWhiteSpace(timestamp))
            {
                var date = changeSetItem.ElementValueOrDefault("date");
                if (string.IsNullOrWhiteSpace(date))
                {
                    BuildStatusMessage = "Could find neither a 'date' nor a 'timestamp' in order to calculate StartedTime.";
                    return;
                }
                StartedTime = ParseUnixTime(date);
            }
            else
            {
                StartedTime = ParseTimestamp(timestamp);
            }

            var durationStr = docRoot.ElementValueOrDefault("duration");
            if (string.IsNullOrWhiteSpace(durationStr))
            {
                BuildStatusMessage = "Could find a 'duration' to calculate FinishedTime.";
                return;
            }

            var duration = int.Parse(durationStr);
            FinishedTime = StartedTime == null ? (DateTime?)null : StartedTime.Value.AddMilliseconds(duration);
        }

        private void DetermineBuildUrl(XElement docRoot)
        {
            Url = docRoot.ElementValueOrDefault("url");
            if (Url != null) Url = Url.Trim();
        }

        private void DetermineChangeAuthorFromCommitInfo(XElement changeSetItem)
        {
            var changeSetAuthor = GetElementByName(changeSetItem, "author", _log);
            if (changeSetAuthor != null)
            {
                RequestedBy = changeSetAuthor.ElementValueOrDefault("fullName");
            }
            else
            {
                var userElem = GetElementByName(changeSetItem, "user", _log);
                if (userElem != null)
                {
                    RequestedBy = userElem.Value;
                }
                else
                {
                    BuildStatusMessage = "Could find neither an 'author' nor a 'user' in order to determine change author.";
                }
            }
        }

        private void DetermineChangeCommentFromBuildCause(XElement causeElement)
        {
            Comment = causeElement.ElementValueOrDefault("shortDescription");
        }

        private void DetermineChangeCommentFromCommitInfo(XElement changeSetItem)
        {
            Comment = changeSetItem.ElementValueOrDefault("msg");
        }

        private void DetermineChangeDetails(XElement docRoot, XElement changeSetItem)
        {
            if (changeSetItem == null)
            {
                var causeElement = GetElementByXPath(docRoot, "//action/cause");
                if (causeElement == null) return;
                DetermineChangeInitiatorFromBuildCause(causeElement);
                DetermineChangeCommentFromBuildCause(causeElement);
            }
            else
            {
                DetermineChangeAuthorFromCommitInfo(changeSetItem);
                DetermineChangeCommentFromCommitInfo(changeSetItem);
            }
        }

        private void DetermineChangeInitiatorFromBuildCause(XElement causeElement)
        {
            RequestedBy = causeElement.ElementValueOrDefault("userName");
        }

        private XElement GetElementByName(XContainer element, string nodeName, ILog logger = null)
        {
            var node = element.Element(nodeName);
            if (node == null)
            {
                BuildStatusMessage = string.Format(CultureInfo.CurrentCulture, "Could not find node '{0}' in the project xml", nodeName);
                if (logger != null) logger.Debug(BuildStatusMessage + Environment.NewLine + element);
            }
            return node;
        }

        private XElement GetElementByXPath(XContainer element, string xpath, ILog logger = null)
        {
            var node = element.XPathSelectElement(xpath);
            if (node == null)
            {
                BuildStatusMessage = string.Format(CultureInfo.CurrentCulture, "Could not find node '{0}' in the project xml", xpath);
                if (logger != null) logger.Debug(BuildStatusMessage + Environment.NewLine + element);
            }
            return node;
        }

        private void InitialiseBuildStatus(BuildDefinitionSetting buildDefinitionSetting, string defaultBuildStatusMessage)
        {
            BuildDefinitionId = buildDefinitionSetting.Id;
            Name = buildDefinitionSetting.Name;
            BuildStatusEnum = BuildStatusEnum.Unknown;
            StartedTime = null;
            FinishedTime = null;
            BuildStatusMessage = defaultBuildStatusMessage ?? string.Empty;
        }

        private XElement LocateDocumentRoot(XDocument doc)
        {
            var docRoot = doc.Root;
            if (docRoot == null)
            {
                BuildStatusMessage = "Unable to parse project xml";
                _log.Debug(BuildStatusMessage + Environment.NewLine + doc);
            }
            return docRoot;
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
