using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame
{
    public sealed partial class ViewBuildSmall : ViewBuildBase
    {
        public ViewBuildSmall(BuildStatusDto buildStatusDto, SirenOfShameSettings settings) : base(settings)
        {
            InitializeComponent();
            InitializeLabels(buildStatusDto);
        }

        protected override void InitializeLabels(BuildStatusDto buildStatusDto)
        {
            base.InitializeLabels(buildStatusDto);
            
            _projectName.Text = buildStatusDto.Name;
            InitializeStartTime(buildStatusDto);
            _duration.Text = buildStatusDto.Duration;
            _requestedBy.Text = buildStatusDto.RequestedBy;
            _comment.Text = buildStatusDto.Comment;
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

        private void DetailsLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Url) && Url.StartsWith("http"))
            {
                Process.Start(Url);
            }
        }

        protected override Label GetStartTimeLabel()
        {
            return _startTime;
        }

        private void BuildMenuOpening(object sender, CancelEventArgs e)
        {
            BuildMenuOpening(sender, e, _buildMenu, _when, _affectsTrayIcon, _stopWatching, _toolStripSeparator1);
        }

        private void EditRulesClick(object sender, EventArgs e)
        {
            EditRulesClick(_editRules, _buildMenu, _affectsTrayIcon);
        }

        private void AffectsTrayIconClick(object sender, EventArgs e)
        {
            AffectsTrayIconClick();
        }

        private void StopWatchingClick(object sender, EventArgs e)
        {
            StopWatchingClick();
        }


    }
}
