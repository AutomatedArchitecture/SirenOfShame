namespace SirenOfShame
{
    partial class ViewAllNews
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewAllNews));
            this._topPanel = new System.Windows.Forms.Panel();
            this._loading = new System.Windows.Forms.PictureBox();
            this._filterButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this._back = new System.Windows.Forms.Button();
            this._topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._loading)).BeginInit();
            this.SuspendLayout();
            // 
            // _topPanel
            // 
            this._topPanel.Controls.Add(this._loading);
            this._topPanel.Controls.Add(this._filterButton);
            this._topPanel.Controls.Add(this.label1);
            this._topPanel.Controls.Add(this._back);
            this._topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._topPanel.Location = new System.Drawing.Point(0, 0);
            this._topPanel.Name = "_topPanel";
            this._topPanel.Size = new System.Drawing.Size(414, 42);
            this._topPanel.TabIndex = 6;
            // 
            // _loading
            // 
            this._loading.Dock = System.Windows.Forms.DockStyle.Left;
            this._loading.Image = global::SirenOfShame.Properties.Resources.loading;
            this._loading.Location = new System.Drawing.Point(176, 0);
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
            this._filterButton.Location = new System.Drawing.Point(152, 0);
            this._filterButton.Name = "_filterButton";
            this._filterButton.Size = new System.Drawing.Size(24, 42);
            this._filterButton.TabIndex = 6;
            this._filterButton.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(32, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label1.Size = new System.Drawing.Size(120, 42);
            this.label1.TabIndex = 5;
            this.label1.Text = "All News";
            // 
            // _back
            // 
            this._back.Dock = System.Windows.Forms.DockStyle.Left;
            this._back.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this._back.FlatAppearance.BorderSize = 0;
            this._back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._back.Image = ((System.Drawing.Image)(resources.GetObject("_back.Image")));
            this._back.Location = new System.Drawing.Point(0, 0);
            this._back.Name = "_back";
            this._back.Size = new System.Drawing.Size(32, 42);
            this._back.TabIndex = 35;
            this._back.UseVisualStyleBackColor = true;
            this._back.Click += new System.EventHandler(this.BackClick);
            // 
            // ViewAllNews
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this.Controls.Add(this._topPanel);
            this.Name = "ViewAllNews";
            this.Size = new System.Drawing.Size(414, 319);
            this._topPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._loading)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _topPanel;
        private System.Windows.Forms.PictureBox _loading;
        private System.Windows.Forms.Button _filterButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _back;
    }
}
