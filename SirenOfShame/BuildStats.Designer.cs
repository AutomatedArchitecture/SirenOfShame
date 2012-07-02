namespace SirenOfShame
{
    partial class BuildStats
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
            this._panelBuildStats = new System.Windows.Forms.Panel();
            this._close = new System.Windows.Forms.PictureBox();
            this._percentFailed = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this._failedBuilds = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this._buildCount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._buildHistoryZedGraph = new ZedGraph.ZedGraphControl();
            this._panelBuildStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._close)).BeginInit();
            this.SuspendLayout();
            // 
            // _panelBuildStats
            // 
            this._panelBuildStats.BackColor = System.Drawing.SystemColors.Window;
            this._panelBuildStats.Controls.Add(this._close);
            this._panelBuildStats.Controls.Add(this._percentFailed);
            this._panelBuildStats.Controls.Add(this.label8);
            this._panelBuildStats.Controls.Add(this._failedBuilds);
            this._panelBuildStats.Controls.Add(this.label7);
            this._panelBuildStats.Controls.Add(this._buildCount);
            this._panelBuildStats.Controls.Add(this.label4);
            this._panelBuildStats.Controls.Add(this._buildHistoryZedGraph);
            this._panelBuildStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panelBuildStats.Location = new System.Drawing.Point(0, 0);
            this._panelBuildStats.Name = "_panelBuildStats";
            this._panelBuildStats.Size = new System.Drawing.Size(166, 203);
            this._panelBuildStats.TabIndex = 7;
            // 
            // _close
            // 
            this._close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._close.Cursor = System.Windows.Forms.Cursors.Hand;
            this._close.Image = global::SirenOfShame.Properties.Resources.CloseButton2;
            this._close.Location = new System.Drawing.Point(151, 3);
            this._close.Name = "_close";
            this._close.Size = new System.Drawing.Size(12, 12);
            this._close.TabIndex = 19;
            this._close.TabStop = false;
            this._close.Click += new System.EventHandler(this.CloseClick);
            // 
            // _percentFailed
            // 
            this._percentFailed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._percentFailed.Location = new System.Drawing.Point(86, 168);
            this._percentFailed.Name = "_percentFailed";
            this._percentFailed.Size = new System.Drawing.Size(68, 13);
            this._percentFailed.TabIndex = 18;
            this._percentFailed.Text = "0";
            this._percentFailed.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 168);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Percent Failed:";
            // 
            // _failedBuilds
            // 
            this._failedBuilds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._failedBuilds.Location = new System.Drawing.Point(86, 144);
            this._failedBuilds.Name = "_failedBuilds";
            this._failedBuilds.Size = new System.Drawing.Size(68, 13);
            this._failedBuilds.TabIndex = 16;
            this._failedBuilds.Text = "0";
            this._failedBuilds.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 144);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Failed Builds:";
            // 
            // _buildCount
            // 
            this._buildCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._buildCount.Location = new System.Drawing.Point(86, 122);
            this._buildCount.Name = "_buildCount";
            this._buildCount.Size = new System.Drawing.Size(68, 13);
            this._buildCount.TabIndex = 14;
            this._buildCount.Text = "0";
            this._buildCount.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Total Builds:";
            // 
            // _buildHistoryZedGraph
            // 
            this._buildHistoryZedGraph.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._buildHistoryZedGraph.Location = new System.Drawing.Point(0, 16);
            this._buildHistoryZedGraph.Name = "_buildHistoryZedGraph";
            this._buildHistoryZedGraph.ScrollGrace = 0D;
            this._buildHistoryZedGraph.ScrollMaxX = 0D;
            this._buildHistoryZedGraph.ScrollMaxY = 0D;
            this._buildHistoryZedGraph.ScrollMaxY2 = 0D;
            this._buildHistoryZedGraph.ScrollMinX = 0D;
            this._buildHistoryZedGraph.ScrollMinY = 0D;
            this._buildHistoryZedGraph.ScrollMinY2 = 0D;
            this._buildHistoryZedGraph.Size = new System.Drawing.Size(166, 100);
            this._buildHistoryZedGraph.TabIndex = 12;
            // 
            // BuildStats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._panelBuildStats);
            this.Name = "BuildStats";
            this.Size = new System.Drawing.Size(166, 203);
            this._panelBuildStats.ResumeLayout(false);
            this._panelBuildStats.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._close)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _panelBuildStats;
        private System.Windows.Forms.Label _percentFailed;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label _failedBuilds;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label _buildCount;
        private System.Windows.Forms.Label label4;
        private ZedGraph.ZedGraphControl _buildHistoryZedGraph;
        private System.Windows.Forms.PictureBox _close;
    }
}
