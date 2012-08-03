using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
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
            _buildId.Text = buildStatusDto.BuildId;
            _startTime.Text = buildStatusDto.StartTime;
            _duration.Text = buildStatusDto.Duration;
            _requestedBy.Text = buildStatusDto.RequestedBy;
            _comment.Text = buildStatusDto.Comment;
            // ImageIndex = buildStatusDto.ImageIndex
            SetBackgroundColors(buildStatusDto.BuildStatusEnum);
        }

        private void SetBackgroundColors(BuildStatusEnum buildStatusEnum)
        {
            Color backgroundColor = GetBackgroundColor(buildStatusEnum);
            BackColor = backgroundColor;
            _projectName.BackColor = backgroundColor;
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

        public string BuildName { get; private set; }

        public void UpdateListItem(BuildStatusDto buildStatus)
        {
            InitializeLabels(buildStatus);
        }

        private void _details_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_url) && _url.StartsWith("http"))
            {
                Process.Start(_url);
            }
        }
    }
}
