using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SirenOfShame.Lib;
using SirenOfShame.Lib.Watcher;
using ZedGraph;
using log4net;

namespace SirenOfShame
{
    public partial class BuildStats : UserControl
    {
        private static readonly ILog _log = MyLogManager.GetLogger(typeof(BuildStats));

        public BuildStats()
        {
            InitializeComponent();
        }

        readonly Fill _failFill = new Fill(Color.FromArgb(255, 222, 64, 82));
        readonly Fill _successFill = new Fill(Color.FromArgb(255, 50, 175, 82));

        public void GraphBuildHistory(IList<BuildStatus> buildStatuses)
        {
            if (buildStatuses == null)
            {
                _log.Warn("buildStatuses was null. Unable to build a graph.");
                return;
            }
            if (_buildHistoryZedGraph == null || _buildHistoryZedGraph.GraphPane == null)
            {
                _log.Warn("_buildHistoryZedGraph was null. Unable to build a graph.");
                return;
            }
            GraphPane myPane = _buildHistoryZedGraph.GraphPane;
            myPane.CurveList.Clear();

            IEnumerable<BuildStatus> lastFewBuildStatuses = buildStatuses.Skip(buildStatuses.Count - 8);
            foreach (BuildStatus buildStatus in lastFewBuildStatuses)
            {
                if (buildStatus == null || buildStatus.FinishedTime == null || buildStatus.StartedTime == null) continue;
                var duration = buildStatus.FinishedTime.Value - buildStatus.StartedTime.Value;
                Fill fill = buildStatus.CurrentBuildStatus == BuildStatusEnum.Broken ? _failFill : _successFill;
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
    }
}
