using SirenOfShame.Lib;

namespace SirenOfShame.Configuration
{
    partial class SyncOnline
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SyncOnline));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._syncAlways = new System.Windows.Forms.RadioButton();
            this._syncNever = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this._proxyPassword = new System.Windows.Forms.TextBox();
            this._proxyPasswordLabel = new System.Windows.Forms.Label();
            this._proxyUsername = new System.Windows.Forms.TextBox();
            this._proxyUsernameLabel = new System.Windows.Forms.Label();
            this._useProxy = new System.Windows.Forms.CheckBox();
            this._proxyUrl = new System.Windows.Forms.TextBox();
            this._proxyUrlLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this._loading = new System.Windows.Forms.PictureBox();
            this._userIAm = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this._sosOnlineStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._verifyCredentials = new SirenOfShame.Lib.SosButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this._sosOnlinePassword = new System.Windows.Forms.TextBox();
            this._createAccount = new System.Windows.Forms.LinkLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this._sosOnlineLogin = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this._details = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this._syncBuildStatuses = new System.Windows.Forms.RadioButton();
            this._syncMyStuffOnly = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._loading)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this._syncAlways);
            this.groupBox1.Controls.Add(this._syncNever);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(268, 219);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(192, 61);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "When to Sync";
            // 
            // _syncAlways
            // 
            this._syncAlways.AutoSize = true;
            this._syncAlways.Location = new System.Drawing.Point(7, 36);
            this._syncAlways.Name = "_syncAlways";
            this._syncAlways.Size = new System.Drawing.Size(171, 17);
            this._syncAlways.TabIndex = 11;
            this._syncAlways.Text = "Sync whenever I trigger a build";
            this._syncAlways.UseVisualStyleBackColor = true;
            this._syncAlways.CheckedChanged += new System.EventHandler(this.SyncAlwaysCheckedChanged);
            // 
            // _syncNever
            // 
            this._syncNever.AutoSize = true;
            this._syncNever.Checked = true;
            this._syncNever.Location = new System.Drawing.Point(7, 13);
            this._syncNever.Name = "_syncNever";
            this._syncNever.Size = new System.Drawing.Size(103, 17);
            this._syncNever.TabIndex = 10;
            this._syncNever.TabStop = true;
            this._syncNever.Text = "Never auto-sync";
            this._syncNever.UseVisualStyleBackColor = true;
            this._syncNever.CheckedChanged += new System.EventHandler(this.SyncNeverCheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox4.Controls.Add(this._proxyPassword);
            this.groupBox4.Controls.Add(this._proxyPasswordLabel);
            this.groupBox4.Controls.Add(this._proxyUsername);
            this.groupBox4.Controls.Add(this._proxyUsernameLabel);
            this.groupBox4.Controls.Add(this._useProxy);
            this.groupBox4.Controls.Add(this._proxyUrl);
            this.groupBox4.Controls.Add(this._proxyUrlLabel);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this._loading);
            this.groupBox4.Controls.Add(this._userIAm);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this._sosOnlineStatus);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this._verifyCredentials);
            this.groupBox4.Controls.Add(this._sosOnlinePassword);
            this.groupBox4.Controls.Add(this._createAccount);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this._sosOnlineLogin);
            this.groupBox4.ForeColor = System.Drawing.Color.White;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(262, 283);
            this.groupBox4.TabIndex = 36;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Credentials";
            // 
            // _proxyPassword
            // 
            this._proxyPassword.Location = new System.Drawing.Point(103, 253);
            this._proxyPassword.Name = "_proxyPassword";
            this._proxyPassword.PasswordChar = '*';
            this._proxyPassword.Size = new System.Drawing.Size(148, 20);
            this._proxyPassword.TabIndex = 9;
            // 
            // _proxyPasswordLabel
            // 
            this._proxyPasswordLabel.AutoSize = true;
            this._proxyPasswordLabel.Location = new System.Drawing.Point(12, 256);
            this._proxyPasswordLabel.Name = "_proxyPasswordLabel";
            this._proxyPasswordLabel.Size = new System.Drawing.Size(56, 13);
            this._proxyPasswordLabel.TabIndex = 40;
            this._proxyPasswordLabel.Text = "Password:";
            // 
            // _proxyUsername
            // 
            this._proxyUsername.Location = new System.Drawing.Point(103, 227);
            this._proxyUsername.Name = "_proxyUsername";
            this._proxyUsername.Size = new System.Drawing.Size(148, 20);
            this._proxyUsername.TabIndex = 8;
            // 
            // _proxyUsernameLabel
            // 
            this._proxyUsernameLabel.AutoSize = true;
            this._proxyUsernameLabel.Location = new System.Drawing.Point(12, 230);
            this._proxyUsernameLabel.Name = "_proxyUsernameLabel";
            this._proxyUsernameLabel.Size = new System.Drawing.Size(58, 13);
            this._proxyUsernameLabel.TabIndex = 38;
            this._proxyUsernameLabel.Text = "Username:";
            // 
            // _useProxy
            // 
            this._useProxy.AutoSize = true;
            this._useProxy.Location = new System.Drawing.Point(103, 179);
            this._useProxy.Name = "_useProxy";
            this._useProxy.Size = new System.Drawing.Size(74, 17);
            this._useProxy.TabIndex = 6;
            this._useProxy.Text = "Use Proxy";
            this._useProxy.UseVisualStyleBackColor = true;
            this._useProxy.CheckedChanged += new System.EventHandler(this.UseProxyCheckedChanged);
            // 
            // _proxyUrl
            // 
            this._proxyUrl.Location = new System.Drawing.Point(103, 201);
            this._proxyUrl.Name = "_proxyUrl";
            this._proxyUrl.Size = new System.Drawing.Size(148, 20);
            this._proxyUrl.TabIndex = 7;
            // 
            // _proxyUrlLabel
            // 
            this._proxyUrlLabel.AutoSize = true;
            this._proxyUrlLabel.Location = new System.Drawing.Point(12, 204);
            this._proxyUrlLabel.Name = "_proxyUrlLabel";
            this._proxyUrlLabel.Size = new System.Drawing.Size(52, 13);
            this._proxyUrlLabel.TabIndex = 35;
            this._proxyUrlLabel.Text = "Proxy Url:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 119);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 34;
            this.label7.Text = "I Am:";
            // 
            // _loading
            // 
            this._loading.Image = global::SirenOfShame.Properties.Resources.loading;
            this._loading.Location = new System.Drawing.Point(82, 42);
            this._loading.Name = "_loading";
            this._loading.Size = new System.Drawing.Size(16, 16);
            this._loading.TabIndex = 33;
            this._loading.TabStop = false;
            this._loading.Visible = false;
            // 
            // _userIAm
            // 
            this._userIAm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._userIAm.DisplayMember = "DisplayName";
            this._userIAm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._userIAm.ForeColor = System.Drawing.SystemColors.ControlText;
            this._userIAm.FormattingEnabled = true;
            this._userIAm.Location = new System.Drawing.Point(103, 116);
            this._userIAm.Name = "_userIAm";
            this._userIAm.Size = new System.Drawing.Size(148, 21);
            this._userIAm.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 32;
            this.label4.Text = "New?";
            // 
            // _sosOnlineStatus
            // 
            this._sosOnlineStatus.AutoSize = true;
            this._sosOnlineStatus.Location = new System.Drawing.Point(100, 43);
            this._sosOnlineStatus.Name = "_sosOnlineStatus";
            this._sosOnlineStatus.Size = new System.Drawing.Size(100, 13);
            this._sosOnlineStatus.TabIndex = 1;
            this._sosOnlineStatus.Text = "Have never synced";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "Status:";
            // 
            // _verifyCredentials
            // 
            this._verifyCredentials.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this._verifyCredentials.FlatAppearance.BorderColor = System.Drawing.SystemColors.Window;
            this._verifyCredentials.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._verifyCredentials.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._verifyCredentials.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._verifyCredentials.ForeColor = System.Drawing.Color.White;
            this._verifyCredentials.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._verifyCredentials.ImageKey = "refresh.bmp";
            this._verifyCredentials.ImageList = this.imageList1;
            this._verifyCredentials.Location = new System.Drawing.Point(103, 146);
            this._verifyCredentials.Name = "_verifyCredentials";
            this._verifyCredentials.Size = new System.Drawing.Size(111, 23);
            this._verifyCredentials.TabIndex = 5;
            this._verifyCredentials.Text = "Verify and Sync";
            this._verifyCredentials.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._verifyCredentials.UseVisualStyleBackColor = false;
            this._verifyCredentials.Click += new System.EventHandler(this.VerifyCredentialsClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList1.Images.SetKeyName(0, "refresh.bmp");
            // 
            // _sosOnlinePassword
            // 
            this._sosOnlinePassword.Location = new System.Drawing.Point(103, 90);
            this._sosOnlinePassword.Name = "_sosOnlinePassword";
            this._sosOnlinePassword.PasswordChar = '*';
            this._sosOnlinePassword.Size = new System.Drawing.Size(148, 20);
            this._sosOnlinePassword.TabIndex = 3;
            this._sosOnlinePassword.TextChanged += new System.EventHandler(this._sosOnlinePassword_TextChanged);
            // 
            // _createAccount
            // 
            this._createAccount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._createAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._createAccount.LinkColor = System.Drawing.Color.White;
            this._createAccount.Location = new System.Drawing.Point(95, 16);
            this._createAccount.Name = "_createAccount";
            this._createAccount.Size = new System.Drawing.Size(156, 21);
            this._createAccount.TabIndex = 0;
            this._createAccount.TabStop = true;
            this._createAccount.Text = "Create New Account";
            this._createAccount.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this._createAccount.VisitedLinkColor = System.Drawing.Color.White;
            this._createAccount.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.CreateAccountLinkClicked);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Login:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "Password:";
            // 
            // _sosOnlineLogin
            // 
            this._sosOnlineLogin.Location = new System.Drawing.Point(103, 64);
            this._sosOnlineLogin.Name = "_sosOnlineLogin";
            this._sosOnlineLogin.Size = new System.Drawing.Size(148, 20);
            this._sosOnlineLogin.TabIndex = 2;
            this._sosOnlineLogin.TextChanged += new System.EventHandler(this._sosOnlineLogin_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this._details);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this._syncBuildStatuses);
            this.groupBox2.Controls.Add(this._syncMyStuffOnly);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(268, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(192, 207);
            this.groupBox2.TabIndex = 38;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "What To Sync";
            // 
            // _details
            // 
            this._details.AutoSize = true;
            this._details.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this._details.Location = new System.Drawing.Point(25, 131);
            this._details.Name = "_details";
            this._details.Size = new System.Drawing.Size(66, 13);
            this._details.TabIndex = 23;
            this._details.TabStop = true;
            this._details.Text = "More Details";
            this._details.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._details_LinkClicked);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(25, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(161, 61);
            this.label2.TabIndex = 14;
            this.label2.Text = "Enables e-mail notifications and Android/iPhone/iPad/Win8/WinPhone8 clients. Buil" +
    "d info will be kept private.";
            // 
            // _syncBuildStatuses
            // 
            this._syncBuildStatuses.AutoSize = true;
            this._syncBuildStatuses.Checked = true;
            this._syncBuildStatuses.Location = new System.Drawing.Point(7, 49);
            this._syncBuildStatuses.Name = "_syncBuildStatuses";
            this._syncBuildStatuses.Size = new System.Drawing.Size(145, 17);
            this._syncBuildStatuses.TabIndex = 13;
            this._syncBuildStatuses.TabStop = true;
            this._syncBuildStatuses.Text = "Build info (Enables My CI)";
            this._syncBuildStatuses.UseVisualStyleBackColor = true;
            this._syncBuildStatuses.CheckedChanged += new System.EventHandler(this.SyncBuildStatusesCheckedChanged);
            // 
            // _syncMyStuffOnly
            // 
            this._syncMyStuffOnly.AutoSize = true;
            this._syncMyStuffOnly.Location = new System.Drawing.Point(7, 19);
            this._syncMyStuffOnly.Name = "_syncMyStuffOnly";
            this._syncMyStuffOnly.Size = new System.Drawing.Size(182, 17);
            this._syncMyStuffOnly.TabIndex = 12;
            this._syncMyStuffOnly.Text = "My points and achievements only";
            this._syncMyStuffOnly.UseVisualStyleBackColor = true;
            this._syncMyStuffOnly.CheckedChanged += new System.EventHandler(this.SyncMyStuffOnlyCheckedChanged);
            // 
            // SyncOnline
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox4);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "SyncOnline";
            this.Size = new System.Drawing.Size(463, 283);
            this.Load += new System.EventHandler(this.SyncOnlineLoad);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._loading)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton _syncAlways;
        private System.Windows.Forms.RadioButton _syncNever;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox _loading;
        private System.Windows.Forms.ComboBox _userIAm;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label _sosOnlineStatus;
        private System.Windows.Forms.Label label1;
        private SosButton _verifyCredentials;
        private System.Windows.Forms.TextBox _sosOnlinePassword;
        private System.Windows.Forms.LinkLabel _createAccount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox _sosOnlineLogin;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TextBox _proxyUrl;
        private System.Windows.Forms.Label _proxyUrlLabel;
        private System.Windows.Forms.CheckBox _useProxy;
        private System.Windows.Forms.TextBox _proxyPassword;
        private System.Windows.Forms.Label _proxyPasswordLabel;
        private System.Windows.Forms.TextBox _proxyUsername;
        private System.Windows.Forms.Label _proxyUsernameLabel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton _syncBuildStatuses;
        private System.Windows.Forms.RadioButton _syncMyStuffOnly;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel _details;
    }
}
