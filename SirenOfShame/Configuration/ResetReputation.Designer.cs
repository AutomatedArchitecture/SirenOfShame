namespace SirenOfShame.Configuration
{
    partial class ResetReputation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResetReputation));
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ResetDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.ResetButton = new SirenOfShame.Lib.SosButton();
            this.CancelButton = new SirenOfShame.Lib.SosButton();
            this.label4 = new System.Windows.Forms.Label();
            this.ResetAndRebuildSinceDate = new System.Windows.Forms.RadioButton();
            this.ResetAndRebuildFromStart = new System.Windows.Forms.RadioButton();
            this.ResetOnly = new System.Windows.Forms.RadioButton();
            this.resetStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(385, 31);
            this.label1.TabIndex = 1;
            this.label1.Text = "Reputation Reset";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(14, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(381, 170);
            this.label5.TabIndex = 21;
            this.label5.Text = resources.GetString("label5.Text");
            // 
            // ResetDate
            // 
            this.ResetDate.Location = new System.Drawing.Point(17, 328);
            this.ResetDate.Name = "ResetDate";
            this.ResetDate.Size = new System.Drawing.Size(200, 20);
            this.ResetDate.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 231);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(385, 31);
            this.label2.TabIndex = 23;
            this.label2.Text = "Let\'s Do This Thing";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ResetButton
            // 
            this.ResetButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ResetButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.ResetButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this.ResetButton.FlatAppearance.BorderSize = 0;
            this.ResetButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(55)))), ((int)(((byte)(0)))));
            this.ResetButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(65)))), ((int)(((byte)(0)))));
            this.ResetButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ResetButton.ForeColor = System.Drawing.Color.White;
            this.ResetButton.Location = new System.Drawing.Point(236, 310);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(159, 38);
            this.ResetButton.TabIndex = 24;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = false;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this.CancelButton.FlatAppearance.BorderSize = 0;
            this.CancelButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(55)))), ((int)(((byte)(0)))));
            this.CancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(65)))), ((int)(((byte)(0)))));
            this.CancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelButton.ForeColor = System.Drawing.Color.White;
            this.CancelButton.Location = new System.Drawing.Point(17, 419);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(378, 33);
            this.CancelButton.TabIndex = 26;
            this.CancelButton.Text = "Abort, Abort!";
            this.CancelButton.UseVisualStyleBackColor = false;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(17, 385);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(378, 31);
            this.label4.TabIndex = 27;
            this.label4.Text = "Get Me Outta Here";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ResetAndRebuildSinceDate
            // 
            this.ResetAndRebuildSinceDate.AutoSize = true;
            this.ResetAndRebuildSinceDate.Location = new System.Drawing.Point(17, 305);
            this.ResetAndRebuildSinceDate.Name = "ResetAndRebuildSinceDate";
            this.ResetAndRebuildSinceDate.Size = new System.Drawing.Size(139, 17);
            this.ResetAndRebuildSinceDate.TabIndex = 28;
            this.ResetAndRebuildSinceDate.TabStop = true;
            this.ResetAndRebuildSinceDate.Text = "Reset and rebuild since:";
            this.ResetAndRebuildSinceDate.UseVisualStyleBackColor = true;
            this.ResetAndRebuildSinceDate.CheckedChanged += new System.EventHandler(this.ResetAndRebuildSinceDate_CheckedChanged);
            // 
            // ResetAndRebuildFromStart
            // 
            this.ResetAndRebuildFromStart.AutoSize = true;
            this.ResetAndRebuildFromStart.Location = new System.Drawing.Point(17, 287);
            this.ResetAndRebuildFromStart.Name = "ResetAndRebuildFromStart";
            this.ResetAndRebuildFromStart.Size = new System.Drawing.Size(237, 17);
            this.ResetAndRebuildFromStart.TabIndex = 29;
            this.ResetAndRebuildFromStart.TabStop = true;
            this.ResetAndRebuildFromStart.Text = "Reset and rebuild since the beginning of time";
            this.ResetAndRebuildFromStart.UseVisualStyleBackColor = true;
            this.ResetAndRebuildFromStart.CheckedChanged += new System.EventHandler(this.ResetAndRebuildFromStart_CheckedChanged);
            // 
            // ResetOnly
            // 
            this.ResetOnly.AutoSize = true;
            this.ResetOnly.Location = new System.Drawing.Point(17, 271);
            this.ResetOnly.Name = "ResetOnly";
            this.ResetOnly.Size = new System.Drawing.Size(75, 17);
            this.ResetOnly.TabIndex = 30;
            this.ResetOnly.TabStop = true;
            this.ResetOnly.Text = "Reset only";
            this.ResetOnly.UseVisualStyleBackColor = true;
            this.ResetOnly.CheckedChanged += new System.EventHandler(this.ResetOnly_CheckedChanged);
            // 
            // resetStatus
            // 
            this.resetStatus.Location = new System.Drawing.Point(19, 360);
            this.resetStatus.Name = "resetStatus";
            this.resetStatus.Size = new System.Drawing.Size(376, 23);
            this.resetStatus.TabIndex = 31;
            this.resetStatus.Text = "{";
            this.resetStatus.Visible = false;
            // 
            // ResetReputation
            // 
            this.AcceptButton = this.CancelButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this.ClientSize = new System.Drawing.Size(409, 476);
            this.Controls.Add(this.resetStatus);
            this.Controls.Add(this.ResetOnly);
            this.Controls.Add(this.ResetAndRebuildFromStart);
            this.Controls.Add(this.ResetAndRebuildSinceDate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ResetDate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "ResetReputation";
            this.Text = "Reset Reputation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker ResetDate;
        private System.Windows.Forms.Label label2;
        private Lib.SosButton ResetButton;
        private Lib.SosButton CancelButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton ResetAndRebuildSinceDate;
        private System.Windows.Forms.RadioButton ResetAndRebuildFromStart;
        private System.Windows.Forms.RadioButton ResetOnly;
        private System.Windows.Forms.Label resetStatus;
    }
}