namespace SirenOfShame.Configuration
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this._ok = new System.Windows.Forms.Button();
            this._cancel = new System.Windows.Forms.Button();
            this._duration = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._pollInterval = new System.Windows.Forms.TrackBar();
            this._viewLog = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this._updateLocations = new System.Windows.Forms.Panel();
            this._updateLocationNever = new System.Windows.Forms.RadioButton();
            this._updateLocationOtherLocation = new System.Windows.Forms.TextBox();
            this._updateLocationOther = new System.Windows.Forms.RadioButton();
            this._checkForUpdates = new System.Windows.Forms.Button();
            this._updateLocationAuto = new System.Windows.Forms.RadioButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this._resetReputation = new System.Windows.Forms.Button();
            this._hideReputation = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this._pollInterval)).BeginInit();
            this._updateLocations.SuspendLayout();
            this.SuspendLayout();
            // 
            // _ok
            // 
            this._ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._ok.Location = new System.Drawing.Point(425, 271);
            this._ok.Name = "_ok";
            this._ok.Size = new System.Drawing.Size(75, 23);
            this._ok.TabIndex = 2;
            this._ok.Text = "Ok";
            this._ok.UseVisualStyleBackColor = true;
            this._ok.Click += new System.EventHandler(this.OkClick);
            // 
            // _cancel
            // 
            this._cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancel.Location = new System.Drawing.Point(506, 271);
            this._cancel.Name = "_cancel";
            this._cancel.Size = new System.Drawing.Size(75, 23);
            this._cancel.TabIndex = 3;
            this._cancel.Text = "Cancel";
            this._cancel.UseVisualStyleBackColor = true;
            this._cancel.Click += new System.EventHandler(this.CancelClick);
            // 
            // _duration
            // 
            this._duration.AutoSize = true;
            this._duration.Location = new System.Drawing.Point(114, 44);
            this._duration.Name = "_duration";
            this._duration.Size = new System.Drawing.Size(59, 13);
            this._duration.TabIndex = 7;
            this._duration.Text = "X Seconds";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Poll interval";
            // 
            // _pollInterval
            // 
            this._pollInterval.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._pollInterval.Location = new System.Drawing.Point(104, 12);
            this._pollInterval.Maximum = 60;
            this._pollInterval.Name = "_pollInterval";
            this._pollInterval.Size = new System.Drawing.Size(477, 45);
            this._pollInterval.TabIndex = 0;
            this._pollInterval.Value = 1;
            this._pollInterval.ValueChanged += new System.EventHandler(this.PollIntervalValueChanged);
            // 
            // _viewLog
            // 
            this._viewLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._viewLog.Location = new System.Drawing.Point(344, 271);
            this._viewLog.Name = "_viewLog";
            this._viewLog.Size = new System.Drawing.Size(75, 23);
            this._viewLog.TabIndex = 8;
            this._viewLog.Text = "View Log...";
            this._viewLog.UseVisualStyleBackColor = true;
            this._viewLog.Click += new System.EventHandler(this.ViewLogClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Update Location";
            // 
            // _updateLocations
            // 
            this._updateLocations.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._updateLocations.Controls.Add(this._updateLocationNever);
            this._updateLocations.Controls.Add(this._updateLocationOtherLocation);
            this._updateLocations.Controls.Add(this._updateLocationOther);
            this._updateLocations.Controls.Add(this._checkForUpdates);
            this._updateLocations.Controls.Add(this._updateLocationAuto);
            this._updateLocations.Controls.Add(this.textBox1);
            this._updateLocations.Location = new System.Drawing.Point(104, 67);
            this._updateLocations.Name = "_updateLocations";
            this._updateLocations.Size = new System.Drawing.Size(477, 120);
            this._updateLocations.TabIndex = 11;
            // 
            // _updateLocationNever
            // 
            this._updateLocationNever.AutoSize = true;
            this._updateLocationNever.Location = new System.Drawing.Point(3, 94);
            this._updateLocationNever.Name = "_updateLocationNever";
            this._updateLocationNever.Size = new System.Drawing.Size(270, 17);
            this._updateLocationNever.TabIndex = 14;
            this._updateLocationNever.Text = "Never check for updates (why mess with perfection)";
            this._updateLocationNever.UseVisualStyleBackColor = true;
            // 
            // _updateLocationOtherLocation
            // 
            this._updateLocationOtherLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._updateLocationOtherLocation.Enabled = false;
            this._updateLocationOtherLocation.Location = new System.Drawing.Point(56, 25);
            this._updateLocationOtherLocation.Name = "_updateLocationOtherLocation";
            this._updateLocationOtherLocation.Size = new System.Drawing.Size(418, 20);
            this._updateLocationOtherLocation.TabIndex = 13;
            // 
            // _updateLocationOther
            // 
            this._updateLocationOther.AutoSize = true;
            this._updateLocationOther.Location = new System.Drawing.Point(3, 26);
            this._updateLocationOther.Name = "_updateLocationOther";
            this._updateLocationOther.Size = new System.Drawing.Size(47, 17);
            this._updateLocationOther.TabIndex = 12;
            this._updateLocationOther.Text = "URL";
            this._updateLocationOther.UseVisualStyleBackColor = true;
            this._updateLocationOther.CheckedChanged += new System.EventHandler(this.UpdateLocationOtherCheckedChanged);
            // 
            // _checkForUpdates
            // 
            this._checkForUpdates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._checkForUpdates.Location = new System.Drawing.Point(335, 94);
            this._checkForUpdates.Name = "_checkForUpdates";
            this._checkForUpdates.Size = new System.Drawing.Size(139, 23);
            this._checkForUpdates.TabIndex = 12;
            this._checkForUpdates.Text = "Check for Updates Now";
            this._checkForUpdates.UseVisualStyleBackColor = true;
            this._checkForUpdates.Click += new System.EventHandler(this.CheckForUpdatesClick);
            // 
            // _updateLocationAuto
            // 
            this._updateLocationAuto.AutoSize = true;
            this._updateLocationAuto.Checked = true;
            this._updateLocationAuto.Location = new System.Drawing.Point(3, 3);
            this._updateLocationAuto.Name = "_updateLocationAuto";
            this._updateLocationAuto.Size = new System.Drawing.Size(177, 17);
            this._updateLocationAuto.TabIndex = 11;
            this._updateLocationAuto.TabStop = true;
            this._updateLocationAuto.Text = "Update from SirenOfShame.com";
            this._updateLocationAuto.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.ForeColor = System.Drawing.Color.Gray;
            this.textBox1.Location = new System.Drawing.Point(56, 48);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(414, 44);
            this.textBox1.TabIndex = 12;
            this.textBox1.Text = "Examples:\r\nfile:///c|/temp/\r\nhttp://myupdate.com/";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 198);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Statistics";
            // 
            // _resetReputation
            // 
            this._resetReputation.Location = new System.Drawing.Point(104, 193);
            this._resetReputation.Name = "_resetReputation";
            this._resetReputation.Size = new System.Drawing.Size(180, 23);
            this._resetReputation.TabIndex = 14;
            this._resetReputation.Text = "Reset User\'s Reputation";
            this._resetReputation.UseVisualStyleBackColor = true;
            this._resetReputation.Click += new System.EventHandler(this.ResetReputationClick);
            // 
            // _hideReputation
            // 
            this._hideReputation.AutoSize = true;
            this._hideReputation.Location = new System.Drawing.Point(290, 197);
            this._hideReputation.Name = "_hideReputation";
            this._hideReputation.Size = new System.Drawing.Size(171, 17);
            this._hideReputation.TabIndex = 15;
            this._hideReputation.Text = "Hide reputation (aka I\'m losing)";
            this._hideReputation.UseVisualStyleBackColor = true;
            // 
            // Settings
            // 
            this.AcceptButton = this._ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this._cancel;
            this.ClientSize = new System.Drawing.Size(593, 306);
            this.Controls.Add(this._hideReputation);
            this.Controls.Add(this._resetReputation);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._updateLocations);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._viewLog);
            this.Controls.Add(this._duration);
            this.Controls.Add(this._cancel);
            this.Controls.Add(this._ok);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._pollInterval);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Settings";
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this._pollInterval)).EndInit();
            this._updateLocations.ResumeLayout(false);
            this._updateLocations.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar _pollInterval;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _ok;
        private System.Windows.Forms.Button _cancel;
        private System.Windows.Forms.Label _duration;
        private System.Windows.Forms.Button _viewLog;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel _updateLocations;
        private System.Windows.Forms.RadioButton _updateLocationAuto;
        private System.Windows.Forms.TextBox _updateLocationOtherLocation;
        private System.Windows.Forms.RadioButton _updateLocationOther;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button _checkForUpdates;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button _resetReputation;
        private System.Windows.Forms.CheckBox _hideReputation;
        private System.Windows.Forms.RadioButton _updateLocationNever;
    }
}