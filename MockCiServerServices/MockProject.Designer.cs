namespace MockCiServerServices
{
    partial class MockProject
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
            this._projectGroupBox = new System.Windows.Forms.GroupBox();
            this._requestedBy = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this._finishTime = new System.Windows.Forms.TextBox();
            this._startTime = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this._status = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this._comment = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._projectGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // _projectGroupBox
            // 
            this._projectGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._projectGroupBox.Controls.Add(this._requestedBy);
            this._projectGroupBox.Controls.Add(this.label5);
            this._projectGroupBox.Controls.Add(this._finishTime);
            this._projectGroupBox.Controls.Add(this._startTime);
            this._projectGroupBox.Controls.Add(this.label4);
            this._projectGroupBox.Controls.Add(this.label3);
            this._projectGroupBox.Controls.Add(this._status);
            this._projectGroupBox.Controls.Add(this.label2);
            this._projectGroupBox.Controls.Add(this._comment);
            this._projectGroupBox.Controls.Add(this.label1);
            this._projectGroupBox.Location = new System.Drawing.Point(3, 3);
            this._projectGroupBox.Name = "_projectGroupBox";
            this._projectGroupBox.Size = new System.Drawing.Size(374, 156);
            this._projectGroupBox.TabIndex = 0;
            this._projectGroupBox.TabStop = false;
            this._projectGroupBox.Text = "Project Name";
            // 
            // _requestedBy
            // 
            this._requestedBy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._requestedBy.Location = new System.Drawing.Point(86, 124);
            this._requestedBy.Name = "_requestedBy";
            this._requestedBy.Size = new System.Drawing.Size(282, 20);
            this._requestedBy.TabIndex = 9;
            this._requestedBy.Text = "Zzz";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 127);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Requested By";
            // 
            // _finishTime
            // 
            this._finishTime.Location = new System.Drawing.Point(86, 98);
            this._finishTime.Name = "_finishTime";
            this._finishTime.ReadOnly = true;
            this._finishTime.Size = new System.Drawing.Size(152, 20);
            this._finishTime.TabIndex = 7;
            // 
            // _startTime
            // 
            this._startTime.Location = new System.Drawing.Point(86, 72);
            this._startTime.Name = "_startTime";
            this._startTime.ReadOnly = true;
            this._startTime.Size = new System.Drawing.Size(152, 20);
            this._startTime.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "End Time";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Start Time";
            // 
            // _status
            // 
            this._status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._status.FormattingEnabled = true;
            this._status.Items.AddRange(new object[] {
            "Unknown",
            "Working",
            "Broken",
            "InProgress"});
            this._status.Location = new System.Drawing.Point(86, 45);
            this._status.Name = "_status";
            this._status.Size = new System.Drawing.Size(121, 21);
            this._status.TabIndex = 3;
            this._status.SelectedIndexChanged += new System.EventHandler(this._status_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Status";
            // 
            // _comment
            // 
            this._comment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._comment.Location = new System.Drawing.Point(86, 19);
            this._comment.Name = "_comment";
            this._comment.Size = new System.Drawing.Size(282, 20);
            this._comment.TabIndex = 1;
            this._comment.Text = "This is a test";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Comment";
            // 
            // MockProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._projectGroupBox);
            this.MaximumSize = new System.Drawing.Size(380, 162);
            this.MinimumSize = new System.Drawing.Size(380, 162);
            this.Name = "MockProject";
            this.Size = new System.Drawing.Size(380, 162);
            this._projectGroupBox.ResumeLayout(false);
            this._projectGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox _projectGroupBox;
        private System.Windows.Forms.TextBox _comment;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox _status;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox _finishTime;
        private System.Windows.Forms.TextBox _startTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox _requestedBy;
        private System.Windows.Forms.Label label5;
    }
}
