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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this._status = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._ok = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this._configureSosOnline = new System.Windows.Forms.CheckBox();
            this._findOldAchievements = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(434, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "What\'s New In 1.6?";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 226);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(459, 12);
            this.progressBar1.TabIndex = 1;
            // 
            // _status
            // 
            this._status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._status.AutoSize = true;
            this._status.Location = new System.Drawing.Point(18, 207);
            this._status.Name = "_status";
            this._status.Size = new System.Drawing.Size(37, 13);
            this._status.TabIndex = 2;
            this._status.Text = "Status";
            this._status.Visible = false;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(434, 34);
            this.label2.TabIndex = 3;
            this.label2.Text = "Siren of Shame now has an achievement system!  For instance you\'ll get \'CI Ninja\'" +
                " if you fix someone elses build.";
            // 
            // _ok
            // 
            this._ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._ok.BackColor = System.Drawing.Color.Transparent;
            this._ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._ok.FlatAppearance.BorderColor = System.Drawing.SystemColors.Window;
            this._ok.FlatAppearance.BorderSize = 0;
            this._ok.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._ok.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._ok.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._ok.ForeColor = System.Drawing.SystemColors.ControlText;
            this._ok.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._ok.ImageIndex = 0;
            this._ok.ImageList = this.imageList1;
            this._ok.Location = new System.Drawing.Point(291, 176);
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
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "ball_green.png");
            this.imageList1.Images.SetKeyName(1, "ball_red.png");
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(434, 22);
            this.label3.TabIndex = 18;
            this.label3.Text = "SoS Online";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(434, 22);
            this.label4.TabIndex = 19;
            this.label4.Text = "Achievements";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(14, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(434, 34);
            this.label5.TabIndex = 20;
            this.label5.Text = "Backup your achievements, compete with your friends, bedazzle your enemies ... al" +
                "l for free.";
            // 
            // _configureSosOnline
            // 
            this._configureSosOnline.AutoSize = true;
            this._configureSosOnline.Checked = true;
            this._configureSosOnline.CheckState = System.Windows.Forms.CheckState.Checked;
            this._configureSosOnline.Location = new System.Drawing.Point(319, 109);
            this._configureSosOnline.Name = "_configureSosOnline";
            this._configureSosOnline.Size = new System.Drawing.Size(127, 17);
            this._configureSosOnline.TabIndex = 21;
            this._configureSosOnline.Text = "Configure SoS Online";
            this._configureSosOnline.UseVisualStyleBackColor = true;
            // 
            // _findOldAchievements
            // 
            this._findOldAchievements.AutoSize = true;
            this._findOldAchievements.Checked = true;
            this._findOldAchievements.CheckState = System.Windows.Forms.CheckState.Checked;
            this._findOldAchievements.Location = new System.Drawing.Point(313, 44);
            this._findOldAchievements.Name = "_findOldAchievements";
            this._findOldAchievements.Size = new System.Drawing.Size(135, 17);
            this._findOldAchievements.TabIndex = 22;
            this._findOldAchievements.Text = "Find Old Achievements";
            this._findOldAchievements.UseVisualStyleBackColor = true;
            // 
            // FindOldAchievements
            // 
            this.AcceptButton = this._ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(459, 238);
            this.Controls.Add(this._findOldAchievements);
            this.Controls.Add(this._configureSosOnline);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._ok);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._status);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FindOldAchievements";
            this.Text = "Find Old Achievements";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label _status;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button _ok;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox _configureSosOnline;
        private System.Windows.Forms.CheckBox _findOldAchievements;
    }
}