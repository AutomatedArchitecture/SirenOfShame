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
            this.panel1 = new System.Windows.Forms.Panel();
            this._userName = new System.Windows.Forms.Label();
            this._metroPanel = new System.Windows.Forms.Panel();
            this._reputationChange = new SirenOfShame.Pill();
            this.avatar1 = new SirenOfShame.Avatar();
            this.richTextBox1 = new SirenOfShame.TransparentRichTextBox();
            this.panel1.SuspendLayout();
            this._metroPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.richTextBox1);
            this.panel1.Controls.Add(this._userName);
            this.panel1.Location = new System.Drawing.Point(59, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(4);
            this.panel1.Size = new System.Drawing.Size(117, 109);
            this.panel1.TabIndex = 5;
            // 
            // _userName
            // 
            this._userName.AutoEllipsis = true;
            this._userName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(95)))), ((int)(((byte)(152)))));
            this._userName.Dock = System.Windows.Forms.DockStyle.Top;
            this._userName.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._userName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._userName.ForeColor = System.Drawing.Color.White;
            this._userName.Location = new System.Drawing.Point(4, 4);
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
            this._metroPanel.Controls.Add(this._reputationChange);
            this._metroPanel.Controls.Add(this.avatar1);
            this._metroPanel.Controls.Add(this.panel1);
            this._metroPanel.Location = new System.Drawing.Point(3, 3);
            this._metroPanel.Name = "_metroPanel";
            this._metroPanel.Size = new System.Drawing.Size(179, 109);
            this._metroPanel.TabIndex = 6;
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
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Window;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.ForeColor = System.Drawing.Color.White;
            this.richTextBox1.Location = new System.Drawing.Point(4, 24);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox1.Size = new System.Drawing.Size(109, 81);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "Joe Ferner just checked in with a comment of \"Fixing Lee\'s bunk check-in\"";
            this.richTextBox1.MouseEnter += new System.EventHandler(this.RichTextBox1MouseEnter);
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
            this.Size = new System.Drawing.Size(185, 115);
            this.Resize += new System.EventHandler(this.NewsItemResize);
            this.panel1.ResumeLayout(false);
            this._metroPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Avatar avatar1;
        private System.Windows.Forms.Panel panel1;
        private TransparentRichTextBox richTextBox1;
        private System.Windows.Forms.Label _userName;
        private Pill _reputationChange;
        private System.Windows.Forms.Panel _metroPanel;
    }
}
