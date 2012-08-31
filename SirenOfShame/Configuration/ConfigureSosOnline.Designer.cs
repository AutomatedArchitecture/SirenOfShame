using SirenOfShame.Lib;

namespace SirenOfShame.Configuration
{
    partial class ConfigureSosOnline
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigureSosOnline));
            this._viewLeaderboards = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this._done = new SosButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this._alwaysOffline = new System.Windows.Forms.CheckBox();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _viewLeaderboards
            // 
            this._viewLeaderboards.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._viewLeaderboards.AutoSize = true;
            this._viewLeaderboards.DisabledLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this._viewLeaderboards.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._viewLeaderboards.LinkColor = System.Drawing.Color.White;
            this._viewLeaderboards.Location = new System.Drawing.Point(328, 7);
            this._viewLeaderboards.Name = "_viewLeaderboards";
            this._viewLeaderboards.Size = new System.Drawing.Size(154, 17);
            this._viewLeaderboards.TabIndex = 32;
            this._viewLeaderboards.TabStop = true;
            this._viewLeaderboards.Text = "View the Leaderboards";
            this._viewLeaderboards.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this._viewLeaderboards.VisitedLinkColor = System.Drawing.Color.White;
            this._viewLeaderboards.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ViewLeaderboardsLinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(82, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 26);
            this.label2.TabIndex = 33;
            this.label2.Text = "SoS Online";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(86, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(375, 13);
            this.label3.TabIndex = 34;
            this.label3.Text = "Backup your achievements, compete with your friends, bedazzle your enemies";
            // 
            // _done
            // 
            this._done.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._done.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this._done.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._done.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this._done.FlatAppearance.BorderSize = 0;
            this._done.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._done.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._done.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._done.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._done.ForeColor = System.Drawing.Color.White;
            this._done.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._done.ImageIndex = 0;
            this._done.ImageList = this.imageList1;
            this._done.Location = new System.Drawing.Point(409, 388);
            this._done.Name = "_done";
            this._done.Size = new System.Drawing.Size(72, 25);
            this._done.TabIndex = 38;
            this._done.Text = "Done";
            this._done.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._done.UseVisualStyleBackColor = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList1.Images.SetKeyName(0, "check.bmp");
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(13, 81);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(468, 301);
            this.panel1.TabIndex = 40;
            // 
            // _alwaysOffline
            // 
            this._alwaysOffline.AutoSize = true;
            this._alwaysOffline.Location = new System.Drawing.Point(367, 28);
            this._alwaysOffline.Name = "_alwaysOffline";
            this._alwaysOffline.Size = new System.Drawing.Size(114, 17);
            this._alwaysOffline.TabIndex = 41;
            this._alwaysOffline.Text = "I\'m always offline :(";
            this._alwaysOffline.UseVisualStyleBackColor = true;
            this._alwaysOffline.CheckedChanged += new System.EventHandler(this.AlwaysOfflineCheckedChanged);
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList2.Images.SetKeyName(0, "earth.bmp");
            // 
            // label1
            // 
            this.label1.ImageKey = "earth.bmp";
            this.label1.ImageList = this.imageList2;
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 64);
            this.label1.TabIndex = 42;
            // 
            // ConfigureSosOnline
            // 
            this.AcceptButton = this._done;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this.CancelButton = this._done;
            this.ClientSize = new System.Drawing.Size(493, 425);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._alwaysOffline);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this._viewLeaderboards);
            this.Controls.Add(this._done);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConfigureSosOnline";
            this.Text = "ConfigureSosOnline";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel _viewLeaderboards;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private SosButton _done;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox _alwaysOffline;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.Label label1;
    }
}