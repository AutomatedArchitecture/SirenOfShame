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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this._toolStripSplitErrorButton = new System.Windows.Forms.ToolStripDropDownButton();
            this._lastStatusUpdate = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this._sosOnlineError = new System.Windows.Forms.ToolStripDropDownButton();
            this._sosOnlineStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.minimizedMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._ribbonPanel = new System.Windows.Forms.Panel();
            this._userMappings = new System.Windows.Forms.Button();
            this._toolbar16 = new System.Windows.Forms.ImageList(this.components);
            this._viewAllUsers = new System.Windows.Forms.Button();
            this._sosOnline = new System.Windows.Forms.Button();
            this._toolbar32 = new System.Windows.Forms.ImageList(this.components);
            this._mute = new System.Windows.Forms.Button();
            this._fullscreen = new System.Windows.Forms.Button();
            this._configureSounds = new System.Windows.Forms.Button();
            this._sirenMore = new System.Windows.Forms.Button();
            this._configureCiServer = new System.Windows.Forms.Button();
            this._help = new System.Windows.Forms.Button();
            this._configurationMore = new System.Windows.Forms.Button();
            this._configureRules = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this._timeboxEnforcer = new System.Windows.Forms.Button();
            this._automaticUpdater = new wyDay.Controls.AutomaticUpdater();
            this._openSettings = new System.Windows.Forms.Button();
            this._testSiren = new System.Windows.Forms.Button();
            this._refresh = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._configurationMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._checkForUpdates = new System.Windows.Forms.ToolStripMenuItem();
            this._viewLog = new System.Windows.Forms.ToolStripMenuItem();
            this._sirenMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._upgradeFirmwareMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList29 = new System.Windows.Forms.ImageList(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._buildHistoryZedGraph = new ZedGraph.ZedGraphControl();
            this._panelAlert = new System.Windows.Forms.Panel();
            this._details = new System.Windows.Forms.LinkLabel();
            this._labelAlert = new System.Windows.Forms.Label();
            this._closeAlert = new System.Windows.Forms.Button();
            this._avatarImageList = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this._showRibbon = new System.Windows.Forms.Button();
            this._highlightPanel2 = new System.Windows.Forms.Panel();
            this._highlightPanel = new System.Windows.Forms.Panel();
            this._highlightPanel3 = new System.Windows.Forms.Panel();
            this.buildStatusBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this._viewBuilds = new SirenOfShame.ViewBuilds();
            this.viewUser1 = new SirenOfShame.ViewUser();
            this._newsFeed1 = new SirenOfShame.NewsFeed();
            this._userList = new SirenOfShame.Leaders();
            this.separator4 = new SirenOfShame.Separator();
            this.separator3 = new SirenOfShame.Separator();
            this.separator2 = new SirenOfShame.Separator();
            this.separator1 = new SirenOfShame.Separator();
            this.statusStrip1.SuspendLayout();
            this.minimizedMenu.SuspendLayout();
            this._ribbonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._automaticUpdater)).BeginInit();
            this._configurationMenu.SuspendLayout();
            this._sirenMenu.SuspendLayout();
            this._panelAlert.SuspendLayout();
            this.panel1.SuspendLayout();
            this._highlightPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buildStatusBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.Transparent;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._toolStripSplitErrorButton,
            this._lastStatusUpdate,
            this.toolStripStatusLabel2,
            this._sosOnlineError,
            this._sosOnlineStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 461);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1005, 22);
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
            this._lastStatusUpdate.ForeColor = System.Drawing.Color.Silver;
            this._lastStatusUpdate.Name = "_lastStatusUpdate";
            this._lastStatusUpdate.Size = new System.Drawing.Size(131, 17);
            this._lastStatusUpdate.Text = "Build Last Checked: n/a";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.ForeColor = System.Drawing.Color.Silver;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(10, 17);
            this.toolStripStatusLabel2.Text = "|";
            // 
            // _sosOnlineError
            // 
            this._sosOnlineError.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._sosOnlineError.Image = global::SirenOfShame.Properties.Resources.question_big;
            this._sosOnlineError.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._sosOnlineError.Name = "_sosOnlineError";
            this._sosOnlineError.ShowDropDownArrow = false;
            this._sosOnlineError.Size = new System.Drawing.Size(20, 20);
            this._sosOnlineError.Text = "toolStripSplitButton1";
            this._sosOnlineError.ToolTipText = "Error Occured";
            this._sosOnlineError.Visible = false;
            this._sosOnlineError.Click += new System.EventHandler(this.SosOnlineErrorClick);
            // 
            // _sosOnlineStatus
            // 
            this._sosOnlineStatus.ForeColor = System.Drawing.Color.Silver;
            this._sosOnlineStatus.Name = "_sosOnlineStatus";
            this._sosOnlineStatus.Size = new System.Drawing.Size(131, 17);
            this._sosOnlineStatus.Text = "Sos Online: Connecting";
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.ContextMenuStrip = this.minimizedMenu;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Siren of Shame";
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
            // _ribbonPanel
            // 
            this._ribbonPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this._ribbonPanel.Controls.Add(this._userMappings);
            this._ribbonPanel.Controls.Add(this._viewAllUsers);
            this._ribbonPanel.Controls.Add(this.separator4);
            this._ribbonPanel.Controls.Add(this.separator3);
            this._ribbonPanel.Controls.Add(this.separator2);
            this._ribbonPanel.Controls.Add(this.separator1);
            this._ribbonPanel.Controls.Add(this._sosOnline);
            this._ribbonPanel.Controls.Add(this._mute);
            this._ribbonPanel.Controls.Add(this._fullscreen);
            this._ribbonPanel.Controls.Add(this._configureSounds);
            this._ribbonPanel.Controls.Add(this._sirenMore);
            this._ribbonPanel.Controls.Add(this._configureCiServer);
            this._ribbonPanel.Controls.Add(this._help);
            this._ribbonPanel.Controls.Add(this._configurationMore);
            this._ribbonPanel.Controls.Add(this._configureRules);
            this._ribbonPanel.Controls.Add(this.label3);
            this._ribbonPanel.Controls.Add(this._timeboxEnforcer);
            this._ribbonPanel.Controls.Add(this._automaticUpdater);
            this._ribbonPanel.Controls.Add(this._openSettings);
            this._ribbonPanel.Controls.Add(this._testSiren);
            this._ribbonPanel.Controls.Add(this._refresh);
            this._ribbonPanel.Controls.Add(this.label5);
            this._ribbonPanel.Controls.Add(this.label2);
            this._ribbonPanel.Controls.Add(this.label1);
            this._ribbonPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._ribbonPanel.Location = new System.Drawing.Point(0, 16);
            this._ribbonPanel.Name = "_ribbonPanel";
            this._ribbonPanel.Size = new System.Drawing.Size(1005, 88);
            this._ribbonPanel.TabIndex = 37;
            // 
            // _userMappings
            // 
            this._userMappings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this._userMappings.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._userMappings.FlatAppearance.BorderSize = 0;
            this._userMappings.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(55)))), ((int)(((byte)(0)))));
            this._userMappings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(65)))), ((int)(((byte)(0)))));
            this._userMappings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._userMappings.ForeColor = System.Drawing.Color.White;
            this._userMappings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._userMappings.ImageKey = "id_cards.bmp";
            this._userMappings.ImageList = this._toolbar16;
            this._userMappings.Location = new System.Drawing.Point(544, 26);
            this._userMappings.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this._userMappings.Name = "_userMappings";
            this._userMappings.Size = new System.Drawing.Size(112, 21);
            this._userMappings.TabIndex = 29;
            this._userMappings.Text = "        User Mappings";
            this._userMappings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._userMappings.UseVisualStyleBackColor = false;
            this._userMappings.Click += new System.EventHandler(this.UserMappingsClick);
            // 
            // _toolbar16
            // 
            this._toolbar16.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_toolbar16.ImageStream")));
            this._toolbar16.TransparentColor = System.Drawing.Color.Magenta;
            this._toolbar16.Images.SetKeyName(0, "alarm.bmp");
            this._toolbar16.Images.SetKeyName(1, "recycle.bmp");
            this._toolbar16.Images.SetKeyName(2, "window_gear.bmp");
            this._toolbar16.Images.SetKeyName(3, "users4_checkbox_checked.bmp");
            this._toolbar16.Images.SetKeyName(4, "users4_checkbox_unchecked.bmp");
            this._toolbar16.Images.SetKeyName(5, "id_cards.bmp");
            this._toolbar16.Images.SetKeyName(6, "music.bmp");
            // 
            // _viewAllUsers
            // 
            this._viewAllUsers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this._viewAllUsers.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._viewAllUsers.FlatAppearance.BorderSize = 0;
            this._viewAllUsers.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(55)))), ((int)(((byte)(0)))));
            this._viewAllUsers.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(65)))), ((int)(((byte)(0)))));
            this._viewAllUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._viewAllUsers.ForeColor = System.Drawing.Color.White;
            this._viewAllUsers.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._viewAllUsers.ImageKey = "users4_checkbox_unchecked.bmp";
            this._viewAllUsers.ImageList = this._toolbar16;
            this._viewAllUsers.Location = new System.Drawing.Point(544, 2);
            this._viewAllUsers.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this._viewAllUsers.Name = "_viewAllUsers";
            this._viewAllUsers.Size = new System.Drawing.Size(112, 21);
            this._viewAllUsers.TabIndex = 28;
            this._viewAllUsers.Text = "        Show All Users";
            this._viewAllUsers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._viewAllUsers.UseVisualStyleBackColor = false;
            this._viewAllUsers.Click += new System.EventHandler(this.ViewAllUsersClick);
            // 
            // _sosOnline
            // 
            this._sosOnline.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this._sosOnline.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._sosOnline.FlatAppearance.BorderSize = 0;
            this._sosOnline.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(55)))), ((int)(((byte)(0)))));
            this._sosOnline.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(65)))), ((int)(((byte)(0)))));
            this._sosOnline.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._sosOnline.ForeColor = System.Drawing.Color.White;
            this._sosOnline.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this._sosOnline.ImageKey = "earth.bmp";
            this._sosOnline.ImageList = this._toolbar32;
            this._sosOnline.Location = new System.Drawing.Point(142, 2);
            this._sosOnline.Name = "_sosOnline";
            this._sosOnline.Size = new System.Drawing.Size(66, 69);
            this._sosOnline.TabIndex = 23;
            this._sosOnline.Text = "SoS Online";
            this._sosOnline.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this._sosOnline.UseVisualStyleBackColor = false;
            this._sosOnline.Click += new System.EventHandler(this.SosOnlineClick);
            // 
            // _toolbar32
            // 
            this._toolbar32.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_toolbar32.ImageStream")));
            this._toolbar32.TransparentColor = System.Drawing.Color.Magenta;
            this._toolbar32.Images.SetKeyName(0, "bell3.bmp");
            this._toolbar32.Images.SetKeyName(1, "businessmen.bmp");
            this._toolbar32.Images.SetKeyName(2, "businessmen_hot.bmp");
            this._toolbar32.Images.SetKeyName(3, "earth.bmp");
            this._toolbar32.Images.SetKeyName(4, "gears.bmp");
            this._toolbar32.Images.SetKeyName(5, "help.bmp");
            this._toolbar32.Images.SetKeyName(6, "lighthouse.bmp");
            this._toolbar32.Images.SetKeyName(7, "loudspeaker.bmp");
            this._toolbar32.Images.SetKeyName(8, "loudspeaker_forbidden.bmp");
            this._toolbar32.Images.SetKeyName(9, "messages.bmp");
            this._toolbar32.Images.SetKeyName(10, "server.bmp");
            this._toolbar32.Images.SetKeyName(11, "window_size.bmp");
            // 
            // _mute
            // 
            this._mute.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this._mute.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._mute.FlatAppearance.BorderSize = 0;
            this._mute.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(55)))), ((int)(((byte)(0)))));
            this._mute.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(65)))), ((int)(((byte)(0)))));
            this._mute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._mute.ForeColor = System.Drawing.Color.White;
            this._mute.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this._mute.ImageKey = "loudspeaker.bmp";
            this._mute.ImageList = this._toolbar32;
            this._mute.Location = new System.Drawing.Point(399, 2);
            this._mute.Name = "_mute";
            this._mute.Size = new System.Drawing.Size(66, 69);
            this._mute.TabIndex = 21;
            this._mute.Text = "Mute";
            this._mute.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this._mute.UseVisualStyleBackColor = false;
            this._mute.Click += new System.EventHandler(this.MuteClick);
            // 
            // _fullscreen
            // 
            this._fullscreen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this._fullscreen.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._fullscreen.FlatAppearance.BorderSize = 0;
            this._fullscreen.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(55)))), ((int)(((byte)(0)))));
            this._fullscreen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(65)))), ((int)(((byte)(0)))));
            this._fullscreen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._fullscreen.ForeColor = System.Drawing.Color.White;
            this._fullscreen.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this._fullscreen.ImageKey = "window_size.bmp";
            this._fullscreen.ImageList = this._toolbar32;
            this._fullscreen.Location = new System.Drawing.Point(472, 2);
            this._fullscreen.Name = "_fullscreen";
            this._fullscreen.Size = new System.Drawing.Size(66, 69);
            this._fullscreen.TabIndex = 20;
            this._fullscreen.Text = "Full Screen";
            this._fullscreen.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this._fullscreen.UseVisualStyleBackColor = false;
            this._fullscreen.Click += new System.EventHandler(this.FullscreenClick);
            // 
            // _configureSounds
            // 
            this._configureSounds.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this._configureSounds.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._configureSounds.FlatAppearance.BorderSize = 0;
            this._configureSounds.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(55)))), ((int)(((byte)(0)))));
            this._configureSounds.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(65)))), ((int)(((byte)(0)))));
            this._configureSounds.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._configureSounds.ForeColor = System.Drawing.Color.White;
            this._configureSounds.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._configureSounds.ImageKey = "music.bmp";
            this._configureSounds.ImageList = this._toolbar16;
            this._configureSounds.Location = new System.Drawing.Point(211, 50);
            this._configureSounds.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this._configureSounds.Name = "_configureSounds";
            this._configureSounds.Size = new System.Drawing.Size(112, 21);
            this._configureSounds.TabIndex = 17;
            this._configureSounds.Text = "        Sounds";
            this._configureSounds.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._configureSounds.UseVisualStyleBackColor = false;
            this._configureSounds.Click += new System.EventHandler(this.ConfigureSirenClick);
            // 
            // _sirenMore
            // 
            this._sirenMore.BackColor = System.Drawing.Color.Transparent;
            this._sirenMore.FlatAppearance.BorderSize = 0;
            this._sirenMore.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._sirenMore.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._sirenMore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._sirenMore.Image = global::SirenOfShame.Properties.Resources.RibbonMore;
            this._sirenMore.Location = new System.Drawing.Point(444, 72);
            this._sirenMore.Name = "_sirenMore";
            this._sirenMore.Size = new System.Drawing.Size(12, 12);
            this._sirenMore.TabIndex = 16;
            this._sirenMore.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this._sirenMore.UseVisualStyleBackColor = false;
            this._sirenMore.Click += new System.EventHandler(this.SirenMoreClick);
            // 
            // _configureCiServer
            // 
            this._configureCiServer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this._configureCiServer.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._configureCiServer.FlatAppearance.BorderSize = 0;
            this._configureCiServer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(55)))), ((int)(((byte)(0)))));
            this._configureCiServer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(65)))), ((int)(((byte)(0)))));
            this._configureCiServer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._configureCiServer.ForeColor = System.Drawing.Color.White;
            this._configureCiServer.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this._configureCiServer.ImageKey = "server.bmp";
            this._configureCiServer.ImageList = this._toolbar32;
            this._configureCiServer.Location = new System.Drawing.Point(4, 2);
            this._configureCiServer.Name = "_configureCiServer";
            this._configureCiServer.Size = new System.Drawing.Size(66, 69);
            this._configureCiServer.TabIndex = 15;
            this._configureCiServer.Text = "Configure Server(s)";
            this._configureCiServer.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this._configureCiServer.UseVisualStyleBackColor = false;
            this._configureCiServer.Click += new System.EventHandler(this.ConfigureServersClick);
            // 
            // _help
            // 
            this._help.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this._help.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._help.FlatAppearance.BorderSize = 0;
            this._help.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(55)))), ((int)(((byte)(0)))));
            this._help.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(65)))), ((int)(((byte)(0)))));
            this._help.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._help.ForeColor = System.Drawing.Color.White;
            this._help.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this._help.ImageKey = "help.bmp";
            this._help.ImageList = this._toolbar32;
            this._help.Location = new System.Drawing.Point(733, 0);
            this._help.Name = "_help";
            this._help.Size = new System.Drawing.Size(66, 69);
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
            this._configurationMore.Location = new System.Drawing.Point(311, 74);
            this._configurationMore.Name = "_configurationMore";
            this._configurationMore.Size = new System.Drawing.Size(12, 12);
            this._configurationMore.TabIndex = 14;
            this._configurationMore.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this._configurationMore.UseVisualStyleBackColor = false;
            this._configurationMore.Click += new System.EventHandler(this.ConfigurationMoreClick);
            // 
            // _configureRules
            // 
            this._configureRules.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this._configureRules.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._configureRules.FlatAppearance.BorderSize = 0;
            this._configureRules.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(55)))), ((int)(((byte)(0)))));
            this._configureRules.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(65)))), ((int)(((byte)(0)))));
            this._configureRules.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._configureRules.ForeColor = System.Drawing.Color.White;
            this._configureRules.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this._configureRules.ImageKey = "gears.bmp";
            this._configureRules.ImageList = this._toolbar32;
            this._configureRules.Location = new System.Drawing.Point(73, 2);
            this._configureRules.Name = "_configureRules";
            this._configureRules.Size = new System.Drawing.Size(66, 69);
            this._configureRules.TabIndex = 0;
            this._configureRules.Text = "Configure Rules";
            this._configureRules.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this._configureRules.UseVisualStyleBackColor = false;
            this._configureRules.Click += new System.EventHandler(this.ConfigureRulesClick);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.label3.Location = new System.Drawing.Point(668, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "Else";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _timeboxEnforcer
            // 
            this._timeboxEnforcer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this._timeboxEnforcer.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._timeboxEnforcer.FlatAppearance.BorderSize = 0;
            this._timeboxEnforcer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(55)))), ((int)(((byte)(0)))));
            this._timeboxEnforcer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(65)))), ((int)(((byte)(0)))));
            this._timeboxEnforcer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._timeboxEnforcer.ForeColor = System.Drawing.Color.White;
            this._timeboxEnforcer.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this._timeboxEnforcer.ImageKey = "bell3.bmp";
            this._timeboxEnforcer.ImageList = this._toolbar32;
            this._timeboxEnforcer.Location = new System.Drawing.Point(664, 0);
            this._timeboxEnforcer.Name = "_timeboxEnforcer";
            this._timeboxEnforcer.Size = new System.Drawing.Size(66, 69);
            this._timeboxEnforcer.TabIndex = 6;
            this._timeboxEnforcer.Text = "The Enforcer";
            this._timeboxEnforcer.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this._timeboxEnforcer.UseVisualStyleBackColor = false;
            this._timeboxEnforcer.Click += new System.EventHandler(this.TimeboxEnforcerClick);
            // 
            // _automaticUpdater
            // 
            this._automaticUpdater.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._automaticUpdater.BackColor = System.Drawing.Color.Transparent;
            this._automaticUpdater.ContainerForm = this;
            this._automaticUpdater.DaysBetweenChecks = 0;
            this._automaticUpdater.Enabled = false;
            this._automaticUpdater.ForeColor = System.Drawing.Color.White;
            this._automaticUpdater.GUID = "2a0c1820-2647-40bc-9114-57045d626825";
            this._automaticUpdater.Location = new System.Drawing.Point(977, 8);
            this._automaticUpdater.Name = "_automaticUpdater";
            this._automaticUpdater.Size = new System.Drawing.Size(16, 16);
            this._automaticUpdater.TabIndex = 5;
            this._automaticUpdater.wyUpdateCommandline = null;
            // 
            // _openSettings
            // 
            this._openSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this._openSettings.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._openSettings.FlatAppearance.BorderSize = 0;
            this._openSettings.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(55)))), ((int)(((byte)(0)))));
            this._openSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(65)))), ((int)(((byte)(0)))));
            this._openSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._openSettings.ForeColor = System.Drawing.Color.White;
            this._openSettings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._openSettings.ImageKey = "window_gear.bmp";
            this._openSettings.ImageList = this._toolbar16;
            this._openSettings.Location = new System.Drawing.Point(211, 2);
            this._openSettings.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this._openSettings.Name = "_openSettings";
            this._openSettings.Size = new System.Drawing.Size(112, 21);
            this._openSettings.TabIndex = 4;
            this._openSettings.Text = "        Settings";
            this._openSettings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._openSettings.UseVisualStyleBackColor = false;
            this._openSettings.Click += new System.EventHandler(this.OpenSettingsClick);
            // 
            // _testSiren
            // 
            this._testSiren.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this._testSiren.Enabled = false;
            this._testSiren.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._testSiren.FlatAppearance.BorderSize = 0;
            this._testSiren.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(55)))), ((int)(((byte)(0)))));
            this._testSiren.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(65)))), ((int)(((byte)(0)))));
            this._testSiren.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._testSiren.ForeColor = System.Drawing.Color.White;
            this._testSiren.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this._testSiren.ImageKey = "lighthouse.bmp";
            this._testSiren.ImageList = this._toolbar32;
            this._testSiren.Location = new System.Drawing.Point(330, 2);
            this._testSiren.Name = "_testSiren";
            this._testSiren.Size = new System.Drawing.Size(66, 69);
            this._testSiren.TabIndex = 2;
            this._testSiren.Text = "Show Off";
            this._testSiren.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this._testSiren.UseVisualStyleBackColor = false;
            this._testSiren.Click += new System.EventHandler(this.TestSirenClick);
            // 
            // _refresh
            // 
            this._refresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this._refresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._refresh.FlatAppearance.BorderSize = 0;
            this._refresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(55)))), ((int)(((byte)(0)))));
            this._refresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(65)))), ((int)(((byte)(0)))));
            this._refresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._refresh.ForeColor = System.Drawing.Color.White;
            this._refresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._refresh.ImageKey = "recycle.bmp";
            this._refresh.ImageList = this._toolbar16;
            this._refresh.Location = new System.Drawing.Point(211, 26);
            this._refresh.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this._refresh.Name = "_refresh";
            this._refresh.Size = new System.Drawing.Size(112, 21);
            this._refresh.TabIndex = 1;
            this._refresh.Text = "        Refresh All";
            this._refresh.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._refresh.UseVisualStyleBackColor = false;
            this._refresh.Click += new System.EventHandler(this.RefreshClick);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.label5.Location = new System.Drawing.Point(467, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 15);
            this.label5.TabIndex = 18;
            this.label5.Text = "View";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.label2.Location = new System.Drawing.Point(329, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 18);
            this.label2.TabIndex = 10;
            this.label2.Text = "Siren";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.label1.Location = new System.Drawing.Point(0, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(327, 14);
            this.label1.TabIndex = 9;
            this.label1.Text = "Configuration";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            // imageList29
            // 
            this.imageList29.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList29.ImageStream")));
            this.imageList29.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList29.Images.SetKeyName(0, "NewsDeselected.png");
            this.imageList29.Images.SetKeyName(1, "NewsSelected.png");
            this.imageList29.Images.SetKeyName(2, "PersonDeselected.png");
            this.imageList29.Images.SetKeyName(3, "PersonSelected.png");
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
            // _panelAlert
            // 
            this._panelAlert.BackColor = System.Drawing.Color.IndianRed;
            this._panelAlert.Controls.Add(this._details);
            this._panelAlert.Controls.Add(this._labelAlert);
            this._panelAlert.Controls.Add(this._closeAlert);
            this._panelAlert.Dock = System.Windows.Forms.DockStyle.Top;
            this._panelAlert.Location = new System.Drawing.Point(0, 107);
            this._panelAlert.Name = "_panelAlert";
            this._panelAlert.Size = new System.Drawing.Size(1005, 21);
            this._panelAlert.TabIndex = 40;
            this._panelAlert.Visible = false;
            // 
            // _details
            // 
            this._details.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._details.AutoSize = true;
            this._details.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._details.LinkColor = System.Drawing.Color.White;
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
            this._labelAlert.ForeColor = System.Drawing.Color.White;
            this._labelAlert.Location = new System.Drawing.Point(4, 2);
            this._labelAlert.Name = "_labelAlert";
            this._labelAlert.Size = new System.Drawing.Size(325, 17);
            this._labelAlert.TabIndex = 42;
            this._labelAlert.Text = "Did you know there was a new version available?";
            // 
            // _closeAlert
            // 
            this._closeAlert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._closeAlert.BackColor = System.Drawing.Color.IndianRed;
            this._closeAlert.BackgroundImage = global::SirenOfShame.Properties.Resources.CloseButton1;
            this._closeAlert.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this._closeAlert.FlatAppearance.BorderSize = 0;
            this._closeAlert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._closeAlert.Location = new System.Drawing.Point(977, 0);
            this._closeAlert.Name = "_closeAlert";
            this._closeAlert.Size = new System.Drawing.Size(25, 21);
            this._closeAlert.TabIndex = 41;
            this._closeAlert.UseVisualStyleBackColor = false;
            this._closeAlert.Click += new System.EventHandler(this.CloseAlertClick);
            // 
            // _avatarImageList
            // 
            this._avatarImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_avatarImageList.ImageStream")));
            this._avatarImageList.TransparentColor = System.Drawing.Color.Magenta;
            this._avatarImageList.Images.SetKeyName(0, "american_eskimo_puppy.png");
            this._avatarImageList.Images.SetKeyName(1, "angry_dog.png");
            this._avatarImageList.Images.SetKeyName(2, "basset_hound.png");
            this._avatarImageList.Images.SetKeyName(3, "beagle_harrier.png");
            this._avatarImageList.Images.SetKeyName(4, "black_and_white_dog.png");
            this._avatarImageList.Images.SetKeyName(5, "black_lab.png");
            this._avatarImageList.Images.SetKeyName(6, "cat_angry.png");
            this._avatarImageList.Images.SetKeyName(7, "cat_black.png");
            this._avatarImageList.Images.SetKeyName(8, "cat_fat.png");
            this._avatarImageList.Images.SetKeyName(9, "cat_tabby.png");
            this._avatarImageList.Images.SetKeyName(10, "cat_tongue_out.png");
            this._avatarImageList.Images.SetKeyName(11, "cat_white.png");
            this._avatarImageList.Images.SetKeyName(12, "chocolate_lab.png");
            this._avatarImageList.Images.SetKeyName(13, "chow.png");
            this._avatarImageList.Images.SetKeyName(14, "german_shepherd.png");
            this._avatarImageList.Images.SetKeyName(15, "golden_retriever_puppy.png");
            this._avatarImageList.Images.SetKeyName(16, "growling_pup.png");
            this._avatarImageList.Images.SetKeyName(17, "jack_russell_terrier.png");
            this._avatarImageList.Images.SetKeyName(18, "kromfohrlander.png");
            this._avatarImageList.Images.SetKeyName(19, "poodle.png");
            this._avatarImageList.Images.SetKeyName(20, "retriever.png");
            this._avatarImageList.Images.SetKeyName(21, "cloud.png");
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.panel1.Controls.Add(this._showRibbon);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1005, 16);
            this.panel1.TabIndex = 24;
            // 
            // _showRibbon
            // 
            this._showRibbon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._showRibbon.FlatAppearance.BorderSize = 0;
            this._showRibbon.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(55)))), ((int)(((byte)(0)))));
            this._showRibbon.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(65)))), ((int)(((byte)(0)))));
            this._showRibbon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._showRibbon.Image = global::SirenOfShame.Properties.Resources.navigate_up;
            this._showRibbon.Location = new System.Drawing.Point(977, 0);
            this._showRibbon.Name = "_showRibbon";
            this._showRibbon.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this._showRibbon.Size = new System.Drawing.Size(19, 16);
            this._showRibbon.TabIndex = 0;
            this._showRibbon.UseVisualStyleBackColor = true;
            this._showRibbon.Click += new System.EventHandler(this.ShowRibbonClick);
            // 
            // _highlightPanel2
            // 
            this._highlightPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this._highlightPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._highlightPanel2.Location = new System.Drawing.Point(0, 2);
            this._highlightPanel2.Name = "_highlightPanel2";
            this._highlightPanel2.Size = new System.Drawing.Size(1005, 1);
            this._highlightPanel2.TabIndex = 25;
            // 
            // _highlightPanel
            // 
            this._highlightPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this._highlightPanel.Controls.Add(this._highlightPanel3);
            this._highlightPanel.Controls.Add(this._highlightPanel2);
            this._highlightPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._highlightPanel.Location = new System.Drawing.Point(0, 104);
            this._highlightPanel.Name = "_highlightPanel";
            this._highlightPanel.Size = new System.Drawing.Size(1005, 3);
            this._highlightPanel.TabIndex = 44;
            // 
            // _highlightPanel3
            // 
            this._highlightPanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this._highlightPanel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._highlightPanel3.Location = new System.Drawing.Point(0, 1);
            this._highlightPanel3.Name = "_highlightPanel3";
            this._highlightPanel3.Size = new System.Drawing.Size(1005, 1);
            this._highlightPanel3.TabIndex = 26;
            // 
            // buildStatusBindingSource
            // 
            this.buildStatusBindingSource.DataSource = typeof(SirenOfShame.Lib.Watcher.BuildStatus);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // _viewBuilds
            // 
            this._viewBuilds.BackColor = System.Drawing.Color.Transparent;
            this._viewBuilds.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._viewBuilds.Dock = System.Windows.Forms.DockStyle.Fill;
            this._viewBuilds.Location = new System.Drawing.Point(0, 128);
            this._viewBuilds.Margin = new System.Windows.Forms.Padding(0);
            this._viewBuilds.Name = "_viewBuilds";
            this._viewBuilds.Padding = new System.Windows.Forms.Padding(32, 0, 0, 0);
            this._viewBuilds.Size = new System.Drawing.Size(528, 333);
            this._viewBuilds.TabIndex = 42;
            // 
            // viewUser1
            // 
            this.viewUser1.BackColor = System.Drawing.Color.Transparent;
            this.viewUser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewUser1.Location = new System.Drawing.Point(0, 128);
            this.viewUser1.Name = "viewUser1";
            this.viewUser1.Size = new System.Drawing.Size(528, 333);
            this.viewUser1.TabIndex = 41;
            // 
            // _newsFeed1
            // 
            this._newsFeed1.BackColor = System.Drawing.Color.Transparent;
            this._newsFeed1.Dock = System.Windows.Forms.DockStyle.Right;
            this._newsFeed1.Location = new System.Drawing.Point(528, 128);
            this._newsFeed1.Margin = new System.Windows.Forms.Padding(0);
            this._newsFeed1.Name = "_newsFeed1";
            this._newsFeed1.Size = new System.Drawing.Size(258, 333);
            this._newsFeed1.TabIndex = 10;
            // 
            // _userList
            // 
            this._userList.BackColor = System.Drawing.Color.Transparent;
            this._userList.Dock = System.Windows.Forms.DockStyle.Right;
            this._userList.Location = new System.Drawing.Point(796, 128);
            this._userList.Margin = new System.Windows.Forms.Padding(0);
            this._userList.Name = "_userList";
            this._userList.Size = new System.Drawing.Size(219, 333);
            this._userList.TabIndex = 11;
            // 
            // separator4
            // 
            this.separator4.Location = new System.Drawing.Point(326, 0);
            this.separator4.Name = "separator4";
            this.separator4.Size = new System.Drawing.Size(2, 88);
            this.separator4.TabIndex = 27;
            this.separator4.Text = "separator4";
            // 
            // separator3
            // 
            this.separator3.Location = new System.Drawing.Point(468, 0);
            this.separator3.Name = "separator3";
            this.separator3.Size = new System.Drawing.Size(2, 88);
            this.separator3.TabIndex = 26;
            this.separator3.Text = "separator3";
            // 
            // separator2
            // 
            this.separator2.Location = new System.Drawing.Point(659, 0);
            this.separator2.Name = "separator2";
            this.separator2.Size = new System.Drawing.Size(2, 88);
            this.separator2.TabIndex = 25;
            this.separator2.Text = "separator2";
            // 
            // separator1
            // 
            this.separator1.Location = new System.Drawing.Point(803, -2);
            this.separator1.Name = "separator1";
            this.separator1.Size = new System.Drawing.Size(2, 88);
            this.separator1.TabIndex = 24;
            this.separator1.Text = "separator1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1005, 483);
            this.Controls.Add(this._viewBuilds);
            this.Controls.Add(this.viewUser1);
            this.Controls.Add(this._newsFeed1);
            this.Controls.Add(this._userList);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this._panelAlert);
            this.Controls.Add(this._highlightPanel);
            this.Controls.Add(this._ribbonPanel);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.ShowInTaskbar = true;
            this.Text = "Siren of Shame";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormFormClosing);
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.ResizeBegin += new System.EventHandler(this.MainFormResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.MainFormResizeEnd);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.Move += new System.EventHandler(this.MainFormMove);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.minimizedMenu.ResumeLayout(false);
            this._ribbonPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._automaticUpdater)).EndInit();
            this._configurationMenu.ResumeLayout(false);
            this._sirenMenu.ResumeLayout(false);
            this._panelAlert.ResumeLayout(false);
            this._panelAlert.PerformLayout();
            this.panel1.ResumeLayout(false);
            this._highlightPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.buildStatusBindingSource)).EndInit();
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
        //private System.Windows.Forms.ColumnHeader Name;
        private System.Windows.Forms.Panel _ribbonPanel;
        private System.Windows.Forms.Button _configureRules;
        private System.Windows.Forms.Button _refresh;
        private System.Windows.Forms.Button _testSiren;
        private System.Windows.Forms.Button _openSettings;
      private wyDay.Controls.AutomaticUpdater _automaticUpdater;
      private System.Windows.Forms.Button _timeboxEnforcer;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Button _help;
      private System.Windows.Forms.Button _configurationMore;
      private System.Windows.Forms.ContextMenuStrip _configurationMenu;
      private System.Windows.Forms.ToolStripMenuItem _checkForUpdates;
      private System.Windows.Forms.ToolStripMenuItem _viewLog;
      private System.Windows.Forms.Button _configureCiServer;
      private System.Windows.Forms.Button _sirenMore;
      private System.Windows.Forms.ContextMenuStrip _sirenMenu;
      private System.Windows.Forms.ToolStripMenuItem _upgradeFirmwareMenuItem;
      private System.Windows.Forms.Button _configureSounds;
      //private System.Windows.Forms.Panel _panelBuildStats;
      //private System.Windows.Forms.Label _percentFailed;
      private System.Windows.Forms.Label label8;
      //private System.Windows.Forms.Label _failedBuilds;
      private System.Windows.Forms.Label label7;
      //private System.Windows.Forms.Label _buildCount;
      private System.Windows.Forms.Label label4;
      private ZedGraph.ZedGraphControl _buildHistoryZedGraph;
      private System.Windows.Forms.Button _fullscreen;
      private System.Windows.Forms.Label label5;
      private System.Windows.Forms.Button _mute;
      private System.Windows.Forms.Button _closeAlert;
      private System.Windows.Forms.Panel _panelAlert;
      private System.Windows.Forms.Label _labelAlert;
      private System.Windows.Forms.LinkLabel _details;
      private System.Windows.Forms.ToolStripDropDownButton _toolStripSplitErrorButton;
      private System.Windows.Forms.Button _sosOnline;
      private System.Windows.Forms.ImageList imageList29;
      private System.Windows.Forms.ImageList _avatarImageList;
      private System.Windows.Forms.ImageList _toolbar32;
      private System.Windows.Forms.ImageList _toolbar16;
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.Button _showRibbon;
      private System.Windows.Forms.Panel _highlightPanel;
      private System.Windows.Forms.Panel _highlightPanel3;
      private System.Windows.Forms.Panel _highlightPanel2;
      private Separator separator1;
      private Separator separator4;
      private Separator separator3;
      private Separator separator2;
      private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
      private System.Windows.Forms.ToolStripStatusLabel _sosOnlineStatus;
      private System.Windows.Forms.ToolStripDropDownButton _sosOnlineError;
      private System.Windows.Forms.Button _viewAllUsers;
      private System.Windows.Forms.Button _userMappings;
      private System.Windows.Forms.OpenFileDialog openFileDialog1;
      private ViewBuilds _viewBuilds;
      private ViewUser viewUser1;
      private NewsFeed _newsFeed1;
      private Leaders _userList;
	}
}

