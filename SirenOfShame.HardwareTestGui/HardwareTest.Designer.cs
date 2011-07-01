namespace SirenOfShame.HardwareTestGui
{
    partial class HardwareTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HardwareTest));
            this._connect = new System.Windows.Forms.Button();
            this._disconnect = new System.Windows.Forms.Button();
            this._ledPatterns = new System.Windows.Forms.ListView();
            this._ledPatternNameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._audioPatterns = new System.Windows.Forms.ListView();
            this._audioPatternNameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this._audioDuration = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this._audioStart = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this._ledStartStop = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this._ledDuration = new System.Windows.Forms.NumericUpDown();
            this._audioStop = new System.Windows.Forms.Button();
            this._ledStop = new System.Windows.Forms.Button();
            this._startBoth = new System.Windows.Forms.Button();
            this._stopBoth = new System.Windows.Forms.Button();
            this._firmwareUpgrade = new System.Windows.Forms.Button();
            this._readDeviceInfo = new System.Windows.Forms.Button();
            this._deviceInfo = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._audioPattern2 = new System.Windows.Forms.TextBox();
            this._audioPattern1 = new System.Windows.Forms.TextBox();
            this._audioPatternName2 = new System.Windows.Forms.TextBox();
            this._audioPatternName1 = new System.Windows.Forms.TextBox();
            this._ledPattern2 = new System.Windows.Forms.TextBox();
            this._ledPatternName2 = new System.Windows.Forms.TextBox();
            this._ledPatternName1 = new System.Windows.Forms.TextBox();
            this._send = new System.Windows.Forms.Button();
            this._ledPattern1 = new System.Windows.Forms.TextBox();
            this._uploadProgress = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this._audioDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._ledDuration)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _connect
            // 
            this._connect.Location = new System.Drawing.Point(12, 12);
            this._connect.Name = "_connect";
            this._connect.Size = new System.Drawing.Size(75, 23);
            this._connect.TabIndex = 0;
            this._connect.Text = "Connect";
            this._connect.UseVisualStyleBackColor = true;
            this._connect.Click += new System.EventHandler(this._connect_Click);
            // 
            // _disconnect
            // 
            this._disconnect.Location = new System.Drawing.Point(93, 12);
            this._disconnect.Name = "_disconnect";
            this._disconnect.Size = new System.Drawing.Size(75, 23);
            this._disconnect.TabIndex = 1;
            this._disconnect.Text = "Disconnect";
            this._disconnect.UseVisualStyleBackColor = true;
            this._disconnect.Click += new System.EventHandler(this._disconnect_Click);
            // 
            // _ledPatterns
            // 
            this._ledPatterns.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._ledPatternNameColumn});
            this._ledPatterns.FullRowSelect = true;
            this._ledPatterns.HideSelection = false;
            this._ledPatterns.Location = new System.Drawing.Point(12, 226);
            this._ledPatterns.MultiSelect = false;
            this._ledPatterns.Name = "_ledPatterns";
            this._ledPatterns.Size = new System.Drawing.Size(363, 97);
            this._ledPatterns.TabIndex = 9;
            this._ledPatterns.UseCompatibleStateImageBehavior = false;
            this._ledPatterns.View = System.Windows.Forms.View.Details;
            // 
            // _ledPatternNameColumn
            // 
            this._ledPatternNameColumn.Text = "Name";
            this._ledPatternNameColumn.Width = 200;
            // 
            // _audioPatterns
            // 
            this._audioPatterns.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._audioPatternNameColumn});
            this._audioPatterns.FullRowSelect = true;
            this._audioPatterns.HideSelection = false;
            this._audioPatterns.Location = new System.Drawing.Point(12, 58);
            this._audioPatterns.MultiSelect = false;
            this._audioPatterns.Name = "_audioPatterns";
            this._audioPatterns.Size = new System.Drawing.Size(363, 97);
            this._audioPatterns.TabIndex = 8;
            this._audioPatterns.UseCompatibleStateImageBehavior = false;
            this._audioPatterns.View = System.Windows.Forms.View.Details;
            // 
            // _audioPatternNameColumn
            // 
            this._audioPatternNameColumn.Text = "Name";
            this._audioPatternNameColumn.Width = 200;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Audio";
            // 
            // _audioDuration
            // 
            this._audioDuration.Location = new System.Drawing.Point(123, 164);
            this._audioDuration.Name = "_audioDuration";
            this._audioDuration.Size = new System.Drawing.Size(70, 20);
            this._audioDuration.TabIndex = 11;
            this._audioDuration.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 166);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Duration (1/10th sec)";
            // 
            // _audioStart
            // 
            this._audioStart.Location = new System.Drawing.Point(199, 161);
            this._audioStart.Name = "_audioStart";
            this._audioStart.Size = new System.Drawing.Size(75, 23);
            this._audioStart.TabIndex = 13;
            this._audioStart.Text = "Start";
            this._audioStart.UseVisualStyleBackColor = true;
            this._audioStart.Click += new System.EventHandler(this._audioStart_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 210);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "LEDs";
            // 
            // _ledStartStop
            // 
            this._ledStartStop.Location = new System.Drawing.Point(199, 329);
            this._ledStartStop.Name = "_ledStartStop";
            this._ledStartStop.Size = new System.Drawing.Size(75, 23);
            this._ledStartStop.TabIndex = 17;
            this._ledStartStop.Text = "Start";
            this._ledStartStop.UseVisualStyleBackColor = true;
            this._ledStartStop.Click += new System.EventHandler(this._ledStart_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 334);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Duration (1/10th sec)";
            // 
            // _ledDuration
            // 
            this._ledDuration.Location = new System.Drawing.Point(123, 332);
            this._ledDuration.Name = "_ledDuration";
            this._ledDuration.Size = new System.Drawing.Size(70, 20);
            this._ledDuration.TabIndex = 15;
            this._ledDuration.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // _audioStop
            // 
            this._audioStop.Location = new System.Drawing.Point(280, 161);
            this._audioStop.Name = "_audioStop";
            this._audioStop.Size = new System.Drawing.Size(75, 23);
            this._audioStop.TabIndex = 18;
            this._audioStop.Text = "Stop";
            this._audioStop.UseVisualStyleBackColor = true;
            this._audioStop.Click += new System.EventHandler(this._audioStop_Click);
            // 
            // _ledStop
            // 
            this._ledStop.Location = new System.Drawing.Point(280, 329);
            this._ledStop.Name = "_ledStop";
            this._ledStop.Size = new System.Drawing.Size(75, 23);
            this._ledStop.TabIndex = 19;
            this._ledStop.Text = "Stop";
            this._ledStop.UseVisualStyleBackColor = true;
            this._ledStop.Click += new System.EventHandler(this._ledStop_Click);
            // 
            // _startBoth
            // 
            this._startBoth.Location = new System.Drawing.Point(123, 368);
            this._startBoth.Name = "_startBoth";
            this._startBoth.Size = new System.Drawing.Size(75, 23);
            this._startBoth.TabIndex = 20;
            this._startBoth.Text = "Start Both";
            this._startBoth.UseVisualStyleBackColor = true;
            this._startBoth.Click += new System.EventHandler(this._startBoth_Click);
            // 
            // _stopBoth
            // 
            this._stopBoth.Location = new System.Drawing.Point(199, 368);
            this._stopBoth.Name = "_stopBoth";
            this._stopBoth.Size = new System.Drawing.Size(75, 23);
            this._stopBoth.TabIndex = 21;
            this._stopBoth.Text = "Stop Both";
            this._stopBoth.UseVisualStyleBackColor = true;
            this._stopBoth.Click += new System.EventHandler(this._stopBoth_Click);
            // 
            // _firmwareUpgrade
            // 
            this._firmwareUpgrade.Location = new System.Drawing.Point(272, 12);
            this._firmwareUpgrade.Name = "_firmwareUpgrade";
            this._firmwareUpgrade.Size = new System.Drawing.Size(103, 23);
            this._firmwareUpgrade.TabIndex = 22;
            this._firmwareUpgrade.Text = "Firmware Upgrade";
            this._firmwareUpgrade.UseVisualStyleBackColor = true;
            this._firmwareUpgrade.Click += new System.EventHandler(this._firmwareUpgrade_Click);
            // 
            // _readDeviceInfo
            // 
            this._readDeviceInfo.Location = new System.Drawing.Point(644, 32);
            this._readDeviceInfo.Name = "_readDeviceInfo";
            this._readDeviceInfo.Size = new System.Drawing.Size(114, 23);
            this._readDeviceInfo.TabIndex = 28;
            this._readDeviceInfo.Text = "Read Device Info";
            this._readDeviceInfo.UseVisualStyleBackColor = true;
            this._readDeviceInfo.Click += new System.EventHandler(this._readDeviceInfo_Click);
            // 
            // _deviceInfo
            // 
            this._deviceInfo.Location = new System.Drawing.Point(644, 61);
            this._deviceInfo.Multiline = true;
            this._deviceInfo.Name = "_deviceInfo";
            this._deviceInfo.Size = new System.Drawing.Size(207, 229);
            this._deviceInfo.TabIndex = 29;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._uploadProgress);
            this.groupBox1.Controls.Add(this._audioPattern2);
            this.groupBox1.Controls.Add(this._audioPattern1);
            this.groupBox1.Controls.Add(this._audioPatternName2);
            this.groupBox1.Controls.Add(this._audioPatternName1);
            this.groupBox1.Controls.Add(this._ledPattern2);
            this.groupBox1.Controls.Add(this._ledPatternName2);
            this.groupBox1.Controls.Add(this._ledPatternName1);
            this.groupBox1.Controls.Add(this._send);
            this.groupBox1.Controls.Add(this._ledPattern1);
            this.groupBox1.Location = new System.Drawing.Point(381, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(257, 371);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Upload";
            // 
            // _audioPattern2
            // 
            this._audioPattern2.Location = new System.Drawing.Point(6, 276);
            this._audioPattern2.Name = "_audioPattern2";
            this._audioPattern2.Size = new System.Drawing.Size(245, 20);
            this._audioPattern2.TabIndex = 36;
            this._audioPattern2.Text = "..\\siren2.wav";
            // 
            // _audioPattern1
            // 
            this._audioPattern1.Location = new System.Drawing.Point(6, 224);
            this._audioPattern1.Name = "_audioPattern1";
            this._audioPattern1.Size = new System.Drawing.Size(245, 20);
            this._audioPattern1.TabIndex = 35;
            this._audioPattern1.Text = "..\\siren1.wav";
            // 
            // _audioPatternName2
            // 
            this._audioPatternName2.Location = new System.Drawing.Point(6, 250);
            this._audioPatternName2.Name = "_audioPatternName2";
            this._audioPatternName2.Size = new System.Drawing.Size(100, 20);
            this._audioPatternName2.TabIndex = 34;
            this._audioPatternName2.Text = "Audio #2";
            // 
            // _audioPatternName1
            // 
            this._audioPatternName1.Location = new System.Drawing.Point(6, 198);
            this._audioPatternName1.Name = "_audioPatternName1";
            this._audioPatternName1.Size = new System.Drawing.Size(100, 20);
            this._audioPatternName1.TabIndex = 33;
            this._audioPatternName1.Text = "Audio #1";
            // 
            // _ledPattern2
            // 
            this._ledPattern2.Location = new System.Drawing.Point(133, 48);
            this._ledPattern2.Multiline = true;
            this._ledPattern2.Name = "_ledPattern2";
            this._ledPattern2.Size = new System.Drawing.Size(118, 139);
            this._ledPattern2.TabIndex = 32;
            this._ledPattern2.Text = resources.GetString("_ledPattern2.Text");
            // 
            // _ledPatternName2
            // 
            this._ledPatternName2.Location = new System.Drawing.Point(133, 22);
            this._ledPatternName2.Name = "_ledPatternName2";
            this._ledPatternName2.Size = new System.Drawing.Size(118, 20);
            this._ledPatternName2.TabIndex = 31;
            this._ledPatternName2.Text = "Led #2";
            // 
            // _ledPatternName1
            // 
            this._ledPatternName1.Location = new System.Drawing.Point(6, 22);
            this._ledPatternName1.Name = "_ledPatternName1";
            this._ledPatternName1.Size = new System.Drawing.Size(121, 20);
            this._ledPatternName1.TabIndex = 30;
            this._ledPatternName1.Text = "Led #1";
            // 
            // _send
            // 
            this._send.Location = new System.Drawing.Point(6, 311);
            this._send.Name = "_send";
            this._send.Size = new System.Drawing.Size(245, 23);
            this._send.TabIndex = 29;
            this._send.Text = "Upload";
            this._send.UseVisualStyleBackColor = true;
            this._send.Click += new System.EventHandler(this._send_Click);
            // 
            // _ledPattern1
            // 
            this._ledPattern1.Location = new System.Drawing.Point(6, 48);
            this._ledPattern1.Multiline = true;
            this._ledPattern1.Name = "_ledPattern1";
            this._ledPattern1.Size = new System.Drawing.Size(121, 139);
            this._ledPattern1.TabIndex = 28;
            this._ledPattern1.Text = resources.GetString("_ledPattern1.Text");
            // 
            // _uploadProgress
            // 
            this._uploadProgress.Location = new System.Drawing.Point(6, 340);
            this._uploadProgress.Name = "_uploadProgress";
            this._uploadProgress.Size = new System.Drawing.Size(245, 23);
            this._uploadProgress.TabIndex = 37;
            // 
            // HardwareTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 403);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this._deviceInfo);
            this.Controls.Add(this._readDeviceInfo);
            this.Controls.Add(this._firmwareUpgrade);
            this.Controls.Add(this._stopBoth);
            this.Controls.Add(this._startBoth);
            this.Controls.Add(this._ledStop);
            this.Controls.Add(this._audioStop);
            this.Controls.Add(this._ledStartStop);
            this.Controls.Add(this.label4);
            this.Controls.Add(this._ledDuration);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._audioStart);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._audioDuration);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._ledPatterns);
            this.Controls.Add(this._audioPatterns);
            this.Controls.Add(this._disconnect);
            this.Controls.Add(this._connect);
            this.Name = "HardwareTest";
            this.Text = "Siren Of Shame - Hardware Test";
            ((System.ComponentModel.ISupportInitialize)(this._audioDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._ledDuration)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _connect;
        private System.Windows.Forms.Button _disconnect;
        private System.Windows.Forms.ListView _ledPatterns;
        private System.Windows.Forms.ColumnHeader _ledPatternNameColumn;
        private System.Windows.Forms.ListView _audioPatterns;
        private System.Windows.Forms.ColumnHeader _audioPatternNameColumn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown _audioDuration;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button _audioStart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button _ledStartStop;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown _ledDuration;
        private System.Windows.Forms.Button _audioStop;
        private System.Windows.Forms.Button _ledStop;
        private System.Windows.Forms.Button _startBoth;
        private System.Windows.Forms.Button _stopBoth;
        private System.Windows.Forms.Button _firmwareUpgrade;
        private System.Windows.Forms.Button _readDeviceInfo;
        private System.Windows.Forms.TextBox _deviceInfo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox _audioPattern2;
        private System.Windows.Forms.TextBox _audioPattern1;
        private System.Windows.Forms.TextBox _audioPatternName2;
        private System.Windows.Forms.TextBox _audioPatternName1;
        private System.Windows.Forms.TextBox _ledPattern2;
        private System.Windows.Forms.TextBox _ledPatternName2;
        private System.Windows.Forms.TextBox _ledPatternName1;
        private System.Windows.Forms.Button _send;
        private System.Windows.Forms.TextBox _ledPattern1;
        private System.Windows.Forms.ProgressBar _uploadProgress;
    }
}

