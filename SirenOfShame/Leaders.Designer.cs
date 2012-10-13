namespace SirenOfShame
{
    partial class Leaders
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
            this._usersPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this._userMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._hiddenButton = new System.Windows.Forms.ToolStripMenuItem();
            this._userMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // _usersPanel
            // 
            this._usersPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._usersPanel.AutoScroll = true;
            this._usersPanel.BackColor = System.Drawing.Color.Transparent;
            this._usersPanel.Location = new System.Drawing.Point(0, 38);
            this._usersPanel.Name = "_usersPanel";
            this._usersPanel.Size = new System.Drawing.Size(229, 231);
            this._usersPanel.TabIndex = 2;
            this._usersPanel.MouseEnter += new System.EventHandler(this.UsersPanelMouseEnter);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label1.Size = new System.Drawing.Size(209, 38);
            this.label1.TabIndex = 3;
            this.label1.Text = "Leaders";
            // 
            // _userMenu
            // 
            this._userMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._hiddenButton});
            this._userMenu.Name = "contextMenuStrip1";
            this._userMenu.Size = new System.Drawing.Size(153, 48);
            // 
            // _hiddenButton
            // 
            this._hiddenButton.CheckOnClick = true;
            this._hiddenButton.Name = "_hiddenButton";
            this._hiddenButton.Size = new System.Drawing.Size(152, 22);
            this._hiddenButton.Text = "Hidden";
            this._hiddenButton.Click += new System.EventHandler(this.HiddenButtonClick);
            // 
            // Leaders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this.Controls.Add(this._usersPanel);
            this.Controls.Add(this.label1);
            this.Name = "Leaders";
            this.Size = new System.Drawing.Size(209, 273);
            this._userMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel _usersPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip _userMenu;
        private System.Windows.Forms.ToolStripMenuItem _hiddenButton;
    }
}
