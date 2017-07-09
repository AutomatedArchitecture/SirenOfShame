namespace SirenOfShame.HardwareTestGui
{
    partial class FullTest
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._audioPatterns = new System.Windows.Forms.ListView();
            this._audioPatternName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._ledPatterns = new System.Windows.Forms.ListView();
            this._ledPatternName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._deviceInfo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this._audioStart = new System.Windows.Forms.Button();
            this._audioStop = new System.Windows.Forms.Button();
            this._ledsStop = new System.Windows.Forms.Button();
            this._ledsStart = new System.Windows.Forms.Button();
            this._timer = new System.Windows.Forms.Timer(this.components);
            this._audioDuration = new System.Windows.Forms.NumericUpDown();
            this._ledDuration = new System.Windows.Forms.NumericUpDown();
            this._audioRunTime = new System.Windows.Forms.Label();
            this._ledRunTime = new System.Windows.Forms.Label();
            this._configureSiren = new System.Windows.Forms.Button();
            this._uploadPatternsToPro = new System.Windows.Forms.Button();
            this._uploadProgress = new System.Windows.Forms.ProgressBar();
            this._test = new System.Windows.Forms.Button();
            this._runTheGambit = new System.Windows.Forms.Button();
            this._deviceConnect = new SirenOfShame.HardwareTestGui.DeviceConnect();
            ((System.ComponentModel.ISupportInitialize)(this._audioDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._ledDuration)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Audio Patterns";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(192, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "LED Patterns";
            // 
            // _audioPatterns
            // 
            this._audioPatterns.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._audioPatternName});
            this._audioPatterns.FullRowSelect = true;
            this._audioPatterns.HideSelection = false;
            this._audioPatterns.Location = new System.Drawing.Point(10, 103);
            this._audioPatterns.Name = "_audioPatterns";
            this._audioPatterns.Size = new System.Drawing.Size(152, 150);
            this._audioPatterns.TabIndex = 5;
            this._audioPatterns.UseCompatibleStateImageBehavior = false;
            this._audioPatterns.View = System.Windows.Forms.View.Details;
            // 
            // _audioPatternName
            // 
            this._audioPatternName.Text = "Name";
            this._audioPatternName.Width = 100;
            // 
            // _ledPatterns
            // 
            this._ledPatterns.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._ledPatternName});
            this._ledPatterns.FullRowSelect = true;
            this._ledPatterns.HideSelection = false;
            this._ledPatterns.Location = new System.Drawing.Point(168, 103);
            this._ledPatterns.Name = "_ledPatterns";
            this._ledPatterns.Size = new System.Drawing.Size(148, 150);
            this._ledPatterns.TabIndex = 6;
            this._ledPatterns.UseCompatibleStateImageBehavior = false;
            this._ledPatterns.View = System.Windows.Forms.View.Details;
            // 
            // _ledPatternName
            // 
            this._ledPatternName.Text = "Name";
            this._ledPatternName.Width = 100;
            // 
            // _deviceInfo
            // 
            this._deviceInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._deviceInfo.Location = new System.Drawing.Point(329, 103);
            this._deviceInfo.Multiline = true;
            this._deviceInfo.Name = "_deviceInfo";
            this._deviceInfo.ReadOnly = true;
            this._deviceInfo.Size = new System.Drawing.Size(300, 150);
            this._deviceInfo.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(326, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Device Info";
            // 
            // _audioStart
            // 
            this._audioStart.Location = new System.Drawing.Point(10, 285);
            this._audioStart.Name = "_audioStart";
            this._audioStart.Size = new System.Drawing.Size(76, 66);
            this._audioStart.TabIndex = 10;
            this._audioStart.Text = "Start";
            this._audioStart.UseVisualStyleBackColor = true;
            this._audioStart.Click += new System.EventHandler(this._audioStart_Click);
            // 
            // _audioStop
            // 
            this._audioStop.Location = new System.Drawing.Point(92, 285);
            this._audioStop.Name = "_audioStop";
            this._audioStop.Size = new System.Drawing.Size(70, 66);
            this._audioStop.TabIndex = 11;
            this._audioStop.Text = "Stop";
            this._audioStop.UseVisualStyleBackColor = true;
            this._audioStop.Click += new System.EventHandler(this._audioStop_Click);
            // 
            // _ledsStop
            // 
            this._ledsStop.Location = new System.Drawing.Point(247, 285);
            this._ledsStop.Name = "_ledsStop";
            this._ledsStop.Size = new System.Drawing.Size(69, 66);
            this._ledsStop.TabIndex = 13;
            this._ledsStop.Text = "Stop";
            this._ledsStop.UseVisualStyleBackColor = true;
            this._ledsStop.Click += new System.EventHandler(this._ledsStop_Click);
            // 
            // _ledsStart
            // 
            this._ledsStart.Location = new System.Drawing.Point(168, 285);
            this._ledsStart.Name = "_ledsStart";
            this._ledsStart.Size = new System.Drawing.Size(73, 66);
            this._ledsStart.TabIndex = 12;
            this._ledsStart.Text = "Start";
            this._ledsStart.UseVisualStyleBackColor = true;
            this._ledsStart.Click += new System.EventHandler(this._ledsStart_Click);
            // 
            // _timer
            // 
            this._timer.Tick += new System.EventHandler(this._timer_Tick);
            // 
            // _audioDuration
            // 
            this._audioDuration.Location = new System.Drawing.Point(10, 259);
            this._audioDuration.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this._audioDuration.Name = "_audioDuration";
            this._audioDuration.Size = new System.Drawing.Size(152, 20);
            this._audioDuration.TabIndex = 14;
            this._audioDuration.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // _ledDuration
            // 
            this._ledDuration.Location = new System.Drawing.Point(168, 259);
            this._ledDuration.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this._ledDuration.Name = "_ledDuration";
            this._ledDuration.Size = new System.Drawing.Size(148, 20);
            this._ledDuration.TabIndex = 15;
            this._ledDuration.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // _audioRunTime
            // 
            this._audioRunTime.AutoSize = true;
            this._audioRunTime.Location = new System.Drawing.Point(10, 354);
            this._audioRunTime.Name = "_audioRunTime";
            this._audioRunTime.Size = new System.Drawing.Size(25, 13);
            this._audioRunTime.TabIndex = 16;
            this._audioRunTime.Text = "000";
            // 
            // _ledRunTime
            // 
            this._ledRunTime.AutoSize = true;
            this._ledRunTime.Location = new System.Drawing.Point(166, 354);
            this._ledRunTime.Name = "_ledRunTime";
            this._ledRunTime.Size = new System.Drawing.Size(25, 13);
            this._ledRunTime.TabIndex = 17;
            this._ledRunTime.Text = "000";
            // 
            // _configureSiren
            // 
            this._configureSiren.Location = new System.Drawing.Point(329, 259);
            this._configureSiren.Name = "_configureSiren";
            this._configureSiren.Size = new System.Drawing.Size(150, 23);
            this._configureSiren.TabIndex = 18;
            this._configureSiren.Text = "Configure Siren...";
            this._configureSiren.UseVisualStyleBackColor = true;
            this._configureSiren.Click += new System.EventHandler(this._configureSiren_Click);
            // 
            // _uploadPatternsToPro
            // 
            this._uploadPatternsToPro.Location = new System.Drawing.Point(329, 285);
            this._uploadPatternsToPro.Name = "_uploadPatternsToPro";
            this._uploadPatternsToPro.Size = new System.Drawing.Size(150, 23);
            this._uploadPatternsToPro.TabIndex = 19;
            this._uploadPatternsToPro.Text = "Upload Patterns To Pro...";
            this._uploadPatternsToPro.UseVisualStyleBackColor = true;
            this._uploadPatternsToPro.Click += new System.EventHandler(this._uploadPatternsToPro_Click);
            // 
            // _uploadProgress
            // 
            this._uploadProgress.Location = new System.Drawing.Point(329, 314);
            this._uploadProgress.Name = "_uploadProgress";
            this._uploadProgress.Size = new System.Drawing.Size(150, 10);
            this._uploadProgress.TabIndex = 20;
            // 
            // _test
            // 
            this._test.Location = new System.Drawing.Point(10, 386);
            this._test.Name = "_test";
            this._test.Size = new System.Drawing.Size(156, 45);
            this._test.TabIndex = 21;
            this._test.Text = "Connect";
            this._test.UseVisualStyleBackColor = true;
            this._test.Click += new System.EventHandler(this._runTheGambit_Click);
            // 
            // _runTheGambit
            // 
            this._runTheGambit.Location = new System.Drawing.Point(181, 386);
            this._runTheGambit.Name = "_runTheGambit";
            this._runTheGambit.Size = new System.Drawing.Size(156, 45);
            this._runTheGambit.TabIndex = 22;
            this._runTheGambit.Text = "Run The Gambit";
            this._runTheGambit.UseVisualStyleBackColor = true;
            this._runTheGambit.Click += new System.EventHandler(this._runTheGambit_Click_1);
            // 
            // _deviceConnect
            // 
            this._deviceConnect.Location = new System.Drawing.Point(3, 3);
            this._deviceConnect.MinimumSize = new System.Drawing.Size(162, 30);
            this._deviceConnect.Name = "_deviceConnect";
            this._deviceConnect.Size = new System.Drawing.Size(322, 81);
            this._deviceConnect.TabIndex = 0;
            // 
            // FullTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._runTheGambit);
            this.Controls.Add(this._test);
            this.Controls.Add(this._uploadProgress);
            this.Controls.Add(this._uploadPatternsToPro);
            this.Controls.Add(this._configureSiren);
            this.Controls.Add(this._ledRunTime);
            this.Controls.Add(this._audioRunTime);
            this.Controls.Add(this._ledDuration);
            this.Controls.Add(this._audioDuration);
            this.Controls.Add(this._ledsStop);
            this.Controls.Add(this._ledsStart);
            this.Controls.Add(this._audioStop);
            this.Controls.Add(this._audioStart);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._deviceInfo);
            this.Controls.Add(this._ledPatterns);
            this.Controls.Add(this._audioPatterns);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._deviceConnect);
            this.Name = "FullTest";
            this.Size = new System.Drawing.Size(632, 516);
            this.Load += new System.EventHandler(this.FullTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this._audioDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._ledDuration)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DeviceConnect _deviceConnect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView _audioPatterns;
        private System.Windows.Forms.ListView _ledPatterns;
        private System.Windows.Forms.TextBox _deviceInfo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ColumnHeader _audioPatternName;
        private System.Windows.Forms.ColumnHeader _ledPatternName;
        private System.Windows.Forms.Button _audioStart;
        private System.Windows.Forms.Button _audioStop;
        private System.Windows.Forms.Button _ledsStop;
        private System.Windows.Forms.Button _ledsStart;
        private System.Windows.Forms.Timer _timer;
        private System.Windows.Forms.NumericUpDown _audioDuration;
        private System.Windows.Forms.NumericUpDown _ledDuration;
        private System.Windows.Forms.Label _audioRunTime;
        private System.Windows.Forms.Label _ledRunTime;
        private System.Windows.Forms.Button _configureSiren;
        private System.Windows.Forms.Button _uploadPatternsToPro;
        private System.Windows.Forms.ProgressBar _uploadProgress;
        private System.Windows.Forms.Button _test;
        private System.Windows.Forms.Button _runTheGambit;
    }
}
