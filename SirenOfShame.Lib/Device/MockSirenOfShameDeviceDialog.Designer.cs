namespace SirenOfShame.Lib.Device
{
    partial class MockSirenOfShameDeviceDialog
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
            this.components = new System.ComponentModel.Container();
            this._connected = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this._audioPattern = new System.Windows.Forms.Label();
            this._ledPattern = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._audioPatterns = new System.Windows.Forms.TextBox();
            this._ledPatterns = new System.Windows.Forms.TextBox();
            this._timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // _connected
            // 
            this._connected.AutoSize = true;
            this._connected.Location = new System.Drawing.Point(12, 12);
            this._connected.Name = "_connected";
            this._connected.Size = new System.Drawing.Size(78, 17);
            this._connected.TabIndex = 0;
            this._connected.Text = "Connected";
            this._connected.UseVisualStyleBackColor = true;
            this._connected.CheckedChanged += new System.EventHandler(this._connected_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Audio Pattern:";
            // 
            // _audioPattern
            // 
            this._audioPattern.AutoSize = true;
            this._audioPattern.Location = new System.Drawing.Point(92, 42);
            this._audioPattern.Name = "_audioPattern";
            this._audioPattern.Size = new System.Drawing.Size(33, 13);
            this._audioPattern.TabIndex = 3;
            this._audioPattern.Text = "None";
            // 
            // _ledPattern
            // 
            this._ledPattern.AutoSize = true;
            this._ledPattern.Location = new System.Drawing.Point(92, 65);
            this._ledPattern.Name = "_ledPattern";
            this._ledPattern.Size = new System.Drawing.Size(33, 13);
            this._ledPattern.TabIndex = 6;
            this._ledPattern.Text = "None";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "LED Pattern:";
            // 
            // _audioPatterns
            // 
            this._audioPatterns.Location = new System.Drawing.Point(12, 94);
            this._audioPatterns.Multiline = true;
            this._audioPatterns.Name = "_audioPatterns";
            this._audioPatterns.Size = new System.Drawing.Size(100, 99);
            this._audioPatterns.TabIndex = 7;
            this._audioPatterns.Text = "Audio Pattern 1";
            // 
            // _ledPatterns
            // 
            this._ledPatterns.Location = new System.Drawing.Point(118, 94);
            this._ledPatterns.Multiline = true;
            this._ledPatterns.Name = "_ledPatterns";
            this._ledPatterns.Size = new System.Drawing.Size(100, 99);
            this._ledPatterns.TabIndex = 8;
            this._ledPatterns.Text = "LED Pattern 1";
            // 
            // _timer
            // 
            this._timer.Enabled = true;
            this._timer.Tick += new System.EventHandler(this._timer_Tick);
            // 
            // MockSirenOfShameDeviceDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 207);
            this.Controls.Add(this._ledPatterns);
            this.Controls.Add(this._audioPatterns);
            this.Controls.Add(this._ledPattern);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._audioPattern);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._connected);
            this.Name = "MockSirenOfShameDeviceDialog";
            this.Text = "Mock SoS Device";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox _connected;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label _audioPattern;
        private System.Windows.Forms.Label _ledPattern;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox _audioPatterns;
        private System.Windows.Forms.TextBox _ledPatterns;
        private System.Windows.Forms.Timer _timer;
    }
}