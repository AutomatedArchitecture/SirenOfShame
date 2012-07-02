namespace SirenOfShame
{
    partial class NewAchievement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewAchievement));
            this._user = new System.Windows.Forms.Label();
            this._accomplishment = new System.Windows.Forms.Label();
            this._userIAm = new System.Windows.Forms.ComboBox();
            this._iAmLabel = new System.Windows.Forms.Label();
            this._onlyShowMyAchievements = new System.Windows.Forms.RadioButton();
            this._alwaysShowNewAchievements = new System.Windows.Forms.RadioButton();
            this._neverShowAchievements = new System.Windows.Forms.RadioButton();
            this._title = new System.Windows.Forms.Label();
            this._ok = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this._shareOnTwitter = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _user
            // 
            this._user.BackColor = System.Drawing.Color.Transparent;
            this._user.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._user.ForeColor = System.Drawing.Color.White;
            this._user.Location = new System.Drawing.Point(1, 50);
            this._user.Name = "_user";
            this._user.Size = new System.Drawing.Size(659, 34);
            this._user.TabIndex = 2;
            this._user.Text = "Lee Richardson";
            this._user.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _accomplishment
            // 
            this._accomplishment.BackColor = System.Drawing.Color.Transparent;
            this._accomplishment.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._accomplishment.ForeColor = System.Drawing.Color.White;
            this._accomplishment.Location = new System.Drawing.Point(-1, 81);
            this._accomplishment.Name = "_accomplishment";
            this._accomplishment.Size = new System.Drawing.Size(661, 28);
            this._accomplishment.TabIndex = 4;
            this._accomplishment.Text = "Queued a back to back build 5+ times";
            this._accomplishment.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _userIAm
            // 
            this._userIAm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._userIAm.DisplayMember = "DisplayName";
            this._userIAm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._userIAm.FormattingEnabled = true;
            this._userIAm.Location = new System.Drawing.Point(261, 135);
            this._userIAm.Name = "_userIAm";
            this._userIAm.Size = new System.Drawing.Size(121, 21);
            this._userIAm.TabIndex = 9;
            this._userIAm.Visible = false;
            // 
            // _iAmLabel
            // 
            this._iAmLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._iAmLabel.AutoSize = true;
            this._iAmLabel.BackColor = System.Drawing.Color.Transparent;
            this._iAmLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._iAmLabel.ForeColor = System.Drawing.Color.White;
            this._iAmLabel.Location = new System.Drawing.Point(220, 136);
            this._iAmLabel.Name = "_iAmLabel";
            this._iAmLabel.Size = new System.Drawing.Size(35, 17);
            this._iAmLabel.TabIndex = 10;
            this._iAmLabel.Text = "I Am";
            this._iAmLabel.Visible = false;
            // 
            // _onlyShowMyAchievements
            // 
            this._onlyShowMyAchievements.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._onlyShowMyAchievements.AutoSize = true;
            this._onlyShowMyAchievements.BackColor = System.Drawing.Color.Transparent;
            this._onlyShowMyAchievements.ForeColor = System.Drawing.Color.White;
            this._onlyShowMyAchievements.Location = new System.Drawing.Point(31, 136);
            this._onlyShowMyAchievements.Name = "_onlyShowMyAchievements";
            this._onlyShowMyAchievements.Size = new System.Drawing.Size(182, 17);
            this._onlyShowMyAchievements.TabIndex = 11;
            this._onlyShowMyAchievements.Text = "Only show my new achievements";
            this._onlyShowMyAchievements.UseVisualStyleBackColor = false;
            this._onlyShowMyAchievements.CheckedChanged += new System.EventHandler(this.OnlyShowMyAchievementsCheckedChanged);
            // 
            // _alwaysShowNewAchievements
            // 
            this._alwaysShowNewAchievements.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._alwaysShowNewAchievements.AutoSize = true;
            this._alwaysShowNewAchievements.BackColor = System.Drawing.Color.Transparent;
            this._alwaysShowNewAchievements.Checked = true;
            this._alwaysShowNewAchievements.ForeColor = System.Drawing.Color.White;
            this._alwaysShowNewAchievements.Location = new System.Drawing.Point(31, 121);
            this._alwaysShowNewAchievements.Name = "_alwaysShowNewAchievements";
            this._alwaysShowNewAchievements.Size = new System.Drawing.Size(198, 17);
            this._alwaysShowNewAchievements.TabIndex = 12;
            this._alwaysShowNewAchievements.TabStop = true;
            this._alwaysShowNewAchievements.Text = "Show everyone\'s new achievements";
            this._alwaysShowNewAchievements.UseVisualStyleBackColor = false;
            this._alwaysShowNewAchievements.CheckedChanged += new System.EventHandler(this.AlwaysShowNewAchievementsCheckedChanged);
            // 
            // _neverShowAchievements
            // 
            this._neverShowAchievements.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._neverShowAchievements.AutoSize = true;
            this._neverShowAchievements.BackColor = System.Drawing.Color.Transparent;
            this._neverShowAchievements.ForeColor = System.Drawing.Color.White;
            this._neverShowAchievements.Location = new System.Drawing.Point(31, 151);
            this._neverShowAchievements.Name = "_neverShowAchievements";
            this._neverShowAchievements.Size = new System.Drawing.Size(151, 17);
            this._neverShowAchievements.TabIndex = 13;
            this._neverShowAchievements.Text = "Never show achievements";
            this._neverShowAchievements.UseVisualStyleBackColor = false;
            this._neverShowAchievements.CheckedChanged += new System.EventHandler(this.NeverShowAchievementsCheckedChanged);
            // 
            // _title
            // 
            this._title.BackColor = System.Drawing.Color.Transparent;
            this._title.Dock = System.Windows.Forms.DockStyle.Top;
            this._title.Font = new System.Drawing.Font("Moire ExtraBold", 18F);
            this._title.ForeColor = System.Drawing.Color.Gold;
            this._title.Location = new System.Drawing.Point(0, 0);
            this._title.Name = "_title";
            this._title.Size = new System.Drawing.Size(660, 29);
            this._title.TabIndex = 14;
            this._title.Text = "ArribaArribaAndaleAndale!!";
            this._title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _ok
            // 
            this._ok.BackColor = System.Drawing.Color.Transparent;
            this._ok.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._ok.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._ok.FlatAppearance.BorderSize = 0;
            this._ok.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._ok.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._ok.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._ok.ForeColor = System.Drawing.Color.White;
            this._ok.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._ok.ImageIndex = 1;
            this._ok.ImageList = this.imageList1;
            this._ok.Location = new System.Drawing.Point(473, 143);
            this._ok.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this._ok.Name = "_ok";
            this._ok.Size = new System.Drawing.Size(160, 25);
            this._ok.TabIndex = 15;
            this._ok.Text = "Excellent, Carry On!";
            this._ok.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._ok.UseVisualStyleBackColor = false;
            this._ok.Click += new System.EventHandler(this.OkClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "twitter.png");
            this.imageList1.Images.SetKeyName(1, "Smiley.png");
            // 
            // _shareOnTwitter
            // 
            this._shareOnTwitter.BackColor = System.Drawing.Color.Transparent;
            this._shareOnTwitter.FlatAppearance.BorderSize = 0;
            this._shareOnTwitter.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._shareOnTwitter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._shareOnTwitter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._shareOnTwitter.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._shareOnTwitter.ForeColor = System.Drawing.Color.White;
            this._shareOnTwitter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._shareOnTwitter.ImageIndex = 0;
            this._shareOnTwitter.ImageList = this.imageList1;
            this._shareOnTwitter.Location = new System.Drawing.Point(389, 143);
            this._shareOnTwitter.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this._shareOnTwitter.Name = "_shareOnTwitter";
            this._shareOnTwitter.Size = new System.Drawing.Size(81, 25);
            this._shareOnTwitter.TabIndex = 16;
            this._shareOnTwitter.Text = "Share";
            this._shareOnTwitter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._shareOnTwitter.UseVisualStyleBackColor = false;
            this._shareOnTwitter.Click += new System.EventHandler(this.ShareOnTwitterClick);
            // 
            // NewAchievement
            // 
            this.AcceptButton = this._ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = global::SirenOfShame.Properties.Resources.AchievementBackground;
            this.CancelButton = this._ok;
            this.ClientSize = new System.Drawing.Size(660, 195);
            this.Controls.Add(this._shareOnTwitter);
            this.Controls.Add(this._ok);
            this.Controls.Add(this._title);
            this.Controls.Add(this._neverShowAchievements);
            this.Controls.Add(this._alwaysShowNewAchievements);
            this.Controls.Add(this._onlyShowMyAchievements);
            this.Controls.Add(this._iAmLabel);
            this.Controls.Add(this._userIAm);
            this.Controls.Add(this._accomplishment);
            this.Controls.Add(this._user);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewAchievement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Achievement";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _user;
        private System.Windows.Forms.Label _accomplishment;
        private System.Windows.Forms.ComboBox _userIAm;
        private System.Windows.Forms.Label _iAmLabel;
        private System.Windows.Forms.RadioButton _onlyShowMyAchievements;
        private System.Windows.Forms.RadioButton _alwaysShowNewAchievements;
        private System.Windows.Forms.RadioButton _neverShowAchievements;
        private System.Windows.Forms.Label _title;
        private System.Windows.Forms.Button _ok;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button _shareOnTwitter;
    }
}