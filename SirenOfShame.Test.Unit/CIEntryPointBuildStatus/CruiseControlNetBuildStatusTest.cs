using System;
using System.Xml.Linq;
using CruiseControlNetServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;
using SirenOfShame.Test.Unit.CiEntryPointBuildStatus;
using SirenOfShame.Test.Unit.Resources;

namespace SirenOfShame.Test.Unit.CIEntryPointBuildStatus
{
    [TestClass]
    public class CruiseControlNetBuildStatusTest
    {
        [TestMethod]
        public void ParseCruiseControlDateToId_RemovesUpToDotAndRemovesNonNumeric()
        {
            string actual = CruiseControlNetBuildStatus.ParseCruiseControlDateToId("2012-02-25T22:56:14.4092432-05:00");
            Assert.AreEqual("20120225225614", actual);
        }

        [TestMethod]
        public void CruiseControlNetBuildStatus_BrokenWithAuthor()
        {
            var document = ResourceManager.CruiseControlNetBrokenWithAuthor;
            BuildDefinitionSetting buildDefinitionSetting = new BuildDefinitionSetting();
            buildDefinitionSetting.Name = "Name";
            buildDefinitionSetting.Id = "BuildDefinitionId";
            XElement projectElement = document.Root.Element("Project");
            CruiseControlNetBuildStatus.ClearCache();
            CruiseControlNetBuildStatus buildStatus = new CruiseControlNetBuildStatus(projectElement);

            Assert.AreEqual(BuildStatusEnum.Broken, buildStatus.BuildStatusEnum);
            Assert.AreEqual("Lee", buildStatus.RequestedBy);
            Assert.IsNotNull(buildStatus.StartedTime);
            AssertAreClose(new DateTime(2012, 8, 16, 19, 23, 34, 276), buildStatus.StartedTime.Value);
            Assert.IsNull(buildStatus.Comment);
            Assert.IsNotNull(buildStatus.FinishedTime);
            AssertAreClose(new DateTime(2012, 8, 16, 19, 23, 34, 276), buildStatus.FinishedTime.Value);
            Assert.AreEqual("20120816192334", buildStatus.BuildId);
        }

        private void AssertAreClose(DateTime expected, DateTime actual)
        {
            var diff = expected - actual;
            Assert.IsTrue(diff.TotalSeconds <= 1, HudsonBuildStatusTest.DateAsCode(actual));
        }

        [TestMethod]
        public void CruiseControlNetBuildStatus_InProgress()
        {
            var document = ResourceManager.CruiseControlNetJoesProject1;
            BuildDefinitionSetting buildDefinitionSetting = new BuildDefinitionSetting();
            buildDefinitionSetting.Name = "Name";
            buildDefinitionSetting.Id = "BuildDefinitionId";
            XElement projectElement = document.Root.Element("Project");
            CruiseControlNetBuildStatus.ClearCache();
            CruiseControlNetBuildStatus buildStatus = new CruiseControlNetBuildStatus(projectElement);

            Assert.AreEqual(BuildStatusEnum.InProgress, buildStatus.BuildStatusEnum);
            Assert.AreEqual("CruiseControlNetProj1", buildStatus.BuildDefinitionId);
            Assert.AreEqual("CruiseControlNetProj1", buildStatus.Name);
            Assert.AreEqual(null, buildStatus.RequestedBy);
            Assert.IsNotNull(buildStatus.StartedTime);
            Assert.AreEqual(new DateTime(2011, 8, 28, 17, 20, 5, 375), buildStatus.StartedTime.Value.ToUniversalTime(), HudsonBuildStatusTest.DateAsCode(buildStatus.StartedTime.Value.ToUniversalTime()));
            Assert.IsNull(buildStatus.Comment);
            Assert.IsNotNull(buildStatus.FinishedTime);
            Assert.AreEqual(new DateTime(2011, 8, 28, 17, 20, 5, 375), buildStatus.FinishedTime.Value.ToUniversalTime(), HudsonBuildStatusTest.DateAsCode(buildStatus.FinishedTime.Value.ToUniversalTime()));
            //Assert.AreEqual("http://win7ci:8081/job/SvnTest/30/", buildStatus.Url);
            //Assert.AreEqual(30, buildStatus.BuildId);
        }
        
        [TestMethod]
        public void CruiseControlNetBuildStatus_ChangesToInProgress()
        {
            var inProgressStatus = ResourceManager.CruiseControlNetJoesProject1;
            var notInProgressStatus = ResourceManager.CruiseControlNetJoesProject2;
            BuildDefinitionSetting buildDefinitionSetting = new BuildDefinitionSetting();
            buildDefinitionSetting.Name = "Name";
            buildDefinitionSetting.Id = "BuildDefinitionId";
            XElement notInProgressStatusProjectElement = notInProgressStatus.Root.Element("Project");
            XElement inProgressStatusProjectElement = inProgressStatus.Root.Element("Project");
            CruiseControlNetBuildStatus.ClearCache();
            new CruiseControlNetBuildStatus(notInProgressStatusProjectElement);
            CruiseControlNetBuildStatus buildStatus = new CruiseControlNetBuildStatus(inProgressStatusProjectElement);

            Assert.AreEqual(BuildStatusEnum.InProgress, buildStatus.BuildStatusEnum);
            Assert.AreEqual("CruiseControlNetProj1", buildStatus.BuildDefinitionId);
            Assert.AreEqual("CruiseControlNetProj1", buildStatus.Name);
            Assert.AreEqual(null, buildStatus.RequestedBy);
            Assert.IsNotNull(buildStatus.StartedTime);
            Assert.AreEqual(DateTime.Now.ToString(), buildStatus.StartedTime.Value.ToString());
            Assert.IsNull(buildStatus.Comment);
            Assert.IsNull(buildStatus.FinishedTime);
            Assert.AreEqual("http://VMXP/ccnet/server/local/project/CruiseControlNetProj1/build/log20110828202005.xml/ViewBuildReport.aspx", buildStatus.Url);
            Assert.AreEqual("3", buildStatus.BuildId);
        }
        
        [TestMethod]
        public void CruiseControlNetBuildStatus_TwoBackToBackNotStartedBuilds_StartedTimeShouldBeLastBuild()
        {
            var document = ResourceManager.CruiseControlNetJoesProject2;
            BuildDefinitionSetting buildDefinitionSetting = new BuildDefinitionSetting();
            buildDefinitionSetting.Name = "Name";
            buildDefinitionSetting.Id = "BuildDefinitionId";
            XElement projectElement = document.Root.Element("Project");
            CruiseControlNetBuildStatus.ClearCache();
            new CruiseControlNetBuildStatus(projectElement);
            CruiseControlNetBuildStatus buildStatus = new CruiseControlNetBuildStatus(projectElement);

            Assert.AreEqual(BuildStatusEnum.Unknown, buildStatus.BuildStatusEnum);
            Assert.AreEqual("CruiseControlNetProj1", buildStatus.BuildDefinitionId);
            Assert.AreEqual("CruiseControlNetProj1", buildStatus.Name);
            Assert.AreEqual(null, buildStatus.RequestedBy);
            Assert.IsNotNull(buildStatus.StartedTime);
            Assert.AreEqual(new DateTime(2011, 8, 27, 17, 21, 27, 843).ToString(), buildStatus.StartedTime.Value.ToUniversalTime().ToString(), HudsonBuildStatusTest.DateAsCode(buildStatus.StartedTime.Value.ToUniversalTime()));
            Assert.IsNull(buildStatus.Comment);
            Assert.IsNotNull(buildStatus.FinishedTime);
            Assert.AreEqual(new DateTime(2011, 8, 27, 17, 21, 27, 843).ToString(), buildStatus.FinishedTime.Value.ToUniversalTime().ToString(), HudsonBuildStatusTest.DateAsCode(buildStatus.FinishedTime.Value.ToUniversalTime()));
            //Assert.AreEqual("http://win7ci:8081/job/SvnTest/30/", buildStatus.Url);
            //Assert.AreEqual(30, buildStatus.BuildId);
        }
    }
}