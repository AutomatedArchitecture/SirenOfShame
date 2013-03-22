namespace SimpleSirenOfShameDeviceExample
{
    partial class SimpleSirenOfShameExampleForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SimpleSirenOfShameExampleForm));
            this._tryConnect = new System.Windows.Forms.Button();
            this._disconnect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this._connectionStatus = new System.Windows.Forms.Label();
            this._deviceInfo = new System.Windows.Forms.TextBox();
            this._refreshDeviceInfo = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this._audioGroupBox = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._audioPattern = new System.Windows.Forms.ComboBox();
            this._audioDuration = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this._playAudio = new System.Windows.Forms.Button();
            this._ledGroupBox = new System.Windows.Forms.GroupBox();
            this._playLed = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this._ledDuration = new System.Windows.Forms.NumericUpDown();
            this._ledPattern = new System.Windows.Forms.ComboBox();
            this._stopAudio = new System.Windows.Forms.Button();
            this._stopLed = new System.Windows.Forms.Button();
            this._audioGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._audioDuration)).BeginInit();
            this._ledGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._ledDuration)).BeginInit();
            this.SuspendLayout();
            // 
            // _tryConnect
            // 
            this._tryConnect.Location = new System.Drawing.Point(12, 25);
            this._tryConnect.Name = "_tryConnect";
            this._tryConnect.Size = new System.Drawing.Size(75, 23);
            this._tryConnect.TabIndex = 0;
            this._tryConnect.Text = "Try Connect";
            this._tryConnect.UseVisualStyleBackColor = true;
            this._tryConnect.Click += new System.EventHandler(this._tryConnect_Click);
            // 
            // _disconnect
            // 
            this._disconnect.Location = new System.Drawing.Point(12, 54);
            this._disconnect.Name = "_disconnect";
            this._disconnect.Size = new System.Drawing.Size(75, 23);
            this._disconnect.TabIndex = 1;
            this._disconnect.Text = "Disconnect";
            this._disconnect.UseVisualStyleBackColor = true;
            this._disconnect.Click += new System.EventHandler(this._disconnect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Connection Status:";
            // 
            // _connectionStatus
            // 
            this._connectionStatus.AutoSize = true;
            this._connectionStatus.Location = new System.Drawing.Point(115, 9);
            this._connectionStatus.Name = "_connectionStatus";
            this._connectionStatus.Size = new System.Drawing.Size(73, 13);
            this._connectionStatus.TabIndex = 3;
            this._connectionStatus.Text = "Disconnected";
            // 
            // _deviceInfo
            // 
            this._deviceInfo.Location = new System.Drawing.Point(225, 25);
            this._deviceInfo.Multiline = true;
            this._deviceInfo.Name = "_deviceInfo";
            this._deviceInfo.ReadOnly = true;
            this._deviceInfo.Size = new System.Drawing.Size(254, 245);
            this._deviceInfo.TabIndex = 4;
            // 
            // _refreshDeviceInfo
            // 
            this._refreshDeviceInfo.Location = new System.Drawing.Point(404, 276);
            this._refreshDeviceInfo.Name = "_refreshDeviceInfo";
            this._refreshDeviceInfo.Size = new System.Drawing.Size(75, 23);
            this._refreshDeviceInfo.TabIndex = 5;
            this._refreshDeviceInfo.Text = "Refresh Device Info";
            this._refreshDeviceInfo.UseVisualStyleBackColor = true;
            this._refreshDeviceInfo.Click += new System.EventHandler(this._refreshDeviceInfo_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(222, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Device Info:";
            // 
            // _audioGroupBox
            // 
            this._audioGroupBox.Controls.Add(this._stopAudio);
            this._audioGroupBox.Controls.Add(this._playAudio);
            this._audioGroupBox.Controls.Add(this.label5);
            this._audioGroupBox.Controls.Add(this.label4);
            this._audioGroupBox.Controls.Add(this.label3);
            this._audioGroupBox.Controls.Add(this._audioDuration);
            this._audioGroupBox.Controls.Add(this._audioPattern);
            this._audioGroupBox.Location = new System.Drawing.Point(12, 83);
            this._audioGroupBox.Name = "_audioGroupBox";
            this._audioGroupBox.Size = new System.Drawing.Size(200, 105);
            this._audioGroupBox.TabIndex = 7;
            this._audioGroupBox.TabStop = false;
            this._audioGroupBox.Text = "Audio";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Duration:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Pattern:";
            // 
            // _audioPattern
            // 
            this._audioPattern.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._audioPattern.FormattingEnabled = true;
            this._audioPattern.Location = new System.Drawing.Point(62, 19);
            this._audioPattern.Name = "_audioPattern";
            this._audioPattern.Size = new System.Drawing.Size(132, 21);
            this._audioPattern.TabIndex = 10;
            // 
            // _audioDuration
            // 
            this._audioDuration.Location = new System.Drawing.Point(62, 46);
            this._audioDuration.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this._audioDuration.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._audioDuration.Name = "_audioDuration";
            this._audioDuration.Size = new System.Drawing.Size(69, 20);
            this._audioDuration.TabIndex = 11;
            this._audioDuration.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(137, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "sec";
            // 
            // _playAudio
            // 
            this._playAudio.Location = new System.Drawing.Point(119, 72);
            this._playAudio.Name = "_playAudio";
            this._playAudio.Size = new System.Drawing.Size(75, 23);
            this._playAudio.TabIndex = 8;
            this._playAudio.Text = "Play";
            this._playAudio.UseVisualStyleBackColor = true;
            this._playAudio.Click += new System.EventHandler(this._playAudio_Click);
            // 
            // _ledGroupBox
            // 
            this._ledGroupBox.Controls.Add(this._stopLed);
            this._ledGroupBox.Controls.Add(this._playLed);
            this._ledGroupBox.Controls.Add(this.label6);
            this._ledGroupBox.Controls.Add(this.label7);
            this._ledGroupBox.Controls.Add(this.label8);
            this._ledGroupBox.Controls.Add(this._ledDuration);
            this._ledGroupBox.Controls.Add(this._ledPattern);
            this._ledGroupBox.Location = new System.Drawing.Point(12, 194);
            this._ledGroupBox.Name = "_ledGroupBox";
            this._ledGroupBox.Size = new System.Drawing.Size(200, 105);
            this._ledGroupBox.TabIndex = 13;
            this._ledGroupBox.TabStop = false;
            this._ledGroupBox.Text = "LED";
            // 
            // _playLed
            // 
            this._playLed.Location = new System.Drawing.Point(119, 72);
            this._playLed.Name = "_playLed";
            this._playLed.Size = new System.Drawing.Size(75, 23);
            this._playLed.TabIndex = 8;
            this._playLed.Text = "Play";
            this._playLed.UseVisualStyleBackColor = true;
            this._playLed.Click += new System.EventHandler(this._playLed_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(137, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "sec";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Pattern:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Duration:";
            // 
            // _ledDuration
            // 
            this._ledDuration.Location = new System.Drawing.Point(62, 46);
            this._ledDuration.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this._ledDuration.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._ledDuration.Name = "_ledDuration";
            this._ledDuration.Size = new System.Drawing.Size(69, 20);
            this._ledDuration.TabIndex = 11;
            this._ledDuration.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // _ledPattern
            // 
            this._ledPattern.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._ledPattern.FormattingEnabled = true;
            this._ledPattern.Location = new System.Drawing.Point(62, 19);
            this._ledPattern.Name = "_ledPattern";
            this._ledPattern.Size = new System.Drawing.Size(132, 21);
            this._ledPattern.TabIndex = 10;
            // 
            // _stopAudio
            // 
            this._stopAudio.Location = new System.Drawing.Point(38, 72);
            this._stopAudio.Name = "_stopAudio";
            this._stopAudio.Size = new System.Drawing.Size(75, 23);
            this._stopAudio.TabIndex = 14;
            this._stopAudio.Text = "Stop";
            this._stopAudio.UseVisualStyleBackColor = true;
            this._stopAudio.Click += new System.EventHandler(this._stopAudio_Click);
            // 
            // _stopLed
            // 
            this._stopLed.Location = new System.Drawing.Point(38, 72);
            this._stopLed.Name = "_stopLed";
            this._stopLed.Size = new System.Drawing.Size(75, 23);
            this._stopLed.TabIndex = 15;
            this._stopLed.Text = "Stop";
            this._stopLed.UseVisualStyleBackColor = true;
            this._stopLed.Click += new System.EventHandler(this._stopLed_Click);
            // 
            // SimpleSirenOfShameExampleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 311);
            this.Controls.Add(this._ledGroupBox);
            this.Controls.Add(this._audioGroupBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._refreshDeviceInfo);
            this.Controls.Add(this._deviceInfo);
            this.Controls.Add(this._connectionStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._disconnect);
            this.Controls.Add(this._tryConnect);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SimpleSirenOfShameExampleForm";
            this.Text = "Simple Siren of Shame API Example";
            this._audioGroupBox.ResumeLayout(false);
            this._audioGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._audioDuration)).EndInit();
            this._ledGroupBox.ResumeLayout(false);
            this._ledGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._ledDuration)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _tryConnect;
        private System.Windows.Forms.Button _disconnect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label _connectionStatus;
        private System.Windows.Forms.TextBox _deviceInfo;
        private System.Windows.Forms.Button _refreshDeviceInfo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox _audioGroupBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown _audioDuration;
        private System.Windows.Forms.ComboBox _audioPattern;
        private System.Windows.Forms.Button _playAudio;
        private System.Windows.Forms.GroupBox _ledGroupBox;
        private System.Windows.Forms.Button _playLed;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown _ledDuration;
        private System.Windows.Forms.ComboBox _ledPattern;
        private System.Windows.Forms.Button _stopAudio;
        private System.Windows.Forms.Button _stopLed;
    }
}

