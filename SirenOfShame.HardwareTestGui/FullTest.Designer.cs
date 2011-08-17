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
            this._deviceConnect = new SirenOfShame.HardwareTestGui.DeviceConnect();
            ((System.ComponentModel.ISupportInitialize)(this._audioDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._ledDuration)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Audio Patterns";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(129, 36);
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
            this._audioPatterns.Location = new System.Drawing.Point(3, 52);
            this._audioPatterns.Name = "_audioPatterns";
            this._audioPatterns.Size = new System.Drawing.Size(121, 150);
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
            this._ledPatterns.Location = new System.Drawing.Point(132, 52);
            this._ledPatterns.Name = "_ledPatterns";
            this._ledPatterns.Size = new System.Drawing.Size(121, 150);
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
            this._deviceInfo.Location = new System.Drawing.Point(259, 52);
            this._deviceInfo.Multiline = true;
            this._deviceInfo.Name = "_deviceInfo";
            this._deviceInfo.ReadOnly = true;
            this._deviceInfo.Size = new System.Drawing.Size(170, 150);
            this._deviceInfo.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(256, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Device Info";
            // 
            // _audioStart
            // 
            this._audioStart.Location = new System.Drawing.Point(3, 234);
            this._audioStart.Name = "_audioStart";
            this._audioStart.Size = new System.Drawing.Size(50, 23);
            this._audioStart.TabIndex = 10;
            this._audioStart.Text = "Start";
            this._audioStart.UseVisualStyleBackColor = true;
            this._audioStart.Click += new System.EventHandler(this._audioStart_Click);
            // 
            // _audioStop
            // 
            this._audioStop.Location = new System.Drawing.Point(59, 234);
            this._audioStop.Name = "_audioStop";
            this._audioStop.Size = new System.Drawing.Size(50, 23);
            this._audioStop.TabIndex = 11;
            this._audioStop.Text = "Stop";
            this._audioStop.UseVisualStyleBackColor = true;
            this._audioStop.Click += new System.EventHandler(this._audioStop_Click);
            // 
            // _ledsStop
            // 
            this._ledsStop.Location = new System.Drawing.Point(188, 234);
            this._ledsStop.Name = "_ledsStop";
            this._ledsStop.Size = new System.Drawing.Size(50, 23);
            this._ledsStop.TabIndex = 13;
            this._ledsStop.Text = "Stop";
            this._ledsStop.UseVisualStyleBackColor = true;
            this._ledsStop.Click += new System.EventHandler(this._ledsStop_Click);
            // 
            // _ledsStart
            // 
            this._ledsStart.Location = new System.Drawing.Point(132, 234);
            this._ledsStart.Name = "_ledsStart";
            this._ledsStart.Size = new System.Drawing.Size(50, 23);
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
            this._audioDuration.Location = new System.Drawing.Point(3, 208);
            this._audioDuration.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this._audioDuration.Name = "_audioDuration";
            this._audioDuration.Size = new System.Drawing.Size(121, 20);
            this._audioDuration.TabIndex = 14;
            this._audioDuration.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // _ledDuration
            // 
            this._ledDuration.Location = new System.Drawing.Point(132, 208);
            this._ledDuration.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this._ledDuration.Name = "_ledDuration";
            this._ledDuration.Size = new System.Drawing.Size(121, 20);
            this._ledDuration.TabIndex = 15;
            this._ledDuration.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // _audioRunTime
            // 
            this._audioRunTime.AutoSize = true;
            this._audioRunTime.Location = new System.Drawing.Point(3, 260);
            this._audioRunTime.Name = "_audioRunTime";
            this._audioRunTime.Size = new System.Drawing.Size(25, 13);
            this._audioRunTime.TabIndex = 16;
            this._audioRunTime.Text = "000";
            // 
            // _ledRunTime
            // 
            this._ledRunTime.AutoSize = true;
            this._ledRunTime.Location = new System.Drawing.Point(130, 260);
            this._ledRunTime.Name = "_ledRunTime";
            this._ledRunTime.Size = new System.Drawing.Size(25, 13);
            this._ledRunTime.TabIndex = 17;
            this._ledRunTime.Text = "000";
            // 
            // _deviceConnect
            // 
            this._deviceConnect.Location = new System.Drawing.Point(3, 3);
            this._deviceConnect.MaximumSize = new System.Drawing.Size(162, 30);
            this._deviceConnect.MinimumSize = new System.Drawing.Size(162, 30);
            this._deviceConnect.Name = "_deviceConnect";
            this._deviceConnect.Size = new System.Drawing.Size(162, 30);
            this._deviceConnect.TabIndex = 0;
            // 
            // FullTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
            this.Size = new System.Drawing.Size(439, 324);
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
    }
}
