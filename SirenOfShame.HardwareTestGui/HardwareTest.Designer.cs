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
            this._tabControl = new System.Windows.Forms.TabControl();
            this._deviceSetupTab = new System.Windows.Forms.TabPage();
            this._deviceSetup = new SirenOfShame.HardwareTestGui.DeviceSetup();
            this._installFirmwareTab = new System.Windows.Forms.TabPage();
            this._installFirmwarePage = new SirenOfShame.HardwareTestGui.InstallFirmware();
            this._manualControlTab = new System.Windows.Forms.TabPage();
            this._manualControl = new SirenOfShame.HardwareTestGui.ManualControl();
            this._fullTestTab = new System.Windows.Forms.TabPage();
            this._fullTest = new SirenOfShame.HardwareTestGui.FullTest();
            this._tabControl.SuspendLayout();
            this._deviceSetupTab.SuspendLayout();
            this._installFirmwareTab.SuspendLayout();
            this._manualControlTab.SuspendLayout();
            this._fullTestTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // _tabControl
            // 
            this._tabControl.Controls.Add(this._deviceSetupTab);
            this._tabControl.Controls.Add(this._installFirmwareTab);
            this._tabControl.Controls.Add(this._manualControlTab);
            this._tabControl.Controls.Add(this._fullTestTab);
            this._tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tabControl.Location = new System.Drawing.Point(0, 0);
            this._tabControl.Name = "_tabControl";
            this._tabControl.SelectedIndex = 0;
            this._tabControl.Size = new System.Drawing.Size(863, 403);
            this._tabControl.TabIndex = 0;
            // 
            // _deviceSetupTab
            // 
            this._deviceSetupTab.Controls.Add(this._deviceSetup);
            this._deviceSetupTab.Location = new System.Drawing.Point(4, 22);
            this._deviceSetupTab.Name = "_deviceSetupTab";
            this._deviceSetupTab.Size = new System.Drawing.Size(855, 377);
            this._deviceSetupTab.TabIndex = 3;
            this._deviceSetupTab.Text = "Device Setup";
            this._deviceSetupTab.UseVisualStyleBackColor = true;
            // 
            // _deviceSetup
            // 
            this._deviceSetup.Dock = System.Windows.Forms.DockStyle.Fill;
            this._deviceSetup.Location = new System.Drawing.Point(0, 0);
            this._deviceSetup.Name = "_deviceSetup";
            this._deviceSetup.Size = new System.Drawing.Size(855, 377);
            this._deviceSetup.TabIndex = 0;
            // 
            // _installFirmwareTab
            // 
            this._installFirmwareTab.Controls.Add(this._installFirmwarePage);
            this._installFirmwareTab.Location = new System.Drawing.Point(4, 22);
            this._installFirmwareTab.Name = "_installFirmwareTab";
            this._installFirmwareTab.Padding = new System.Windows.Forms.Padding(3);
            this._installFirmwareTab.Size = new System.Drawing.Size(855, 377);
            this._installFirmwareTab.TabIndex = 0;
            this._installFirmwareTab.Text = "Install Firmware";
            this._installFirmwareTab.UseVisualStyleBackColor = true;
            // 
            // _installFirmwarePage
            // 
            this._installFirmwarePage.Dock = System.Windows.Forms.DockStyle.Fill;
            this._installFirmwarePage.Location = new System.Drawing.Point(3, 3);
            this._installFirmwarePage.Name = "_installFirmwarePage";
            this._installFirmwarePage.Size = new System.Drawing.Size(849, 371);
            this._installFirmwarePage.TabIndex = 0;
            // 
            // _manualControlTab
            // 
            this._manualControlTab.Controls.Add(this._manualControl);
            this._manualControlTab.Location = new System.Drawing.Point(4, 22);
            this._manualControlTab.Name = "_manualControlTab";
            this._manualControlTab.Size = new System.Drawing.Size(855, 377);
            this._manualControlTab.TabIndex = 1;
            this._manualControlTab.Text = "Manual Control";
            this._manualControlTab.UseVisualStyleBackColor = true;
            // 
            // _manualControl
            // 
            this._manualControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._manualControl.Location = new System.Drawing.Point(0, 0);
            this._manualControl.Name = "_manualControl";
            this._manualControl.Size = new System.Drawing.Size(855, 377);
            this._manualControl.TabIndex = 0;
            // 
            // _fullTestTab
            // 
            this._fullTestTab.Controls.Add(this._fullTest);
            this._fullTestTab.Location = new System.Drawing.Point(4, 22);
            this._fullTestTab.Name = "_fullTestTab";
            this._fullTestTab.Size = new System.Drawing.Size(855, 377);
            this._fullTestTab.TabIndex = 2;
            this._fullTestTab.Text = "Full Test";
            this._fullTestTab.UseVisualStyleBackColor = true;
            // 
            // _fullTest
            // 
            this._fullTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this._fullTest.Location = new System.Drawing.Point(0, 0);
            this._fullTest.Name = "_fullTest";
            this._fullTest.Size = new System.Drawing.Size(855, 377);
            this._fullTest.TabIndex = 0;
            // 
            // HardwareTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 403);
            this.Controls.Add(this._tabControl);
            this.Name = "HardwareTest";
            this.Text = "Siren Of Shame - Hardware Test";
            this._tabControl.ResumeLayout(false);
            this._deviceSetupTab.ResumeLayout(false);
            this._installFirmwareTab.ResumeLayout(false);
            this._manualControlTab.ResumeLayout(false);
            this._fullTestTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl _tabControl;
        private System.Windows.Forms.TabPage _installFirmwareTab;
        private InstallFirmware _installFirmwarePage;
        private System.Windows.Forms.TabPage _manualControlTab;
        private ManualControl _manualControl;
        private System.Windows.Forms.TabPage _fullTestTab;
        private FullTest _fullTest;
        private System.Windows.Forms.TabPage _deviceSetupTab;
        private DeviceSetup _deviceSetup;

    }
}

