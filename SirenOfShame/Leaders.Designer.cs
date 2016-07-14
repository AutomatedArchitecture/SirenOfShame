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
            this._isADuplicate = new System.Windows.Forms.ToolStripMenuItem();
            this._userMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // _usersPanel
            // 
            this._usersPanel.AutoScroll = true;
            this._usersPanel.BackColor = System.Drawing.Color.Transparent;
            this._usersPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._usersPanel.Location = new System.Drawing.Point(0, 47);
            this._usersPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._usersPanel.Name = "_usersPanel";
            this._usersPanel.Size = new System.Drawing.Size(279, 289);
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
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label1.Size = new System.Drawing.Size(279, 47);
            this.label1.TabIndex = 3;
            this.label1.Text = "Leaders";
            // 
            // _userMenu
            // 
            this._userMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this._userMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._hiddenButton,
            this._isADuplicate});
            this._userMenu.Name = "contextMenuStrip1";
            this._userMenu.Size = new System.Drawing.Size(177, 56);
            this._userMenu.Opening += new System.ComponentModel.CancelEventHandler(this.UserMenuOpening);
            // 
            // _hiddenButton
            // 
            this._hiddenButton.CheckOnClick = true;
            this._hiddenButton.Name = "_hiddenButton";
            this._hiddenButton.Size = new System.Drawing.Size(176, 26);
            this._hiddenButton.Text = "Hidden";
            this._hiddenButton.Click += new System.EventHandler(this.HiddenButtonClick);
            // 
            // _isADuplicate
            // 
            this._isADuplicate.Name = "_isADuplicate";
            this._isADuplicate.Size = new System.Drawing.Size(176, 26);
            this._isADuplicate.Text = "Is A Duplicate";
            this._isADuplicate.Click += new System.EventHandler(this.IsADuplicateClick);
            // 
            // Leaders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this.Controls.Add(this._usersPanel);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Leaders";
            this.Size = new System.Drawing.Size(279, 336);
            this._userMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel _usersPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip _userMenu;
        private System.Windows.Forms.ToolStripMenuItem _hiddenButton;
        private System.Windows.Forms.ToolStripMenuItem _isADuplicate;
    }
}
