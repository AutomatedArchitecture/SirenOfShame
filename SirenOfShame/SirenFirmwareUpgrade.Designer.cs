namespace SirenOfShame
{
    partial class SirenFirmwareUpgrade
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SirenFirmwareUpgrade));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._currentVersion = new System.Windows.Forms.TextBox();
            this._newVersion = new System.Windows.Forms.TextBox();
            this._cancel = new System.Windows.Forms.Button();
            this._upgrade = new System.Windows.Forms.Button();
            this._status = new System.Windows.Forms.Label();
            this._progressBar = new System.Windows.Forms.ProgressBar();
            this._selectFile = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Current Version:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "New Version:";
            // 
            // _currentVersion
            // 
            this._currentVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._currentVersion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._currentVersion.Location = new System.Drawing.Point(100, 12);
            this._currentVersion.Name = "_currentVersion";
            this._currentVersion.ReadOnly = true;
            this._currentVersion.Size = new System.Drawing.Size(193, 13);
            this._currentVersion.TabIndex = 2;
            this._currentVersion.TabStop = false;
            this._currentVersion.Text = "Current Version #";
            // 
            // _newVersion
            // 
            this._newVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._newVersion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._newVersion.Location = new System.Drawing.Point(100, 31);
            this._newVersion.Name = "_newVersion";
            this._newVersion.ReadOnly = true;
            this._newVersion.Size = new System.Drawing.Size(193, 13);
            this._newVersion.TabIndex = 3;
            this._newVersion.TabStop = false;
            this._newVersion.Text = "New Version #";
            // 
            // _cancel
            // 
            this._cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cancel.Location = new System.Drawing.Point(218, 96);
            this._cancel.Name = "_cancel";
            this._cancel.Size = new System.Drawing.Size(75, 23);
            this._cancel.TabIndex = 1;
            this._cancel.Text = "Cancel";
            this._cancel.UseVisualStyleBackColor = true;
            this._cancel.Click += new System.EventHandler(this._cancel_Click);
            // 
            // _upgrade
            // 
            this._upgrade.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._upgrade.Enabled = false;
            this._upgrade.Location = new System.Drawing.Point(137, 96);
            this._upgrade.Name = "_upgrade";
            this._upgrade.Size = new System.Drawing.Size(75, 23);
            this._upgrade.TabIndex = 5;
            this._upgrade.Text = "Upgrade";
            this._upgrade.UseVisualStyleBackColor = true;
            this._upgrade.Click += new System.EventHandler(this._upgrade_Click);
            // 
            // _status
            // 
            this._status.AutoSize = true;
            this._status.Location = new System.Drawing.Point(12, 76);
            this._status.Name = "_status";
            this._status.Size = new System.Drawing.Size(132, 13);
            this._status.TabIndex = 6;
            this._status.Text = "Click Upgrade when ready";
            // 
            // _progressBar
            // 
            this._progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._progressBar.Location = new System.Drawing.Point(12, 50);
            this._progressBar.Name = "_progressBar";
            this._progressBar.Size = new System.Drawing.Size(281, 23);
            this._progressBar.TabIndex = 7;
            // 
            // _selectFile
            // 
            this._selectFile.Location = new System.Drawing.Point(56, 96);
            this._selectFile.Name = "_selectFile";
            this._selectFile.Size = new System.Drawing.Size(75, 23);
            this._selectFile.TabIndex = 8;
            this._selectFile.Text = "Select File";
            this._selectFile.UseVisualStyleBackColor = true;
            this._selectFile.Click += new System.EventHandler(this._selectFile_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Siren of Shame Firmware|*.xml";
            // 
            // SirenFirmwareUpgrade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 131);
            this.Controls.Add(this._selectFile);
            this.Controls.Add(this._status);
            this.Controls.Add(this._upgrade);
            this.Controls.Add(this._cancel);
            this.Controls.Add(this._newVersion);
            this.Controls.Add(this._currentVersion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._progressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SirenFirmwareUpgrade";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SirenFirmwareUpgrade";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SirenFirmwareUpgrade_FormClosing);
            this.Load += new System.EventHandler(this.SirenFirmwareUpgrade_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox _currentVersion;
        private System.Windows.Forms.TextBox _newVersion;
        private System.Windows.Forms.Button _cancel;
        private System.Windows.Forms.Button _upgrade;
        private System.Windows.Forms.Label _status;
        private System.Windows.Forms.ProgressBar _progressBar;
        private System.Windows.Forms.Button _selectFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}