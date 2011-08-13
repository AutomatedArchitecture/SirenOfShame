namespace SirenOfShame.HardwareTestGui
{
    partial class InstallFirmware
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
            this._begin = new System.Windows.Forms.Button();
            this._browse = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this._firmwareFile = new System.Windows.Forms.TextBox();
            this._progressBar = new System.Windows.Forms.ProgressBar();
            this._status = new System.Windows.Forms.Label();
            this._deviceInfo = new System.Windows.Forms.TextBox();
            this.deviceConnect1 = new SirenOfShame.HardwareTestGui.DeviceConnect();
            this.SuspendLayout();
            // 
            // _begin
            // 
            this._begin.Location = new System.Drawing.Point(6, 31);
            this._begin.Name = "_begin";
            this._begin.Size = new System.Drawing.Size(75, 23);
            this._begin.TabIndex = 0;
            this._begin.Text = "Begin";
            this._begin.UseVisualStyleBackColor = true;
            this._begin.Click += new System.EventHandler(this._begin_Click);
            // 
            // _browse
            // 
            this._browse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._browse.Location = new System.Drawing.Point(271, 3);
            this._browse.Name = "_browse";
            this._browse.Size = new System.Drawing.Size(75, 23);
            this._browse.TabIndex = 2;
            this._browse.Text = "Browse...";
            this._browse.UseVisualStyleBackColor = true;
            this._browse.Click += new System.EventHandler(this._browse_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "File";
            // 
            // _firmwareFile
            // 
            this._firmwareFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._firmwareFile.Location = new System.Drawing.Point(32, 5);
            this._firmwareFile.Name = "_firmwareFile";
            this._firmwareFile.Size = new System.Drawing.Size(233, 20);
            this._firmwareFile.TabIndex = 4;
            // 
            // _progressBar
            // 
            this._progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._progressBar.Location = new System.Drawing.Point(6, 60);
            this._progressBar.Name = "_progressBar";
            this._progressBar.Size = new System.Drawing.Size(340, 23);
            this._progressBar.TabIndex = 5;
            // 
            // _status
            // 
            this._status.AutoSize = true;
            this._status.Location = new System.Drawing.Point(3, 86);
            this._status.Name = "_status";
            this._status.Size = new System.Drawing.Size(71, 13);
            this._status.TabIndex = 6;
            this._status.Text = "Nothing to do";
            // 
            // _deviceInfo
            // 
            this._deviceInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._deviceInfo.Location = new System.Drawing.Point(6, 111);
            this._deviceInfo.Multiline = true;
            this._deviceInfo.Name = "_deviceInfo";
            this._deviceInfo.ReadOnly = true;
            this._deviceInfo.Size = new System.Drawing.Size(340, 188);
            this._deviceInfo.TabIndex = 7;
            // 
            // deviceConnect1
            // 
            this.deviceConnect1.Location = new System.Drawing.Point(6, 305);
            this.deviceConnect1.Name = "deviceConnect1";
            this.deviceConnect1.Size = new System.Drawing.Size(162, 30);
            this.deviceConnect1.TabIndex = 8;
            // 
            // InstallFirmware
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.deviceConnect1);
            this.Controls.Add(this._deviceInfo);
            this.Controls.Add(this._status);
            this.Controls.Add(this._progressBar);
            this.Controls.Add(this._firmwareFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._browse);
            this.Controls.Add(this._begin);
            this.Name = "InstallFirmware";
            this.Size = new System.Drawing.Size(349, 348);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _begin;
        private System.Windows.Forms.Button _browse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _firmwareFile;
        private System.Windows.Forms.ProgressBar _progressBar;
        private System.Windows.Forms.Label _status;
        private System.Windows.Forms.TextBox _deviceInfo;
        private DeviceConnect deviceConnect1;
    }
}
