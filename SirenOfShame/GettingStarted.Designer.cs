namespace SirenOfShame
{
    partial class GettingStarted
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GettingStarted));
            this._configureCiServer = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this._sosOnline = new System.Windows.Forms.Button();
            this._hide = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // _configureCiServer
            // 
            this._configureCiServer.BackColor = System.Drawing.Color.Transparent;
            this._configureCiServer.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._configureCiServer.FlatAppearance.BorderSize = 0;
            this._configureCiServer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(55)))), ((int)(((byte)(0)))));
            this._configureCiServer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(65)))), ((int)(((byte)(0)))));
            this._configureCiServer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._configureCiServer.ForeColor = System.Drawing.Color.White;
            this._configureCiServer.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this._configureCiServer.ImageKey = "server.bmp";
            this._configureCiServer.ImageList = this.imageList1;
            this._configureCiServer.Location = new System.Drawing.Point(144, 15);
            this._configureCiServer.Name = "_configureCiServer";
            this._configureCiServer.Size = new System.Drawing.Size(83, 93);
            this._configureCiServer.TabIndex = 16;
            this._configureCiServer.Text = "Configure";
            this._configureCiServer.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this._configureCiServer.UseVisualStyleBackColor = false;
            this._configureCiServer.Click += new System.EventHandler(this.ConfigureCiServerClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList1.Images.SetKeyName(0, "earth.bmp");
            this.imageList1.Images.SetKeyName(1, "server.bmp");
            this.imageList1.Images.SetKeyName(2, "server_checkbox_checked.bmp");
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(18, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 31);
            this.label1.TabIndex = 17;
            this.label1.Text = "Getting Started:\r\n";
            // 
            // _sosOnline
            // 
            this._sosOnline.BackColor = System.Drawing.Color.Transparent;
            this._sosOnline.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._sosOnline.FlatAppearance.BorderSize = 0;
            this._sosOnline.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(55)))), ((int)(((byte)(0)))));
            this._sosOnline.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(65)))), ((int)(((byte)(0)))));
            this._sosOnline.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._sosOnline.ForeColor = System.Drawing.Color.White;
            this._sosOnline.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this._sosOnline.ImageKey = "earth.bmp";
            this._sosOnline.ImageList = this.imageList1;
            this._sosOnline.Location = new System.Drawing.Point(250, 15);
            this._sosOnline.Name = "_sosOnline";
            this._sosOnline.Size = new System.Drawing.Size(83, 93);
            this._sosOnline.TabIndex = 18;
            this._sosOnline.Text = "Connect";
            this._sosOnline.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this._sosOnline.UseVisualStyleBackColor = false;
            this._sosOnline.Click += new System.EventHandler(this.SosOnlineClick);
            // 
            // _hide
            // 
            this._hide.AutoSize = true;
            this._hide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._hide.ForeColor = System.Drawing.Color.White;
            this._hide.Location = new System.Drawing.Point(22, 135);
            this._hide.Name = "_hide";
            this._hide.Size = new System.Drawing.Size(199, 17);
            this._hide.TabIndex = 19;
            this._hide.Text = "Never show the getting started dialog";
            this._hide.UseVisualStyleBackColor = true;
            this._hide.CheckedChanged += new System.EventHandler(this.HideCheckedChanged);
            // 
            // GettingStarted
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this.Controls.Add(this._hide);
            this.Controls.Add(this._sosOnline);
            this.Controls.Add(this._configureCiServer);
            this.Controls.Add(this.label1);
            this.Name = "GettingStarted";
            this.Size = new System.Drawing.Size(374, 238);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _configureCiServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button _sosOnline;
        private System.Windows.Forms.CheckBox _hide;
    }
}
