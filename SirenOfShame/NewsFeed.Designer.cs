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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this._newsItemsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this._noNews = new System.Windows.Forms.Label();
            this._topPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this._addMessage = new System.Windows.Forms.Button();
            this._clearNews = new System.Windows.Forms.Button();
            this._loading = new System.Windows.Forms.PictureBox();
            this._filterButton = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this._message = new SirenOfShame.WaterMarkTextBox();
            this.panel1.SuspendLayout();
            this._topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._loading)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this._addMessage);
            this.panel1.Controls.Add(this._message);
            this.panel1.Controls.Add(this._clearNews);
            this.panel1.Location = new System.Drawing.Point(0, 219);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(234, 18);
            this.panel1.TabIndex = 0;
            // 
            // _newsItemsPanel
            // 
            this._newsItemsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._newsItemsPanel.AutoScroll = true;
            this._newsItemsPanel.BackColor = System.Drawing.Color.Transparent;
            this._newsItemsPanel.Location = new System.Drawing.Point(0, 45);
            this._newsItemsPanel.Name = "_newsItemsPanel";
            this._newsItemsPanel.Size = new System.Drawing.Size(278, 173);
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
            this._noNews.Size = new System.Drawing.Size(258, 43);
            this._noNews.TabIndex = 2;
            this._noNews.Text = "No news is good news";
            this._noNews.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _topPanel
            // 
            this._topPanel.Controls.Add(this._loading);
            this._topPanel.Controls.Add(this._filterButton);
            this._topPanel.Controls.Add(this.label1);
            this._topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._topPanel.Location = new System.Drawing.Point(0, 0);
            this._topPanel.Name = "_topPanel";
            this._topPanel.Size = new System.Drawing.Size(258, 42);
            this._topPanel.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label1.Size = new System.Drawing.Size(89, 42);
            this.label1.TabIndex = 5;
            this.label1.Text = "News";
            // 
            // _addMessage
            // 
            this._addMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._addMessage.FlatAppearance.BorderSize = 0;
            this._addMessage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(55)))), ((int)(((byte)(0)))));
            this._addMessage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(65)))), ((int)(((byte)(0)))));
            this._addMessage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._addMessage.Image = global::SirenOfShame.Properties.Resources.nav_right;
            this._addMessage.Location = new System.Drawing.Point(190, -1);
            this._addMessage.Name = "_addMessage";
            this._addMessage.Size = new System.Drawing.Size(18, 18);
            this._addMessage.TabIndex = 3;
            this.toolTip1.SetToolTip(this._addMessage, "Submit message to SoS Online");
            this._addMessage.UseVisualStyleBackColor = true;
            this._addMessage.Click += new System.EventHandler(this.AddMessageClick);
            // 
            // _clearNews
            // 
            this._clearNews.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._clearNews.FlatAppearance.BorderSize = 0;
            this._clearNews.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(55)))), ((int)(((byte)(0)))));
            this._clearNews.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(65)))), ((int)(((byte)(0)))));
            this._clearNews.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._clearNews.Image = global::SirenOfShame.Properties.Resources.delete2;
            this._clearNews.Location = new System.Drawing.Point(216, -1);
            this._clearNews.Name = "_clearNews";
            this._clearNews.Size = new System.Drawing.Size(18, 18);
            this._clearNews.TabIndex = 1;
            this.toolTip1.SetToolTip(this._clearNews, "Clear all messages");
            this._clearNews.UseVisualStyleBackColor = true;
            this._clearNews.Click += new System.EventHandler(this.ClearNewsClick);
            // 
            // _loading
            // 
            this._loading.Dock = System.Windows.Forms.DockStyle.Left;
            this._loading.Image = global::SirenOfShame.Properties.Resources.loading;
            this._loading.Location = new System.Drawing.Point(113, 0);
            this._loading.Name = "_loading";
            this._loading.Size = new System.Drawing.Size(21, 42);
            this._loading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._loading.TabIndex = 34;
            this._loading.TabStop = false;
            this._loading.Visible = false;
            // 
            // _filterButton
            // 
            this._filterButton.BackColor = System.Drawing.Color.Transparent;
            this._filterButton.Dock = System.Windows.Forms.DockStyle.Left;
            this._filterButton.FlatAppearance.BorderSize = 0;
            this._filterButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this._filterButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this._filterButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._filterButton.Image = global::SirenOfShame.Properties.Resources.funnel1;
            this._filterButton.Location = new System.Drawing.Point(89, 0);
            this._filterButton.Name = "_filterButton";
            this._filterButton.Size = new System.Drawing.Size(24, 42);
            this._filterButton.TabIndex = 6;
            this._filterButton.UseVisualStyleBackColor = false;
            // 
            // _message
            // 
            this._message.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._message.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this._message.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._message.ForeColor = System.Drawing.Color.White;
            this._message.Location = new System.Drawing.Point(6, 2);
            this._message.Name = "_message";
            this._message.Size = new System.Drawing.Size(186, 13);
            this._message.TabIndex = 2;
            this._message.WaterMarkText = "enter a message";
            this._message.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MessageKeyDown);
            // 
            // NewsFeed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this.Controls.Add(this._noNews);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this._newsItemsPanel);
            this.Controls.Add(this._topPanel);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "NewsFeed";
            this.Size = new System.Drawing.Size(258, 237);
            this.MouseEnter += new System.EventHandler(this.NewsFeedMouseEnter);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this._topPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._loading)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button _clearNews;
        private System.Windows.Forms.FlowLayoutPanel _newsItemsPanel;
        private System.Windows.Forms.Label _noNews;
        private System.Windows.Forms.Panel _topPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _filterButton;
        private System.Windows.Forms.PictureBox _loading;
        private WaterMarkTextBox _message;
        private System.Windows.Forms.Button _addMessage;
        private System.Windows.Forms.ToolTip toolTip1;


    }
}
