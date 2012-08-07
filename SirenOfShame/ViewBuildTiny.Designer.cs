namespace SirenOfShame
{
    partial class ViewBuildTiny
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
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this._buildMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._affectsTrayIcon = new System.Windows.Forms.ToolStripMenuItem();
            this._stopWatching = new System.Windows.Forms.ToolStripMenuItem();
            this._when = new System.Windows.Forms.ToolStripMenuItem();
            this._toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._buildStatusIcon = new System.Windows.Forms.Label();
            this._editRules = new System.Windows.Forms.Label();
            this._buildMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // _projectName
            // 
            this._projectName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._projectName.AutoEllipsis = true;
            this._projectName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(95)))), ((int)(((byte)(152)))));
            this._projectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._projectName.ForeColor = System.Drawing.Color.White;
            this._projectName.Location = new System.Drawing.Point(0, 0);
            this._projectName.Name = "_projectName";
            this._projectName.Padding = new System.Windows.Forms.Padding(6, 2, 2, 2);
            this._projectName.Size = new System.Drawing.Size(230, 24);
            this._projectName.TabIndex = 0;
            this._projectName.Text = "Project Name";
            this._projectName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _startTime
            // 
            this._startTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._startTime.ForeColor = System.Drawing.Color.Black;
            this._startTime.Location = new System.Drawing.Point(98, 34);
            this._startTime.Name = "_startTime";
            this._startTime.Size = new System.Drawing.Size(119, 13);
            this._startTime.TabIndex = 2;
            this._startTime.Text = "8/2/2012";
            this._startTime.TextAlign = System.Drawing.ContentAlignment.TopRight;
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
            this._editRules.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(95)))), ((int)(((byte)(152)))));
            this._editRules.Image = global::SirenOfShame.Properties.Resources.gear_white;
            this._editRules.Location = new System.Drawing.Point(188, 4);
            this._editRules.Name = "_editRules";
            this._editRules.Size = new System.Drawing.Size(16, 16);
            this._editRules.TabIndex = 8;
            this._editRules.Click += new System.EventHandler(this.EditRulesClick);
            // 
            // ViewBuildSmall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._buildStatusIcon);
            this.Controls.Add(this._requestedBy);
            this.Controls.Add(this._editRules);
            this.Controls.Add(this._startTime);
            this.Controls.Add(this._projectName);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ViewBuildSmall";
            this.Size = new System.Drawing.Size(230, 60);
            this._buildMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _projectName;
        private System.Windows.Forms.Label _startTime;
        private System.Windows.Forms.Label _requestedBy;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label _buildStatusIcon;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label _editRules;
        private System.Windows.Forms.ContextMenuStrip _buildMenu;
        private System.Windows.Forms.ToolStripMenuItem _affectsTrayIcon;
        private System.Windows.Forms.ToolStripMenuItem _stopWatching;
        private System.Windows.Forms.ToolStripMenuItem _when;
        private System.Windows.Forms.ToolStripSeparator _toolStripSeparator1;
    }
}
