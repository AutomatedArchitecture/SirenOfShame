namespace SirenOfShame.SirenConfiguration
{
    partial class TestSiren
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestSiren));
            this._test = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._audio = new System.Windows.Forms.ComboBox();
            this._lights = new System.Windows.Forms.ComboBox();
            this._audioAndLights = new System.Windows.Forms.RadioButton();
            this._lightsOnly = new System.Windows.Forms.RadioButton();
            this._audioOnly = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // _test
            // 
            this._test.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._test.Location = new System.Drawing.Point(190, 92);
            this._test.Name = "_test";
            this._test.Size = new System.Drawing.Size(103, 23);
            this._test.TabIndex = 6;
            this._test.Text = "Start Test";
            this._test.UseVisualStyleBackColor = true;
            this._test.Click += new System.EventHandler(this.TestClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Audio:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Light Pattern:";
            // 
            // _audio
            // 
            this._audio.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._audio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._audio.FormattingEnabled = true;
            this._audio.Location = new System.Drawing.Point(87, 39);
            this._audio.Name = "_audio";
            this._audio.Size = new System.Drawing.Size(206, 21);
            this._audio.TabIndex = 3;
            // 
            // _lights
            // 
            this._lights.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._lights.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lights.FormattingEnabled = true;
            this._lights.Location = new System.Drawing.Point(87, 12);
            this._lights.Name = "_lights";
            this._lights.Size = new System.Drawing.Size(206, 21);
            this._lights.TabIndex = 2;
            // 
            // _audioAndLights
            // 
            this._audioAndLights.AutoSize = true;
            this._audioAndLights.Checked = true;
            this._audioAndLights.Location = new System.Drawing.Point(87, 67);
            this._audioAndLights.Name = "_audioAndLights";
            this._audioAndLights.Size = new System.Drawing.Size(92, 17);
            this._audioAndLights.TabIndex = 7;
            this._audioAndLights.TabStop = true;
            this._audioAndLights.Text = "Audio && Lights";
            this._audioAndLights.UseVisualStyleBackColor = true;
            // 
            // _lightsOnly
            // 
            this._lightsOnly.AutoSize = true;
            this._lightsOnly.Location = new System.Drawing.Point(185, 67);
            this._lightsOnly.Name = "_lightsOnly";
            this._lightsOnly.Size = new System.Drawing.Size(53, 17);
            this._lightsOnly.TabIndex = 8;
            this._lightsOnly.Text = "Lights";
            this._lightsOnly.UseVisualStyleBackColor = true;
            // 
            // _audioOnly
            // 
            this._audioOnly.AutoSize = true;
            this._audioOnly.Location = new System.Drawing.Point(244, 66);
            this._audioOnly.Name = "_audioOnly";
            this._audioOnly.Size = new System.Drawing.Size(52, 17);
            this._audioOnly.TabIndex = 9;
            this._audioOnly.Text = "Audio";
            this._audioOnly.UseVisualStyleBackColor = true;
            // 
            // TestSiren
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 126);
            this.Controls.Add(this._audioOnly);
            this.Controls.Add(this._lightsOnly);
            this.Controls.Add(this._audioAndLights);
            this.Controls.Add(this._test);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._audio);
            this.Controls.Add(this._lights);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TestSiren";
            this.Text = "Test Siren";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TestSiren_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox _lights;
        private System.Windows.Forms.ComboBox _audio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button _test;
        private System.Windows.Forms.RadioButton _audioAndLights;
        private System.Windows.Forms.RadioButton _lightsOnly;
        private System.Windows.Forms.RadioButton _audioOnly;
    }
}