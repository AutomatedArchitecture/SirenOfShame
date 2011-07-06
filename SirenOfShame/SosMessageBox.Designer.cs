namespace SirenOfShame
{
    partial class SosMessageBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SosMessageBox));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this._button2 = new System.Windows.Forms.Button();
            this._button1 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this._body = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 93);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(312, 43);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this._button2);
            this.panel3.Controls.Add(this._button1);
            this.panel3.Location = new System.Drawing.Point(12, 6);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(288, 25);
            this.panel3.TabIndex = 2;
            // 
            // _button2
            // 
            this._button2.AutoSize = true;
            this._button2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._button2.Dock = System.Windows.Forms.DockStyle.Right;
            this._button2.Location = new System.Drawing.Point(182, 0);
            this._button2.Name = "_button2";
            this._button2.Size = new System.Drawing.Size(53, 25);
            this._button2.TabIndex = 2;
            this._button2.Text = "button2";
            this._button2.UseVisualStyleBackColor = true;
            this._button2.Click += new System.EventHandler(this.Button2Click);
            // 
            // _button1
            // 
            this._button1.AutoSize = true;
            this._button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._button1.Dock = System.Windows.Forms.DockStyle.Right;
            this._button1.Location = new System.Drawing.Point(235, 0);
            this._button1.Name = "_button1";
            this._button1.Size = new System.Drawing.Size(53, 25);
            this._button1.TabIndex = 1;
            this._button1.Text = "button1";
            this._button1.UseVisualStyleBackColor = true;
            this._button1.Click += new System.EventHandler(this.Button1Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Window;
            this.panel2.Controls.Add(this._body);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(312, 93);
            this.panel2.TabIndex = 1;
            // 
            // _body
            // 
            this._body.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._body.Location = new System.Drawing.Point(12, 9);
            this._body.Name = "_body";
            this._body.Size = new System.Drawing.Size(288, 81);
            this._body.TabIndex = 0;
            this._body.Text = "label1";
            // 
            // SosMessageBox
            // 
            this.AcceptButton = this._button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 136);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SosMessageBox";
            this.Text = "SosMessageBox";
            this.TopMost = true;
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label _body;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button _button2;
        private System.Windows.Forms.Button _button1;
    }
}