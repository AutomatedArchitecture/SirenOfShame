namespace SirenOfShame.Extruder
{
    partial class ConfigureSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigureSettings));
            this._connectButton = new System.Windows.Forms.Button();
            this._username = new System.Windows.Forms.TextBox();
            this._password = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this._myname = new System.Windows.Forms.TextBox();
            this._notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this._minimizedMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.configureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this._connectionStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this._sirenStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this._testSiren = new System.Windows.Forms.Button();
            this._minimizedMenuStrip.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _connectButton
            // 
            this._connectButton.Location = new System.Drawing.Point(94, 90);
            this._connectButton.Name = "_connectButton";
            this._connectButton.Size = new System.Drawing.Size(80, 26);
            this._connectButton.TabIndex = 0;
            this._connectButton.Text = "Connect";
            this._connectButton.UseVisualStyleBackColor = true;
            this._connectButton.Click += new System.EventHandler(this.Connect_Click);
            // 
            // _username
            // 
            this._username.Location = new System.Drawing.Point(94, 12);
            this._username.Name = "_username";
            this._username.Size = new System.Drawing.Size(178, 20);
            this._username.TabIndex = 2;
            // 
            // _password
            // 
            this._password.Location = new System.Drawing.Point(94, 38);
            this._password.Name = "_password";
            this._password.Size = new System.Drawing.Size(178, 20);
            this._password.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "My Name";
            // 
            // _myname
            // 
            this._myname.Location = new System.Drawing.Point(94, 64);
            this._myname.Name = "_myname";
            this._myname.Size = new System.Drawing.Size(178, 20);
            this._myname.TabIndex = 6;
            // 
            // _notifyIcon
            // 
            this._notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this._notifyIcon.ContextMenuStrip = this._minimizedMenuStrip;
            this._notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("_notifyIcon.Icon")));
            this._notifyIcon.Text = "Shame Extruder";
            this._notifyIcon.Visible = true;
            this._notifyIcon.DoubleClick += new System.EventHandler(this._notifyIcon_DoubleClick);
            // 
            // _minimizedMenuStrip
            // 
            this._minimizedMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configureToolStripMenuItem,
            this.exitToolStripMenuItem});
            this._minimizedMenuStrip.Name = "_minimizedMenuStrip";
            this._minimizedMenuStrip.Size = new System.Drawing.Size(128, 48);
            // 
            // configureToolStripMenuItem
            // 
            this.configureToolStripMenuItem.Name = "configureToolStripMenuItem";
            this.configureToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.configureToolStripMenuItem.Text = "Configure";
            this.configureToolStripMenuItem.Click += new System.EventHandler(this.configureToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this._connectionStatus,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3,
            this._sirenStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 135);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(284, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(42, 17);
            this.toolStripStatusLabel1.Text = "Server:";
            // 
            // _connectionStatus
            // 
            this._connectionStatus.Name = "_connectionStatus";
            this._connectionStatus.Size = new System.Drawing.Size(79, 17);
            this._connectionStatus.Text = "Disconnected";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(10, 17);
            this.toolStripStatusLabel2.Text = "|";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(36, 17);
            this.toolStripStatusLabel3.Text = "Siren:";
            // 
            // _sirenStatus
            // 
            this._sirenStatus.Name = "_sirenStatus";
            this._sirenStatus.Size = new System.Drawing.Size(79, 17);
            this._sirenStatus.Text = "Disconnected";
            // 
            // _testSiren
            // 
            this._testSiren.Location = new System.Drawing.Point(180, 90);
            this._testSiren.Name = "_testSiren";
            this._testSiren.Size = new System.Drawing.Size(92, 26);
            this._testSiren.TabIndex = 9;
            this._testSiren.Text = "Test Siren";
            this._testSiren.UseVisualStyleBackColor = true;
            // 
            // ConfigureSettings
            // 
            this.AcceptButton = this._connectButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 157);
            this.Controls.Add(this._testSiren);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._myname);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._password);
            this.Controls.Add(this._username);
            this.Controls.Add(this._connectButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ConfigureSettings";
            this.ShowInTaskbar = false;
            this.Text = "Extruder Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigureSettings_FormClosing);
            this.Load += new System.EventHandler(this.ConfigureSettings_Load);
            this._minimizedMenuStrip.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _connectButton;
        private System.Windows.Forms.TextBox _username;
        private System.Windows.Forms.TextBox _password;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox _myname;
        private System.Windows.Forms.NotifyIcon _notifyIcon;
        private System.Windows.Forms.ContextMenuStrip _minimizedMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem configureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel _connectionStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel _sirenStatus;
        private System.Windows.Forms.Button _testSiren;
    }
}

