using System;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame {
	partial class MainForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
            try
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);
            } catch (Exception)
            {
                
            }
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("sdf");
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this._toolStripSplitErrorButton = new System.Windows.Forms.ToolStripDropDownButton();
            this._lastStatusUpdate = new System.Windows.Forms.ToolStripStatusLabel();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.minimizedMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.balls = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this._sosOnline = new System.Windows.Forms.Button();
            this.bigIcons = new System.Windows.Forms.ImageList(this.components);
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this._mute = new System.Windows.Forms.Button();
            this._fullscreen = new System.Windows.Forms.Button();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this._configureSiren = new System.Windows.Forms.Button();
            this._sirenMore = new System.Windows.Forms.Button();
            this._configureCiServer = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this._help = new System.Windows.Forms.Button();
            this._configurationMore = new System.Windows.Forms.Button();
            this._configureRules = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this._timeboxEnforcer = new System.Windows.Forms.Button();
            this._automaticUpdater = new wyDay.Controls.AutomaticUpdater();
            this._openSettings = new System.Windows.Forms.Button();
            this._testSiren = new System.Windows.Forms.Button();
            this._refresh = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buildStatusBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this._buildMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._affectsTrayIcon = new System.Windows.Forms.ToolStripMenuItem();
            this._stopWatching = new System.Windows.Forms.ToolStripMenuItem();
            this._when = new System.Windows.Forms.ToolStripMenuItem();
            this._toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._configurationMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._checkForUpdates = new System.Windows.Forms.ToolStripMenuItem();
            this._viewLog = new System.Windows.Forms.ToolStripMenuItem();
            this._sirenMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._upgradeFirmwareMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._panelRight = new System.Windows.Forms.Panel();
            this._userStats = new System.Windows.Forms.Panel();
            this._users = new System.Windows.Forms.ListView();
            this.User = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Reputation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._buildHistoryZedGraph = new ZedGraph.ZedGraphControl();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this._userMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._editUserName = new System.Windows.Forms.ToolStripMenuItem();
            this._hideUser = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._showAllUsers = new System.Windows.Forms.ToolStripMenuItem();
            this._panelAlert = new System.Windows.Forms.Panel();
            this._details = new System.Windows.Forms.LinkLabel();
            this._labelAlert = new System.Windows.Forms.Label();
            this._closeAlert = new System.Windows.Forms.Button();
            this._buildDefinitions = new SirenOfShame.BuildStatusListView();
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.date = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.duration2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.checkedInBy = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.comment = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.viewUser1 = new SirenOfShame.ViewUser();
            this._buildStats = new SirenOfShame.BuildStats();
            this.statusStrip1.SuspendLayout();
            this.minimizedMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._automaticUpdater)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buildStatusBindingSource)).BeginInit();
            this._buildMenu.SuspendLayout();
            this._configurationMenu.SuspendLayout();
            this._sirenMenu.SuspendLayout();
            this._panelRight.SuspendLayout();
            this._userStats.SuspendLayout();
            this._userMenu.SuspendLayout();
            this._panelAlert.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._toolStripSplitErrorButton,
            this._lastStatusUpdate});
            this.statusStrip1.Location = new System.Drawing.Point(0, 305);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(869, 22);
            this.statusStrip1.TabIndex = 29;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // _toolStripSplitErrorButton
            // 
            this._toolStripSplitErrorButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._toolStripSplitErrorButton.Image = global::SirenOfShame.Properties.Resources.question_big;
            this._toolStripSplitErrorButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._toolStripSplitErrorButton.Name = "_toolStripSplitErrorButton";
            this._toolStripSplitErrorButton.ShowDropDownArrow = false;
            this._toolStripSplitErrorButton.Size = new System.Drawing.Size(20, 20);
            this._toolStripSplitErrorButton.Text = "toolStripSplitButton1";
            this._toolStripSplitErrorButton.ToolTipText = "Error Occured";
            this._toolStripSplitErrorButton.Visible = false;
            this._toolStripSplitErrorButton.Click += new System.EventHandler(this.ToolStripSplitErrorButtonClick);
            // 
            // _lastStatusUpdate
            // 
            this._lastStatusUpdate.Name = "_lastStatusUpdate";
            this._lastStatusUpdate.Size = new System.Drawing.Size(131, 17);
            this._lastStatusUpdate.Text = "Build Last Checked: n/a";
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.ContextMenuStrip = this.minimizedMenu;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Siren of Shame";
            this.notifyIcon.Visible = true;
            this.notifyIcon.DoubleClick += new System.EventHandler(this.NotifyIconDoubleClick);
            // 
            // minimizedMenu
            // 
            this.minimizedMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.minimizedMenu.Name = "contextMenuStrip1";
            this.minimizedMenu.Size = new System.Drawing.Size(104, 48);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItemClick);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
            // 
            // balls
            // 
            this.balls.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("balls.ImageStream")));
            this.balls.TransparentColor = System.Drawing.Color.Transparent;
            this.balls.Images.SetKeyName(0, "ball_red.png");
            this.balls.Images.SetKeyName(1, "ball_green.png");
            this.balls.Images.SetKeyName(2, "ball_gray.png");
            this.balls.Images.SetKeyName(3, "ConfigureRules.png");
            this.balls.Images.SetKeyName(4, "ConfigureServer.png");
            this.balls.Images.SetKeyName(5, "TestSiren.png");
            this.balls.Images.SetKeyName(6, "ConfigureSiren.png");
            this.balls.Images.SetKeyName(7, "question.png");
            this.balls.Images.SetKeyName(8, "Tools.png");
            this.balls.Images.SetKeyName(9, "refresh16.png");
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.BackgroundImage = global::SirenOfShame.Properties.Resources.RibbonBackground;
            this.panel1.Controls.Add(this._sosOnline);
            this.panel1.Controls.Add(this.pictureBox5);
            this.panel1.Controls.Add(this._mute);
            this.panel1.Controls.Add(this._fullscreen);
            this.panel1.Controls.Add(this.pictureBox4);
            this.panel1.Controls.Add(this._configureSiren);
            this.panel1.Controls.Add(this._sirenMore);
            this.panel1.Controls.Add(this._configureCiServer);
            this.panel1.Controls.Add(this.pictureBox3);
            this.panel1.Controls.Add(this._help);
            this.panel1.Controls.Add(this._configurationMore);
            this.panel1.Controls.Add(this._configureRules);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this._timeboxEnforcer);
            this.panel1.Controls.Add(this._automaticUpdater);
            this.panel1.Controls.Add(this._openSettings);
            this.panel1.Controls.Add(this._testSiren);
            this.panel1.Controls.Add(this._refresh);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(869, 85);
            this.panel1.TabIndex = 37;
            // 
            // _sosOnline
            // 
            this._sosOnline.BackColor = System.Drawing.Color.Transparent;
            this._sosOnline.FlatAppearance.BorderSize = 0;
            this._sosOnline.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._sosOnline.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._sosOnline.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._sosOnline.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this._sosOnline.ImageIndex = 7;
            this._sosOnline.ImageList = this.bigIcons;
            this._sosOnline.Location = new System.Drawing.Point(135, 1);
            this._sosOnline.Name = "_sosOnline";
            this._sosOnline.Size = new System.Drawing.Size(63, 70);
            this._sosOnline.TabIndex = 23;
            this._sosOnline.Text = "SoS Online";
            this._sosOnline.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this._sosOnline.UseVisualStyleBackColor = false;
            this._sosOnline.Click += new System.EventHandler(this.SosOnlineClick);
            // 
            // bigIcons
            // 
            this.bigIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("bigIcons.ImageStream")));
            this.bigIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.bigIcons.Images.SetKeyName(0, "alarm.png");
            this.bigIcons.Images.SetKeyName(1, "ConfigureRulesBig.png");
            this.bigIcons.Images.SetKeyName(2, "TestSirenBig.png");
            this.bigIcons.Images.SetKeyName(3, "Towlie.png");
            this.bigIcons.Images.SetKeyName(4, "refresh.png");
            this.bigIcons.Images.SetKeyName(5, "mute.png");
            this.bigIcons.Images.SetKeyName(6, "unmute.png");
            this.bigIcons.Images.SetKeyName(7, "cloud.png");
            this.bigIcons.Images.SetKeyName(8, "ConfigureServer.png");
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::SirenOfShame.Properties.Resources.separater;
            this.pictureBox5.Location = new System.Drawing.Point(544, -2);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(3, 85);
            this.pictureBox5.TabIndex = 22;
            this.pictureBox5.TabStop = false;
            // 
            // _mute
            // 
            this._mute.BackColor = System.Drawing.Color.Transparent;
            this._mute.FlatAppearance.BorderSize = 0;
            this._mute.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._mute.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._mute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._mute.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this._mute.ImageIndex = 6;
            this._mute.ImageList = this.bigIcons;
            this._mute.Location = new System.Drawing.Point(403, 1);
            this._mute.Name = "_mute";
            this._mute.Size = new System.Drawing.Size(66, 68);
            this._mute.TabIndex = 21;
            this._mute.Text = "Mute";
            this._mute.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this._mute.UseVisualStyleBackColor = false;
            this._mute.Click += new System.EventHandler(this.MuteClick);
            // 
            // _fullscreen
            // 
            this._fullscreen.BackColor = System.Drawing.Color.Transparent;
            this._fullscreen.FlatAppearance.BorderSize = 0;
            this._fullscreen.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._fullscreen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._fullscreen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._fullscreen.Image = ((System.Drawing.Image)(resources.GetObject("_fullscreen.Image")));
            this._fullscreen.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this._fullscreen.Location = new System.Drawing.Point(472, 1);
            this._fullscreen.Name = "_fullscreen";
            this._fullscreen.Size = new System.Drawing.Size(73, 70);
            this._fullscreen.TabIndex = 20;
            this._fullscreen.Text = "Full Screen";
            this._fullscreen.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this._fullscreen.UseVisualStyleBackColor = false;
            this._fullscreen.Click += new System.EventHandler(this.FullscreenClick);
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::SirenOfShame.Properties.Resources.separater;
            this.pictureBox4.Location = new System.Drawing.Point(469, -2);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(3, 85);
            this.pictureBox4.TabIndex = 19;
            this.pictureBox4.TabStop = false;
            // 
            // _configureSiren
            // 
            this._configureSiren.BackColor = System.Drawing.Color.Transparent;
            this._configureSiren.FlatAppearance.BorderSize = 0;
            this._configureSiren.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._configureSiren.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._configureSiren.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._configureSiren.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._configureSiren.ImageIndex = 6;
            this._configureSiren.ImageList = this.balls;
            this._configureSiren.Location = new System.Drawing.Point(200, 47);
            this._configureSiren.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this._configureSiren.Name = "_configureSiren";
            this._configureSiren.Size = new System.Drawing.Size(108, 23);
            this._configureSiren.TabIndex = 17;
            this._configureSiren.Text = "Configure Siren";
            this._configureSiren.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._configureSiren.UseVisualStyleBackColor = false;
            this._configureSiren.Click += new System.EventHandler(this.ConfigureSirenClick);
            // 
            // _sirenMore
            // 
            this._sirenMore.BackColor = System.Drawing.Color.Transparent;
            this._sirenMore.FlatAppearance.BorderSize = 0;
            this._sirenMore.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._sirenMore.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._sirenMore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._sirenMore.Image = global::SirenOfShame.Properties.Resources.RibbonMore;
            this._sirenMore.Location = new System.Drawing.Point(448, 69);
            this._sirenMore.Name = "_sirenMore";
            this._sirenMore.Size = new System.Drawing.Size(12, 12);
            this._sirenMore.TabIndex = 16;
            this._sirenMore.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this._sirenMore.UseVisualStyleBackColor = false;
            this._sirenMore.Click += new System.EventHandler(this.SirenMoreClick);
            // 
            // _configureCiServer
            // 
            this._configureCiServer.BackColor = System.Drawing.Color.Transparent;
            this._configureCiServer.FlatAppearance.BorderSize = 0;
            this._configureCiServer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._configureCiServer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._configureCiServer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._configureCiServer.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this._configureCiServer.ImageIndex = 8;
            this._configureCiServer.ImageList = this.bigIcons;
            this._configureCiServer.Location = new System.Drawing.Point(4, 1);
            this._configureCiServer.Name = "_configureCiServer";
            this._configureCiServer.Size = new System.Drawing.Size(62, 70);
            this._configureCiServer.TabIndex = 15;
            this._configureCiServer.Text = "Configure Server(s)";
            this._configureCiServer.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this._configureCiServer.UseVisualStyleBackColor = false;
            this._configureCiServer.Click += new System.EventHandler(this.ConfigureServersClick);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::SirenOfShame.Properties.Resources.separater;
            this.pictureBox3.Location = new System.Drawing.Point(694, -2);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(3, 85);
            this.pictureBox3.TabIndex = 13;
            this.pictureBox3.TabStop = false;
            // 
            // _help
            // 
            this._help.BackColor = System.Drawing.Color.Transparent;
            this._help.FlatAppearance.BorderSize = 0;
            this._help.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._help.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._help.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._help.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this._help.ImageIndex = 3;
            this._help.ImageList = this.bigIcons;
            this._help.Location = new System.Drawing.Point(625, 2);
            this._help.Name = "_help";
            this._help.Size = new System.Drawing.Size(73, 70);
            this._help.TabIndex = 12;
            this._help.Text = "What\'s Going On?";
            this._help.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this._help.UseVisualStyleBackColor = false;
            this._help.Click += new System.EventHandler(this.HelpClick);
            // 
            // _configurationMore
            // 
            this._configurationMore.BackColor = System.Drawing.Color.Transparent;
            this._configurationMore.FlatAppearance.BorderSize = 0;
            this._configurationMore.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._configurationMore.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._configurationMore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._configurationMore.Image = global::SirenOfShame.Properties.Resources.RibbonMore;
            this._configurationMore.Location = new System.Drawing.Point(311, 66);
            this._configurationMore.Name = "_configurationMore";
            this._configurationMore.Size = new System.Drawing.Size(12, 12);
            this._configurationMore.TabIndex = 14;
            this._configurationMore.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this._configurationMore.UseVisualStyleBackColor = false;
            this._configurationMore.Click += new System.EventHandler(this.ConfigurationMoreClick);
            // 
            // _configureRules
            // 
            this._configureRules.BackColor = System.Drawing.Color.Transparent;
            this._configureRules.FlatAppearance.BorderSize = 0;
            this._configureRules.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._configureRules.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._configureRules.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._configureRules.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this._configureRules.ImageIndex = 1;
            this._configureRules.ImageList = this.bigIcons;
            this._configureRules.Location = new System.Drawing.Point(70, 1);
            this._configureRules.Name = "_configureRules";
            this._configureRules.Size = new System.Drawing.Size(63, 70);
            this._configureRules.TabIndex = 0;
            this._configureRules.Text = "Configure Rules";
            this._configureRules.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this._configureRules.UseVisualStyleBackColor = false;
            this._configureRules.Click += new System.EventHandler(this.ConfigureRulesClick);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label3.Location = new System.Drawing.Point(551, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "Else";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::SirenOfShame.Properties.Resources.separater;
            this.pictureBox2.Location = new System.Drawing.Point(330, -3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(3, 85);
            this.pictureBox2.TabIndex = 8;
            this.pictureBox2.TabStop = false;
            // 
            // _timeboxEnforcer
            // 
            this._timeboxEnforcer.BackColor = System.Drawing.Color.Transparent;
            this._timeboxEnforcer.FlatAppearance.BorderSize = 0;
            this._timeboxEnforcer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._timeboxEnforcer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._timeboxEnforcer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._timeboxEnforcer.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this._timeboxEnforcer.ImageIndex = 0;
            this._timeboxEnforcer.ImageList = this.bigIcons;
            this._timeboxEnforcer.Location = new System.Drawing.Point(552, 1);
            this._timeboxEnforcer.Name = "_timeboxEnforcer";
            this._timeboxEnforcer.Size = new System.Drawing.Size(73, 70);
            this._timeboxEnforcer.TabIndex = 6;
            this._timeboxEnforcer.Text = "The Enforcer";
            this._timeboxEnforcer.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this._timeboxEnforcer.UseVisualStyleBackColor = false;
            this._timeboxEnforcer.Click += new System.EventHandler(this.TimeboxEnforcerClick);
            // 
            // _automaticUpdater
            // 
            this._automaticUpdater.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._automaticUpdater.ContainerForm = this;
            this._automaticUpdater.DaysBetweenChecks = 0;
            this._automaticUpdater.GUID = "2a0c1820-2647-40bc-9114-57045d626825";
            this._automaticUpdater.Location = new System.Drawing.Point(841, 8);
            this._automaticUpdater.Name = "_automaticUpdater";
            this._automaticUpdater.Size = new System.Drawing.Size(16, 16);
            this._automaticUpdater.TabIndex = 5;
            this._automaticUpdater.wyUpdateCommandline = null;
            // 
            // _openSettings
            // 
            this._openSettings.BackColor = System.Drawing.Color.Transparent;
            this._openSettings.FlatAppearance.BorderSize = 0;
            this._openSettings.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._openSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._openSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._openSettings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._openSettings.ImageIndex = 8;
            this._openSettings.ImageList = this.balls;
            this._openSettings.Location = new System.Drawing.Point(200, 3);
            this._openSettings.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this._openSettings.Name = "_openSettings";
            this._openSettings.Size = new System.Drawing.Size(73, 23);
            this._openSettings.TabIndex = 4;
            this._openSettings.Text = "Settings";
            this._openSettings.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._openSettings.UseVisualStyleBackColor = false;
            this._openSettings.Click += new System.EventHandler(this.OpenSettingsClick);
            // 
            // _testSiren
            // 
            this._testSiren.BackColor = System.Drawing.Color.Transparent;
            this._testSiren.Enabled = false;
            this._testSiren.FlatAppearance.BorderSize = 0;
            this._testSiren.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._testSiren.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._testSiren.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._testSiren.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this._testSiren.ImageIndex = 2;
            this._testSiren.ImageList = this.bigIcons;
            this._testSiren.Location = new System.Drawing.Point(337, 1);
            this._testSiren.Name = "_testSiren";
            this._testSiren.Size = new System.Drawing.Size(68, 68);
            this._testSiren.TabIndex = 2;
            this._testSiren.Text = "Show Off";
            this._testSiren.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this._testSiren.UseVisualStyleBackColor = false;
            this._testSiren.Click += new System.EventHandler(this.TestSirenClick);
            // 
            // _refresh
            // 
            this._refresh.BackColor = System.Drawing.Color.Transparent;
            this._refresh.FlatAppearance.BorderSize = 0;
            this._refresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._refresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._refresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._refresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._refresh.ImageIndex = 9;
            this._refresh.ImageList = this.balls;
            this._refresh.Location = new System.Drawing.Point(200, 26);
            this._refresh.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this._refresh.Name = "_refresh";
            this._refresh.Size = new System.Drawing.Size(86, 23);
            this._refresh.TabIndex = 1;
            this._refresh.Text = "Refresh All";
            this._refresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._refresh.UseVisualStyleBackColor = false;
            this._refresh.Click += new System.EventHandler(this.RefreshClick);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label5.Location = new System.Drawing.Point(469, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 16);
            this.label5.TabIndex = 18;
            this.label5.Text = "View";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(333, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 18);
            this.label2.TabIndex = 10;
            this.label2.Text = "Siren";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(0, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(327, 14);
            this.label1.TabIndex = 9;
            this.label1.Text = "Configuration";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buildStatusBindingSource
            // 
            this.buildStatusBindingSource.DataSource = typeof(SirenOfShame.Lib.Watcher.BuildStatus);
            // 
            // _buildMenu
            // 
            this._buildMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._affectsTrayIcon,
            this._stopWatching,
            this._when,
            this._toolStripSeparator1});
            this._buildMenu.Name = "_buildMenu";
            this._buildMenu.Size = new System.Drawing.Size(164, 76);
            this._buildMenu.Text = "BuildMenu";
            this._buildMenu.Opening += new System.ComponentModel.CancelEventHandler(this.BuildMenuOpening);
            // 
            // _affectsTrayIcon
            // 
            this._affectsTrayIcon.Checked = true;
            this._affectsTrayIcon.CheckState = System.Windows.Forms.CheckState.Checked;
            this._affectsTrayIcon.Name = "_affectsTrayIcon";
            this._affectsTrayIcon.Size = new System.Drawing.Size(163, 22);
            this._affectsTrayIcon.Text = "Affects Tray Icon";
            this._affectsTrayIcon.Click += new System.EventHandler(this.AffectsTrayIconClick);
            // 
            // _stopWatching
            // 
            this._stopWatching.Name = "_stopWatching";
            this._stopWatching.Size = new System.Drawing.Size(163, 22);
            this._stopWatching.Text = "Stop Watching";
            this._stopWatching.Click += new System.EventHandler(this.StopWatchingClick);
            // 
            // _when
            // 
            this._when.Name = "_when";
            this._when.Size = new System.Drawing.Size(163, 22);
            this._when.Text = "When";
            // 
            // _toolStripSeparator1
            // 
            this._toolStripSeparator1.Name = "_toolStripSeparator1";
            this._toolStripSeparator1.Size = new System.Drawing.Size(160, 6);
            // 
            // _configurationMenu
            // 
            this._configurationMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._checkForUpdates,
            this._viewLog});
            this._configurationMenu.Name = "_configurationMenu";
            this._configurationMenu.Size = new System.Drawing.Size(174, 48);
            // 
            // _checkForUpdates
            // 
            this._checkForUpdates.Name = "_checkForUpdates";
            this._checkForUpdates.Size = new System.Drawing.Size(173, 22);
            this._checkForUpdates.Text = "Check For Updates";
            this._checkForUpdates.Click += new System.EventHandler(this.CheckForUpdatesClick);
            // 
            // _viewLog
            // 
            this._viewLog.Name = "_viewLog";
            this._viewLog.Size = new System.Drawing.Size(173, 22);
            this._viewLog.Text = "View Logs...";
            this._viewLog.Click += new System.EventHandler(this.ViewLogClick);
            // 
            // _sirenMenu
            // 
            this._sirenMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._upgradeFirmwareMenuItem});
            this._sirenMenu.Name = "_configurationMenu";
            this._sirenMenu.Size = new System.Drawing.Size(181, 26);
            // 
            // _upgradeFirmwareMenuItem
            // 
            this._upgradeFirmwareMenuItem.Name = "_upgradeFirmwareMenuItem";
            this._upgradeFirmwareMenuItem.Size = new System.Drawing.Size(180, 22);
            this._upgradeFirmwareMenuItem.Text = "Upgrade Firmware...";
            this._upgradeFirmwareMenuItem.Click += new System.EventHandler(this.SirenUpgradeFirmwareClick);
            // 
            // _panelRight
            // 
            this._panelRight.Controls.Add(this._userStats);
            this._panelRight.Controls.Add(this._buildStats);
            this._panelRight.Dock = System.Windows.Forms.DockStyle.Right;
            this._panelRight.Location = new System.Drawing.Point(698, 106);
            this._panelRight.Name = "_panelRight";
            this._panelRight.Size = new System.Drawing.Size(171, 199);
            this._panelRight.TabIndex = 38;
            // 
            // _userStats
            // 
            this._userStats.Controls.Add(this._users);
            this._userStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this._userStats.Location = new System.Drawing.Point(0, 0);
            this._userStats.Name = "_userStats";
            this._userStats.Size = new System.Drawing.Size(171, 199);
            this._userStats.TabIndex = 6;
            // 
            // _users
            // 
            this._users.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this._users.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.User,
            this.Reputation});
            this._users.Dock = System.Windows.Forms.DockStyle.Fill;
            this._users.LabelEdit = true;
            this._users.Location = new System.Drawing.Point(0, 0);
            this._users.Name = "_users";
            this._users.Size = new System.Drawing.Size(171, 199);
            this._users.TabIndex = 0;
            this._users.UseCompatibleStateImageBehavior = false;
            this._users.View = System.Windows.Forms.View.Details;
            this._users.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.UsersAfterLabelEdit);
            this._users.SelectedIndexChanged += new System.EventHandler(this._users_SelectedIndexChanged);
            this._users.MouseUp += new System.Windows.Forms.MouseEventHandler(this.UsersMouseUp);
            // 
            // User
            // 
            this.User.Text = "User";
            this.User.Width = 80;
            // 
            // Reputation
            // 
            this.Reputation.Text = "Reputation";
            this.Reputation.Width = 75;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 168);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Percent Failed:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 144);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Failed Builds:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Total Builds:";
            // 
            // _buildHistoryZedGraph
            // 
            this._buildHistoryZedGraph.Dock = System.Windows.Forms.DockStyle.Top;
            this._buildHistoryZedGraph.Location = new System.Drawing.Point(0, 0);
            this._buildHistoryZedGraph.Name = "_buildHistoryZedGraph";
            this._buildHistoryZedGraph.ScrollGrace = 0D;
            this._buildHistoryZedGraph.ScrollMaxX = 0D;
            this._buildHistoryZedGraph.ScrollMaxY = 0D;
            this._buildHistoryZedGraph.ScrollMaxY2 = 0D;
            this._buildHistoryZedGraph.ScrollMinX = 0D;
            this._buildHistoryZedGraph.ScrollMinY = 0D;
            this._buildHistoryZedGraph.ScrollMinY2 = 0D;
            this._buildHistoryZedGraph.Size = new System.Drawing.Size(171, 116);
            this._buildHistoryZedGraph.TabIndex = 12;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(695, 106);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 199);
            this.splitter1.TabIndex = 39;
            this.splitter1.TabStop = false;
            // 
            // _userMenu
            // 
            this._userMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._editUserName,
            this._hideUser,
            this.toolStripSeparator1,
            this._showAllUsers});
            this._userMenu.Name = "_userMenu";
            this._userMenu.Size = new System.Drawing.Size(130, 76);
            // 
            // _editUserName
            // 
            this._editUserName.Name = "_editUserName";
            this._editUserName.Size = new System.Drawing.Size(129, 22);
            this._editUserName.Text = "Edit Name";
            this._editUserName.Click += new System.EventHandler(this.EditUserNameClick);
            // 
            // _hideUser
            // 
            this._hideUser.Name = "_hideUser";
            this._hideUser.Size = new System.Drawing.Size(129, 22);
            this._hideUser.Text = "Hidden";
            this._hideUser.Click += new System.EventHandler(this.HideUserClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(126, 6);
            // 
            // _showAllUsers
            // 
            this._showAllUsers.CheckOnClick = true;
            this._showAllUsers.Name = "_showAllUsers";
            this._showAllUsers.Size = new System.Drawing.Size(129, 22);
            this._showAllUsers.Text = "Show All";
            this._showAllUsers.CheckedChanged += new System.EventHandler(this.ShowAllUsersCheckedChanged);
            // 
            // _panelAlert
            // 
            this._panelAlert.BackColor = System.Drawing.Color.LightCoral;
            this._panelAlert.Controls.Add(this._details);
            this._panelAlert.Controls.Add(this._labelAlert);
            this._panelAlert.Controls.Add(this._closeAlert);
            this._panelAlert.Dock = System.Windows.Forms.DockStyle.Top;
            this._panelAlert.Location = new System.Drawing.Point(0, 85);
            this._panelAlert.Name = "_panelAlert";
            this._panelAlert.Size = new System.Drawing.Size(869, 21);
            this._panelAlert.TabIndex = 40;
            this._panelAlert.Visible = false;
            // 
            // _details
            // 
            this._details.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._details.AutoSize = true;
            this._details.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._details.Location = new System.Drawing.Point(418, 2);
            this._details.Name = "_details";
            this._details.Size = new System.Drawing.Size(107, 17);
            this._details.TabIndex = 43;
            this._details.TabStop = true;
            this._details.Text = "Click for details";
            this._details.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.DetailsLinkClicked);
            // 
            // _labelAlert
            // 
            this._labelAlert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._labelAlert.AutoSize = true;
            this._labelAlert.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelAlert.Location = new System.Drawing.Point(4, 2);
            this._labelAlert.Name = "_labelAlert";
            this._labelAlert.Size = new System.Drawing.Size(325, 17);
            this._labelAlert.TabIndex = 42;
            this._labelAlert.Text = "Did you know there was a new version available?";
            // 
            // _closeAlert
            // 
            this._closeAlert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._closeAlert.BackColor = System.Drawing.Color.LightCoral;
            this._closeAlert.BackgroundImage = global::SirenOfShame.Properties.Resources.CloseButton1;
            this._closeAlert.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this._closeAlert.FlatAppearance.BorderSize = 0;
            this._closeAlert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._closeAlert.Location = new System.Drawing.Point(841, 0);
            this._closeAlert.Name = "_closeAlert";
            this._closeAlert.Size = new System.Drawing.Size(25, 20);
            this._closeAlert.TabIndex = 41;
            this._closeAlert.UseVisualStyleBackColor = false;
            this._closeAlert.Click += new System.EventHandler(this.CloseAlertClick);
            // 
            // _buildDefinitions
            // 
            this._buildDefinitions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name,
            this.ID,
            this.date,
            this.duration2,
            this.checkedInBy,
            this.comment});
            this._buildDefinitions.Dock = System.Windows.Forms.DockStyle.Fill;
            this._buildDefinitions.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this._buildDefinitions.Location = new System.Drawing.Point(0, 106);
            this._buildDefinitions.Name = "_buildDefinitions";
            this._buildDefinitions.Size = new System.Drawing.Size(695, 199);
            this._buildDefinitions.SmallImageList = this.balls;
            this._buildDefinitions.TabIndex = 36;
            this._buildDefinitions.UseCompatibleStateImageBehavior = false;
            this._buildDefinitions.View = System.Windows.Forms.View.Details;
            this._buildDefinitions.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.BuildDefinitionsColumnClick);
            this._buildDefinitions.SelectedIndexChanged += new System.EventHandler(this.BuildDefinitionsSelectedIndexChanged);
            this._buildDefinitions.DoubleClick += new System.EventHandler(this.BuildDefinitionsDoubleClick);
            this._buildDefinitions.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BuildDefinitionsMouseUp);
            // 
            // name
            // 
            this.name.Text = "Build";
            this.name.Width = 150;
            // 
            // ID
            // 
            this.ID.Text = "ID";
            this.ID.Width = 40;
            // 
            // date
            // 
            this.date.Text = "Date";
            this.date.Width = 105;
            // 
            // duration2
            // 
            this.duration2.Text = "Duration";
            this.duration2.Width = 54;
            // 
            // checkedInBy
            // 
            this.checkedInBy.Text = "Checked In By";
            this.checkedInBy.Width = 102;
            // 
            // comment
            // 
            this.comment.Text = "Comment";
            this.comment.Width = 250;
            // 
            // viewUser1
            // 
            this.viewUser1.BackColor = System.Drawing.SystemColors.Window;
            this.viewUser1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.viewUser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewUser1.Location = new System.Drawing.Point(0, 106);
            this.viewUser1.Name = "viewUser1";
            this.viewUser1.Size = new System.Drawing.Size(695, 199);
            this.viewUser1.TabIndex = 41;
            // 
            // _buildStats
            // 
            this._buildStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this._buildStats.Location = new System.Drawing.Point(0, 0);
            this._buildStats.Name = "_buildStats";
            this._buildStats.Size = new System.Drawing.Size(171, 199);
            this._buildStats.TabIndex = 8;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 327);
            this.Controls.Add(this._buildDefinitions);
            this.Controls.Add(this.viewUser1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this._panelRight);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this._panelAlert);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Siren of Shame";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormFormClosing);
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.Move += new System.EventHandler(this.MainFormMove);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.minimizedMenu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._automaticUpdater)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buildStatusBindingSource)).EndInit();
            this._buildMenu.ResumeLayout(false);
            this._configurationMenu.ResumeLayout(false);
            this._sirenMenu.ResumeLayout(false);
            this._panelRight.ResumeLayout(false);
            this._userStats.ResumeLayout(false);
            this._userMenu.ResumeLayout(false);
            this._panelAlert.ResumeLayout(false);
            this._panelAlert.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel _lastStatusUpdate;
        private System.Windows.Forms.BindingSource buildStatusBindingSource;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip minimizedMenu;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private BuildStatusListView _buildDefinitions;
        //private System.Windows.Forms.ColumnHeader Name;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.ColumnHeader date;
        private System.Windows.Forms.ColumnHeader checkedInBy;
        private System.Windows.Forms.ImageList balls;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button _configureRules;
        private System.Windows.Forms.Button _refresh;
        private System.Windows.Forms.Button _testSiren;
        private System.Windows.Forms.ContextMenuStrip _buildMenu;
        private System.Windows.Forms.ToolStripMenuItem _affectsTrayIcon;
        private System.Windows.Forms.ToolStripMenuItem _when;
        private System.Windows.Forms.ToolStripSeparator _toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem _stopWatching;
      private System.Windows.Forms.Button _openSettings;
      private System.Windows.Forms.ColumnHeader comment;
      private System.Windows.Forms.ColumnHeader duration2;
      private wyDay.Controls.AutomaticUpdater _automaticUpdater;
      private System.Windows.Forms.Button _timeboxEnforcer;
      private System.Windows.Forms.ImageList bigIcons;
      private System.Windows.Forms.PictureBox pictureBox2;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Button _help;
      private System.Windows.Forms.PictureBox pictureBox3;
      private System.Windows.Forms.Button _configurationMore;
      private System.Windows.Forms.ContextMenuStrip _configurationMenu;
      private System.Windows.Forms.ToolStripMenuItem _checkForUpdates;
      private System.Windows.Forms.ToolStripMenuItem _viewLog;
      private System.Windows.Forms.Button _configureCiServer;
      private System.Windows.Forms.Button _sirenMore;
      private System.Windows.Forms.ContextMenuStrip _sirenMenu;
      private System.Windows.Forms.ToolStripMenuItem _upgradeFirmwareMenuItem;
      private System.Windows.Forms.Button _configureSiren;
      private System.Windows.Forms.Panel _panelRight;
      private System.Windows.Forms.Splitter splitter1;
      //private System.Windows.Forms.Panel _panelBuildStats;
	  private BuildStats _buildStats;
      //private System.Windows.Forms.Label _percentFailed;
      private System.Windows.Forms.Label label8;
      //private System.Windows.Forms.Label _failedBuilds;
      private System.Windows.Forms.Label label7;
      //private System.Windows.Forms.Label _buildCount;
      private System.Windows.Forms.Label label4;
      private ZedGraph.ZedGraphControl _buildHistoryZedGraph;
      private System.Windows.Forms.Button _fullscreen;
      private System.Windows.Forms.PictureBox pictureBox4;
      private System.Windows.Forms.Label label5;
      private System.Windows.Forms.ContextMenuStrip _userMenu;
      private System.Windows.Forms.ToolStripMenuItem _editUserName;
      private System.Windows.Forms.ToolStripMenuItem _hideUser;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
      private System.Windows.Forms.ToolStripMenuItem _showAllUsers;
      private System.Windows.Forms.PictureBox pictureBox5;
      private System.Windows.Forms.Button _mute;
      private System.Windows.Forms.Button _closeAlert;
      private System.Windows.Forms.Panel _panelAlert;
      private System.Windows.Forms.Label _labelAlert;
      private System.Windows.Forms.LinkLabel _details;
      private System.Windows.Forms.ColumnHeader ID;
      private ViewUser viewUser1;
      private System.Windows.Forms.Panel _userStats;
      private System.Windows.Forms.ListView _users;
      private System.Windows.Forms.ColumnHeader User;
      private System.Windows.Forms.ColumnHeader Reputation;
      private System.Windows.Forms.ToolStripDropDownButton _toolStripSplitErrorButton;
      private System.Windows.Forms.Button _sosOnline;
	}
}

