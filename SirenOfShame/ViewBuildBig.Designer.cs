namespace SirenOfShame
{
    partial class ViewBuildBig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewBuildBig));
            this._duration = new System.Windows.Forms.Label();
            this._comment = new System.Windows.Forms.Label();
            this._requestedBy = new System.Windows.Forms.Label();
            this._startTime = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this._buildStatusIcon = new System.Windows.Forms.Label();
            this._projectName = new System.Windows.Forms.Label();
            this._details = new System.Windows.Forms.LinkLabel();
            this._editRules = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _duration
            // 
            this._duration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._duration.AutoSize = true;
            this._duration.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._duration.ForeColor = System.Drawing.Color.Black;
            this._duration.Location = new System.Drawing.Point(12, 110);
            this._duration.Name = "_duration";
            this._duration.Size = new System.Drawing.Size(40, 20);
            this._duration.TabIndex = 12;
            this._duration.Text = "9:53";
            // 
            // _comment
            // 
            this._comment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._comment.AutoEllipsis = true;
            this._comment.ForeColor = System.Drawing.Color.Black;
            this._comment.Location = new System.Drawing.Point(13, 53);
            this._comment.Name = "_comment";
            this._comment.Size = new System.Drawing.Size(383, 53);
            this._comment.TabIndex = 11;
            this._comment.Text = "Fixing Lee\'s bunk check-in from yesterday where he broke the build and then left " +
    "for the day, the jerk.  Build and run is a terrible, terrible thing to do.";
            // 
            // _requestedBy
            // 
            this._requestedBy.AutoSize = true;
            this._requestedBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._requestedBy.ForeColor = System.Drawing.Color.Black;
            this._requestedBy.Location = new System.Drawing.Point(13, 32);
            this._requestedBy.Name = "_requestedBy";
            this._requestedBy.Size = new System.Drawing.Size(96, 13);
            this._requestedBy.TabIndex = 10;
            this._requestedBy.Text = "Lee Richardson";
            // 
            // _startTime
            // 
            this._startTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._startTime.ForeColor = System.Drawing.Color.Black;
            this._startTime.Location = new System.Drawing.Point(277, 32);
            this._startTime.Name = "_startTime";
            this._startTime.Size = new System.Drawing.Size(119, 13);
            this._startTime.TabIndex = 9;
            this._startTime.Text = "8/2/2012";
            this._startTime.TextAlign = System.Drawing.ContentAlignment.TopRight;
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
            // _buildStatusIcon
            // 
            this._buildStatusIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._buildStatusIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(95)))), ((int)(((byte)(152)))));
            this._buildStatusIcon.ImageKey = "clock.bmp";
            this._buildStatusIcon.ImageList = this.imageList1;
            this._buildStatusIcon.Location = new System.Drawing.Point(384, 0);
            this._buildStatusIcon.Name = "_buildStatusIcon";
            this._buildStatusIcon.Size = new System.Drawing.Size(24, 24);
            this._buildStatusIcon.TabIndex = 16;
            // 
            // _projectName
            // 
            this._projectName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._projectName.AutoEllipsis = true;
            this._projectName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(95)))), ((int)(((byte)(152)))));
            this._projectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._projectName.ForeColor = System.Drawing.Color.White;
            this._projectName.Location = new System.Drawing.Point(-3, 0);
            this._projectName.Name = "_projectName";
            this._projectName.Padding = new System.Windows.Forms.Padding(6, 2, 2, 2);
            this._projectName.Size = new System.Drawing.Size(411, 24);
            this._projectName.TabIndex = 15;
            this._projectName.Text = "Project Name";
            this._projectName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _details
            // 
            this._details.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._details.BackColor = System.Drawing.Color.Transparent;
            this._details.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._details.ForeColor = System.Drawing.Color.Black;
            this._details.Image = global::SirenOfShame.Properties.Resources.nav_up_right;
            this._details.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._details.LinkColor = System.Drawing.Color.White;
            this._details.Location = new System.Drawing.Point(384, 110);
            this._details.Name = "_details";
            this._details.Size = new System.Drawing.Size(18, 20);
            this._details.TabIndex = 17;
            this._details.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _editRules
            // 
            this._editRules.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._editRules.Image = global::SirenOfShame.Properties.Resources.gear;
            this._editRules.Location = new System.Drawing.Point(362, 112);
            this._editRules.Name = "_editRules";
            this._editRules.Size = new System.Drawing.Size(16, 16);
            this._editRules.TabIndex = 18;
            // 
            // ViewBuildBig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._editRules);
            this.Controls.Add(this._details);
            this.Controls.Add(this._buildStatusIcon);
            this.Controls.Add(this._projectName);
            this.Controls.Add(this._duration);
            this.Controls.Add(this._comment);
            this.Controls.Add(this._requestedBy);
            this.Controls.Add(this._startTime);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ViewBuildBig";
            this.Size = new System.Drawing.Size(408, 140);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _duration;
        private System.Windows.Forms.Label _comment;
        private System.Windows.Forms.Label _requestedBy;
        private System.Windows.Forms.Label _startTime;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label _buildStatusIcon;
        private System.Windows.Forms.Label _projectName;
        private System.Windows.Forms.LinkLabel _details;
        private System.Windows.Forms.Label _editRules;
    }
}
