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
            this._details = new System.Windows.Forms.LinkLabel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this._buildStatusIcon = new System.Windows.Forms.Label();
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
            this._projectName.Size = new System.Drawing.Size(328, 24);
            this._projectName.TabIndex = 0;
            this._projectName.Text = "Project Name";
            this._projectName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _startTime
            // 
            this._startTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._startTime.ForeColor = System.Drawing.Color.Black;
            this._startTime.Location = new System.Drawing.Point(196, 34);
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
            // _comment
            // 
            this._comment.AutoEllipsis = true;
            this._comment.ForeColor = System.Drawing.Color.Black;
            this._comment.Location = new System.Drawing.Point(5, 53);
            this._comment.Name = "_comment";
            this._comment.Size = new System.Drawing.Size(320, 32);
            this._comment.TabIndex = 4;
            this._comment.Text = "Resolving user story #192 - refactoring the dependency on the calendar control";
            // 
            // _duration
            // 
            this._duration.AutoSize = true;
            this._duration.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._duration.ForeColor = System.Drawing.Color.Black;
            this._duration.Location = new System.Drawing.Point(5, 85);
            this._duration.Name = "_duration";
            this._duration.Size = new System.Drawing.Size(36, 17);
            this._duration.TabIndex = 5;
            this._duration.Text = "9:53";
            // 
            // _details
            // 
            this._details.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._details.BackColor = System.Drawing.Color.Transparent;
            this._details.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._details.ForeColor = System.Drawing.Color.Black;
            this._details.Image = global::SirenOfShame.Properties.Resources.nav_up_right;
            this._details.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._details.LinkArea = new System.Windows.Forms.LinkArea(0, 0);
            this._details.LinkColor = System.Drawing.Color.White;
            this._details.Location = new System.Drawing.Point(258, 82);
            this._details.Name = "_details";
            this._details.Size = new System.Drawing.Size(57, 20);
            this._details.TabIndex = 6;
            this._details.Text = "Details";
            this._details.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._details.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._details_LinkClicked);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList1.Images.SetKeyName(0, "error.bmp");
            this.imageList1.Images.SetKeyName(1, "ok.bmp");
            this.imageList1.Images.SetKeyName(2, "unknown.bmp");
            this.imageList1.Images.SetKeyName(3, "clock.bmp");
            // 
            // _buildStatusIcon
            // 
            this._buildStatusIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._buildStatusIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(95)))), ((int)(((byte)(152)))));
            this._buildStatusIcon.ImageKey = "clock.bmp";
            this._buildStatusIcon.ImageList = this.imageList1;
            this._buildStatusIcon.Location = new System.Drawing.Point(304, 0);
            this._buildStatusIcon.Name = "_buildStatusIcon";
            this._buildStatusIcon.Size = new System.Drawing.Size(24, 24);
            this._buildStatusIcon.TabIndex = 7;
            // 
            // ViewBuildSmall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._buildStatusIcon);
            this.Controls.Add(this._details);
            this.Controls.Add(this._duration);
            this.Controls.Add(this._comment);
            this.Controls.Add(this._requestedBy);
            this.Controls.Add(this._startTime);
            this.Controls.Add(this._projectName);
            this.Name = "ViewBuildSmall";
            this.Size = new System.Drawing.Size(328, 112);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _projectName;
        private System.Windows.Forms.Label _startTime;
        private System.Windows.Forms.Label _requestedBy;
        private System.Windows.Forms.Label _comment;
        private System.Windows.Forms.Label _duration;
        private System.Windows.Forms.LinkLabel _details;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label _buildStatusIcon;
    }
}
