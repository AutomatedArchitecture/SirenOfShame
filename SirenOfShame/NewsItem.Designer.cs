namespace SirenOfShame
{
    partial class NewsItem
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
            this._userName = new System.Windows.Forms.Label();
            this._metroPanel = new System.Windows.Forms.Panel();
            this._project = new System.Windows.Forms.Label();
            this._when = new System.Windows.Forms.Label();
            this._title = new System.Windows.Forms.Label();
            this._reputationChange = new SirenOfShame.Pill();
            this.avatar1 = new SirenOfShame.Avatar();
            this._metroPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _userName
            // 
            this._userName.AutoEllipsis = true;
            this._userName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(95)))), ((int)(((byte)(152)))));
            this._userName.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._userName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._userName.ForeColor = System.Drawing.Color.White;
            this._userName.Location = new System.Drawing.Point(60, 5);
            this._userName.Margin = new System.Windows.Forms.Padding(0);
            this._userName.Name = "_userName";
            this._userName.Size = new System.Drawing.Size(109, 20);
            this._userName.TabIndex = 1;
            this._userName.Text = "Bob Smith";
            // 
            // _metroPanel
            // 
            this._metroPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._metroPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(95)))), ((int)(((byte)(152)))));
            this._metroPanel.Controls.Add(this._project);
            this._metroPanel.Controls.Add(this._when);
            this._metroPanel.Controls.Add(this._title);
            this._metroPanel.Controls.Add(this._reputationChange);
            this._metroPanel.Controls.Add(this._userName);
            this._metroPanel.Controls.Add(this.avatar1);
            this._metroPanel.Location = new System.Drawing.Point(3, 3);
            this._metroPanel.Name = "_metroPanel";
            this._metroPanel.Size = new System.Drawing.Size(179, 104);
            this._metroPanel.TabIndex = 6;
            // 
            // _project
            // 
            this._project.AutoSize = true;
            this._project.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._project.ForeColor = System.Drawing.Color.LightGray;
            this._project.Location = new System.Drawing.Point(58, 39);
            this._project.Name = "_project";
            this._project.Size = new System.Drawing.Size(49, 13);
            this._project.TabIndex = 7;
            this._project.Text = "Project 1";
            // 
            // _when
            // 
            this._when.AutoSize = true;
            this._when.BackColor = System.Drawing.Color.Transparent;
            this._when.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._when.ForeColor = System.Drawing.Color.LightGray;
            this._when.Location = new System.Drawing.Point(58, 23);
            this._when.Name = "_when";
            this._when.Size = new System.Drawing.Size(73, 13);
            this._when.TabIndex = 6;
            this._when.Text = "8 minutes ago";
            // 
            // _title
            // 
            this._title.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._title.AutoEllipsis = true;
            this._title.ForeColor = System.Drawing.Color.White;
            this._title.Location = new System.Drawing.Point(3, 56);
            this._title.Name = "_title";
            this._title.Size = new System.Drawing.Size(173, 48);
            this._title.TabIndex = 5;
            this._title.Text = "Fixing Lee\'s bunk check-in from yesterday where he broke the build and then left " +
                "for the day";
            this._title.MouseEnter += new System.EventHandler(this.TitleMouseEnter);
            // 
            // _reputationChange
            // 
            this._reputationChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._reputationChange.BackColor = System.Drawing.Color.DarkRed;
            this._reputationChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._reputationChange.ForeColor = System.Drawing.Color.White;
            this._reputationChange.Location = new System.Drawing.Point(158, 0);
            this._reputationChange.Name = "_reputationChange";
            this._reputationChange.PillColor = System.Drawing.Color.Empty;
            this._reputationChange.Size = new System.Drawing.Size(21, 17);
            this._reputationChange.TabIndex = 2;
            this._reputationChange.Text = "+1";
            this._reputationChange.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // avatar1
            // 
            this.avatar1.BackColor = System.Drawing.Color.Transparent;
            this.avatar1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.avatar1.ImageIndex = -1;
            this.avatar1.Location = new System.Drawing.Point(5, 5);
            this.avatar1.Name = "avatar1";
            this.avatar1.Size = new System.Drawing.Size(48, 48);
            this.avatar1.TabIndex = 4;
            this.avatar1.Click += new System.EventHandler(this.Avatar1Click);
            this.avatar1.MouseEnter += new System.EventHandler(this.Avatar1MouseEnter);
            // 
            // NewsItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this.Controls.Add(this._metroPanel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MinimumSize = new System.Drawing.Size(0, 65);
            this.Name = "NewsItem";
            this.Size = new System.Drawing.Size(185, 110);
            this.Resize += new System.EventHandler(this.NewsItemResize);
            this._metroPanel.ResumeLayout(false);
            this._metroPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Avatar avatar1;
        private System.Windows.Forms.Label _userName;
        private Pill _reputationChange;
        private System.Windows.Forms.Panel _metroPanel;
        private System.Windows.Forms.Label _when;
        private System.Windows.Forms.Label _title;
        private System.Windows.Forms.Label _project;
    }
}
