using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame
{
    public enum ViewBuildDisplayMode
    {
        Normal = 0,
        Tiny = 1
    }
    
    public sealed partial class ViewBuildSmall : ViewBuildBase
    {
        const int NORMAL_HEIGHT = 132;
        const int TINY_HEIGHT = 60;
        public const int WIDTH = 230;
        public const int MARGIN = 4;
        private ViewBuildDisplayMode _displayMode;

        public ViewBuildSmall(BuildStatusDto buildStatusDto, SirenOfShameSettings settings)
            : base(settings)
        {
            InitializeComponent();
            InitializeLabels(buildStatusDto);
        }

        public void SetDisplayMode(ViewBuildDisplayMode displayMode)
        {
            _displayMode = displayMode;
            _editRulesTop.Visible = displayMode == ViewBuildDisplayMode.Tiny;
            _editRules.Visible = displayMode == ViewBuildDisplayMode.Normal;
            _comment.Visible = displayMode == ViewBuildDisplayMode.Normal;
            _duration.Visible = displayMode == ViewBuildDisplayMode.Normal;
            SetDetailsVisibility();
            Height = displayMode == ViewBuildDisplayMode.Normal ? NORMAL_HEIGHT : TINY_HEIGHT;
            Width = WIDTH;
        }

        private void SetDetailsVisibility()
        {
            _details.Visible = _displayMode == ViewBuildDisplayMode.Normal && !string.IsNullOrEmpty(Url);
        }

        protected override void InitializeLabels(BuildStatusDto buildStatusDto)
        {
            base.InitializeLabels(buildStatusDto);
            
            _projectName.Text = buildStatusDto.BuildDefinitionDisplayName;
            InitializeStartTime(buildStatusDto);
            _duration.Text = buildStatusDto.Duration;
            _requestedBy.Text = buildStatusDto.RequestedByDisplayName;
            _comment.Text = buildStatusDto.Comment;
            SetBuildStatusIcon(buildStatusDto);
            SetBackgroundColors(buildStatusDto.BuildStatusEnum);
            SetDetailsVisibility();
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
            _editRulesTop.BackColor = backgroundColor;
            _loading.BackColor = backgroundColor;
        }

        protected override Label GetStartTimeLabel()
        {
            return _startTime;
        }

        private void BuildMenuOpening(object sender, CancelEventArgs e)
        {
            BuildMenuOpening(_buildMenu, _when, _affectsTrayIcon, _stopWatching, _toolStripSeparator1);
        }

        private void EditRulesClick(object sender, EventArgs e)
        {
            EditRulesClick((Control)sender, _buildMenu, _affectsTrayIcon);
        }

        private void AffectsTrayIconClick(object sender, EventArgs e)
        {
            AffectsTrayIconClick();
        }

        private void StopWatchingClick(object sender, EventArgs e)
        {
            StopWatchingClick();
        }

        private void CommentClick(object sender, EventArgs e)
        {
            OnClick(e);
        }

        private void ProjectNameClick(object sender, EventArgs e)
        {
            OnClick(e);
        }

        private void RequestedByClick(object sender, EventArgs e)
        {
            OnClick(e);
        }

        private void StartTimeClick(object sender, EventArgs e)
        {
            OnClick(e);
        }

        private void DurationClick(object sender, EventArgs e)
        {
            OnClick(e);
        }

        private void DetailsClick(object sender, EventArgs e)
        {
            LaunchUrl();
        }

        private void CommentMouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void ProjectNameMouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void DurationMouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void RequestedByMouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void StartTimeMouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }


    }
}
