using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using SirenOfShame.Lib.Watcher;
using ZedGraph;

namespace SirenOfShame
{
    public partial class BuildStats : UserControl
    {
        public BuildStats()
        {
            InitializeComponent();
        }

        readonly Fill _failFill = new Fill(Color.FromArgb(192, 80, 77));
        readonly Fill _successFill = new Fill(Color.FromArgb(79, 129, 189));

        public void SetStats(int count, int failed, double percentFailed)
        {
            _buildCount.Text = count.ToString(CultureInfo.InvariantCulture);
            _failedBuilds.Text = failed.ToString(CultureInfo.InvariantCulture);
            _percentFailed.Text = percentFailed.ToString("p");
        }

        public void GraphBuildHistory(IList<BuildStatus> buildStatuses)
        {
            GraphPane myPane = _buildHistoryZedGraph.GraphPane;
            myPane.CurveList.Clear();

            IEnumerable<BuildStatus> lastFiveBuildStatuses = buildStatuses.Skip(buildStatuses.Count - 8);
            foreach (BuildStatus buildStatus in lastFiveBuildStatuses)
            {
                if (buildStatus.FinishedTime == null || buildStatus.StartedTime == null) continue;
                var duration = buildStatus.FinishedTime.Value - buildStatus.StartedTime.Value;
                Fill fill = buildStatus.BuildStatusEnum == BuildStatusEnum.Broken ? _failFill : _successFill;
                var bar = myPane.AddBar(null, null, new[] { duration.TotalMinutes }, Color.White);
                bar.Bar.Fill = fill;
                bar.Bar.Border.Color = Color.White;
            }

            _buildHistoryZedGraph.AxisChange();
            _buildHistoryZedGraph.Invalidate();
        }

        public void InitializeBuildHistoryChart()
        {
            GraphPane myPane = _buildHistoryZedGraph.GraphPane;
            myPane.Margin.All = 0;
            myPane.Legend.IsVisible = false;
            myPane.Title.IsVisible = false;
            myPane.XAxis.IsVisible = false;

            myPane.YAxis.IsVisible = true;
            myPane.YAxis.MinorTic.IsOpposite = false;
            myPane.YAxis.IsAxisSegmentVisible = true;
            myPane.YAxis.MinorTic.Color = Color.White;

            myPane.YAxis.MajorTic.IsCrossOutside = false;
            myPane.YAxis.MajorTic.IsCrossInside = false;
            myPane.YAxis.MajorTic.IsInside = false;
            myPane.YAxis.MajorTic.IsOutside = false;

            myPane.YAxis.Scale.Min = 0;
            myPane.YAxis.Scale.IsSkipFirstLabel = true;
            myPane.YAxis.Scale.IsSkipLastLabel = true;
            myPane.YAxis.MajorTic.IsOpposite = false;
            myPane.YAxis.Title.IsVisible = false;
            myPane.XAxis.Type = AxisType.Text;
            myPane.IsFontsScaled = false;
            myPane.YAxis.Scale.FontSpec.Size = 10;

            myPane.Chart.Border.IsVisible = false;
            myPane.Border.IsVisible = false;

            _buildHistoryZedGraph.IsEnableZoom = false;
            myPane.BarSettings.ClusterScaleWidth = 60;
        }

        public event CloseBuildStats OnClose;
        
        private void CloseClick(object sender, System.EventArgs e)
        {
            OnClose(this, new CloseBuildStatsArgs());
        }
    }

    public delegate void CloseBuildStats(object sender, CloseBuildStatsArgs args);

    public class CloseBuildStatsArgs
    {
    }
}
