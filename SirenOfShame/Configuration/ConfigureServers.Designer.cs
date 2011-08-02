namespace SirenOfShame.Configuration
{
    partial class ConfigureServers
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
            this._add = new System.Windows.Forms.Button();
            this._servers = new System.Windows.Forms.ListBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this._close = new System.Windows.Forms.Button();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker3 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker4 = new System.ComponentModel.BackgroundWorker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.backgroundWorker5 = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _add
            // 
            this._add.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._add.Location = new System.Drawing.Point(0, 1);
            this._add.Name = "_add";
            this._add.Size = new System.Drawing.Size(126, 23);
            this._add.TabIndex = 0;
            this._add.Text = "Add";
            this._add.UseVisualStyleBackColor = true;
            this._add.Click += new System.EventHandler(this.AddClick);
            // 
            // _servers
            // 
            this._servers.Dock = System.Windows.Forms.DockStyle.Fill;
            this._servers.FormattingEnabled = true;
            this._servers.Location = new System.Drawing.Point(0, 0);
            this._servers.Name = "_servers";
            this._servers.Size = new System.Drawing.Size(243, 130);
            this._servers.TabIndex = 1;
            this._servers.DoubleClick += new System.EventHandler(this.ServersDoubleClick);
            // 
            // _close
            // 
            this._close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._close.Location = new System.Drawing.Point(126, 1);
            this._close.Name = "_close";
            this._close.Size = new System.Drawing.Size(116, 23);
            this._close.TabIndex = 2;
            this._close.Text = "Close";
            this._close.UseVisualStyleBackColor = true;
            this._close.Click += new System.EventHandler(this.CloseClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._close);
            this.panel1.Controls.Add(this._add);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 130);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(243, 24);
            this.panel1.TabIndex = 3;
            // 
            // ConfigureServers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(243, 154);
            this.Controls.Add(this._servers);
            this.Controls.Add(this.panel1);
            this.Name = "ConfigureServers";
            this.Text = "ConfigureServers";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _add;
        private System.Windows.Forms.ListBox _servers;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button _close;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.ComponentModel.BackgroundWorker backgroundWorker3;
        private System.ComponentModel.BackgroundWorker backgroundWorker4;
        private System.Windows.Forms.Panel panel1;
        private System.ComponentModel.BackgroundWorker backgroundWorker5;
    }
}