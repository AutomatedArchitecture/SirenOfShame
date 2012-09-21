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
            this._project = new System.Windows.Forms.Label();
            this._when = new System.Windows.Forms.Label();
            this._title = new System.Windows.Forms.Label();
            this._reputationChange = new System.Windows.Forms.Label();
            this._leftPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.avatar1 = new SirenOfShame.Avatar();
            this._leftPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _userName
            // 
            this._userName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(95)))), ((int)(((byte)(152)))));
            this._userName.Cursor = System.Windows.Forms.Cursors.Hand;
            this._userName.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._userName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._userName.ForeColor = System.Drawing.Color.White;
            this._userName.Location = new System.Drawing.Point(4, 0);
            this._userName.Margin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this._userName.Name = "_userName";
            this._userName.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this._userName.Size = new System.Drawing.Size(73, 15);
            this._userName.TabIndex = 1;
            this._userName.Text = "Bob Smith";
            this._userName.Click += new System.EventHandler(this.UserNameClick);
            // 
            // _project
            // 
            this._project.AutoSize = true;
            this._project.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this._project.ForeColor = System.Drawing.Color.White;
            this._project.Location = new System.Drawing.Point(164, 0);
            this._project.Margin = new System.Windows.Forms.Padding(0);
            this._project.Name = "_project";
            this._project.Size = new System.Drawing.Size(49, 13);
            this._project.TabIndex = 7;
            this._project.Text = "Project 1";
            this._project.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // _when
            // 
            this._when.AutoSize = true;
            this._when.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(95)))), ((int)(((byte)(152)))));
            this._when.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._when.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._when.ForeColor = System.Drawing.Color.Gainsboro;
            this._when.Location = new System.Drawing.Point(91, 0);
            this._when.Margin = new System.Windows.Forms.Padding(0);
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
            this._title.ForeColor = System.Drawing.Color.Black;
            this._title.Location = new System.Drawing.Point(54, 4);
            this._title.Name = "_title";
            this._title.Size = new System.Drawing.Size(153, 40);
            this._title.TabIndex = 5;
            this._title.Text = "Fixing Lee\'s bunk check-in from yesterday where he broke the build and then left " +
                "for the day, the rat";
            this._title.MouseEnter += new System.EventHandler(this.TitleMouseEnter);
            // 
            // _reputationChange
            // 
            this._reputationChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._reputationChange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(95)))), ((int)(((byte)(152)))));
            this._reputationChange.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._reputationChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._reputationChange.ForeColor = System.Drawing.Color.White;
            this._reputationChange.Location = new System.Drawing.Point(77, 0);
            this._reputationChange.Margin = new System.Windows.Forms.Padding(0);
            this._reputationChange.Name = "_reputationChange";
            this._reputationChange.Size = new System.Drawing.Size(14, 13);
            this._reputationChange.TabIndex = 2;
            this._reputationChange.Text = "+1";
            this._reputationChange.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _leftPanel
            // 
            this._leftPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._leftPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(95)))), ((int)(((byte)(152)))));
            this._leftPanel.Controls.Add(this._userName);
            this._leftPanel.Controls.Add(this._reputationChange);
            this._leftPanel.Controls.Add(this._when);
            this._leftPanel.Controls.Add(this._project);
            this._leftPanel.Location = new System.Drawing.Point(0, 47);
            this._leftPanel.Margin = new System.Windows.Forms.Padding(0);
            this._leftPanel.Name = "_leftPanel";
            this._leftPanel.Size = new System.Drawing.Size(300, 14);
            this._leftPanel.TabIndex = 8;
            // 
            // avatar1
            // 
            this.avatar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this.avatar1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.avatar1.ImageIndex = -1;
            this.avatar1.Location = new System.Drawing.Point(0, 0);
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
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._leftPanel);
            this.Controls.Add(this.avatar1);
            this.Controls.Add(this._title);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4, 8, 4, 0);
            this.Name = "NewsItem";
            this.Size = new System.Drawing.Size(230, 61);
            this.Resize += new System.EventHandler(this.NewsItemResize);
            this._leftPanel.ResumeLayout(false);
            this._leftPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Avatar avatar1;
        private System.Windows.Forms.Label _userName;
        private System.Windows.Forms.Label _reputationChange;
        private System.Windows.Forms.Label _when;
        private System.Windows.Forms.Label _title;
        private System.Windows.Forms.Label _project;
        private System.Windows.Forms.FlowLayoutPanel _leftPanel;
    }
}
