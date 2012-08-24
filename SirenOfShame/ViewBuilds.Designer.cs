namespace SirenOfShame
{
    partial class ViewBuilds
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewBuilds));
            this._mainFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this._buildsLabel = new System.Windows.Forms.Label();
            this._back = new System.Windows.Forms.Button();
            this._scrollbarHider = new System.Windows.Forms.Panel();
            this._viewBuildBig = new SirenOfShame.ViewBuildBig();
            this._gettingStarted = new SirenOfShame.GettingStarted();
            this._mainFlowLayoutPanel.SuspendLayout();
            this._scrollbarHider.SuspendLayout();
            this.SuspendLayout();
            // 
            // _mainFlowLayoutPanel
            // 
            this._mainFlowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._mainFlowLayoutPanel.AutoScroll = true;
            this._mainFlowLayoutPanel.BackColor = System.Drawing.Color.Transparent;
            this._mainFlowLayoutPanel.Controls.Add(this._viewBuildBig);
            this._mainFlowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this._mainFlowLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this._mainFlowLayoutPanel.Name = "_mainFlowLayoutPanel";
            this._mainFlowLayoutPanel.Size = new System.Drawing.Size(550, 260);
            this._mainFlowLayoutPanel.TabIndex = 0;
            this._mainFlowLayoutPanel.MouseEnter += new System.EventHandler(this.MainFlowLayoutPanelMouseEnter);
            // 
            // _buildsLabel
            // 
            this._buildsLabel.BackColor = System.Drawing.Color.Transparent;
            this._buildsLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._buildsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._buildsLabel.ForeColor = System.Drawing.Color.White;
            this._buildsLabel.Location = new System.Drawing.Point(38, 0);
            this._buildsLabel.Name = "_buildsLabel";
            this._buildsLabel.Size = new System.Drawing.Size(530, 42);
            this._buildsLabel.TabIndex = 1;
            this._buildsLabel.Text = "Builds";
            // 
            // _back
            // 
            this._back.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._back.FlatAppearance.BorderSize = 0;
            this._back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._back.Image = ((System.Drawing.Image)(resources.GetObject("_back.Image")));
            this._back.Location = new System.Drawing.Point(0, 0);
            this._back.Name = "_back";
            this._back.Size = new System.Drawing.Size(32, 32);
            this._back.TabIndex = 3;
            this._back.UseVisualStyleBackColor = true;
            this._back.Visible = false;
            this._back.Click += new System.EventHandler(this.BackClick);
            // 
            // _scrollbarHider
            // 
            this._scrollbarHider.Controls.Add(this._mainFlowLayoutPanel);
            this._scrollbarHider.Dock = System.Windows.Forms.DockStyle.Fill;
            this._scrollbarHider.Location = new System.Drawing.Point(38, 199);
            this._scrollbarHider.Name = "_scrollbarHider";
            this._scrollbarHider.Size = new System.Drawing.Size(530, 260);
            this._scrollbarHider.TabIndex = 5;
            // 
            // _viewBuildBig
            // 
            this._viewBuildBig.BackColor = System.Drawing.Color.White;
            this._viewBuildBig.Location = new System.Drawing.Point(4, 4);
            this._viewBuildBig.Margin = new System.Windows.Forms.Padding(4);
            this._viewBuildBig.Name = "_viewBuildBig";
            this._viewBuildBig.Size = new System.Drawing.Size(408, 173);
            this._viewBuildBig.TabIndex = 0;
            this._viewBuildBig.Visible = false;
            // 
            // _gettingStarted
            // 
            this._gettingStarted.BackColor = System.Drawing.Color.Transparent;
            this._gettingStarted.Dock = System.Windows.Forms.DockStyle.Top;
            this._gettingStarted.Location = new System.Drawing.Point(38, 42);
            this._gettingStarted.Name = "_gettingStarted";
            this._gettingStarted.Size = new System.Drawing.Size(530, 157);
            this._gettingStarted.TabIndex = 4;
            this._gettingStarted.Visible = false;
            // 
            // ViewBuilds
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this.Controls.Add(this._scrollbarHider);
            this.Controls.Add(this._gettingStarted);
            this.Controls.Add(this._back);
            this.Controls.Add(this._buildsLabel);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ViewBuilds";
            this.Padding = new System.Windows.Forms.Padding(38, 0, 0, 0);
            this.Size = new System.Drawing.Size(568, 459);
            this.Resize += new System.EventHandler(this.ViewBuildsResize);
            this._mainFlowLayoutPanel.ResumeLayout(false);
            this._scrollbarHider.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel _mainFlowLayoutPanel;
        private System.Windows.Forms.Label _buildsLabel;
        private System.Windows.Forms.Button _back;
        private ViewBuildBig _viewBuildBig;
        private GettingStarted _gettingStarted;
        private System.Windows.Forms.Panel _scrollbarHider;
    }
}
