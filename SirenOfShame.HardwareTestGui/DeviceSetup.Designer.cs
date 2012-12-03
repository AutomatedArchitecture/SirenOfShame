namespace SirenOfShame.HardwareTestGui
{
    partial class DeviceSetup
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
            this._verify = new System.Windows.Forms.Button();
            this._output = new System.Windows.Forms.TextBox();
            this._verifyResults = new System.Windows.Forms.Label();
            this._eraseResults = new System.Windows.Forms.Label();
            this._erase = new System.Windows.Forms.Button();
            this._setFusesResult = new System.Windows.Forms.Label();
            this._setFuses = new System.Windows.Forms.Button();
            this._verifyFusesResults = new System.Windows.Forms.Label();
            this._verifyFuses = new System.Windows.Forms.Button();
            this._clearResults = new System.Windows.Forms.Button();
            this._writeBootloaderResults = new System.Windows.Forms.Label();
            this._writeBootloader = new System.Windows.Forms.Button();
            this._verifyBootloaderResults = new System.Windows.Forms.Label();
            this._verifyBootloader = new System.Windows.Forms.Button();
            this._bootloaderFilename = new System.Windows.Forms.TextBox();
            this._browse = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this._runGambit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _verify
            // 
            this._verify.Location = new System.Drawing.Point(3, 3);
            this._verify.Name = "_verify";
            this._verify.Size = new System.Drawing.Size(113, 23);
            this._verify.TabIndex = 0;
            this._verify.Text = "Verify";
            this._verify.UseVisualStyleBackColor = true;
            this._verify.Click += new System.EventHandler(this._verify_Click);
            // 
            // _output
            // 
            this._output.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._output.Location = new System.Drawing.Point(3, 206);
            this._output.Multiline = true;
            this._output.Name = "_output";
            this._output.ReadOnly = true;
            this._output.Size = new System.Drawing.Size(593, 114);
            this._output.TabIndex = 1;
            // 
            // _verifyResults
            // 
            this._verifyResults.AutoSize = true;
            this._verifyResults.Location = new System.Drawing.Point(122, 8);
            this._verifyResults.Name = "_verifyResults";
            this._verifyResults.Size = new System.Drawing.Size(71, 13);
            this._verifyResults.TabIndex = 2;
            this._verifyResults.Text = "Verify Results";
            // 
            // _eraseResults
            // 
            this._eraseResults.AutoSize = true;
            this._eraseResults.Location = new System.Drawing.Point(122, 37);
            this._eraseResults.Name = "_eraseResults";
            this._eraseResults.Size = new System.Drawing.Size(72, 13);
            this._eraseResults.TabIndex = 4;
            this._eraseResults.Text = "Erase Results";
            // 
            // _erase
            // 
            this._erase.Location = new System.Drawing.Point(3, 32);
            this._erase.Name = "_erase";
            this._erase.Size = new System.Drawing.Size(113, 23);
            this._erase.TabIndex = 3;
            this._erase.Text = "Erase";
            this._erase.UseVisualStyleBackColor = true;
            this._erase.Click += new System.EventHandler(this._erase_Click);
            // 
            // _setFusesResult
            // 
            this._setFusesResult.AutoSize = true;
            this._setFusesResult.Location = new System.Drawing.Point(122, 66);
            this._setFusesResult.Name = "_setFusesResult";
            this._setFusesResult.Size = new System.Drawing.Size(92, 13);
            this._setFusesResult.TabIndex = 6;
            this._setFusesResult.Text = "Set Fuses Results";
            // 
            // _setFuses
            // 
            this._setFuses.Location = new System.Drawing.Point(3, 61);
            this._setFuses.Name = "_setFuses";
            this._setFuses.Size = new System.Drawing.Size(113, 23);
            this._setFuses.TabIndex = 5;
            this._setFuses.Text = "Set Fuses";
            this._setFuses.UseVisualStyleBackColor = true;
            this._setFuses.Click += new System.EventHandler(this._setFuses_Click);
            // 
            // _verifyFusesResults
            // 
            this._verifyFusesResults.AutoSize = true;
            this._verifyFusesResults.Location = new System.Drawing.Point(122, 95);
            this._verifyFusesResults.Name = "_verifyFusesResults";
            this._verifyFusesResults.Size = new System.Drawing.Size(102, 13);
            this._verifyFusesResults.TabIndex = 8;
            this._verifyFusesResults.Text = "Verify Fuses Results";
            // 
            // _verifyFuses
            // 
            this._verifyFuses.Location = new System.Drawing.Point(3, 90);
            this._verifyFuses.Name = "_verifyFuses";
            this._verifyFuses.Size = new System.Drawing.Size(113, 23);
            this._verifyFuses.TabIndex = 7;
            this._verifyFuses.Text = "Verify Fuses";
            this._verifyFuses.UseVisualStyleBackColor = true;
            this._verifyFuses.Click += new System.EventHandler(this._verifyFuses_Click);
            // 
            // _clearResults
            // 
            this._clearResults.Location = new System.Drawing.Point(3, 177);
            this._clearResults.Name = "_clearResults";
            this._clearResults.Size = new System.Drawing.Size(113, 23);
            this._clearResults.TabIndex = 9;
            this._clearResults.Text = "Clear Results";
            this._clearResults.UseVisualStyleBackColor = true;
            this._clearResults.Click += new System.EventHandler(this._clearResults_Click);
            // 
            // _writeBootloaderResults
            // 
            this._writeBootloaderResults.AutoSize = true;
            this._writeBootloaderResults.Location = new System.Drawing.Point(122, 124);
            this._writeBootloaderResults.Name = "_writeBootloaderResults";
            this._writeBootloaderResults.Size = new System.Drawing.Size(124, 13);
            this._writeBootloaderResults.TabIndex = 11;
            this._writeBootloaderResults.Text = "Write Bootloader Results";
            // 
            // _writeBootloader
            // 
            this._writeBootloader.Location = new System.Drawing.Point(3, 119);
            this._writeBootloader.Name = "_writeBootloader";
            this._writeBootloader.Size = new System.Drawing.Size(113, 23);
            this._writeBootloader.TabIndex = 10;
            this._writeBootloader.Text = "Write Bootloader";
            this._writeBootloader.UseVisualStyleBackColor = true;
            this._writeBootloader.Click += new System.EventHandler(this._writeBootloader_Click);
            // 
            // _verifyBootloaderResults
            // 
            this._verifyBootloaderResults.AutoSize = true;
            this._verifyBootloaderResults.Location = new System.Drawing.Point(122, 153);
            this._verifyBootloaderResults.Name = "_verifyBootloaderResults";
            this._verifyBootloaderResults.Size = new System.Drawing.Size(125, 13);
            this._verifyBootloaderResults.TabIndex = 13;
            this._verifyBootloaderResults.Text = "Verify Bootloader Results";
            // 
            // _verifyBootloader
            // 
            this._verifyBootloader.Location = new System.Drawing.Point(3, 148);
            this._verifyBootloader.Name = "_verifyBootloader";
            this._verifyBootloader.Size = new System.Drawing.Size(113, 23);
            this._verifyBootloader.TabIndex = 12;
            this._verifyBootloader.Text = "Verify Bootloader";
            this._verifyBootloader.UseVisualStyleBackColor = true;
            this._verifyBootloader.Click += new System.EventHandler(this._verifyBootloader_Click);
            // 
            // _bootloaderFilename
            // 
            this._bootloaderFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._bootloaderFilename.Location = new System.Drawing.Point(317, 23);
            this._bootloaderFilename.Name = "_bootloaderFilename";
            this._bootloaderFilename.Size = new System.Drawing.Size(198, 20);
            this._bootloaderFilename.TabIndex = 14;
            // 
            // _browse
            // 
            this._browse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._browse.Location = new System.Drawing.Point(521, 21);
            this._browse.Name = "_browse";
            this._browse.Size = new System.Drawing.Size(75, 23);
            this._browse.TabIndex = 15;
            this._browse.Text = "Browse...";
            this._browse.UseVisualStyleBackColor = true;
            this._browse.Click += new System.EventHandler(this._browse_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(314, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Bootloader Hex";
            // 
            // _runGambit
            // 
            this._runGambit.Location = new System.Drawing.Point(317, 90);
            this._runGambit.Name = "_runGambit";
            this._runGambit.Size = new System.Drawing.Size(279, 47);
            this._runGambit.TabIndex = 17;
            this._runGambit.Text = "Run The Gambit";
            this._runGambit.UseVisualStyleBackColor = true;
            this._runGambit.Click += new System.EventHandler(this._runGambit_Click);
            // 
            // DeviceSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._runGambit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._browse);
            this.Controls.Add(this._bootloaderFilename);
            this.Controls.Add(this._verifyBootloaderResults);
            this.Controls.Add(this._verifyBootloader);
            this.Controls.Add(this._writeBootloaderResults);
            this.Controls.Add(this._writeBootloader);
            this.Controls.Add(this._clearResults);
            this.Controls.Add(this._verifyFusesResults);
            this.Controls.Add(this._verifyFuses);
            this.Controls.Add(this._setFusesResult);
            this.Controls.Add(this._setFuses);
            this.Controls.Add(this._eraseResults);
            this.Controls.Add(this._erase);
            this.Controls.Add(this._verifyResults);
            this.Controls.Add(this._output);
            this.Controls.Add(this._verify);
            this.Name = "DeviceSetup";
            this.Size = new System.Drawing.Size(599, 323);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _verify;
        private System.Windows.Forms.TextBox _output;
        private System.Windows.Forms.Label _verifyResults;
        private System.Windows.Forms.Label _eraseResults;
        private System.Windows.Forms.Button _erase;
        private System.Windows.Forms.Label _setFusesResult;
        private System.Windows.Forms.Button _setFuses;
        private System.Windows.Forms.Label _verifyFusesResults;
        private System.Windows.Forms.Button _verifyFuses;
        private System.Windows.Forms.Button _clearResults;
        private System.Windows.Forms.Label _writeBootloaderResults;
        private System.Windows.Forms.Button _writeBootloader;
        private System.Windows.Forms.Label _verifyBootloaderResults;
        private System.Windows.Forms.Button _verifyBootloader;
        private System.Windows.Forms.TextBox _bootloaderFilename;
        private System.Windows.Forms.Button _browse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _runGambit;
    }
}
