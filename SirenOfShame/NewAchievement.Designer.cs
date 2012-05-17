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
            this._usersIAm = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
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
            this._user.Location = new System.Drawing.Point(1, 53);
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
            this._accomplishment.Location = new System.Drawing.Point(-1, 84);
            this._accomplishment.Name = "_accomplishment";
            this._accomplishment.Size = new System.Drawing.Size(661, 28);
            this._accomplishment.TabIndex = 4;
            this._accomplishment.Text = "Queued a back to back build 5+ times";
            this._accomplishment.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _usersIAm
            // 
            this._usersIAm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._usersIAm.FormattingEnabled = true;
            this._usersIAm.Location = new System.Drawing.Point(251, 146);
            this._usersIAm.Name = "_usersIAm";
            this._usersIAm.Size = new System.Drawing.Size(121, 21);
            this._usersIAm.TabIndex = 9;
            this._usersIAm.Visible = false;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(210, 147);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 17);
            this.label6.TabIndex = 10;
            this.label6.Text = "I Am";
            this.label6.Visible = false;
            // 
            // radioButton1
            // 
            this.radioButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButton1.AutoSize = true;
            this.radioButton1.BackColor = System.Drawing.Color.Transparent;
            this.radioButton1.ForeColor = System.Drawing.Color.White;
            this.radioButton1.Location = new System.Drawing.Point(21, 147);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(182, 17);
            this.radioButton1.TabIndex = 11;
            this.radioButton1.Text = "Only show my new achievements";
            this.radioButton1.UseVisualStyleBackColor = false;
            // 
            // radioButton2
            // 
            this.radioButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButton2.AutoSize = true;
            this.radioButton2.BackColor = System.Drawing.Color.Transparent;
            this.radioButton2.Checked = true;
            this.radioButton2.ForeColor = System.Drawing.Color.White;
            this.radioButton2.Location = new System.Drawing.Point(21, 132);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(178, 17);
            this.radioButton2.TabIndex = 12;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Always show new achievements";
            this.radioButton2.UseVisualStyleBackColor = false;
            // 
            // radioButton3
            // 
            this.radioButton3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButton3.AutoSize = true;
            this.radioButton3.BackColor = System.Drawing.Color.Transparent;
            this.radioButton3.ForeColor = System.Drawing.Color.White;
            this.radioButton3.Location = new System.Drawing.Point(21, 162);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(151, 17);
            this.radioButton3.TabIndex = 13;
            this.radioButton3.Text = "Never show achievements";
            this.radioButton3.UseVisualStyleBackColor = false;
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
            this._ok.FlatAppearance.BorderSize = 0;
            this._ok.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._ok.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._ok.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._ok.ForeColor = System.Drawing.Color.White;
            this._ok.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._ok.ImageIndex = 1;
            this._ok.ImageList = this.imageList1;
            this._ok.Location = new System.Drawing.Point(561, 152);
            this._ok.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this._ok.Name = "_ok";
            this._ok.Size = new System.Drawing.Size(70, 25);
            this._ok.TabIndex = 15;
            this._ok.Text = "Cool!";
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
            this._shareOnTwitter.Location = new System.Drawing.Point(406, 152);
            this._shareOnTwitter.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this._shareOnTwitter.Name = "_shareOnTwitter";
            this._shareOnTwitter.Size = new System.Drawing.Size(149, 25);
            this._shareOnTwitter.TabIndex = 16;
            this._shareOnTwitter.Text = "Share On Twitter";
            this._shareOnTwitter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._shareOnTwitter.UseVisualStyleBackColor = false;
            this._shareOnTwitter.Click += new System.EventHandler(this.ShareOnTwitterClick);
            // 
            // NewAchievement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = global::SirenOfShame.Properties.Resources.AchievementBackground;
            this.CancelButton = this._ok;
            this.ClientSize = new System.Drawing.Size(660, 195);
            this.Controls.Add(this._shareOnTwitter);
            this.Controls.Add(this._ok);
            this.Controls.Add(this._title);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this._usersIAm);
            this.Controls.Add(this._accomplishment);
            this.Controls.Add(this._user);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "NewAchievement";
            this.Text = "New Achievement";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _user;
        private System.Windows.Forms.Label _accomplishment;
        private System.Windows.Forms.ComboBox _usersIAm;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.Label _title;
        private System.Windows.Forms.Button _ok;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button _shareOnTwitter;
    }
}