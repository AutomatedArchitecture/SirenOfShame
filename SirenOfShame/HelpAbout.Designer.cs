namespace SirenOfShame
{
    partial class HelpAbout
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
            this._ok = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this._url = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this._version = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _ok
            // 
            this._ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._ok.Location = new System.Drawing.Point(204, 120);
            this._ok.Name = "_ok";
            this._ok.Size = new System.Drawing.Size(75, 23);
            this._ok.TabIndex = 0;
            this._ok.Text = "Ok";
            this._ok.UseVisualStyleBackColor = true;
            this._ok.Click += new System.EventHandler(this.OkClick);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(13, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(266, 48);
            this.label1.TabIndex = 1;
            this.label1.Text = "A devious solution for keeping your team from breaking the build ... while simult" +
                "aneously giving the illusion of making the project more fun.";
            // 
            // _url
            // 
            this._url.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._url.AutoSize = true;
            this._url.Location = new System.Drawing.Point(13, 125);
            this._url.Name = "_url";
            this._url.Size = new System.Drawing.Size(150, 13);
            this._url.TabIndex = 2;
            this._url.TabStop = true;
            this._url.Text = "http://www.sirenofshame.com";
            this._url.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.UrlLinkClicked);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(266, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Siren of Shame";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // _version
            // 
            this._version.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._version.Location = new System.Drawing.Point(12, 109);
            this._version.Name = "_version";
            this._version.Size = new System.Drawing.Size(266, 15);
            this._version.TabIndex = 4;
            this._version.Text = "Version X.X";
            // 
            // HelpAbout
            // 
            this.AcceptButton = this._ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 155);
            this.Controls.Add(this._ok);
            this.Controls.Add(this._version);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._url);
            this.Controls.Add(this.label1);
            this.Name = "HelpAbout";
            this.Text = "About";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _ok;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel _url;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label _version;
    }
}