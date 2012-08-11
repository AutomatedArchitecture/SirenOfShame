using System;
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
            InitializeBuildStats(buildStatusDto);
        }

        private void InitializeBuildStats(BuildStatusDto buildStatusDto)
        {
            var sosDb = new SosDb();
            var lastFiveBuilds = sosDb.ReadAll(buildStatusDto.Id).Reverse().Take(5);
            buildStats1.GraphBuildHistory(lastFiveBuilds.ToList());
        }

        protected override void InitializeLabels(BuildStatusDto buildStatusDto)
        {
            base.InitializeLabels(buildStatusDto);

            InitializeStartTime(buildStatusDto);
            _duration.Text = buildStatusDto.Duration;
            _requestedBy.Text = buildStatusDto.RequestedBy;
            _comment.Text = buildStatusDto.Comment;
            _projectName.Text = buildStatusDto.Name;
            _buildStatusIcon.ImageIndex = buildStatusDto.ImageIndex;
            SetBackgroundColors(buildStatusDto.BuildStatusEnum);
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

        private void EditRulesClick(object sender, System.EventArgs e)
        {
            EditRulesClick(_editRules, _buildMenu, _affectsTrayIcon);
        }

        private void BuildMenuOpening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            BuildMenuOpening(sender, e, _buildMenu, _when, _affectsTrayIcon, _stopWatching, _toolStripSeparator1);
        }

        private void StopWatchingClick(object sender, EventArgs e)
        {
            StopWatchingClick();
        }

        private void AffectsTrayIconClick(object sender, EventArgs e)
        {
            AffectsTrayIconClick();
        }

    }
}
