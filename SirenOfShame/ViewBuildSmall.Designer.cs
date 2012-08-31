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
            this._startTime = new System.Windows.Forms.Label();
            this._requestedBy = new System.Windows.Forms.Label();
            this._comment = new System.Windows.Forms.Label();
            this._duration = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this._buildMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._affectsTrayIcon = new System.Windows.Forms.ToolStripMenuItem();
            this._stopWatching = new System.Windows.Forms.ToolStripMenuItem();
            this._when = new System.Windows.Forms.ToolStripMenuItem();
            this._toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._loading = new System.Windows.Forms.PictureBox();
            this._editRulesTop = new System.Windows.Forms.Label();
            this._buildStatusIcon = new System.Windows.Forms.Label();
            this._editRules = new System.Windows.Forms.Label();
            this._details = new System.Windows.Forms.Label();
            this._buildMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._loading)).BeginInit();
            this.SuspendLayout();
            // 
            // _projectName
            // 
            this._projectName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._projectName.AutoEllipsis = true;
            this._projectName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(95)))), ((int)(((byte)(152)))));
            this._projectName.Cursor = System.Windows.Forms.Cursors.Hand;
            this._projectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._projectName.ForeColor = System.Drawing.Color.WhiteSmoke;
            this._projectName.Location = new System.Drawing.Point(0, 0);
            this._projectName.Name = "_projectName";
            this._projectName.Padding = new System.Windows.Forms.Padding(6, 2, 2, 2);
            this._projectName.Size = new System.Drawing.Size(230, 24);
            this._projectName.TabIndex = 0;
            this._projectName.Text = "Project Name";
            this._projectName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._projectName.Click += new System.EventHandler(this.ProjectNameClick);
            this._projectName.MouseEnter += new System.EventHandler(this.ProjectNameMouseEnter);
            // 
            // _startTime
            // 
            this._startTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._startTime.ForeColor = System.Drawing.Color.DimGray;
            this._startTime.Location = new System.Drawing.Point(98, 34);
            this._startTime.Name = "_startTime";
            this._startTime.Size = new System.Drawing.Size(119, 13);
            this._startTime.TabIndex = 2;
            this._startTime.Text = "8/2/2012";
            this._startTime.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this._startTime.Click += new System.EventHandler(this.StartTimeClick);
            this._startTime.MouseEnter += new System.EventHandler(this.StartTimeMouseEnter);
            // 
            // _requestedBy
            // 
            this._requestedBy.AutoSize = true;
            this._requestedBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._requestedBy.ForeColor = System.Drawing.Color.Black;
            this._requestedBy.Location = new System.Drawing.Point(5, 34);
            this._requestedBy.Name = "_requestedBy";
            this._requestedBy.Size = new System.Drawing.Size(96, 13);
            this._requestedBy.TabIndex = 3;
            this._requestedBy.Text = "Lee Richardson";
            this._requestedBy.Click += new System.EventHandler(this.RequestedByClick);
            this._requestedBy.MouseEnter += new System.EventHandler(this.RequestedByMouseEnter);
            // 
            // _comment
            // 
            this._comment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._comment.AutoEllipsis = true;
            this._comment.ForeColor = System.Drawing.Color.Black;
            this._comment.Location = new System.Drawing.Point(5, 53);
            this._comment.Name = "_comment";
            this._comment.Size = new System.Drawing.Size(222, 52);
            this._comment.TabIndex = 4;
            this._comment.Text = "Fixing Lee\'s bunk check-in from yesterday where he broke the build and then left " +
    "for the day, the jerk.  Build and run is a terrible, terrible thing to do.";
            this._comment.Click += new System.EventHandler(this.CommentClick);
            this._comment.MouseEnter += new System.EventHandler(this.CommentMouseEnter);
            // 
            // _duration
            // 
            this._duration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._duration.AutoSize = true;
            this._duration.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._duration.ForeColor = System.Drawing.Color.Black;
            this._duration.Location = new System.Drawing.Point(4, 108);
            this._duration.Name = "_duration";
            this._duration.Size = new System.Drawing.Size(40, 20);
            this._duration.TabIndex = 5;
            this._duration.Text = "9:53";
            this._duration.Click += new System.EventHandler(this.DurationClick);
            this._duration.MouseEnter += new System.EventHandler(this.DurationMouseEnter);
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
            // _buildMenu
            // 
            this._buildMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._affectsTrayIcon,
            this._stopWatching,
            this._when,
            this._toolStripSeparator1});
            this._buildMenu.Name = "_buildMenu";
            this._buildMenu.Size = new System.Drawing.Size(164, 76);
            this._buildMenu.Text = "BuildMenu";
            this._buildMenu.Opening += new System.ComponentModel.CancelEventHandler(this.BuildMenuOpening);
            // 
            // _affectsTrayIcon
            // 
            this._affectsTrayIcon.Checked = true;
            this._affectsTrayIcon.CheckState = System.Windows.Forms.CheckState.Checked;
            this._affectsTrayIcon.Name = "_affectsTrayIcon";
            this._affectsTrayIcon.Size = new System.Drawing.Size(163, 22);
            this._affectsTrayIcon.Text = "Affects Tray Icon";
            this._affectsTrayIcon.Click += new System.EventHandler(this.AffectsTrayIconClick);
            // 
            // _stopWatching
            // 
            this._stopWatching.Name = "_stopWatching";
            this._stopWatching.Size = new System.Drawing.Size(163, 22);
            this._stopWatching.Text = "Stop Watching";
            this._stopWatching.Click += new System.EventHandler(this.StopWatchingClick);
            // 
            // _when
            // 
            this._when.Name = "_when";
            this._when.Size = new System.Drawing.Size(163, 22);
            this._when.Text = "When";
            // 
            // _toolStripSeparator1
            // 
            this._toolStripSeparator1.Name = "_toolStripSeparator1";
            this._toolStripSeparator1.Size = new System.Drawing.Size(160, 6);
            // 
            // _loading
            // 
            this._loading.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(95)))), ((int)(((byte)(152)))));
            this._loading.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this._loading.Image = global::SirenOfShame.Properties.Resources.loadingBlue;
            this._loading.Location = new System.Drawing.Point(209, 4);
            this._loading.Name = "_loading";
            this._loading.Size = new System.Drawing.Size(16, 16);
            this._loading.TabIndex = 34;
            this._loading.TabStop = false;
            this._loading.Visible = false;
            // 
            // _editRulesTop
            // 
            this._editRulesTop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._editRulesTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(95)))), ((int)(((byte)(152)))));
            this._editRulesTop.Cursor = System.Windows.Forms.Cursors.Hand;
            this._editRulesTop.Image = ((System.Drawing.Image)(resources.GetObject("_editRulesTop.Image")));
            this._editRulesTop.Location = new System.Drawing.Point(182, 0);
            this._editRulesTop.Name = "_editRulesTop";
            this._editRulesTop.Size = new System.Drawing.Size(24, 24);
            this._editRulesTop.TabIndex = 9;
            this._editRulesTop.Click += new System.EventHandler(this.EditRulesClick);
            // 
            // _buildStatusIcon
            // 
            this._buildStatusIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._buildStatusIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(95)))), ((int)(((byte)(152)))));
            this._buildStatusIcon.ImageKey = "clock.bmp";
            this._buildStatusIcon.ImageList = this.imageList1;
            this._buildStatusIcon.Location = new System.Drawing.Point(206, 0);
            this._buildStatusIcon.Name = "_buildStatusIcon";
            this._buildStatusIcon.Size = new System.Drawing.Size(24, 24);
            this._buildStatusIcon.TabIndex = 7;
            // 
            // _editRules
            // 
            this._editRules.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._editRules.Cursor = System.Windows.Forms.Cursors.Hand;
            this._editRules.Image = global::SirenOfShame.Properties.Resources.gear;
            this._editRules.Location = new System.Drawing.Point(212, 111);
            this._editRules.Name = "_editRules";
            this._editRules.Size = new System.Drawing.Size(16, 16);
            this._editRules.TabIndex = 8;
            this._editRules.Click += new System.EventHandler(this.EditRulesClick);
            // 
            // _details
            // 
            this._details.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._details.BackColor = System.Drawing.Color.Transparent;
            this._details.Cursor = System.Windows.Forms.Cursors.Hand;
            this._details.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._details.ForeColor = System.Drawing.Color.Black;
            this._details.Image = global::SirenOfShame.Properties.Resources.nav_up_right;
            this._details.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._details.Location = new System.Drawing.Point(188, 109);
            this._details.Name = "_details";
            this._details.Size = new System.Drawing.Size(18, 20);
            this._details.TabIndex = 6;
            this._details.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this._details, "Open webpage to view details");
            this._details.Visible = false;
            this._details.Click += new System.EventHandler(this.DetailsClick);
            // 
            // ViewBuildSmall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._loading);
            this.Controls.Add(this._editRulesTop);
            this.Controls.Add(this._buildStatusIcon);
            this.Controls.Add(this._duration);
            this.Controls.Add(this._editRules);
            this.Controls.Add(this._comment);
            this.Controls.Add(this._requestedBy);
            this.Controls.Add(this._details);
            this.Controls.Add(this._startTime);
            this.Controls.Add(this._projectName);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ViewBuildSmall";
            this.Size = new System.Drawing.Size(230, 132);
            this._buildMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._loading)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _projectName;
        private System.Windows.Forms.Label _startTime;
        private System.Windows.Forms.Label _requestedBy;
        private System.Windows.Forms.Label _comment;
        private System.Windows.Forms.Label _duration;
        private System.Windows.Forms.Label _details;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label _buildStatusIcon;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label _editRules;
        private System.Windows.Forms.ContextMenuStrip _buildMenu;
        private System.Windows.Forms.ToolStripMenuItem _affectsTrayIcon;
        private System.Windows.Forms.ToolStripMenuItem _stopWatching;
        private System.Windows.Forms.ToolStripMenuItem _when;
        private System.Windows.Forms.ToolStripSeparator _toolStripSeparator1;
        private System.Windows.Forms.Label _editRulesTop;
        private System.Windows.Forms.PictureBox _loading;
    }
}
