namespace SirenOfShame
{
    partial class UserList
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
            this._usersPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // _usersPanel
            // 
            this._usersPanel.AutoScroll = true;
            this._usersPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._usersPanel.Location = new System.Drawing.Point(0, 0);
            this._usersPanel.Name = "_usersPanel";
            this._usersPanel.Size = new System.Drawing.Size(172, 194);
            this._usersPanel.TabIndex = 2;
            // 
            // UserList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this._usersPanel);
            this.Name = "UserList";
            this.Size = new System.Drawing.Size(172, 194);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _usersPanel;
    }
}
