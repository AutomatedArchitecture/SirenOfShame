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
            this._projectName = new System.Windows.Forms.Label();
            this._buildId = new System.Windows.Forms.Label();
            this._startTime = new System.Windows.Forms.Label();
            this._requestedBy = new System.Windows.Forms.Label();
            this._comment = new System.Windows.Forms.Label();
            this._duration = new System.Windows.Forms.Label();
            this._details = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // _projectName
            // 
            this._projectName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._projectName.AutoEllipsis = true;
            this._projectName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(95)))), ((int)(((byte)(152)))));
            this._projectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._projectName.ForeColor = System.Drawing.Color.White;
            this._projectName.Location = new System.Drawing.Point(0, 0);
            this._projectName.Name = "_projectName";
            this._projectName.Size = new System.Drawing.Size(328, 35);
            this._projectName.TabIndex = 0;
            this._projectName.Text = "Project Name";
            // 
            // _buildId
            // 
            this._buildId.AutoSize = true;
            this._buildId.ForeColor = System.Drawing.Color.White;
            this._buildId.Location = new System.Drawing.Point(4, 73);
            this._buildId.Name = "_buildId";
            this._buildId.Size = new System.Drawing.Size(31, 13);
            this._buildId.TabIndex = 1;
            this._buildId.Text = "1002";
            // 
            // _startTime
            // 
            this._startTime.AutoSize = true;
            this._startTime.ForeColor = System.Drawing.Color.White;
            this._startTime.Location = new System.Drawing.Point(5, 54);
            this._startTime.Name = "_startTime";
            this._startTime.Size = new System.Drawing.Size(53, 13);
            this._startTime.TabIndex = 2;
            this._startTime.Text = "8/2/2012";
            // 
            // _requestedBy
            // 
            this._requestedBy.AutoSize = true;
            this._requestedBy.ForeColor = System.Drawing.Color.White;
            this._requestedBy.Location = new System.Drawing.Point(4, 35);
            this._requestedBy.Name = "_requestedBy";
            this._requestedBy.Size = new System.Drawing.Size(82, 13);
            this._requestedBy.TabIndex = 3;
            this._requestedBy.Text = "Lee Richardson";
            // 
            // _comment
            // 
            this._comment.AutoEllipsis = true;
            this._comment.ForeColor = System.Drawing.Color.White;
            this._comment.Location = new System.Drawing.Point(110, 35);
            this._comment.Name = "_comment";
            this._comment.Size = new System.Drawing.Size(215, 65);
            this._comment.TabIndex = 4;
            this._comment.Text = "Resolving user story #192 - refactoring the dependency on the calendar control";
            // 
            // _duration
            // 
            this._duration.AutoSize = true;
            this._duration.ForeColor = System.Drawing.Color.White;
            this._duration.Location = new System.Drawing.Point(5, 92);
            this._duration.Name = "_duration";
            this._duration.Size = new System.Drawing.Size(28, 13);
            this._duration.TabIndex = 5;
            this._duration.Text = "9:53";
            // 
            // _details
            // 
            this._details.AutoSize = true;
            this._details.BackColor = System.Drawing.Color.Transparent;
            this._details.ForeColor = System.Drawing.Color.White;
            this._details.LinkColor = System.Drawing.Color.White;
            this._details.Location = new System.Drawing.Point(286, 100);
            this._details.Name = "_details";
            this._details.Size = new System.Drawing.Size(39, 13);
            this._details.TabIndex = 6;
            this._details.TabStop = true;
            this._details.Text = "Details";
            this._details.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._details_LinkClicked);
            // 
            // ViewBuildSmall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(95)))), ((int)(((byte)(152)))));
            this.Controls.Add(this._details);
            this.Controls.Add(this._duration);
            this.Controls.Add(this._comment);
            this.Controls.Add(this._requestedBy);
            this.Controls.Add(this._startTime);
            this.Controls.Add(this._buildId);
            this.Controls.Add(this._projectName);
            this.Name = "ViewBuildSmall";
            this.Size = new System.Drawing.Size(328, 116);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _projectName;
        private System.Windows.Forms.Label _buildId;
        private System.Windows.Forms.Label _startTime;
        private System.Windows.Forms.Label _requestedBy;
        private System.Windows.Forms.Label _comment;
        private System.Windows.Forms.Label _duration;
        private System.Windows.Forms.LinkLabel _details;
    }
}
