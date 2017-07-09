namespace SirenOfShame.HardwareTestGui
{
    partial class ManualControl
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
            this._ledsGroupBox = new System.Windows.Forms.GroupBox();
            this._led5Value = new System.Windows.Forms.TrackBar();
            this._led4Value = new System.Windows.Forms.TrackBar();
            this._led3Value = new System.Windows.Forms.TrackBar();
            this._led2Value = new System.Windows.Forms.TrackBar();
            this._led1Value = new System.Windows.Forms.TrackBar();
            this._allOff = new System.Windows.Forms.Button();
            this._led5 = new System.Windows.Forms.CheckBox();
            this._allOn = new System.Windows.Forms.Button();
            this._led4 = new System.Windows.Forms.CheckBox();
            this._led3 = new System.Windows.Forms.CheckBox();
            this._led2 = new System.Windows.Forms.CheckBox();
            this._led1 = new System.Windows.Forms.CheckBox();
            this._siren = new System.Windows.Forms.CheckBox();
            this._lightNext = new System.Windows.Forms.Button();
            this.deviceConnect1 = new SirenOfShame.HardwareTestGui.DeviceConnect();
            this._ledsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._led5Value)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._led4Value)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._led3Value)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._led2Value)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._led1Value)).BeginInit();
            this.SuspendLayout();
            // 
            // _ledsGroupBox
            // 
            this._ledsGroupBox.Controls.Add(this._led5Value);
            this._ledsGroupBox.Controls.Add(this._led4Value);
            this._ledsGroupBox.Controls.Add(this._led3Value);
            this._ledsGroupBox.Controls.Add(this._led2Value);
            this._ledsGroupBox.Controls.Add(this._led1Value);
            this._ledsGroupBox.Controls.Add(this._allOff);
            this._ledsGroupBox.Controls.Add(this._led5);
            this._ledsGroupBox.Controls.Add(this._allOn);
            this._ledsGroupBox.Controls.Add(this._led4);
            this._ledsGroupBox.Controls.Add(this._led3);
            this._ledsGroupBox.Controls.Add(this._led2);
            this._ledsGroupBox.Controls.Add(this._led1);
            this._ledsGroupBox.Location = new System.Drawing.Point(6, 83);
            this._ledsGroupBox.Name = "_ledsGroupBox";
            this._ledsGroupBox.Size = new System.Drawing.Size(172, 171);
            this._ledsGroupBox.TabIndex = 2;
            this._ledsGroupBox.TabStop = false;
            this._ledsGroupBox.Text = "LEDs";
            // 
            // _led5Value
            // 
            this._led5Value.AutoSize = false;
            this._led5Value.LargeChange = 10;
            this._led5Value.Location = new System.Drawing.Point(68, 111);
            this._led5Value.Maximum = 254;
            this._led5Value.Name = "_led5Value";
            this._led5Value.Size = new System.Drawing.Size(94, 17);
            this._led5Value.TabIndex = 12;
            this._led5Value.TickStyle = System.Windows.Forms.TickStyle.None;
            this._led5Value.Scroll += new System.EventHandler(this._ledValue_Scroll);
            // 
            // _led4Value
            // 
            this._led4Value.AutoSize = false;
            this._led4Value.LargeChange = 10;
            this._led4Value.Location = new System.Drawing.Point(68, 88);
            this._led4Value.Maximum = 254;
            this._led4Value.Name = "_led4Value";
            this._led4Value.Size = new System.Drawing.Size(94, 17);
            this._led4Value.TabIndex = 11;
            this._led4Value.TickStyle = System.Windows.Forms.TickStyle.None;
            this._led4Value.Scroll += new System.EventHandler(this._ledValue_Scroll);
            // 
            // _led3Value
            // 
            this._led3Value.AutoSize = false;
            this._led3Value.LargeChange = 10;
            this._led3Value.Location = new System.Drawing.Point(68, 65);
            this._led3Value.Maximum = 254;
            this._led3Value.Name = "_led3Value";
            this._led3Value.Size = new System.Drawing.Size(94, 17);
            this._led3Value.TabIndex = 10;
            this._led3Value.TickStyle = System.Windows.Forms.TickStyle.None;
            this._led3Value.Scroll += new System.EventHandler(this._ledValue_Scroll);
            // 
            // _led2Value
            // 
            this._led2Value.AutoSize = false;
            this._led2Value.LargeChange = 10;
            this._led2Value.Location = new System.Drawing.Point(68, 42);
            this._led2Value.Maximum = 254;
            this._led2Value.Name = "_led2Value";
            this._led2Value.Size = new System.Drawing.Size(94, 17);
            this._led2Value.TabIndex = 9;
            this._led2Value.TickStyle = System.Windows.Forms.TickStyle.None;
            this._led2Value.Scroll += new System.EventHandler(this._ledValue_Scroll);
            // 
            // _led1Value
            // 
            this._led1Value.AutoSize = false;
            this._led1Value.LargeChange = 10;
            this._led1Value.Location = new System.Drawing.Point(68, 19);
            this._led1Value.Maximum = 254;
            this._led1Value.Name = "_led1Value";
            this._led1Value.Size = new System.Drawing.Size(94, 17);
            this._led1Value.TabIndex = 4;
            this._led1Value.TickStyle = System.Windows.Forms.TickStyle.None;
            this._led1Value.Scroll += new System.EventHandler(this._ledValue_Scroll);
            // 
            // _allOff
            // 
            this._allOff.Location = new System.Drawing.Point(87, 134);
            this._allOff.Name = "_allOff";
            this._allOff.Size = new System.Drawing.Size(75, 23);
            this._allOff.TabIndex = 5;
            this._allOff.Text = "All Off";
            this._allOff.UseVisualStyleBackColor = true;
            this._allOff.Click += new System.EventHandler(this._allOff_Click);
            // 
            // _led5
            // 
            this._led5.AutoSize = true;
            this._led5.Location = new System.Drawing.Point(6, 111);
            this._led5.Name = "_led5";
            this._led5.Size = new System.Drawing.Size(56, 17);
            this._led5.TabIndex = 8;
            this._led5.Text = "LED 5";
            this._led5.UseVisualStyleBackColor = true;
            this._led5.CheckedChanged += new System.EventHandler(this._led5_CheckedChanged);
            // 
            // _allOn
            // 
            this._allOn.Location = new System.Drawing.Point(6, 134);
            this._allOn.Name = "_allOn";
            this._allOn.Size = new System.Drawing.Size(75, 23);
            this._allOn.TabIndex = 4;
            this._allOn.Text = "All On";
            this._allOn.UseVisualStyleBackColor = true;
            this._allOn.Click += new System.EventHandler(this._allOn_Click);
            // 
            // _led4
            // 
            this._led4.AutoSize = true;
            this._led4.Location = new System.Drawing.Point(6, 88);
            this._led4.Name = "_led4";
            this._led4.Size = new System.Drawing.Size(56, 17);
            this._led4.TabIndex = 7;
            this._led4.Text = "LED 4";
            this._led4.UseVisualStyleBackColor = true;
            this._led4.CheckedChanged += new System.EventHandler(this._led4_CheckedChanged);
            // 
            // _led3
            // 
            this._led3.AutoSize = true;
            this._led3.Location = new System.Drawing.Point(6, 65);
            this._led3.Name = "_led3";
            this._led3.Size = new System.Drawing.Size(56, 17);
            this._led3.TabIndex = 6;
            this._led3.Text = "LED 3";
            this._led3.UseVisualStyleBackColor = true;
            this._led3.CheckedChanged += new System.EventHandler(this._led3_CheckedChanged);
            // 
            // _led2
            // 
            this._led2.AutoSize = true;
            this._led2.Location = new System.Drawing.Point(6, 42);
            this._led2.Name = "_led2";
            this._led2.Size = new System.Drawing.Size(56, 17);
            this._led2.TabIndex = 5;
            this._led2.Text = "LED 2";
            this._led2.UseVisualStyleBackColor = true;
            this._led2.CheckedChanged += new System.EventHandler(this._led2_CheckedChanged);
            // 
            // _led1
            // 
            this._led1.AutoSize = true;
            this._led1.Location = new System.Drawing.Point(6, 19);
            this._led1.Name = "_led1";
            this._led1.Size = new System.Drawing.Size(56, 17);
            this._led1.TabIndex = 4;
            this._led1.Text = "LED 1";
            this._led1.UseVisualStyleBackColor = true;
            this._led1.CheckedChanged += new System.EventHandler(this._led1_CheckedChanged);
            // 
            // _siren
            // 
            this._siren.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._siren.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this._siren.Location = new System.Drawing.Point(184, 83);
            this._siren.Name = "_siren";
            this._siren.Size = new System.Drawing.Size(336, 69);
            this._siren.TabIndex = 3;
            this._siren.Text = "Play Audio Siren";
            this._siren.UseVisualStyleBackColor = false;
            this._siren.CheckedChanged += new System.EventHandler(this._siren_CheckedChanged);
            // 
            // _lightNext
            // 
            this._lightNext.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._lightNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this._lightNext.Location = new System.Drawing.Point(184, 158);
            this._lightNext.Name = "_lightNext";
            this._lightNext.Size = new System.Drawing.Size(336, 109);
            this._lightNext.TabIndex = 13;
            this._lightNext.Text = "Light Next LED";
            this._lightNext.UseVisualStyleBackColor = false;
            this._lightNext.Click += new System.EventHandler(this._lightNext_Click);
            // 
            // deviceConnect1
            // 
            this.deviceConnect1.AutoSize = true;
            this.deviceConnect1.Location = new System.Drawing.Point(7, 3);
            this.deviceConnect1.MinimumSize = new System.Drawing.Size(162, 30);
            this.deviceConnect1.Name = "deviceConnect1";
            this.deviceConnect1.Size = new System.Drawing.Size(253, 74);
            this.deviceConnect1.TabIndex = 0;
            // 
            // ManualControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._lightNext);
            this.Controls.Add(this._siren);
            this.Controls.Add(this._ledsGroupBox);
            this.Controls.Add(this.deviceConnect1);
            this.Name = "ManualControl";
            this.Size = new System.Drawing.Size(520, 329);
            this._ledsGroupBox.ResumeLayout(false);
            this._ledsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._led5Value)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._led4Value)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._led3Value)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._led2Value)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._led1Value)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DeviceConnect deviceConnect1;
        private System.Windows.Forms.GroupBox _ledsGroupBox;
        private System.Windows.Forms.CheckBox _led5;
        private System.Windows.Forms.CheckBox _led4;
        private System.Windows.Forms.CheckBox _led3;
        private System.Windows.Forms.CheckBox _led2;
        private System.Windows.Forms.CheckBox _led1;
        private System.Windows.Forms.CheckBox _siren;
        private System.Windows.Forms.Button _allOff;
        private System.Windows.Forms.Button _allOn;
        private System.Windows.Forms.TrackBar _led5Value;
        private System.Windows.Forms.TrackBar _led4Value;
        private System.Windows.Forms.TrackBar _led3Value;
        private System.Windows.Forms.TrackBar _led2Value;
        private System.Windows.Forms.TrackBar _led1Value;
        private System.Windows.Forms.Button _lightNext;
    }
}
