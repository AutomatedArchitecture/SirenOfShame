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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this._eventDate = new System.Windows.Forms.Label();
            this.bottomLine = new SirenOfShame.Rectangle();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Window;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(3, 0);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox1.Size = new System.Drawing.Size(144, 62);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "Joe Ferner just checked in with a comment of \"Fixing Lee\'s bunk check-in\"";
            // 
            // _eventDate
            // 
            this._eventDate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._eventDate.ForeColor = System.Drawing.Color.DarkGray;
            this._eventDate.Location = new System.Drawing.Point(0, 67);
            this._eventDate.Margin = new System.Windows.Forms.Padding(0);
            this._eventDate.Name = "_eventDate";
            this._eventDate.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this._eventDate.Size = new System.Drawing.Size(150, 15);
            this._eventDate.TabIndex = 2;
            this._eventDate.Text = "59 minutes ago";
            // 
            // bottomLine
            // 
            this.bottomLine.BackColor = System.Drawing.Color.Silver;
            this.bottomLine.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomLine.Enabled = false;
            this.bottomLine.Location = new System.Drawing.Point(0, 82);
            this.bottomLine.Name = "bottomLine";
            this.bottomLine.Size = new System.Drawing.Size(150, 1);
            this.bottomLine.TabIndex = 1;
            // 
            // NewsItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this._eventDate);
            this.Controls.Add(this.bottomLine);
            this.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.Name = "NewsItem";
            this.Size = new System.Drawing.Size(150, 83);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private Rectangle bottomLine;
        private System.Windows.Forms.Label _eventDate;
    }
}
