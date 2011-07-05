namespace SirenOfShame.Configuration
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this._ok = new System.Windows.Forms.Button();
            this._cancel = new System.Windows.Forms.Button();
            this._duration = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._pollInterval = new System.Windows.Forms.TrackBar();
            this._viewLog = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._pollInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // _ok
            // 
            this._ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._ok.Location = new System.Drawing.Point(434, 79);
            this._ok.Name = "_ok";
            this._ok.Size = new System.Drawing.Size(75, 23);
            this._ok.TabIndex = 2;
            this._ok.Text = "Ok";
            this._ok.UseVisualStyleBackColor = true;
            this._ok.Click += new System.EventHandler(this.OkClick);
            // 
            // _cancel
            // 
            this._cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancel.Location = new System.Drawing.Point(515, 79);
            this._cancel.Name = "_cancel";
            this._cancel.Size = new System.Drawing.Size(75, 23);
            this._cancel.TabIndex = 3;
            this._cancel.Text = "Cancel";
            this._cancel.UseVisualStyleBackColor = true;
            this._cancel.Click += new System.EventHandler(this.CancelClick);
            // 
            // _duration
            // 
            this._duration.AutoSize = true;
            this._duration.Location = new System.Drawing.Point(92, 44);
            this._duration.Name = "_duration";
            this._duration.Size = new System.Drawing.Size(59, 13);
            this._duration.TabIndex = 7;
            this._duration.Text = "X Seconds";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Poll interval";
            // 
            // _pollInterval
            // 
            this._pollInterval.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._pollInterval.Location = new System.Drawing.Point(95, 12);
            this._pollInterval.Maximum = 60;
            this._pollInterval.Name = "_pollInterval";
            this._pollInterval.Size = new System.Drawing.Size(495, 42);
            this._pollInterval.TabIndex = 0;
            this._pollInterval.Value = 1;
            this._pollInterval.ValueChanged += new System.EventHandler(this.PollIntervalValueChanged);
            // 
            // _viewLog
            // 
            this._viewLog.Location = new System.Drawing.Point(353, 79);
            this._viewLog.Name = "_viewLog";
            this._viewLog.Size = new System.Drawing.Size(75, 23);
            this._viewLog.TabIndex = 8;
            this._viewLog.Text = "View Log...";
            this._viewLog.UseVisualStyleBackColor = true;
            this._viewLog.Click += new System.EventHandler(this.ViewLogClick);
            // 
            // Settings
            // 
            this.AcceptButton = this._ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cancel;
            this.ClientSize = new System.Drawing.Size(602, 114);
            this.Controls.Add(this._viewLog);
            this.Controls.Add(this._duration);
            this.Controls.Add(this._cancel);
            this.Controls.Add(this._ok);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._pollInterval);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Settings";
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this._pollInterval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar _pollInterval;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _ok;
        private System.Windows.Forms.Button _cancel;
        private System.Windows.Forms.Label _duration;
        private System.Windows.Forms.Button _viewLog;
    }
}