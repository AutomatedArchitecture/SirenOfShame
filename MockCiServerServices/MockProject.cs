using System;
using System.Globalization;
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
            _status.SelectedIndex = 1;
            _localStartTime = DateTime.Now.AddMinutes(-8).AddSeconds(-4);
            _startedTime = DateTime.Now.AddMinutes(-8).AddSeconds(-4);
            _finishedTime = DateTime.Now;
            UpdateDateTimeTextboxes();
        }

        public string ProjectName
        {
            get { return _projectGroupBox.Text; }
            set
            {
                _projectGroupBox.Text = value;
            }
        }

        private string ProjectId
        {
            get { return ProjectName.Replace(" ", ""); }
        }

        public BuildStatus GetBuildStatus()
        {
            BuildStatus buildStatus = null;
            this.Invoke(() =>
                {
                    buildStatus = new BuildStatus
                    {
                        Name = ProjectName,
                        BuildDefinitionId = ProjectId,
                        BuildStatusEnum = _buildStatus,
                        Comment = _comment.Text,
                        FinishedTime = _finishedTime,
                        StartedTime = _startedTime,
                        RequestedBy = _requestedBy.Text,
                        LocalStartTime = _localStartTime,
                        BuildId = _startedTime.HasValue ? _startedTime.Value.Ticks.ToString(CultureInfo.InvariantCulture) : null,
                        Url = "http://www.google.com"
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

            UpdateDateTimeTextboxes();
        }

        private void UpdateDateTimeTextboxes()
        {
            _startTime.Text = _startedTime.ToString();
            _finishTime.Text = _finishedTime.ToString();
        }
    }
}
