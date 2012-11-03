using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Windows.Forms;
using SirenOfShame.Lib.Exceptions;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;
using SirenOfShame.Properties;

namespace SirenOfShame
{
    public partial class FullScreenBuildStatus : FullScreenFormBase
    {
        private IList<BuildStatusDto> _buildStatusDtos;

        private bool _currentSortAscending = true;

        private string _currentSortColumn;

        private int _oldBuildCount = 0;

        public FullScreenBuildStatus()
        {
            InitializeComponent();
        }

        private void FullScreenBuildStatusKeyDown(object sender, KeyEventArgs e)
        {
            ExitFullScreen();
        }

        public void RefreshListViewWithBuildStatus(RefreshStatusEventArgs args, SirenOfShameSettings settings)
        {
            _buildStatusDtos = args.BuildStatusDtos;
            RefreshListView(x => x.BuildDefinitionDisplayName, true);
        }

        private void RefreshListView<TKey>(Expression<Func<BuildStatusDto, TKey>> sortColumn, bool retainCurrentSort = false)
        {
            if (_buildStatusDtos == null) return;

            var buildCount = _buildStatusDtos.Count();
            if (buildCount != _oldBuildCount)
            {
                ClearRowsBelowHeader();
                tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + buildCount + 1;
                for (var i = 0; i < buildCount; i++)
                {
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
                }
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                _oldBuildCount = buildCount;
            }

            var sortColumnName = ((MemberExpression)sortColumn.Body).Member.Name;
            if (!retainCurrentSort || _currentSortColumn == null)
            {
                _currentSortAscending = (sortColumnName != _currentSortColumn) || !_currentSortAscending;
                _currentSortColumn = sortColumnName;
            }

            var buildStatusListViewItems = _buildStatusDtos.AsQueryable().OrderBy(_currentSortColumn + " " + (_currentSortAscending ? "ASC" : "DESC"));

            var row = 2;
            foreach (var buildStatusListViewItem in buildStatusListViewItems)
            {
                var comment = buildStatusListViewItem.BuildStatusMessage;
                if (!string.IsNullOrWhiteSpace(buildStatusListViewItem.Comment)) comment = buildStatusListViewItem.Comment;

                SetImage(GetBallBigResource((BallsEnum)buildStatusListViewItem.ImageIndex), row, 0);
                SetText(buildStatusListViewItem.BuildDefinitionDisplayName, row, 1);
                SetText(buildStatusListViewItem.StartTimeShort, row, 2);
                SetText(buildStatusListViewItem.Duration, row, 3);
                SetText(buildStatusListViewItem.RequestedByDisplayName, row, 4);
                SetText(comment, row, 5);

                row++;
            }
        }

        private void ClearRowsBelowHeader()
        {
            for (int row = 2; row < tableLayoutPanel1.RowCount; row++)
            {
                ClearCell(row, 0);
                ClearCell(row, 1);
                ClearCell(row, 2);
                ClearCell(row, 3);
                ClearCell(row, 4);
                ClearCell(row, 5);
            }
        }

        private void ClearCell(int row, int column)
        {
            var control = tableLayoutPanel1.GetControlFromPosition(column, row);
            if (control != null)
                tableLayoutPanel1.Controls.Remove(control);
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

        private void TableLayoutPanel1Click(object sender, EventArgs e)
        {
            ExitFullScreen();
        }

        private void DateClick(object sender, EventArgs e)
        {
            RefreshListView(x => x.LocalStartTime);
        }

        private void NameClick(object sender, EventArgs e)
        {
            RefreshListView(x => x.BuildDefinitionDisplayName);
        }

        private void DurationClick(object sender, EventArgs e)
        {
            RefreshListView(x => x.Duration);
        }

        private void CommitterClick(object sender, EventArgs e)
        {
            RefreshListView(x => x.RequestedByDisplayName);
        }

        private void CommentClick(object sender, EventArgs e)
        {
            RefreshListView(x => x.Comment);
        }
    }
}
