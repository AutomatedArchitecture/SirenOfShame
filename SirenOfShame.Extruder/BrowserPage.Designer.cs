namespace SirenOfShame.Extruder
{
    partial class BrowserPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BrowserPage));
            this._webBrowser = new System.Windows.Forms.WebBrowser();
            this._refresh = new SirenOfShame.Lib.SosButton();
            this.SuspendLayout();
            // 
            // _webBrowser
            // 
            this._webBrowser.AllowWebBrowserDrop = false;
            this._webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this._webBrowser.IsWebBrowserContextMenuEnabled = false;
            this._webBrowser.Location = new System.Drawing.Point(0, 0);
            this._webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this._webBrowser.Name = "_webBrowser";
            this._webBrowser.ScrollBarsEnabled = false;
            this._webBrowser.Size = new System.Drawing.Size(445, 284);
            this._webBrowser.TabIndex = 0;
            this._webBrowser.Url = new System.Uri("", System.UriKind.Relative);
            this._webBrowser.WebBrowserShortcutsEnabled = false;
            // 
            // _refresh
            // 
            this._refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._refresh.BackColor = System.Drawing.Color.Transparent;
            this._refresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this._refresh.FlatAppearance.BorderSize = 0;
            this._refresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(55)))), ((int)(((byte)(0)))));
            this._refresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(65)))), ((int)(((byte)(0)))));
            this._refresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._refresh.ForeColor = System.Drawing.Color.White;
            this._refresh.Image = ((System.Drawing.Image)(resources.GetObject("_refresh.Image")));
            this._refresh.Location = new System.Drawing.Point(421, 260);
            this._refresh.Name = "_refresh";
            this._refresh.Size = new System.Drawing.Size(24, 24);
            this._refresh.TabIndex = 1;
            this._refresh.UseVisualStyleBackColor = false;
            this._refresh.Click += new System.EventHandler(this._refresh_Click);
            // 
            // LeadersPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.Controls.Add(this._refresh);
            this.Controls.Add(this._webBrowser);
            this.Name = "LeadersPage";
            this.Size = new System.Drawing.Size(445, 284);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser _webBrowser;
        private Lib.SosButton _refresh;

    }
}
