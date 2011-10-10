using System;
using System.Windows.Forms;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.Watcher;

namespace MockCiServerServices
{
    public partial class MockProject : UserControl
    {
        private DateTime? _finishedTime;
        private DateTime? _startedTime;
        private DateTime _localStartTime;
        private BuildStatusEnum _buildStatus;

        public MockProject()
        {
            InitializeComponent();
        }

        public string ProjectName
        {
            get { return _projectGroupBox.Text; }
            set
            {
                _projectGroupBox.Text = value;
            }
        }

        public BuildStatus GetBuildStatus()
        {
            BuildStatus buildStatus = null;
            this.Invoke(() =>
                {
                    buildStatus = new BuildStatus
                    {
                        Name = ProjectName,
                        BuildDefinitionId = ProjectName,
                        BuildStatusEnum = _buildStatus,
                        Comment = _comment.Text,
                        FinishedTime = _finishedTime,
                        StartedTime = _startedTime,
                        RequestedBy = _requestedBy.Text,
                        LocalStartTime = _localStartTime
                    };
                });
            return buildStatus;
        }

        private void _status_SelectedIndexChanged(object sender, EventArgs e)
        {
            var newBuildStatus = (BuildStatusEnum)Enum.Parse(typeof(BuildStatusEnum), _status.Text);

            if ((_buildStatus == BuildStatusEnum.Unknown || _buildStatus == BuildStatusEnum.Working || _buildStatus == BuildStatusEnum.Broken)
                && newBuildStatus == BuildStatusEnum.InProgress)
            {
                _localStartTime = DateTime.Now;
                _startedTime = DateTime.Now;
            }
            else if (_buildStatus == BuildStatusEnum.InProgress && (newBuildStatus == BuildStatusEnum.Working || newBuildStatus == BuildStatusEnum.Broken))
            {
                _finishedTime = DateTime.Now;
            }

            _buildStatus = newBuildStatus;

            _startTime.Text = _startedTime.ToString();
            _finishTime.Text = _finishedTime.ToString();
        }
    }
}
