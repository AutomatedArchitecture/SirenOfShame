namespace SirenOfShame
{
    partial class UserPanel
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
            this._displayName = new System.Windows.Forms.Label();
            this._reputation = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this._achievements = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this._failPercent = new System.Windows.Forms.Label();
            this._totalBuilds = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._fixedSomeoneElsesBuild = new System.Windows.Forms.Label();
            this._successfulBuildsInARow = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this._rank = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.avatar1 = new SirenOfShame.Avatar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _displayName
            // 
            this._displayName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._displayName.AutoSize = true;
            this._displayName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._displayName.ForeColor = System.Drawing.Color.White;
            this._displayName.Location = new System.Drawing.Point(35, 0);
            this._displayName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._displayName.Name = "_displayName";
            this._displayName.Size = new System.Drawing.Size(103, 18);
            this._displayName.TabIndex = 1;
            this._displayName.Text = "Bob Shimpty";
            this._displayName.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // _reputation
            // 
            this._reputation.AutoSize = true;
            this._reputation.BackColor = System.Drawing.Color.Transparent;
            this._reputation.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._reputation.ForeColor = System.Drawing.Color.Black;
            this._reputation.Location = new System.Drawing.Point(72, 2);
            this._reputation.Margin = new System.Windows.Forms.Padding(0);
            this._reputation.Name = "_reputation";
            this._reputation.Size = new System.Drawing.Size(52, 29);
            this._reputation.TabIndex = 0;
            this._reputation.Text = "255";
            this.toolTip1.SetToolTip(this._reputation, "Reputation");
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SirenOfShame.Properties.Resources.AchievementBall;
            this.pictureBox1.Location = new System.Drawing.Point(136, 10);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(13, 12);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // _achievements
            // 
            this._achievements.AutoSize = true;
            this._achievements.ForeColor = System.Drawing.Color.DimGray;
            this._achievements.Location = new System.Drawing.Point(148, 7);
            this._achievements.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._achievements.Name = "_achievements";
            this._achievements.Size = new System.Drawing.Size(16, 17);
            this._achievements.TabIndex = 2;
            this._achievements.Text = "2";
            this.toolTip1.SetToolTip(this._achievements, "Achievements");
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this._failPercent);
            this.panel1.Controls.Add(this._totalBuilds);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this._achievements);
            this.panel1.Controls.Add(this._fixedSomeoneElsesBuild);
            this.panel1.Controls.Add(this._successfulBuildsInARow);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this._reputation);
            this.panel1.Controls.Add(this.avatar1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(251, 59);
            this.panel1.TabIndex = 3;
            // 
            // _failPercent
            // 
            this._failPercent.AutoSize = true;
            this._failPercent.ForeColor = System.Drawing.Color.DimGray;
            this._failPercent.Location = new System.Drawing.Point(203, 7);
            this._failPercent.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._failPercent.Name = "_failPercent";
            this._failPercent.Size = new System.Drawing.Size(48, 17);
            this._failPercent.TabIndex = 6;
            this._failPercent.Text = "00.0%";
            this._failPercent.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.toolTip1.SetToolTip(this._failPercent, "Fail %");
            // 
            // _totalBuilds
            // 
            this._totalBuilds.AutoSize = true;
            this._totalBuilds.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._totalBuilds.ForeColor = System.Drawing.Color.DimGray;
            this._totalBuilds.Location = new System.Drawing.Point(148, 33);
            this._totalBuilds.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._totalBuilds.Name = "_totalBuilds";
            this._totalBuilds.Size = new System.Drawing.Size(28, 15);
            this._totalBuilds.TabIndex = 11;
            this._totalBuilds.Text = "000";
            this.toolTip1.SetToolTip(this._totalBuilds, "Total builds");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(135, 33);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 15);
            this.label5.TabIndex = 10;
            this.label5.Text = "T:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.label5, "Total builds");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(188, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "F:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.label1, "Fail %");
            // 
            // _fixedSomeoneElsesBuild
            // 
            this._fixedSomeoneElsesBuild.AutoSize = true;
            this._fixedSomeoneElsesBuild.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._fixedSomeoneElsesBuild.ForeColor = System.Drawing.Color.DimGray;
            this._fixedSomeoneElsesBuild.Location = new System.Drawing.Point(223, 33);
            this._fixedSomeoneElsesBuild.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._fixedSomeoneElsesBuild.Name = "_fixedSomeoneElsesBuild";
            this._fixedSomeoneElsesBuild.Size = new System.Drawing.Size(14, 15);
            this._fixedSomeoneElsesBuild.TabIndex = 8;
            this._fixedSomeoneElsesBuild.Text = "0";
            this.toolTip1.SetToolTip(this._fixedSomeoneElsesBuild, "# of times fixed someone else\'s build");
            // 
            // _successfulBuildsInARow
            // 
            this._successfulBuildsInARow.AutoSize = true;
            this._successfulBuildsInARow.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._successfulBuildsInARow.ForeColor = System.Drawing.Color.DimGray;
            this._successfulBuildsInARow.Location = new System.Drawing.Point(108, 33);
            this._successfulBuildsInARow.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._successfulBuildsInARow.Name = "_successfulBuildsInARow";
            this._successfulBuildsInARow.Size = new System.Drawing.Size(21, 15);
            this._successfulBuildsInARow.TabIndex = 7;
            this._successfulBuildsInARow.Text = "00";
            this.toolTip1.SetToolTip(this._successfulBuildsInARow, "Consecutive successful builds");
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(179, 33);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "FSB:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.label3, "# of times fixed someone else\'s build");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(73, 33);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "CSB:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.label2, "Consecutive successful builds");
            // 
            // _rank
            // 
            this._rank.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this._rank.AutoSize = true;
            this._rank.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._rank.ForeColor = System.Drawing.Color.White;
            this._rank.Location = new System.Drawing.Point(0, 0);
            this._rank.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._rank.Name = "_rank";
            this._rank.Size = new System.Drawing.Size(35, 18);
            this._rank.TabIndex = 4;
            this._rank.Text = "#99";
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this._rank);
            this.panel2.Controls.Add(this._displayName);
            this.panel2.Location = new System.Drawing.Point(0, 58);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(256, 18);
            this.panel2.TabIndex = 5;
            // 
            // avatar1
            // 
            this.avatar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this.avatar1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.avatar1.ImageIndex = -1;
            this.avatar1.Location = new System.Drawing.Point(0, 0);
            this.avatar1.Margin = new System.Windows.Forms.Padding(5);
            this.avatar1.Name = "avatar1";
            this.avatar1.Size = new System.Drawing.Size(64, 59);
            this.avatar1.TabIndex = 0;
            // 
            // UserPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(100)))), ((int)(((byte)(103)))));
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Margin = new System.Windows.Forms.Padding(5, 10, 5, 0);
            this.Name = "UserPanel";
            this.Size = new System.Drawing.Size(251, 76);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Avatar avatar1;
        private System.Windows.Forms.Label _displayName;
        private System.Windows.Forms.Label _reputation;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label _achievements;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label _fixedSomeoneElsesBuild;
        private System.Windows.Forms.Label _successfulBuildsInARow;
        private System.Windows.Forms.Label _failPercent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label _totalBuilds;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label _rank;
        private System.Windows.Forms.Panel panel2;
    }
}
