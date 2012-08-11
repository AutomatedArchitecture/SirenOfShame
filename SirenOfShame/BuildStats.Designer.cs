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
            this._buildHistoryZedGraph = new ZedGraph.ZedGraphControl();
            this.SuspendLayout();
            // 
            // _buildHistoryZedGraph
            // 
            this._buildHistoryZedGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this._buildHistoryZedGraph.Location = new System.Drawing.Point(0, 0);
            this._buildHistoryZedGraph.Name = "_buildHistoryZedGraph";
            this._buildHistoryZedGraph.ScrollGrace = 0D;
            this._buildHistoryZedGraph.ScrollMaxX = 0D;
            this._buildHistoryZedGraph.ScrollMaxY = 0D;
            this._buildHistoryZedGraph.ScrollMaxY2 = 0D;
            this._buildHistoryZedGraph.ScrollMinX = 0D;
            this._buildHistoryZedGraph.ScrollMinY = 0D;
            this._buildHistoryZedGraph.ScrollMinY2 = 0D;
            this._buildHistoryZedGraph.Size = new System.Drawing.Size(166, 113);
            this._buildHistoryZedGraph.TabIndex = 13;
            // 
            // BuildStats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._buildHistoryZedGraph);
            this.Name = "BuildStats";
            this.Size = new System.Drawing.Size(166, 113);
            this.ResumeLayout(false);

        }

        #endregion

        private ZedGraph.ZedGraphControl _buildHistoryZedGraph;

    }
}
