namespace TfsRestServices.ServerConfiguration
{
    partial class ConfigureTfsRest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigureTfsRest));
            this.label1 = new System.Windows.Forms.Label();
            this._url = new System.Windows.Forms.TextBox();
            this._connect = new SirenOfShame.Lib.SosButton();
            this._projects = new SirenOfShame.Lib.Helpers.ThreeStateTreeView();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._password = new System.Windows.Forms.TextBox();
            this._userName = new System.Windows.Forms.TextBox();
            this._checkAll = new System.Windows.Forms.PictureBox();
            this._search = new System.Windows.Forms.PictureBox();
            this._filter = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this._collection = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._checkAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._search)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(38, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 45;
            this.label1.Text = "URL:";
            // 
            // _url
            // 
            this._url.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._url.Location = new System.Drawing.Point(76, 3);
            this._url.Name = "_url";
            this._url.Size = new System.Drawing.Size(393, 20);
            this._url.TabIndex = 1;
            // 
            // _connect
            // 
            this._connect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._connect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this._connect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._connect.ForeColor = System.Drawing.Color.White;
            this._connect.Location = new System.Drawing.Point(394, 94);
            this._connect.Name = "_connect";
            this._connect.Size = new System.Drawing.Size(75, 23);
            this._connect.TabIndex = 5;
            this._connect.Text = "Connect";
            this._connect.UseVisualStyleBackColor = true;
            this._connect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // _projects
            // 
            this._projects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._projects.CheckBoxes = true;
            this._projects.Location = new System.Drawing.Point(6, 149);
            this._projects.Name = "_projects";
            this._projects.Size = new System.Drawing.Size(466, 128);
            this._projects.TabIndex = 9;
            this._projects.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.ProjectsAfterCheck);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(224, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 13);
            this.label4.TabIndex = 55;
            this.label4.Text = "(stored encrypted)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(14, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 54;
            this.label3.Text = "Password:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(7, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 53;
            this.label2.Text = "User Name:";
            // 
            // _password
            // 
            this._password.Location = new System.Drawing.Point(76, 82);
            this._password.Name = "_password";
            this._password.PasswordChar = '*';
            this._password.Size = new System.Drawing.Size(142, 20);
            this._password.TabIndex = 4;
            // 
            // _userName
            // 
            this._userName.Location = new System.Drawing.Point(76, 56);
            this._userName.Name = "_userName";
            this._userName.Size = new System.Drawing.Size(142, 20);
            this._userName.TabIndex = 3;
            // 
            // _checkAll
            // 
            this._checkAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._checkAll.Image = ((System.Drawing.Image)(resources.GetObject("_checkAll.Image")));
            this._checkAll.Location = new System.Drawing.Point(454, 124);
            this._checkAll.Margin = new System.Windows.Forms.Padding(2);
            this._checkAll.Name = "_checkAll";
            this._checkAll.Size = new System.Drawing.Size(16, 16);
            this._checkAll.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._checkAll.TabIndex = 58;
            this._checkAll.TabStop = false;
            this._checkAll.Click += new System.EventHandler(this._checkAll_Click);
            // 
            // _search
            // 
            this._search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._search.Image = ((System.Drawing.Image)(resources.GetObject("_search.Image")));
            this._search.Location = new System.Drawing.Point(431, 123);
            this._search.Margin = new System.Windows.Forms.Padding(2);
            this._search.Name = "_search";
            this._search.Size = new System.Drawing.Size(16, 16);
            this._search.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._search.TabIndex = 57;
            this._search.TabStop = false;
            this._search.Click += new System.EventHandler(this._search_Click);
            // 
            // _filter
            // 
            this._filter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._filter.Location = new System.Drawing.Point(6, 123);
            this._filter.Name = "_filter";
            this._filter.Size = new System.Drawing.Size(423, 20);
            this._filter.TabIndex = 6;
            this._filter.TextChanged += new System.EventHandler(this._filter_TextChanged);
            // 
            // label5
            // 
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(224, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(246, 20);
            this.label5.TabIndex = 59;
            this.label5.Text = "(or Personal access token)";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(14, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 61;
            this.label6.Text = "Collection:";
            // 
            // _collection
            // 
            this._collection.Location = new System.Drawing.Point(76, 29);
            this._collection.Name = "_collection";
            this._collection.Size = new System.Drawing.Size(142, 20);
            this._collection.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(224, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(223, 20);
            this.label7.TabIndex = 62;
            this.label7.Text = "(empty = see all, requires more permissions)";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ConfigureTfsRest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this._collection);
            this.Controls.Add(this.label5);
            this.Controls.Add(this._checkAll);
            this.Controls.Add(this._search);
            this.Controls.Add(this._filter);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._password);
            this.Controls.Add(this._userName);
            this.Controls.Add(this._projects);
            this.Controls.Add(this._connect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._url);
            this.Name = "ConfigureTfsRest";
            this.Size = new System.Drawing.Size(472, 280);
            ((System.ComponentModel.ISupportInitialize)(this._checkAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._search)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _url;
        private System.Windows.Forms.Label label1;
        private SirenOfShame.Lib.SosButton _connect;
        private SirenOfShame.Lib.Helpers.ThreeStateTreeView _projects;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox _password;
        private System.Windows.Forms.TextBox _userName;
        private System.Windows.Forms.PictureBox _checkAll;
        private System.Windows.Forms.PictureBox _search;
        private System.Windows.Forms.TextBox _filter;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox _collection;
        private System.Windows.Forms.Label label7;
    }
}