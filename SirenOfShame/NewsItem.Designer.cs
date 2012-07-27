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
            this.panel1 = new System.Windows.Forms.Panel();
            this.avatar1 = new SirenOfShame.Avatar();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Window;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(53, 0);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox1.Size = new System.Drawing.Size(94, 82);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "Joe Ferner just checked in with a comment of \"Fixing Lee\'s bunk check-in\"";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackgroundImage = global::SirenOfShame.Properties.Resources.gradient15;
            this.panel1.Location = new System.Drawing.Point(0, 76);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(150, 14);
            this.panel1.TabIndex = 3;
            // 
            // avatar1
            // 
            this.avatar1.BackColor = System.Drawing.Color.Transparent;
            this.avatar1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.avatar1.Location = new System.Drawing.Point(0, 0);
            this.avatar1.Name = "avatar1";
            this.avatar1.Size = new System.Drawing.Size(50, 50);
            this.avatar1.TabIndex = 4;
            this.avatar1.Click += new System.EventHandler(this.Avatar1Click);
            // 
            // NewsItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.avatar1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.richTextBox1);
            this.Margin = new System.Windows.Forms.Padding(0, 6, 0, 10);
            this.MinimumSize = new System.Drawing.Size(0, 65);
            this.Name = "NewsItem";
            this.Size = new System.Drawing.Size(150, 103);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Panel panel1;
        private Avatar avatar1;
    }
}
