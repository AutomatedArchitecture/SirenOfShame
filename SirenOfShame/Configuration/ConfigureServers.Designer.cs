namespace SirenOfShame.Configuration
{
    partial class ConfigureServers
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigureServers));
            this._servers = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this._down = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this._delete = new System.Windows.Forms.Button();
            this._add = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _servers
            // 
            this._servers.Dock = System.Windows.Forms.DockStyle.Fill;
            this._servers.FormattingEnabled = true;
            this._servers.Location = new System.Drawing.Point(0, 30);
            this._servers.Name = "_servers";
            this._servers.Size = new System.Drawing.Size(340, 124);
            this._servers.TabIndex = 1;
            this._servers.DoubleClick += new System.EventHandler(this.ServersDoubleClick);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Window;
            this.panel2.Controls.Add(this._down);
            this.panel2.Controls.Add(this._delete);
            this.panel2.Controls.Add(this._add);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(340, 30);
            this.panel2.TabIndex = 4;
            // 
            // _down
            // 
            this._down.FlatAppearance.BorderSize = 0;
            this._down.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._down.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._down.ImageIndex = 5;
            this._down.ImageList = this.imageList1;
            this._down.Location = new System.Drawing.Point(170, 4);
            this._down.Name = "_down";
            this._down.Size = new System.Drawing.Size(74, 23);
            this._down.TabIndex = 3;
            this._down.Text = "Edit";
            this._down.UseVisualStyleBackColor = true;
            this._down.Click += new System.EventHandler(this.ServersDoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Delete.png");
            this.imageList1.Images.SetKeyName(1, "Down.PNG");
            this.imageList1.Images.SetKeyName(2, "New.png");
            this.imageList1.Images.SetKeyName(3, "Up.png");
            this.imageList1.Images.SetKeyName(4, "Exclamation.png");
            this.imageList1.Images.SetKeyName(5, "Tools.png");
            // 
            // _delete
            // 
            this._delete.FlatAppearance.BorderSize = 0;
            this._delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._delete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._delete.ImageIndex = 0;
            this._delete.ImageList = this.imageList1;
            this._delete.Location = new System.Drawing.Point(99, 4);
            this._delete.Name = "_delete";
            this._delete.Size = new System.Drawing.Size(65, 23);
            this._delete.TabIndex = 1;
            this._delete.Text = "Delete";
            this._delete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._delete.UseVisualStyleBackColor = true;
            this._delete.Click += new System.EventHandler(this.DeleteClick);
            // 
            // _add
            // 
            this._add.FlatAppearance.BorderSize = 0;
            this._add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._add.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._add.ImageIndex = 2;
            this._add.ImageList = this.imageList1;
            this._add.Location = new System.Drawing.Point(4, 3);
            this._add.Name = "_add";
            this._add.Size = new System.Drawing.Size(89, 23);
            this._add.TabIndex = 0;
            this._add.Text = "New Server";
            this._add.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._add.UseVisualStyleBackColor = true;
            this._add.Click += new System.EventHandler(this.AddClick);
            // 
            // ConfigureServers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 154);
            this.Controls.Add(this._servers);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConfigureServers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configure Servers";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigureServers_FormClosing);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox _servers;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button _down;
        private System.Windows.Forms.Button _delete;
        private System.Windows.Forms.Button _add;
        private System.Windows.Forms.ImageList imageList1;
    }
}