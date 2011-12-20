using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SirenOfShame.Lib.Exceptions;
using SirenOfShame.Lib.Watcher;
using SirenOfShame.Properties;

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
            int builds = args.BuildStatusListViewItems.Count();
            tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + builds + 1;
            for (int i = 0; i < builds; i++)
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            
            int row = 2;
            foreach (var buildStatusListViewItem in args.BuildStatusListViewItems)
            {
                SetImage(GetBallBigResource((BallsEnum)buildStatusListViewItem.ImageIndex), row, 0);
                SetText(buildStatusListViewItem.Name, row, 1);
                SetText(buildStatusListViewItem.StartTime, row, 2);
                SetText(buildStatusListViewItem.Duration, row, 3);
                SetText(buildStatusListViewItem.RequestedBy, row, 4);
                SetText(buildStatusListViewItem.Comment, row, 5);

                row++;
            }
        }

        private void SetImage(Bitmap bitmap, int row, int column)
        {
            var pictureBox = tableLayoutPanel1.GetControlFromPosition(column, row) as PictureBox;
            
            if (pictureBox != null)
            {
                pictureBox.Image = bitmap;
                return;
            }
            
            pictureBox = new PictureBox
            {
                Image = bitmap,
                Size = new Size(32, 32),
                TabStop = false
            };
            tableLayoutPanel1.Controls.Add(pictureBox, 0, row);
        }

        private static Bitmap GetBallBigResource(BallsEnum ball)
        {
            if (ball == BallsEnum.Gray) return Resources.ball_gray_big;
            if (ball == BallsEnum.Red) return Resources.ball_red_big;
            if (ball == BallsEnum.Green) return Resources.ball_green_big;
            if (ball == BallsEnum.Triangle) return Resources.question_big;
            throw new SosException("Unknown BallsEnum: " + ball);
        }

        private Font _mainFont;

        protected Font MainFont
        {
            get { return _mainFont ?? (_mainFont = new Font(_headerName.Font.FontFamily, _headerName.Font.Size)); }
        }
        
        private void SetText(string text, int row, int column)
        {
            var label = tableLayoutPanel1.GetControlFromPosition(column, row) as Label;

            if (label != null)
            {
                label.Text = text;
                return;
            }

            label = new Label
            {
                ForeColor = Color.White,
                Text = text,
                Font = MainFont,
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
