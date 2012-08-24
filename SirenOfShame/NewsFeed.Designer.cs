namespace SirenOfShame
{
    partial class NewsFeed
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
            this.panel1 = new System.Windows.Forms.Panel();
            this._clearNews = new System.Windows.Forms.Button();
            this._newsItemsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this._noNews = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this._clearNews);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 219);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(254, 18);
            this.panel1.TabIndex = 0;
            // 
            // _clearNews
            // 
            this._clearNews.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._clearNews.FlatAppearance.BorderSize = 0;
            this._clearNews.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._clearNews.Image = global::SirenOfShame.Properties.Resources.TrashSmall;
            this._clearNews.Location = new System.Drawing.Point(236, -1);
            this._clearNews.Name = "_clearNews";
            this._clearNews.Size = new System.Drawing.Size(18, 18);
            this._clearNews.TabIndex = 1;
            this._clearNews.UseVisualStyleBackColor = true;
            this._clearNews.Click += new System.EventHandler(this.ClearNewsClick);
            // 
            // _newsItemsPanel
            // 
            this._newsItemsPanel.BackColor = System.Drawing.Color.Transparent;
            this._newsItemsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._newsItemsPanel.Location = new System.Drawing.Point(0, 85);
            this._newsItemsPanel.Name = "_newsItemsPanel";
            this._newsItemsPanel.Size = new System.Drawing.Size(254, 134);
            this._newsItemsPanel.TabIndex = 1;
            this._newsItemsPanel.MouseEnter += new System.EventHandler(this.NewsItemsPanelMouseEnter);
            // 
            // _noNews
            // 
            this._noNews.BackColor = System.Drawing.Color.Transparent;
            this._noNews.Dock = System.Windows.Forms.DockStyle.Top;
            this._noNews.ForeColor = System.Drawing.Color.Gray;
            this._noNews.Location = new System.Drawing.Point(0, 42);
            this._noNews.Name = "_noNews";
            this._noNews.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this._noNews.Size = new System.Drawing.Size(254, 43);
            this._noNews.TabIndex = 2;
            this._noNews.Text = "No news is good news";
            this._noNews.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(254, 42);
            this.label1.TabIndex = 4;
            this.label1.Text = "News";
            // 
            // NewsFeed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this.Controls.Add(this._newsItemsPanel);
            this.Controls.Add(this._noNews);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "NewsFeed";
            this.Size = new System.Drawing.Size(254, 237);
            this.MouseEnter += new System.EventHandler(this.NewsFeedMouseEnter);
            this.Resize += new System.EventHandler(this.NewsFeedResize);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button _clearNews;
        private System.Windows.Forms.FlowLayoutPanel _newsItemsPanel;
        private System.Windows.Forms.Label _noNews;
        private System.Windows.Forms.Label label1;


    }
}
