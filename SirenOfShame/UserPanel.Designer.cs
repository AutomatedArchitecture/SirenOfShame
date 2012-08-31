namespace SirenOfShame
{
    partial class UserPanel
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
            this._displayName = new System.Windows.Forms.Label();
            this._reputation = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this._achievements = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.avatar1 = new SirenOfShame.Avatar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _displayName
            // 
            this._displayName.AutoSize = true;
            this._displayName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._displayName.ForeColor = System.Drawing.Color.White;
            this._displayName.Location = new System.Drawing.Point(2, 46);
            this._displayName.Name = "_displayName";
            this._displayName.Size = new System.Drawing.Size(87, 15);
            this._displayName.TabIndex = 1;
            this._displayName.Text = "Bob Shimpty";
            this._displayName.Click += new System.EventHandler(this.DisplayNameClick);
            // 
            // _reputation
            // 
            this._reputation.AutoSize = true;
            this._reputation.BackColor = System.Drawing.Color.Transparent;
            this._reputation.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._reputation.ForeColor = System.Drawing.Color.Black;
            this._reputation.Location = new System.Drawing.Point(54, 2);
            this._reputation.Margin = new System.Windows.Forms.Padding(0);
            this._reputation.Name = "_reputation";
            this._reputation.Size = new System.Drawing.Size(40, 24);
            this._reputation.TabIndex = 0;
            this._reputation.Text = "255";
            this._reputation.Click += new System.EventHandler(this.ReputationClick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SirenOfShame.Properties.Resources.AchievementBall;
            this.pictureBox1.Location = new System.Drawing.Point(58, 29);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(10, 10);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.PictureBox1Click);
            // 
            // _achievements
            // 
            this._achievements.AutoSize = true;
            this._achievements.ForeColor = System.Drawing.Color.Black;
            this._achievements.Location = new System.Drawing.Point(71, 27);
            this._achievements.Name = "_achievements";
            this._achievements.Size = new System.Drawing.Size(13, 13);
            this._achievements.TabIndex = 2;
            this._achievements.Text = "2";
            this._achievements.Click += new System.EventHandler(this.AchievementsClick);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this._reputation);
            this.panel1.Controls.Add(this._achievements);
            this.panel1.Controls.Add(this.avatar1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(248, 48);
            this.panel1.TabIndex = 3;
            this.panel1.Click += new System.EventHandler(this.Panel1Click);
            this.panel1.MouseEnter += new System.EventHandler(this.panel1_MouseEnter);
            // 
            // avatar1
            // 
            this.avatar1.BackColor = System.Drawing.Color.Transparent;
            this.avatar1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.avatar1.ImageIndex = -1;
            this.avatar1.Location = new System.Drawing.Point(0, 0);
            this.avatar1.Name = "avatar1";
            this.avatar1.Size = new System.Drawing.Size(48, 48);
            this.avatar1.TabIndex = 0;
            this.avatar1.Click += new System.EventHandler(this.Avatar1Click);
            this.avatar1.MouseEnter += new System.EventHandler(this.Avatar1MouseEnter);
            // 
            // UserPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(100)))), ((int)(((byte)(103)))));
            this.Controls.Add(this.panel1);
            this.Controls.Add(this._displayName);
            this.Margin = new System.Windows.Forms.Padding(4, 8, 4, 0);
            this.Name = "UserPanel";
            this.Size = new System.Drawing.Size(248, 62);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Avatar avatar1;
        private System.Windows.Forms.Label _displayName;
        private System.Windows.Forms.Label _reputation;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label _achievements;
        private System.Windows.Forms.Panel panel1;
    }
}
