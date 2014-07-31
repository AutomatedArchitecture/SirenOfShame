using SirenOfShame.Extruder.Controls;

namespace SirenOfShame.Extruder
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._newsButton = new SirenOfShame.Extruder.Controls.HoverButton();
            this._settingsButton = new SirenOfShame.Extruder.Controls.HoverButton();
            this._buildsButton = new SirenOfShame.Extruder.Controls.HoverButton();
            this._leadersButton = new SirenOfShame.Extruder.Controls.HoverButton();
            this._mainPanel = new System.Windows.Forms.Panel();
            this._minimizedMenuStrip.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._newsButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._settingsButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._buildsButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._leadersButton)).BeginInit();
            this.SuspendLayout();
            // 
            // _notifyIcon
            // 
            this._notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this._notifyIcon.ContextMenuStrip = this._minimizedMenuStrip;
            this._notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("_notifyIcon.Icon")));
            this._notifyIcon.Text = "Shame Extruder";
            this._notifyIcon.Visible = true;
            this._notifyIcon.MouseDown += new System.Windows.Forms.MouseEventHandler(this._notifyIcon_MouseDown);
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
            this.statusStrip1.BackColor = System.Drawing.Color.Transparent;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this._connectionStatus,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3,
            this._sirenStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 197);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(308, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.White;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(42, 17);
            this.toolStripStatusLabel1.Text = "Server:";
            // 
            // _connectionStatus
            // 
            this._connectionStatus.ForeColor = System.Drawing.Color.White;
            this._connectionStatus.Name = "_connectionStatus";
            this._connectionStatus.Size = new System.Drawing.Size(79, 17);
            this._connectionStatus.Text = "Disconnected";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.ForeColor = System.Drawing.Color.White;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(10, 17);
            this.toolStripStatusLabel2.Text = "|";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.ForeColor = System.Drawing.Color.White;
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(36, 17);
            this.toolStripStatusLabel3.Text = "Siren:";
            // 
            // _sirenStatus
            // 
            this._sirenStatus.ForeColor = System.Drawing.Color.White;
            this._sirenStatus.Name = "_sirenStatus";
            this._sirenStatus.Size = new System.Drawing.Size(79, 17);
            this._sirenStatus.Text = "Disconnected";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this._newsButton, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._settingsButton, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._buildsButton, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._leadersButton, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(308, 44);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // _newsButton
            // 
            this._newsButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this._newsButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this._newsButton.Image = ((System.Drawing.Image)(resources.GetObject("_newsButton.Image")));
            this._newsButton.Location = new System.Drawing.Point(157, 3);
            this._newsButton.Name = "_newsButton";
            this._newsButton.Size = new System.Drawing.Size(71, 38);
            this._newsButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._newsButton.TabIndex = 19;
            this._newsButton.TabStop = false;
            this._newsButton.Click += new System.EventHandler(this._newsButton_Click);
            // 
            // _settingsButton
            // 
            this._settingsButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this._settingsButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this._settingsButton.Image = ((System.Drawing.Image)(resources.GetObject("_settingsButton.Image")));
            this._settingsButton.Location = new System.Drawing.Point(234, 3);
            this._settingsButton.Name = "_settingsButton";
            this._settingsButton.Size = new System.Drawing.Size(71, 38);
            this._settingsButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._settingsButton.TabIndex = 18;
            this._settingsButton.TabStop = false;
            this._settingsButton.Click += new System.EventHandler(this._settingsButton_Click);
            // 
            // _buildsButton
            // 
            this._buildsButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this._buildsButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this._buildsButton.Image = ((System.Drawing.Image)(resources.GetObject("_buildsButton.Image")));
            this._buildsButton.Location = new System.Drawing.Point(3, 3);
            this._buildsButton.Name = "_buildsButton";
            this._buildsButton.Size = new System.Drawing.Size(71, 38);
            this._buildsButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._buildsButton.TabIndex = 17;
            this._buildsButton.TabStop = false;
            this._buildsButton.Click += new System.EventHandler(this._buildsButton_Click);
            // 
            // _leadersButton
            // 
            this._leadersButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this._leadersButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this._leadersButton.Image = ((System.Drawing.Image)(resources.GetObject("_leadersButton.Image")));
            this._leadersButton.Location = new System.Drawing.Point(80, 3);
            this._leadersButton.Name = "_leadersButton";
            this._leadersButton.Size = new System.Drawing.Size(71, 38);
            this._leadersButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._leadersButton.TabIndex = 13;
            this._leadersButton.TabStop = false;
            this._leadersButton.Click += new System.EventHandler(this._leadersButton_Click);
            // 
            // _mainPanel
            // 
            this._mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainPanel.Location = new System.Drawing.Point(0, 44);
            this._mainPanel.Name = "_mainPanel";
            this._mainPanel.Size = new System.Drawing.Size(308, 153);
            this._mainPanel.TabIndex = 16;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(308, 219);
            this.Controls.Add(this._mainPanel);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.Text = "Shame Extruder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigureSettings_FormClosing);
            this.Load += new System.EventHandler(this.ConfigureSettings_Load);
            this._minimizedMenuStrip.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._newsButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._settingsButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._buildsButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._leadersButton)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

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
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private HoverButton _newsButton;
        private HoverButton _settingsButton;
        private HoverButton _buildsButton;
        private HoverButton _leadersButton;
        private System.Windows.Forms.Panel _mainPanel;
    }
}

