﻿namespace SirenOfShame
{
    partial class NewsFeed
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
            this.button1 = new System.Windows.Forms.Button();
            this.newsItem1 = new SirenOfShame.NewsItem();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button1.Location = new System.Drawing.Point(0, 214);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(254, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1Click);
            // 
            // newsItem1
            // 
            this.newsItem1.BackColor = System.Drawing.SystemColors.Window;
            this.newsItem1.Dock = System.Windows.Forms.DockStyle.Top;
            this.newsItem1.Location = new System.Drawing.Point(0, 0);
            this.newsItem1.Name = "newsItem1";
            this.newsItem1.Size = new System.Drawing.Size(254, 58);
            this.newsItem1.TabIndex = 5;
            // 
            // NewsFeed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.newsItem1);
            this.Controls.Add(this.button1);
            this.Name = "NewsFeed";
            this.Size = new System.Drawing.Size(254, 237);
            this.Resize += new System.EventHandler(this.NewsFeedResize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private NewsItem newsItem1;

    }
}
