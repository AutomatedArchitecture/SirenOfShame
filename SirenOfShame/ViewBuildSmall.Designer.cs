namespace SirenOfShame
{
    partial class ViewBuildSmall
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewBuildSmall));
            this._projectName = new System.Windows.Forms.Label();
            this._comment = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this._details = new System.Windows.Forms.Label();
            this._buildMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._affectsTrayIcon = new System.Windows.Forms.ToolStripMenuItem();
            this._stopWatching = new System.Windows.Forms.ToolStripMenuItem();
            this._when = new System.Windows.Forms.ToolStripMenuItem();
            this._toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._buildStatusIcon = new System.Windows.Forms.Label();
            this._panelBottom = new System.Windows.Forms.Panel();
            this._duration = new System.Windows.Forms.Label();
            this._editRules = new System.Windows.Forms.Label();
            this._panelRowTwo = new System.Windows.Forms.Panel();
            this._requestedBy = new System.Windows.Forms.Label();
            this._startTime = new System.Windows.Forms.Label();
            this._loading = new System.Windows.Forms.PictureBox();
            this._panelTop = new System.Windows.Forms.Panel();
            this._editRulesTop = new System.Windows.Forms.Label();
            this._buildMenu.SuspendLayout();
            this._panelBottom.SuspendLayout();
            this._panelRowTwo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._loading)).BeginInit();
            this._panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // _projectName
            // 
            this._projectName.AutoEllipsis = true;
            this._projectName.Cursor = System.Windows.Forms.Cursors.Hand;
            this._projectName.Dock = System.Windows.Forms.DockStyle.Fill;
            this._projectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._projectName.ForeColor = System.Drawing.Color.WhiteSmoke;
            this._projectName.Location = new System.Drawing.Point(0, 0);
            this._projectName.Name = "_projectName";
            this._projectName.Padding = new System.Windows.Forms.Padding(6, 2, 2, 2);
            this._projectName.Size = new System.Drawing.Size(206, 22);
            this._projectName.TabIndex = 0;
            this._projectName.Text = "Project Name";
            this._projectName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._projectName.Click += new System.EventHandler(this.ProjectNameClick);
            this._projectName.MouseEnter += new System.EventHandler(this.ProjectNameMouseEnter);
            // 
            // _comment
            // 
            this._comment.AutoEllipsis = true;
            this._comment.Dock = System.Windows.Forms.DockStyle.Fill;
            this._comment.ForeColor = System.Drawing.Color.Black;
            this._comment.Location = new System.Drawing.Point(0, 45);
            this._comment.Name = "_comment";
            this._comment.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this._comment.Size = new System.Drawing.Size(230, 63);
            this._comment.TabIndex = 4;
            this._comment.Text = "Fixing Lee\'s bunk check-in from yesterday where he broke the build and then left " +
    "for the day, the jerk.  Build and run is a terrible, terrible thing to do.";
            this._comment.Click += new System.EventHandler(this.CommentClick);
            this._comment.MouseEnter += new System.EventHandler(this.CommentMouseEnter);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList1.Images.SetKeyName(0, "error.bmp");
            this.imageList1.Images.SetKeyName(1, "ok.bmp");
            this.imageList1.Images.SetKeyName(2, "clock.bmp");
            this.imageList1.Images.SetKeyName(3, "unknown.bmp");
            // 
            // _details
            // 
            this._details.BackColor = System.Drawing.Color.Transparent;
            this._details.Cursor = System.Windows.Forms.Cursors.Hand;
            this._details.Dock = System.Windows.Forms.DockStyle.Right;
            this._details.ForeColor = System.Drawing.Color.Black;
            this._details.Image = global::SirenOfShame.Properties.Resources.nav_up_right;
            this._details.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._details.Location = new System.Drawing.Point(207, 0);
            this._details.Name = "_details";
            this._details.Size = new System.Drawing.Size(18, 24);
            this._details.TabIndex = 10;
            this._details.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this._details, "Open webpage to view details");
            this._details.Visible = false;
            this._details.Click += new System.EventHandler(this.DetailsClick);
            // 
            // _buildMenu
            // 
            this._buildMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._affectsTrayIcon,
            this._stopWatching,
            this._when,
            this._toolStripSeparator1});
            this._buildMenu.Name = "_buildMenu";
            this._buildMenu.Size = new System.Drawing.Size(163, 76);
            this._buildMenu.Text = "BuildMenu";
            this._buildMenu.Opening += new System.ComponentModel.CancelEventHandler(this.BuildMenuOpening);
            // 
            // _affectsTrayIcon
            // 
            this._affectsTrayIcon.Checked = true;
            this._affectsTrayIcon.CheckState = System.Windows.Forms.CheckState.Checked;
            this._affectsTrayIcon.Name = "_affectsTrayIcon";
            this._affectsTrayIcon.Size = new System.Drawing.Size(162, 22);
            this._affectsTrayIcon.Text = "Affects Tray Icon";
            this._affectsTrayIcon.Click += new System.EventHandler(this.AffectsTrayIconClick);
            // 
            // _stopWatching
            // 
            this._stopWatching.Name = "_stopWatching";
            this._stopWatching.Size = new System.Drawing.Size(162, 22);
            this._stopWatching.Text = "Stop Watching";
            this._stopWatching.Click += new System.EventHandler(this.StopWatchingClick);
            // 
            // _when
            // 
            this._when.Name = "_when";
            this._when.Size = new System.Drawing.Size(162, 22);
            this._when.Text = "When";
            // 
            // _toolStripSeparator1
            // 
            this._toolStripSeparator1.Name = "_toolStripSeparator1";
            this._toolStripSeparator1.Size = new System.Drawing.Size(159, 6);
            // 
            // _buildStatusIcon
            // 
            this._buildStatusIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(95)))), ((int)(((byte)(152)))));
            this._buildStatusIcon.Dock = System.Windows.Forms.DockStyle.Right;
            this._buildStatusIcon.ImageKey = "clock.bmp";
            this._buildStatusIcon.ImageList = this.imageList1;
            this._buildStatusIcon.Location = new System.Drawing.Point(206, 0);
            this._buildStatusIcon.Name = "_buildStatusIcon";
            this._buildStatusIcon.Size = new System.Drawing.Size(24, 22);
            this._buildStatusIcon.TabIndex = 7;
            // 
            // _panelBottom
            // 
            this._panelBottom.Controls.Add(this._duration);
            this._panelBottom.Controls.Add(this._editRules);
            this._panelBottom.Controls.Add(this._details);
            this._panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._panelBottom.Location = new System.Drawing.Point(0, 108);
            this._panelBottom.Name = "_panelBottom";
            this._panelBottom.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this._panelBottom.Size = new System.Drawing.Size(230, 24);
            this._panelBottom.TabIndex = 35;
            // 
            // _duration
            // 
            this._duration.AutoSize = true;
            this._duration.Dock = System.Windows.Forms.DockStyle.Left;
            this._duration.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._duration.ForeColor = System.Drawing.Color.Black;
            this._duration.Location = new System.Drawing.Point(5, 0);
            this._duration.Name = "_duration";
            this._duration.Size = new System.Drawing.Size(40, 20);
            this._duration.TabIndex = 9;
            this._duration.Text = "9:53";
            this._duration.Click += new System.EventHandler(this.DurationClick);
            this._duration.MouseEnter += new System.EventHandler(this.DurationMouseEnter);
            // 
            // _editRules
            // 
            this._editRules.Cursor = System.Windows.Forms.Cursors.Hand;
            this._editRules.Dock = System.Windows.Forms.DockStyle.Right;
            this._editRules.Image = global::SirenOfShame.Properties.Resources.gear;
            this._editRules.Location = new System.Drawing.Point(191, 0);
            this._editRules.Name = "_editRules";
            this._editRules.Size = new System.Drawing.Size(16, 24);
            this._editRules.TabIndex = 11;
            this._editRules.Click += new System.EventHandler(this.EditRulesClick);
            // 
            // _panelRowTwo
            // 
            this._panelRowTwo.Controls.Add(this._requestedBy);
            this._panelRowTwo.Controls.Add(this._startTime);
            this._panelRowTwo.Dock = System.Windows.Forms.DockStyle.Top;
            this._panelRowTwo.Location = new System.Drawing.Point(0, 22);
            this._panelRowTwo.Name = "_panelRowTwo";
            this._panelRowTwo.Padding = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this._panelRowTwo.Size = new System.Drawing.Size(230, 23);
            this._panelRowTwo.TabIndex = 36;
            // 
            // _requestedBy
            // 
            this._requestedBy.Dock = System.Windows.Forms.DockStyle.Fill;
            this._requestedBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._requestedBy.ForeColor = System.Drawing.Color.Black;
            this._requestedBy.Location = new System.Drawing.Point(5, 5);
            this._requestedBy.Name = "_requestedBy";
            this._requestedBy.Size = new System.Drawing.Size(158, 18);
            this._requestedBy.TabIndex = 5;
            this._requestedBy.Text = "Lee Richardson";
            this._requestedBy.Click += new System.EventHandler(this.RequestedByClick);
            this._requestedBy.MouseEnter += new System.EventHandler(this.RequestedByMouseEnter);
            // 
            // _startTime
            // 
            this._startTime.Dock = System.Windows.Forms.DockStyle.Right;
            this._startTime.ForeColor = System.Drawing.Color.DimGray;
            this._startTime.Location = new System.Drawing.Point(163, 5);
            this._startTime.Name = "_startTime";
            this._startTime.Size = new System.Drawing.Size(62, 18);
            this._startTime.TabIndex = 4;
            this._startTime.Text = "8/2/2012";
            this._startTime.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this._startTime.Click += new System.EventHandler(this.StartTimeClick);
            this._startTime.MouseEnter += new System.EventHandler(this.StartTimeMouseEnter);
            // 
            // _loading
            // 
            this._loading.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(95)))), ((int)(((byte)(152)))));
            this._loading.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this._loading.Dock = System.Windows.Forms.DockStyle.Right;
            this._loading.Image = global::SirenOfShame.Properties.Resources.loadingBlue;
            this._loading.Location = new System.Drawing.Point(190, 0);
            this._loading.Name = "_loading";
            this._loading.Size = new System.Drawing.Size(16, 22);
            this._loading.TabIndex = 38;
            this._loading.TabStop = false;
            this._loading.Visible = false;
            // 
            // _panelTop
            // 
            this._panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(95)))), ((int)(((byte)(152)))));
            this._panelTop.Controls.Add(this._editRulesTop);
            this._panelTop.Controls.Add(this._loading);
            this._panelTop.Controls.Add(this._projectName);
            this._panelTop.Controls.Add(this._buildStatusIcon);
            this._panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._panelTop.Location = new System.Drawing.Point(0, 0);
            this._panelTop.Name = "_panelTop";
            this._panelTop.Size = new System.Drawing.Size(230, 22);
            this._panelTop.TabIndex = 39;
            // 
            // _editRulesTop
            // 
            this._editRulesTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(95)))), ((int)(((byte)(152)))));
            this._editRulesTop.Cursor = System.Windows.Forms.Cursors.Hand;
            this._editRulesTop.Dock = System.Windows.Forms.DockStyle.Right;
            this._editRulesTop.Image = ((System.Drawing.Image)(resources.GetObject("_editRulesTop.Image")));
            this._editRulesTop.Location = new System.Drawing.Point(166, 0);
            this._editRulesTop.Name = "_editRulesTop";
            this._editRulesTop.Size = new System.Drawing.Size(24, 22);
            this._editRulesTop.TabIndex = 39;
            // 
            // ViewBuildSmall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._comment);
            this.Controls.Add(this._panelRowTwo);
            this.Controls.Add(this._panelBottom);
            this.Controls.Add(this._panelTop);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ViewBuildSmall";
            this.Size = new System.Drawing.Size(230, 132);
            this._buildMenu.ResumeLayout(false);
            this._panelBottom.ResumeLayout(false);
            this._panelBottom.PerformLayout();
            this._panelRowTwo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._loading)).EndInit();
            this._panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label _projectName;
        private System.Windows.Forms.Label _comment;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label _buildStatusIcon;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip _buildMenu;
        private System.Windows.Forms.ToolStripMenuItem _affectsTrayIcon;
        private System.Windows.Forms.ToolStripMenuItem _stopWatching;
        private System.Windows.Forms.ToolStripMenuItem _when;
        private System.Windows.Forms.ToolStripSeparator _toolStripSeparator1;
        private System.Windows.Forms.Panel _panelBottom;
        private System.Windows.Forms.Label _duration;
        private System.Windows.Forms.Label _editRules;
        private System.Windows.Forms.Label _details;
        private System.Windows.Forms.Panel _panelRowTwo;
        private System.Windows.Forms.Label _requestedBy;
        private System.Windows.Forms.Label _startTime;
        private System.Windows.Forms.PictureBox _loading;
        private System.Windows.Forms.Panel _panelTop;
        private System.Windows.Forms.Label _editRulesTop;
    }
}
