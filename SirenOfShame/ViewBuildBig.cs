using System.Drawing;
using System.Windows.Forms;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame
{
    public partial class ViewBuildBig : ViewBuildBase
    {
        public ViewBuildBig()
        {
            InitializeComponent();
        }

        public void Initialize(BuildStatusDto buildStatusDto)
        {
            InitializeLabels(buildStatusDto);
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

    }
}
