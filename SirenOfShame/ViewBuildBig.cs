using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame
{
    public partial class ViewBuildBig : ViewBuildBase
    {
        public ViewBuildBig()
        {
            InitializeComponent();
            buildStats1.InitializeBuildHistoryChart();
        }

        public void InitializeForBuild(BuildStatusDto buildStatusDto)
        {
            InitializeLabels(buildStatusDto);
            InitializeBuildStats();
        }

        private void InitializeBuildStats()
        {
            if (BuildId == null) return;
            var sosDb = new SosDb();
            var allBuilds = sosDb.ReadAll(BuildId);
            buildStats1.GraphBuildHistory(allBuilds);
        }

        protected override void InitializeLabels(BuildStatusDto buildStatusDto)
        {
            base.InitializeLabels(buildStatusDto);

            InitializeStartTime(buildStatusDto);
            _duration.Text = buildStatusDto.Duration;
            _requestedBy.Text = buildStatusDto.RequestedByDisplayName;
            _comment.Text = buildStatusDto.Comment;
            _projectName.Text = buildStatusDto.Name;
            SetBuildStatusIcon(buildStatusDto);
            SetBackgroundColors(buildStatusDto.BuildStatusEnum);
            _details.Visible = !string.IsNullOrEmpty(buildStatusDto.Url);
        }

        private void SetBuildStatusIcon(BuildStatusDto buildStatusDto)
        {
            bool inProgress = buildStatusDto.BuildStatusEnum == BuildStatusEnum.InProgress;
            _buildStatusIcon.Visible = !inProgress;
            _loading.Visible = inProgress;
            _buildStatusIcon.ImageIndex = buildStatusDto.ImageIndex;
        }

        private void InitializeStartTime(BuildStatusDto buildStatusDto)
        {
            LocalStartTime = buildStatusDto.LocalStartTime;
            RecalculatePrettyDate();
        }

        private void SetBackgroundColors(BuildStatusEnum buildStatusEnum)
        {
            Color backgroundColor = GetBackgroundColor(buildStatusEnum);
            _projectName.BackColor = backgroundColor;
            _buildStatusIcon.BackColor = backgroundColor;
        }

        protected override Label GetStartTimeLabel()
        {
            return _startTime;
        }

        private void EditRulesClick(object sender, EventArgs e)
        {
            EditRulesClick(_editRules, _buildMenu, _affectsTrayIcon);
        }

        private void BuildMenuOpening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            BuildMenuOpening(_buildMenu, _when, _affectsTrayIcon, _stopWatching, _toolStripSeparator1);
        }

        private void StopWatchingClick(object sender, EventArgs e)
        {
            StopWatchingClick();
        }

        private void AffectsTrayIconClick(object sender, EventArgs e)
        {
            AffectsTrayIconClick();
        }

        private void DetailsClick(object sender, EventArgs e)
        {
            LaunchUrl();
        }

        public void RefreshStats()
        {
            InitializeBuildStats();
        }
    }
}
