namespace SirenOfShame.Configuration
{
    partial class ConfigureSosOnline
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigureSosOnline));
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this._loading = new System.Windows.Forms.PictureBox();
            this._userIAm = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this._sosOnlineStatus = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this._verifyCredentials = new System.Windows.Forms.Button();
            this._sosOnlinePassword = new System.Windows.Forms.TextBox();
            this._createAccount = new System.Windows.Forms.LinkLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this._sosOnlineLogin = new System.Windows.Forms.TextBox();
            this._viewLeaderboards = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._done = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._loading)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
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
            this.groupBox4.Location = new System.Drawing.Point(12, 66);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(262, 172);
            this.groupBox4.TabIndex = 32;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Credentials";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 116);
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
            this._userIAm.Location = new System.Drawing.Point(103, 113);
            this._userIAm.Name = "_userIAm";
            this._userIAm.Size = new System.Drawing.Size(148, 21);
            this._userIAm.TabIndex = 33;
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
            this._sosOnlineStatus.ImageList = this.imageList1;
            this._sosOnlineStatus.Location = new System.Drawing.Point(102, 43);
            this._sosOnlineStatus.Name = "_sosOnlineStatus";
            this._sosOnlineStatus.Size = new System.Drawing.Size(100, 13);
            this._sosOnlineStatus.TabIndex = 30;
            this._sosOnlineStatus.Text = "Have never synced";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "ball_green.png");
            this.imageList1.Images.SetKeyName(1, "ball_red.png");
            this.imageList1.Images.SetKeyName(2, "refresh16.png");
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
            this._verifyCredentials.BackColor = System.Drawing.Color.Transparent;
            this._verifyCredentials.FlatAppearance.BorderColor = System.Drawing.SystemColors.Window;
            this._verifyCredentials.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._verifyCredentials.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._verifyCredentials.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._verifyCredentials.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._verifyCredentials.ImageKey = "refresh16.png";
            this._verifyCredentials.ImageList = this.imageList1;
            this._verifyCredentials.Location = new System.Drawing.Point(103, 143);
            this._verifyCredentials.Name = "_verifyCredentials";
            this._verifyCredentials.Size = new System.Drawing.Size(111, 23);
            this._verifyCredentials.TabIndex = 27;
            this._verifyCredentials.Text = "Verify and Sync";
            this._verifyCredentials.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._verifyCredentials.UseVisualStyleBackColor = false;
            this._verifyCredentials.Click += new System.EventHandler(this.VerifyCredentialsClick);
            // 
            // _sosOnlinePassword
            // 
            this._sosOnlinePassword.Location = new System.Drawing.Point(103, 90);
            this._sosOnlinePassword.Name = "_sosOnlinePassword";
            this._sosOnlinePassword.PasswordChar = '*';
            this._sosOnlinePassword.Size = new System.Drawing.Size(148, 20);
            this._sosOnlinePassword.TabIndex = 26;
            // 
            // _createAccount
            // 
            this._createAccount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._createAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._createAccount.Location = new System.Drawing.Point(95, 16);
            this._createAccount.Name = "_createAccount";
            this._createAccount.Size = new System.Drawing.Size(156, 21);
            this._createAccount.TabIndex = 31;
            this._createAccount.TabStop = true;
            this._createAccount.Text = "Create New Account";
            this._createAccount.TextAlign = System.Drawing.ContentAlignment.TopCenter;
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
            this._sosOnlineLogin.TabIndex = 25;
            // 
            // _viewLeaderboards
            // 
            this._viewLeaderboards.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._viewLeaderboards.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._viewLeaderboards.Location = new System.Drawing.Point(346, 8);
            this._viewLeaderboards.Name = "_viewLeaderboards";
            this._viewLeaderboards.Size = new System.Drawing.Size(199, 23);
            this._viewLeaderboards.TabIndex = 32;
            this._viewLeaderboards.TabStop = true;
            this._viewLeaderboards.Text = "View the Leaderboards";
            this._viewLeaderboards.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this._viewLeaderboards.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ViewLeaderboardsLinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(60, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 24);
            this.label2.TabIndex = 33;
            this.label2.Text = "SoS Online";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(60, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(375, 13);
            this.label3.TabIndex = 34;
            this.label3.Text = "Backup your achievements, compete with your friends, bedazzle your enemies";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Location = new System.Drawing.Point(280, 66);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(265, 172);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Synchronize";
            // 
            // _done
            // 
            this._done.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._done.BackColor = System.Drawing.Color.Transparent;
            this._done.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._done.FlatAppearance.BorderColor = System.Drawing.SystemColors.Window;
            this._done.FlatAppearance.BorderSize = 0;
            this._done.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._done.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._done.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._done.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._done.ForeColor = System.Drawing.SystemColors.ControlText;
            this._done.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._done.ImageIndex = 0;
            this._done.ImageList = this.imageList1;
            this._done.Location = new System.Drawing.Point(472, 245);
            this._done.Name = "_done";
            this._done.Size = new System.Drawing.Size(73, 25);
            this._done.TabIndex = 38;
            this._done.Text = "Done";
            this._done.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._done.UseVisualStyleBackColor = false;
            this._done.Click += new System.EventHandler(this.OkClick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SirenOfShame.Properties.Resources.cloud_title;
            this.pictureBox1.Location = new System.Drawing.Point(12, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(45, 45);
            this.pictureBox1.TabIndex = 39;
            this.pictureBox1.TabStop = false;
            // 
            // ConfigureSosOnline
            // 
            this.AcceptButton = this._done;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this._done;
            this.ClientSize = new System.Drawing.Size(557, 282);
            this.Controls.Add(this._viewLeaderboards);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this._done);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConfigureSosOnline";
            this.Text = "ConfigureSosOnline";
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._loading)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.LinkLabel _viewLeaderboards;
        private System.Windows.Forms.LinkLabel _createAccount;
        private System.Windows.Forms.Label _sosOnlineStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _verifyCredentials;
        private System.Windows.Forms.TextBox _sosOnlinePassword;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox _sosOnlineLogin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox _userIAm;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button _done;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox _loading;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}