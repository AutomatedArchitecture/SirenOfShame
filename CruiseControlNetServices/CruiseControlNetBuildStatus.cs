using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.Watcher;

namespace CruiseControlNetServices
{
    /*
     * <Project
     *   name="CruiseControlNetProj2"
     *   category=""
     *   activity="Sleeping"
     *   lastBuildStatus="Unknown"
     *   lastBuildLabel="UNKNOWN"
     *   lastBuildTime="2011-08-27T20:21:27.84375+03:00"
     *   nextBuildTime="2011-08-28T20:38:01.71875+03:00"
     *   webUrl="http://VMXP/ccnet"
     *   CurrentMessage=""
     *   BuildStage=""
     *   serverName="local"
     *   description="">
     *   <messages/>
     * </Project>
     * 
     * <Project name="CruiseControlNetProj1" category="" activity="Building" lastBuildStatus="Success" lastBuildLabel="3" lastBuildTime="2011-08-28T20:20:05.375+03:00" nextBuildTime="2011-08-28T21:06:06.3125+03:00" webUrl="http://VMXP/ccnet" CurrentMessage="Build (IfModificationExists) triggered from IntervalTrigger" 
     *   BuildStage="<data> <Item Time="2011-08-28 21:06:07" Data="Executing run.bat" /> <Item Time="2011-08-28 21:06:07" Data="C:\Program Files\CruiseControl.NET\server\CruiseControlNetProj1\WorkingDirectory>echo Test2 " /> <Item Time="2011-08-28 21:06:07" Data="Test2" /> <Item Time="2011-08-28 21:06:07" Data="C:\Program Files\CruiseControl.NET\server\CruiseControlNetProj1\WorkingDirectory>ping -n 11 www.google.com " /> <Item Time="2011-08-28 21:06:07" Data="Pinging www.l.google.com [74.125.115.103] with 32 bytes of data:" /> <Item Time="2011-08-28 21:06:07" Data="Reply from 74.125.115.103: bytes=32 time=29ms TTL=128" /> </data> " 
     *   serverName="local" description="">
     *   <messages>
     *     <message text="Build (IfModificationExists) triggered from IntervalTrigger" kind="BuildStatus"/>
     *   </messages>
     * </Project>
     */
    public class CruiseControlNetBuildStatus : BuildStatus
    {
        private static readonly Dictionary<string, BuildStatusInfo> _buildStatusInfo = new Dictionary<string, BuildStatusInfo>();

        public static string ParseCruiseControlDateToId(string date)
        {
            if (string.IsNullOrWhiteSpace(date)) return null;
            var priorToDot = date.Split('.').FirstOrDefault();
            if (priorToDot == null) return null;
            return Regex.Replace(priorToDot, "[^0-9]", "");
        }
        
        public CruiseControlNetBuildStatus(XElement projectElem)
        {
            Name = BuildDefinitionId = projectElem.AttributeValue("name");

            BuildStatusInfo buildStatusInfo;
            if (!_buildStatusInfo.TryGetValue(BuildDefinitionId, out buildStatusInfo))
            {
                buildStatusInfo = new BuildStatusInfo();
                _buildStatusInfo.Add(BuildDefinitionId, buildStatusInfo);
            }

            var lastBuildTimeStr = projectElem.AttributeValueOrDefault("lastBuildTime");
            DateTime dt;
            DateTime? lastBuildTime = null;
            if (DateTime.TryParse(lastBuildTimeStr, out dt))
            {
                lastBuildTime = dt;
            }

            BuildStatusEnum = ToBuildStatusEnum(projectElem.AttributeValueOrDefault("activity"), projectElem.AttributeValueOrDefault("lastBuildStatus"));
            StartedTime = GetStartedTime(buildStatusInfo, BuildStatusEnum, lastBuildTime);
            FinishedTime = GetFinishedTime(buildStatusInfo, BuildStatusEnum, lastBuildTime);
            Comment = null;
            RequestedBy = null;

            var webUrl = projectElem.AttributeValueOrDefault("webUrl");
            string lastBuildTimeAsId = ParseCruiseControlDateToId(lastBuildTimeStr);
            Url = string.Format("{0}/server/local/project/{1}/build/log{2}.xml/ViewBuildReport.aspx", webUrl, Name, lastBuildTimeAsId);
            BuildId = lastBuildTimeAsId;

            buildStatusInfo.LastBuildStatusEnum = BuildStatusEnum;
        }

        private DateTime? GetFinishedTime(BuildStatusInfo buildStatusInfo, BuildStatusEnum buildStatusEnum, DateTime? lastBuildTime)
        {
            if (buildStatusInfo.LastBuildStatusEnum == null)
            {
                return lastBuildTime;
            }
            var lastBuildStatusEnum = buildStatusInfo.LastBuildStatusEnum.Value;

            // the build stopped since our last poll time so update the finished time
            if (buildStatusEnum != BuildStatusEnum.InProgress && lastBuildStatusEnum == BuildStatusEnum.InProgress)
            {
                buildStatusInfo.FinishedTime = DateTime.Now;
            }

            if (buildStatusInfo.FinishedTime == null && buildStatusEnum != BuildStatusEnum.InProgress)
                return lastBuildTime;

            return buildStatusInfo.FinishedTime;
        }

        private static DateTime? GetStartedTime(BuildStatusInfo buildStatusInfo, BuildStatusEnum buildStatusEnum, DateTime? lastBuildTime)
        {
            bool thisIsTheFirstBuild = buildStatusInfo.LastBuildStatusEnum == null;
            if (thisIsTheFirstBuild)
            {
                return lastBuildTime;
            }
            var lastBuildStatusEnum = buildStatusInfo.LastBuildStatusEnum.Value;

            // the build started since our last poll time so update the start time
            if (buildStatusEnum == BuildStatusEnum.InProgress && lastBuildStatusEnum != BuildStatusEnum.InProgress)
            {
                buildStatusInfo.StartedTime = DateTime.Now;
            }

            if (buildStatusInfo.StartedTime == null)
                return lastBuildTime;

            return buildStatusInfo.StartedTime;
        }

        private BuildStatusEnum ToBuildStatusEnum(string activity, string lastBuildStatus)
        {
            if (string.Equals(activity, "Building", StringComparison.InvariantCultureIgnoreCase))
            {
                return BuildStatusEnum.InProgress;
            }
            if (string.Equals(lastBuildStatus, "Success", StringComparison.InvariantCultureIgnoreCase))
            {
                return BuildStatusEnum.Working;
            }
            if (string.Equals(lastBuildStatus, "Failure", StringComparison.InvariantCultureIgnoreCase))
            {
                return BuildStatusEnum.Broken;
            }
            return BuildStatusEnum.Unknown;
        }

        private class BuildStatusInfo
        {
            public DateTime? StartedTime { get; set; }
            public DateTime? FinishedTime { get; set; }
            public BuildStatusEnum? LastBuildStatusEnum { get; set; }
        }

        public static void ClearCache()
        {
            _buildStatusInfo.Clear();
        }
    }
}
