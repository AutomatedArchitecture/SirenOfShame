using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using SirenOfShame.Lib.Exceptions;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.Watcher;

namespace MockCiServerServices
{
    public partial class MockCiServerForm : Form
    {
        public MockCiServerForm()
        {
            InitializeComponent();
            _project1.ProjectName = "Project 1";
            _project2.ProjectName = "Project 2";
            _project3.ProjectName = "Project 3";
        }

        public IList<BuildStatus> GetBuildStatus()
        {
            if (_serverUnavailable.Checked)
            {
                throw new ServerUnavailableException("Message", new Exception("Inner Message"));
            }
            IList<BuildStatus> results = null;
            this.Invoke(() =>
            {
                var configurableBuilds = new List<BuildStatus> {
                        _project1.GetBuildStatus(),
                        _project2.GetBuildStatus(),
                        _project3.GetBuildStatus()
                    };
                var additionalBuilds = GetAdditionalBuilds();
                results = configurableBuilds.Union(additionalBuilds).ToList();
            });
            return results;
        }

        private DateTime startedTime = DateTime.Now;
        
        private IEnumerable<BuildStatus> GetAdditionalBuilds()
        {
            var additionalBuilds = GetAdditionalBuildsCount();

            for (int i = 0; i < additionalBuilds; i++)
            {
                yield return new BuildStatus
                {
                    BuildStatusEnum = BuildStatusEnum.Working,
                    Name = "Build " + i,
                    StartedTime = startedTime,
                    FinishedTime = startedTime.AddMinutes(1).AddSeconds(2),
                    RequestedBy = "Lee",
                    Comment = "Performing check-in on build #" + i,
                    BuildDefinitionId = "#" + i,
                    BuildId = "#" + i,
                };
            }
        }

        private int GetAdditionalBuildsCount()
        {
            if (_additionalBuilds == null) return 0;
            if (string.IsNullOrEmpty(_additionalBuilds.Text)) return 0;
            return int.Parse(_additionalBuilds.Text);
        }

        public void StopWatching()
        {

        }
    }
}
