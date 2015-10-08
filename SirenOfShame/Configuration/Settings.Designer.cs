using SirenOfShame.Lib;

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
            this._ok = new SirenOfShame.Lib.SosButton();
            this._cancel = new SirenOfShame.Lib.SosButton();
            this._duration = new System.Windows.Forms.Label();
            this._pollInterval = new System.Windows.Forms.TrackBar();
            this._viewLog = new SirenOfShame.Lib.SosButton();
            this._updateLocations = new System.Windows.Forms.Panel();
            this._updateLocationNever = new System.Windows.Forms.RadioButton();
            this._updateLocationOtherLocation = new System.Windows.Forms.TextBox();
            this._updateLocationOther = new System.Windows.Forms.RadioButton();
            this._checkForUpdates = new SirenOfShame.Lib.SosButton();
            this._updateLocationAuto = new System.Windows.Forms.RadioButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this._neverShowAchievements = new System.Windows.Forms.RadioButton();
            this._alwaysShowNewAchievements = new System.Windows.Forms.RadioButton();
            this._onlyShowMyAchievements = new System.Windows.Forms.RadioButton();
            this._userIAm = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ResetReputation = new SirenOfShame.Lib.SosButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this._startInFullscreen = new System.Windows.Forms.CheckBox();
            this._alwaysOnTop = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._pollInterval)).BeginInit();
            this._updateLocations.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // _ok
            // 
            this._ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._ok.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this._ok.FlatAppearance.BorderSize = 0;
            this._ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._ok.ForeColor = System.Drawing.Color.White;
            this._ok.Location = new System.Drawing.Point(420, 430);
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
            this._cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this._cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancel.FlatAppearance.BorderSize = 0;
            this._cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._cancel.ForeColor = System.Drawing.Color.White;
            this._cancel.Location = new System.Drawing.Point(501, 430);
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
            this._duration.Location = new System.Drawing.Point(8, 73);
            this._duration.Name = "_duration";
            this._duration.Size = new System.Drawing.Size(59, 13);
            this._duration.TabIndex = 7;
            this._duration.Text = "X Seconds";
            // 
            // _pollInterval
            // 
            this._pollInterval.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._pollInterval.Location = new System.Drawing.Point(11, 29);
            this._pollInterval.Maximum = 60;
            this._pollInterval.Name = "_pollInterval";
            this._pollInterval.Size = new System.Drawing.Size(546, 45);
            this._pollInterval.TabIndex = 0;
            this._pollInterval.Value = 1;
            this._pollInterval.ValueChanged += new System.EventHandler(this.PollIntervalValueChanged);
            // 
            // _viewLog
            // 
            this._viewLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._viewLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this._viewLog.FlatAppearance.BorderSize = 0;
            this._viewLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._viewLog.ForeColor = System.Drawing.Color.White;
            this._viewLog.Location = new System.Drawing.Point(339, 430);
            this._viewLog.Name = "_viewLog";
            this._viewLog.Size = new System.Drawing.Size(75, 23);
            this._viewLog.TabIndex = 8;
            this._viewLog.Text = "View Log...";
            this._viewLog.UseVisualStyleBackColor = true;
            this._viewLog.Click += new System.EventHandler(this.ViewLogClick);
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
            this._updateLocations.Location = new System.Drawing.Point(10, 26);
            this._updateLocations.Name = "_updateLocations";
            this._updateLocations.Size = new System.Drawing.Size(546, 120);
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
            this._updateLocationOtherLocation.Size = new System.Drawing.Size(487, 20);
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
            this._checkForUpdates.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this._checkForUpdates.FlatAppearance.BorderSize = 0;
            this._checkForUpdates.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._checkForUpdates.ForeColor = System.Drawing.Color.White;
            this._checkForUpdates.Location = new System.Drawing.Point(404, 94);
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
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.ForeColor = System.Drawing.Color.Gray;
            this.textBox1.Location = new System.Drawing.Point(56, 48);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(483, 44);
            this.textBox1.TabIndex = 12;
            this.textBox1.Text = "Examples:\r\nfile:///c|/temp/\r\nhttp://myupdate.com/";
            // 
            // _neverShowAchievements
            // 
            this._neverShowAchievements.AutoSize = true;
            this._neverShowAchievements.BackColor = System.Drawing.Color.Transparent;
            this._neverShowAchievements.ForeColor = System.Drawing.Color.White;
            this._neverShowAchievements.Location = new System.Drawing.Point(203, 47);
            this._neverShowAchievements.Name = "_neverShowAchievements";
            this._neverShowAchievements.Size = new System.Drawing.Size(151, 17);
            this._neverShowAchievements.TabIndex = 19;
            this._neverShowAchievements.Text = "Never show achievements";
            this._neverShowAchievements.UseVisualStyleBackColor = false;
            // 
            // _alwaysShowNewAchievements
            // 
            this._alwaysShowNewAchievements.AutoSize = true;
            this._alwaysShowNewAchievements.BackColor = System.Drawing.Color.Transparent;
            this._alwaysShowNewAchievements.Checked = true;
            this._alwaysShowNewAchievements.ForeColor = System.Drawing.Color.White;
            this._alwaysShowNewAchievements.Location = new System.Drawing.Point(203, 17);
            this._alwaysShowNewAchievements.Name = "_alwaysShowNewAchievements";
            this._alwaysShowNewAchievements.Size = new System.Drawing.Size(198, 17);
            this._alwaysShowNewAchievements.TabIndex = 18;
            this._alwaysShowNewAchievements.TabStop = true;
            this._alwaysShowNewAchievements.Text = "Show everyone\'s new achievements";
            this._alwaysShowNewAchievements.UseVisualStyleBackColor = false;
            // 
            // _onlyShowMyAchievements
            // 
            this._onlyShowMyAchievements.AutoSize = true;
            this._onlyShowMyAchievements.BackColor = System.Drawing.Color.Transparent;
            this._onlyShowMyAchievements.ForeColor = System.Drawing.Color.White;
            this._onlyShowMyAchievements.Location = new System.Drawing.Point(203, 32);
            this._onlyShowMyAchievements.Name = "_onlyShowMyAchievements";
            this._onlyShowMyAchievements.Size = new System.Drawing.Size(182, 17);
            this._onlyShowMyAchievements.TabIndex = 17;
            this._onlyShowMyAchievements.Text = "Only show my new achievements";
            this._onlyShowMyAchievements.UseVisualStyleBackColor = false;
            // 
            // _userIAm
            // 
            this._userIAm.DisplayMember = "DisplayName";
            this._userIAm.ForeColor = System.Drawing.SystemColors.ControlText;
            this._userIAm.FormattingEnabled = true;
            this._userIAm.Location = new System.Drawing.Point(57, 29);
            this._userIAm.Name = "_userIAm";
            this._userIAm.Size = new System.Drawing.Size(121, 21);
            this._userIAm.TabIndex = 20;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 32);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 27;
            this.label7.Text = "I Am:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this._pollInterval);
            this.groupBox1.Controls.Add(this._duration);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(12, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(564, 102);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Poll Interval";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this._updateLocations);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(13, 119);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(563, 152);
            this.groupBox2.TabIndex = 29;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Update Location";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.ResetReputation);
            this.groupBox3.Controls.Add(this._onlyShowMyAchievements);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this._alwaysShowNewAchievements);
            this.groupBox3.Controls.Add(this._neverShowAchievements);
            this.groupBox3.Controls.Add(this._userIAm);
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(13, 278);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(563, 81);
            this.groupBox3.TabIndex = 30;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Reputation && Achievements";
            // 
            // ResetReputation
            // 
            this.ResetReputation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.ResetReputation.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this.ResetReputation.FlatAppearance.BorderSize = 0;
            this.ResetReputation.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(55)))), ((int)(((byte)(0)))));
            this.ResetReputation.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(65)))), ((int)(((byte)(0)))));
            this.ResetReputation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ResetReputation.ForeColor = System.Drawing.Color.White;
            this.ResetReputation.Location = new System.Drawing.Point(414, 41);
            this.ResetReputation.Name = "ResetReputation";
            this.ResetReputation.Size = new System.Drawing.Size(139, 23);
            this.ResetReputation.TabIndex = 28;
            this.ResetReputation.Text = "Reset Reputation ...";
            this.ResetReputation.UseVisualStyleBackColor = false;
            this.ResetReputation.Click += new System.EventHandler(this.ResetReputationClick);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this._startInFullscreen);
            this.groupBox4.Controls.Add(this._alwaysOnTop);
            this.groupBox4.ForeColor = System.Drawing.Color.White;
            this.groupBox4.Location = new System.Drawing.Point(13, 365);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(564, 54);
            this.groupBox4.TabIndex = 31;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Misc";
            // 
            // _startInFullscreen
            // 
            this._startInFullscreen.AutoSize = true;
            this._startInFullscreen.Location = new System.Drawing.Point(216, 19);
            this._startInFullscreen.Name = "_startInFullscreen";
            this._startInFullscreen.Size = new System.Drawing.Size(111, 17);
            this._startInFullscreen.TabIndex = 1;
            this._startInFullscreen.Text = "Start In Fullscreen";
            this._startInFullscreen.UseVisualStyleBackColor = true;
            // 
            // _alwaysOnTop
            // 
            this._alwaysOnTop.AutoSize = true;
            this._alwaysOnTop.Location = new System.Drawing.Point(15, 19);
            this._alwaysOnTop.Name = "_alwaysOnTop";
            this._alwaysOnTop.Size = new System.Drawing.Size(194, 17);
            this._alwaysOnTop.TabIndex = 0;
            this._alwaysOnTop.Text = "Keep Main Window Always On Top";
            this._alwaysOnTop.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(356, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(193, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "(Zoom with Ctrl+ Ctrl- and Ctrl-0 to reset)";
            // 
            // Settings
            // 
            this.AcceptButton = this._ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this.CancelButton = this._cancel;
            this.ClientSize = new System.Drawing.Size(588, 465);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this._viewLog);
            this.Controls.Add(this._cancel);
            this.Controls.Add(this._ok);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Settings";
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this._pollInterval)).EndInit();
            this._updateLocations.ResumeLayout(false);
            this._updateLocations.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TrackBar _pollInterval;
        private SosButton _ok;
        private SosButton _cancel;
        private System.Windows.Forms.Label _duration;
        private SosButton _viewLog;
        private System.Windows.Forms.Panel _updateLocations;
        private System.Windows.Forms.RadioButton _updateLocationAuto;
        private System.Windows.Forms.TextBox _updateLocationOtherLocation;
        private System.Windows.Forms.RadioButton _updateLocationOther;
        private System.Windows.Forms.TextBox textBox1;
        private SosButton _checkForUpdates;
        private System.Windows.Forms.RadioButton _updateLocationNever;
        private System.Windows.Forms.RadioButton _neverShowAchievements;
        private System.Windows.Forms.RadioButton _alwaysShowNewAchievements;
        private System.Windows.Forms.RadioButton _onlyShowMyAchievements;
        private System.Windows.Forms.ComboBox _userIAm;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox _alwaysOnTop;
        private SosButton ResetReputation;
        private System.Windows.Forms.CheckBox _startInFullscreen;
        private System.Windows.Forms.Label label1;
    }
}