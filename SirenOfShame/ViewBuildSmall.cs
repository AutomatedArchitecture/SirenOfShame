using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame
{
    public partial class ViewBuildSmall : UserControl
    {
        public ViewBuildSmall(BuildStatusDto buildStatusDto)
        {
            InitializeComponent();

            InitializeLabels(buildStatusDto);

        }

        private string _url;
        
        private void InitializeLabels(BuildStatusDto buildStatusDto)
        {
            BuildName = buildStatusDto.Name;
            _url = buildStatusDto.Url;
            
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
            _localStartTime = buildStatusDto.LocalStartTime;
            RecalculatePrettyDate();
        }

        private void SetBackgroundColors(BuildStatusEnum buildStatusEnum)
        {
            Color backgroundColor = GetBackgroundColor(buildStatusEnum);
            _projectName.BackColor = backgroundColor;
            _buildStatusIcon.BackColor = backgroundColor;
        }

        private Color GetBackgroundColor(BuildStatusEnum buildStatusEnum)
        {
            return GetColorForBuildType(buildStatusToColorMap, buildStatusEnum, Color.FromArgb(255, 40, 95, 152));
        }

        private static Color GetColorForBuildType(Dictionary<BuildStatusEnum, Color> dictionary, BuildStatusEnum newsItemEventType, Color defaultColor)
        {
            Color color;
            if (dictionary.TryGetValue(newsItemEventType, out color))
                return color;
            return defaultColor;
        }

        Dictionary<BuildStatusEnum, Color> buildStatusToColorMap = new Dictionary<BuildStatusEnum, Color>
        {
            { BuildStatusEnum.Working, Color.FromArgb(255, 50, 175, 82) },
            { BuildStatusEnum.Broken, Color.FromArgb(255, 222, 64, 82) },
        };

        private DateTime? _localStartTime;

        public string BuildName { get; private set; }

        public void UpdateListItem(BuildStatusDto buildStatus)
        {
            InitializeLabels(buildStatus);
        }

        private void DetailsLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_url) && _url.StartsWith("http"))
            {
                Process.Start(_url);
            }
        }

        public void RecalculatePrettyDate()
        {
            _startTime.Visible = _localStartTime.HasValue;
            if (!_localStartTime.HasValue) return;
            _startTime.Text = _localStartTime.Value.PrettyDate();
        }
    }
}
