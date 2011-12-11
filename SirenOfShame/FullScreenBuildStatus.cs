using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame
{
    public partial class FullScreenBuildStatus : FullScreenFormBase
    {
        public FullScreenBuildStatus()
        {
            InitializeComponent();
        }

        private void FullScreenBuildStatus_KeyDown(object sender, KeyEventArgs e)
        {
            ExitFullScreen();
        }

        public void RefreshListViewWithBuildStatus(RefreshStatusEventArgs args)
        {
            tableLayoutPanel1.RowCount = args.BuildStatusListViewItems.Count() + 3;
            int row = 2;
            foreach (var buildStatusListViewItem in args.BuildStatusListViewItems)
            {
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));

                AddText(buildStatusListViewItem.Comment, row, 4);
                AddText(buildStatusListViewItem.RequestedBy, row, 3);
                AddText(buildStatusListViewItem.Duration, row, 2);
                AddText(buildStatusListViewItem.StartTime, row, 1);
                AddText(buildStatusListViewItem.Name, row, 0);

                row++;
            }
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        }

        private void AddText(string text, int row, int column)
        {
            if (string.IsNullOrEmpty(text)) return;
            Label label = new Label
            {
                ForeColor = Color.White,
                Text = text,
                Font = _headerName.Font,
                AutoSize = false,
                AutoEllipsis = true,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            tableLayoutPanel1.Controls.Add(label, column, row);
        }

        private void _buildDefinitions_KeyDown(object sender, KeyEventArgs e)
        {
            ExitFullScreen();
        }

        private void _buildDefinitions_MouseDown(object sender, MouseEventArgs e)
        {
            ExitFullScreen();
        }
    }
}
