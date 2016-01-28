using SirenOfShame.Lib;

namespace AppVeyorServices.ServerConfiguration
{
    partial class ConfigureAppVeyor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigureAppVeyor));
            this.label1 = new System.Windows.Forms.Label();
            this._url = new System.Windows.Forms.TextBox();
            this._connect = new SirenOfShame.Lib.SosButton();
            this._projects = new SirenOfShame.Lib.Helpers.ThreeStateTreeView();
            this._filter = new System.Windows.Forms.TextBox();
            this._search = new System.Windows.Forms.PictureBox();
            this._checkAll = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this._password = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._search)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._checkAll)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(23, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 43;
            this.label1.Text = "API URL:";
            // 
            // _url
            // 
            this._url.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._url.Location = new System.Drawing.Point(76, 3);
            this._url.Name = "_url";
            this._url.Size = new System.Drawing.Size(308, 20);
            this._url.TabIndex = 0;
            // 
            // _connect
            // 
            this._connect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._connect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this._connect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._connect.ForeColor = System.Drawing.Color.White;
            this._connect.Location = new System.Drawing.Point(309, 52);
            this._connect.Name = "_connect";
            this._connect.Size = new System.Drawing.Size(75, 23);
            this._connect.TabIndex = 3;
            this._connect.Text = "Connect";
            this._connect.UseVisualStyleBackColor = true;
            this._connect.Click += new System.EventHandler(this.ConnectClick);
            // 
            // _projects
            // 
            this._projects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._projects.CheckBoxes = true;
            this._projects.Location = new System.Drawing.Point(3, 103);
            this._projects.Name = "_projects";
            this._projects.Size = new System.Drawing.Size(381, 174);
            this._projects.TabIndex = 4;
            this._projects.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.ProjectsAfterCheck);
            // 
            // _filter
            // 
            this._filter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._filter.Location = new System.Drawing.Point(3, 81);
            this._filter.Name = "_filter";
            this._filter.Size = new System.Drawing.Size(338, 20);
            this._filter.TabIndex = 53;
            this._filter.TextChanged += new System.EventHandler(this._filter_TextChanged);
            // 
            // _search
            // 
            this._search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._search.Image = ((System.Drawing.Image)(resources.GetObject("_search.Image")));
            this._search.Location = new System.Drawing.Point(343, 81);
            this._search.Margin = new System.Windows.Forms.Padding(2);
            this._search.Name = "_search";
            this._search.Size = new System.Drawing.Size(16, 16);
            this._search.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._search.TabIndex = 54;
            this._search.TabStop = false;
            this._search.Click += new System.EventHandler(this._search_Click);
            // 
            // _checkAll
            // 
            this._checkAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._checkAll.Image = ((System.Drawing.Image)(resources.GetObject("_checkAll.Image")));
            this._checkAll.Location = new System.Drawing.Point(366, 82);
            this._checkAll.Margin = new System.Windows.Forms.Padding(2);
            this._checkAll.Name = "_checkAll";
            this._checkAll.Size = new System.Drawing.Size(16, 16);
            this._checkAll.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._checkAll.TabIndex = 55;
            this._checkAll.TabStop = false;
            this._checkAll.Click += new System.EventHandler(this._checkAll_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(224, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 13);
            this.label4.TabIndex = 50;
            this.label4.Text = "(stored encrypted)";
            // 
            // _password
            // 
            this._password.Location = new System.Drawing.Point(76, 26);
            this._password.Name = "_password";
            this._password.PasswordChar = '*';
            this._password.Size = new System.Drawing.Size(142, 20);
            this._password.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(14, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 49;
            this.label3.Text = "API Token:";
            // 
            // ConfigureAppVeyor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this.Controls.Add(this._checkAll);
            this.Controls.Add(this._search);
            this.Controls.Add(this._filter);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._password);
            this.Controls.Add(this._projects);
            this.Controls.Add(this._connect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._url);
            this.Name = "ConfigureAppVeyor";
            this.Size = new System.Drawing.Size(387, 280);
            ((System.ComponentModel.ISupportInitialize)(this._search)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._checkAll)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _url;
        private SosButton _connect;
        private SirenOfShame.Lib.Helpers.ThreeStateTreeView _projects;
        private System.Windows.Forms.TextBox _filter;
        private System.Windows.Forms.PictureBox _search;
        private System.Windows.Forms.PictureBox _checkAll;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox _password;
        private System.Windows.Forms.Label label3;
    }
}
