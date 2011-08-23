namespace SirenOfShame.Lib
{
    partial class WavEditor
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
            this._save = new System.Windows.Forms.Button();
            this._cancel = new System.Windows.Forms.Button();
            this._crop = new System.Windows.Forms.Button();
            this._play = new System.Windows.Forms.Button();
            this._playTimes5 = new System.Windows.Forms.Button();
            this._viewer = new SirenOfShame.Lib.WavViewControl();
            this._stop = new System.Windows.Forms.Button();
            this._appendSilence = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _save
            // 
            this._save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._save.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._save.Location = new System.Drawing.Point(452, 238);
            this._save.Name = "_save";
            this._save.Size = new System.Drawing.Size(75, 23);
            this._save.TabIndex = 1;
            this._save.Text = "Save";
            this._save.UseVisualStyleBackColor = true;
            this._save.Click += new System.EventHandler(this._save_Click);
            // 
            // _cancel
            // 
            this._cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancel.Location = new System.Drawing.Point(533, 238);
            this._cancel.Name = "_cancel";
            this._cancel.Size = new System.Drawing.Size(75, 23);
            this._cancel.TabIndex = 2;
            this._cancel.Text = "Cancel";
            this._cancel.UseVisualStyleBackColor = true;
            // 
            // _crop
            // 
            this._crop.Location = new System.Drawing.Point(12, 174);
            this._crop.Name = "_crop";
            this._crop.Size = new System.Drawing.Size(75, 23);
            this._crop.TabIndex = 3;
            this._crop.Text = "Crop";
            this._crop.UseVisualStyleBackColor = true;
            this._crop.Click += new System.EventHandler(this._crop_Click);
            // 
            // _play
            // 
            this._play.Location = new System.Drawing.Point(93, 174);
            this._play.Name = "_play";
            this._play.Size = new System.Drawing.Size(75, 23);
            this._play.TabIndex = 4;
            this._play.Text = "Play";
            this._play.UseVisualStyleBackColor = true;
            this._play.Click += new System.EventHandler(this._play_Click);
            // 
            // _playTimes5
            // 
            this._playTimes5.Location = new System.Drawing.Point(174, 174);
            this._playTimes5.Name = "_playTimes5";
            this._playTimes5.Size = new System.Drawing.Size(75, 23);
            this._playTimes5.TabIndex = 5;
            this._playTimes5.Text = "Play x5";
            this._playTimes5.UseVisualStyleBackColor = true;
            this._playTimes5.Click += new System.EventHandler(this._playTimes5_Click);
            // 
            // _viewer
            // 
            this._viewer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._viewer.BackColor = System.Drawing.Color.Black;
            this._viewer.Data = null;
            this._viewer.ForeColor = System.Drawing.Color.Yellow;
            this._viewer.Location = new System.Drawing.Point(12, 12);
            this._viewer.Name = "_viewer";
            this._viewer.SelectionEnd = null;
            this._viewer.SelectionStart = null;
            this._viewer.Size = new System.Drawing.Size(596, 156);
            this._viewer.TabIndex = 0;
            this._viewer.Zoom = 1D;
            // 
            // _stop
            // 
            this._stop.Location = new System.Drawing.Point(255, 174);
            this._stop.Name = "_stop";
            this._stop.Size = new System.Drawing.Size(75, 23);
            this._stop.TabIndex = 6;
            this._stop.Text = "Stop";
            this._stop.UseVisualStyleBackColor = true;
            this._stop.Click += new System.EventHandler(this._stop_Click);
            // 
            // _appendSilence
            // 
            this._appendSilence.Location = new System.Drawing.Point(12, 203);
            this._appendSilence.Name = "_appendSilence";
            this._appendSilence.Size = new System.Drawing.Size(104, 23);
            this._appendSilence.TabIndex = 7;
            this._appendSilence.Text = "Append Silence";
            this._appendSilence.UseVisualStyleBackColor = true;
            this._appendSilence.Click += new System.EventHandler(this._appendSilence_Click);
            // 
            // WavEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 273);
            this.Controls.Add(this._appendSilence);
            this.Controls.Add(this._stop);
            this.Controls.Add(this._playTimes5);
            this.Controls.Add(this._play);
            this.Controls.Add(this._crop);
            this.Controls.Add(this._cancel);
            this.Controls.Add(this._save);
            this.Controls.Add(this._viewer);
            this.Name = "WavEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Audio Editor";
            this.ResumeLayout(false);

        }

        #endregion

        private WavViewControl _viewer;
        private System.Windows.Forms.Button _save;
        private System.Windows.Forms.Button _cancel;
        private System.Windows.Forms.Button _crop;
        private System.Windows.Forms.Button _play;
        private System.Windows.Forms.Button _playTimes5;
        private System.Windows.Forms.Button _stop;
        private System.Windows.Forms.Button _appendSilence;
    }
}