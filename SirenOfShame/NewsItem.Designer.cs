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
            this.rectangle1 = new SirenOfShame.Rectangle();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Window;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(150, 82);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "Joe Ferner just checked in with a comment of \"Fixing Lee\'s bunk check-in\"";
            // 
            // rectangle1
            // 
            this.rectangle1.BackColor = System.Drawing.Color.Silver;
            this.rectangle1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rectangle1.Location = new System.Drawing.Point(0, 82);
            this.rectangle1.Name = "rectangle1";
            this.rectangle1.Size = new System.Drawing.Size(150, 1);
            this.rectangle1.TabIndex = 1;
            // 
            // NewsItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.rectangle1);
            this.Name = "NewsItem";
            this.Size = new System.Drawing.Size(150, 83);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private Rectangle rectangle1;
    }
}
