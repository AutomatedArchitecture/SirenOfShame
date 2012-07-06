namespace SirenOfShame.Configuration
{
    partial class SyncOffline
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SyncOffline));
            this._result = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this._exportedAchievements = new System.Windows.Forms.TextBox();
            this._exportedBuilds = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this._saveResults = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._copyBuildsToClipboard = new System.Windows.Forms.LinkLabel();
            this._copyAchievementsToClipboard = new System.Windows.Forms.LinkLabel();
            this.label7 = new System.Windows.Forms.Label();
            this._userIAm = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _result
            // 
            this._result.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._result.Location = new System.Drawing.Point(154, 234);
            this._result.Name = "_result";
            this._result.Size = new System.Drawing.Size(240, 20);
            this._result.TabIndex = 43;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(9, 234);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(139, 31);
            this.label11.TabIndex = 42;
            this.label11.Text = "4. Enter \"New High Water Mark\" from results:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 155);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(127, 13);
            this.label10.TabIndex = 41;
            this.label10.Text = "3. In achievements enter:";
            // 
            // _exportedAchievements
            // 
            this._exportedAchievements.AcceptsReturn = true;
            this._exportedAchievements.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._exportedAchievements.Enabled = false;
            this._exportedAchievements.Location = new System.Drawing.Point(154, 155);
            this._exportedAchievements.Multiline = true;
            this._exportedAchievements.Name = "_exportedAchievements";
            this._exportedAchievements.ReadOnly = true;
            this._exportedAchievements.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._exportedAchievements.Size = new System.Drawing.Size(240, 41);
            this._exportedAchievements.TabIndex = 40;
            // 
            // _exportedBuilds
            // 
            this._exportedBuilds.AcceptsReturn = true;
            this._exportedBuilds.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._exportedBuilds.Enabled = false;
            this._exportedBuilds.Location = new System.Drawing.Point(154, 90);
            this._exportedBuilds.Multiline = true;
            this._exportedBuilds.Name = "_exportedBuilds";
            this._exportedBuilds.ReadOnly = true;
            this._exportedBuilds.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._exportedBuilds.Size = new System.Drawing.Size(240, 41);
            this._exportedBuilds.TabIndex = 39;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 90);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 13);
            this.label9.TabIndex = 38;
            this.label9.Text = "2. In builds enter:";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.Location = new System.Drawing.Point(7, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(387, 27);
            this.label8.TabIndex = 36;
            this.label8.Text = "1. On a connected machine navigate to http://sirenofshame.com. Log in. Navigate t" +
    "o http://sirenofshame.com/ApiV1/TestSynchronize";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "ball_green.png");
            this.imageList1.Images.SetKeyName(1, "ball_red.png");
            this.imageList1.Images.SetKeyName(2, "refresh16.png");
            // 
            // _saveResults
            // 
            this._saveResults.BackColor = System.Drawing.Color.Transparent;
            this._saveResults.FlatAppearance.BorderColor = System.Drawing.SystemColors.Window;
            this._saveResults.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._saveResults.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._saveResults.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._saveResults.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._saveResults.ImageKey = "refresh16.png";
            this._saveResults.ImageList = this.imageList1;
            this._saveResults.Location = new System.Drawing.Point(150, 266);
            this._saveResults.Name = "_saveResults";
            this._saveResults.Size = new System.Drawing.Size(101, 23);
            this._saveResults.TabIndex = 44;
            this._saveResults.Text = "Save Results";
            this._saveResults.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._saveResults.UseVisualStyleBackColor = false;
            this._saveResults.Click += new System.EventHandler(this.SaveResultsClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 271);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 45;
            this.label1.Text = "5. Save";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 211);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 46;
            this.label2.Text = "3. Click Submit";
            // 
            // _copyBuildsToClipboard
            // 
            this._copyBuildsToClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._copyBuildsToClipboard.Location = new System.Drawing.Point(281, 134);
            this._copyBuildsToClipboard.Name = "_copyBuildsToClipboard";
            this._copyBuildsToClipboard.Size = new System.Drawing.Size(113, 18);
            this._copyBuildsToClipboard.TabIndex = 47;
            this._copyBuildsToClipboard.TabStop = true;
            this._copyBuildsToClipboard.Text = "Copy to Clipboard";
            this._copyBuildsToClipboard.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this._copyBuildsToClipboard.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.CopyBuildsToClipboardLinkClicked);
            // 
            // _copyAchievementsToClipboard
            // 
            this._copyAchievementsToClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._copyAchievementsToClipboard.Location = new System.Drawing.Point(281, 198);
            this._copyAchievementsToClipboard.Name = "_copyAchievementsToClipboard";
            this._copyAchievementsToClipboard.Size = new System.Drawing.Size(113, 18);
            this._copyAchievementsToClipboard.TabIndex = 48;
            this._copyAchievementsToClipboard.TabStop = true;
            this._copyAchievementsToClipboard.Text = "Copy to Clipboard";
            this._copyAchievementsToClipboard.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this._copyAchievementsToClipboard.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.CopyAchievementsToClipboardLinkClicked);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 50;
            this.label7.Text = "I Am:";
            // 
            // _userIAm
            // 
            this._userIAm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._userIAm.DisplayMember = "DisplayName";
            this._userIAm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._userIAm.ForeColor = System.Drawing.SystemColors.ControlText;
            this._userIAm.FormattingEnabled = true;
            this._userIAm.Location = new System.Drawing.Point(150, 6);
            this._userIAm.Name = "_userIAm";
            this._userIAm.Size = new System.Drawing.Size(244, 21);
            this._userIAm.TabIndex = 49;
            this._userIAm.SelectedIndexChanged += new System.EventHandler(this.UserIAmSelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 13);
            this.label3.TabIndex = 51;
            this.label3.Text = "Manual Sync Instructions";
            // 
            // SyncOffline
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this._userIAm);
            this.Controls.Add(this._copyAchievementsToClipboard);
            this.Controls.Add(this._copyBuildsToClipboard);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._saveResults);
            this.Controls.Add(this._result);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this._exportedAchievements);
            this.Controls.Add(this._exportedBuilds);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Name = "SyncOffline";
            this.Size = new System.Drawing.Size(397, 324);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _saveResults;
        private System.Windows.Forms.TextBox _result;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox _exportedAchievements;
        private System.Windows.Forms.TextBox _exportedBuilds;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel _copyBuildsToClipboard;
        private System.Windows.Forms.LinkLabel _copyAchievementsToClipboard;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox _userIAm;
        private System.Windows.Forms.Label label3;
    }
}
