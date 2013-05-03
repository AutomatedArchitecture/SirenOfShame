using SirenOfShame.Lib;

namespace SirenOfShame.Configuration
{
    partial class FindOldAchievements
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindOldAchievements));
            this.label1 = new System.Windows.Forms.Label();
            this._ok = new SirenOfShame.Lib.SosButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this._configureSosOnline = new System.Windows.Forms.CheckBox();
            this._details = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(434, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Major New Feature in 2.1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // _ok
            // 
            this._ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._ok.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this._ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._ok.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this._ok.FlatAppearance.BorderSize = 0;
            this._ok.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(55)))), ((int)(((byte)(0)))));
            this._ok.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(65)))), ((int)(((byte)(0)))));
            this._ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._ok.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._ok.ForeColor = System.Drawing.Color.White;
            this._ok.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._ok.ImageIndex = 0;
            this._ok.ImageList = this.imageList1;
            this._ok.Location = new System.Drawing.Point(291, 214);
            this._ok.Name = "_ok";
            this._ok.Size = new System.Drawing.Size(156, 25);
            this._ok.TabIndex = 16;
            this._ok.Text = "Awesome, Lets Go!";
            this._ok.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._ok.UseVisualStyleBackColor = false;
            this._ok.Click += new System.EventHandler(this.OkClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList1.Images.SetKeyName(0, "check.bmp");
            this.imageList1.Images.SetKeyName(1, "ok.bmp");
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(14, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(434, 22);
            this.label3.TabIndex = 18;
            this.label3.Text = "My CI";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(15, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(434, 138);
            this.label5.TabIndex = 20;
            this.label5.Text = resources.GetString("label5.Text");
            // 
            // _configureSosOnline
            // 
            this._configureSosOnline.AutoSize = true;
            this._configureSosOnline.Checked = true;
            this._configureSosOnline.CheckState = System.Windows.Forms.CheckState.Checked;
            this._configureSosOnline.ForeColor = System.Drawing.Color.White;
            this._configureSosOnline.Location = new System.Drawing.Point(330, 52);
            this._configureSosOnline.Name = "_configureSosOnline";
            this._configureSosOnline.Size = new System.Drawing.Size(101, 17);
            this._configureSosOnline.TabIndex = 21;
            this._configureSosOnline.Text = "Configure My CI";
            this._configureSosOnline.UseVisualStyleBackColor = true;
            // 
            // _details
            // 
            this._details.AutoSize = true;
            this._details.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this._details.Location = new System.Drawing.Point(18, 214);
            this._details.Name = "_details";
            this._details.Size = new System.Drawing.Size(66, 13);
            this._details.TabIndex = 22;
            this._details.TabStop = true;
            this._details.Text = "More Details";
            this._details.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._details_LinkClicked);
            // 
            // FindOldAchievements
            // 
            this.AcceptButton = this._ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this.ClientSize = new System.Drawing.Size(459, 251);
            this.Controls.Add(this._details);
            this.Controls.Add(this._configureSosOnline);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._ok);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FindOldAchievements";
            this.Text = "What\'s New";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private SosButton _ok;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox _configureSosOnline;
        private System.Windows.Forms.LinkLabel _details;
    }
}