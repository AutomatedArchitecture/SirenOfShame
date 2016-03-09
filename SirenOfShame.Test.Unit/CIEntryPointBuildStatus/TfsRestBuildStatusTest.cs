using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using SirenOfShame.Lib.Watcher;
using SirenOfShame.Test.Unit.Resources;
using TfsRestServices;

namespace SirenOfShame.Test.Unit.CIEntryPointBuildStatus
{
    [TestFixture]
    public class TfsRestBuildStatusTest
    {
        private CommentsCache GetCommentsCache(int buildDefinitionId, int buildId, string comment)
        {
            var commentsCache = new CommentsCache();

            var projects = GetTfsJsonBuilds(buildDefinitionId, buildId);
            commentsCache.FetchNewComments(projects, new TfsConnectionDetails(), GetCommentFunc(comment)).Wait();
            return commentsCache;
        }

        private static Func<TfsJsonBuild, TfsConnectionDetails, Task<string>> GetCommentFunc(string comment)
        {
            return (build, details) =>
            {
                var tsc = new TaskCompletionSource<string>();
                tsc.SetResult(comment);
                return tsc.Task;
            };
        }

        private static List<TfsJsonBuild> GetTfsJsonBuilds(int buildDefinitionId, int buildId)
        {
            List<TfsJsonBuild> projects = new List<TfsJsonBuild>
            {
                new TfsJsonBuild
                {
                    Id = buildId,
                    Definition = new TfsJsonBuildDefinition
                    {
                        Id = buildDefinitionId
                    }
                }
            };
            return projects;
        }

        [Test]
        public void GivenNoCachedCommentExists_WhenWeAsForComments_ThenTheyAreRetrieved()
        {
            var commentsCache = new CommentsCache();
            var tfsJsonBuilds = GetTfsJsonBuilds(1, 2);
            commentsCache.FetchNewComments(tfsJsonBuilds, new TfsConnectionDetails(), GetCommentFunc("some comment")).Wait();
            Assert.AreEqual("some comment", commentsCache.GetCachedCommentForBuild(tfsJsonBuilds[0]));
        }

        [Test]
        public void GivenOldCachedCommentExists_WhenBuildIdHasChanged_ThenNewCommentsAreRetrieved()
        {
            var commentsCache = new CommentsCache();
            var oldTfsJsonBuilds = GetTfsJsonBuilds(1, 2);
            commentsCache.FetchNewComments(oldTfsJsonBuilds, new TfsConnectionDetails(), GetCommentFunc("old comment")).Wait();
            var newTfsJsonBuilds = GetTfsJsonBuilds(1, 3);
            commentsCache.FetchNewComments(newTfsJsonBuilds, new TfsConnectionDetails(), GetCommentFunc("new comment")).Wait();
            Assert.AreEqual("new comment", commentsCache.GetCachedCommentForBuild(newTfsJsonBuilds[0]));
        }

        [Test]
        public void GivenOldCachedCommentExists_WhenBuildIdHasNotChanged_ThenNewCommentsAreNotRetrieved()
        {
            var commentsCache = new CommentsCache();
            var tfsJsonBuilds = GetTfsJsonBuilds(1, 2);
            commentsCache.FetchNewComments(tfsJsonBuilds, new TfsConnectionDetails(), GetCommentFunc("old comment")).Wait();
            commentsCache.FetchNewComments(tfsJsonBuilds, new TfsConnectionDetails(), GetCommentFunc("new comment")).Wait();
            Assert.AreEqual("old comment", commentsCache.GetCachedCommentForBuild(tfsJsonBuilds[0]));
        }

        [Test]
        public void GivenATraditionalXmlBuildDefinition_WhenWeParseIt_ThenWeParseItCorrectly()
        {
            var tfsRestWorkingBuild = ResourceManager.TfsRestBuildDefinitions1;
            var jsonWrapper = JsonConvert.DeserializeObject<TfsJsonWrapper<TfsJsonBuild>>(tfsRestWorkingBuild);
            var build = jsonWrapper.Value[1];
            var commentsCache = GetCommentsCache(build.Definition.Id, build.Id, "My comment");
            var buildStatus = new TfsRestBuildStatus(build, commentsCache);
            Assert.AreEqual(BuildStatusEnum.Working, buildStatus.BuildStatusEnum);
            Assert.AreEqual("2", buildStatus.BuildDefinitionId);
            Assert.AreEqual("TestingGitTfsOnlineSolution", buildStatus.Name);
            Assert.AreEqual("Lee Richardson", buildStatus.RequestedBy);
            Assert.AreEqual("My comment", buildStatus.Comment);
            Assert.AreEqual("https://sirenofshame.visualstudio.com/DefaultCollection/_permalink/_build/index?collectionId=3be0f19d-62d0-4f45-a140-f219cb9c08ae&projectId=cd1d630e-e0fc-46d3-9540-a477d17a84b1&buildId=18", buildStatus.Url);
            Assert.AreEqual("18", buildStatus.BuildId);
        }
   }
}
