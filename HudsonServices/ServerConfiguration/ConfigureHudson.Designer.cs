using SirenOfShame;
using SirenOfShame.Lib;

namespace HudsonServices.ServerConfiguration
{
    partial class ConfigureHudson
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigureHudson));
            this.label1 = new System.Windows.Forms.Label();
            this._url = new System.Windows.Forms.TextBox();
            this._connect = new SirenOfShame.Lib.SosButton();
            this._projects = new SirenOfShame.Lib.Helpers.ThreeStateTreeView();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._password = new System.Windows.Forms.TextBox();
            this._userName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this._treatUnstableAsSuccess = new System.Windows.Forms.CheckBox();
            this._filter = new System.Windows.Forms.TextBox();
            this._search = new System.Windows.Forms.PictureBox();
            this._checkAll = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this._search)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._checkAll)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(57, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 20);
            this.label1.TabIndex = 43;
            this.label1.Text = "URL:";
            // 
            // _url
            // 
            this._url.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._url.Location = new System.Drawing.Point(114, 5);
            this._url.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this._url.Name = "_url";
            this._url.Size = new System.Drawing.Size(460, 26);
            this._url.TabIndex = 0;
            // 
            // _connect
            // 
            this._connect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._connect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this._connect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._connect.ForeColor = System.Drawing.Color.White;
            this._connect.Location = new System.Drawing.Point(464, 125);
            this._connect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this._connect.Name = "_connect";
            this._connect.Size = new System.Drawing.Size(112, 35);
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
            this._projects.Location = new System.Drawing.Point(4, 201);
            this._projects.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this._projects.Name = "_projects";
            this._projects.Size = new System.Drawing.Size(570, 223);
            this._projects.TabIndex = 4;
            this._projects.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.ProjectsAfterCheck);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(336, 89);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 20);
            this.label4.TabIndex = 50;
            this.label4.Text = "(stored encrypted)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(21, 89);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 20);
            this.label3.TabIndex = 49;
            this.label3.Text = "Password:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(10, 49);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 20);
            this.label2.TabIndex = 48;
            this.label2.Text = "User Name:";
            // 
            // _password
            // 
            this._password.Location = new System.Drawing.Point(114, 85);
            this._password.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this._password.Name = "_password";
            this._password.PasswordChar = '*';
            this._password.Size = new System.Drawing.Size(211, 26);
            this._password.TabIndex = 2;
            // 
            // _userName
            // 
            this._userName.Location = new System.Drawing.Point(114, 45);
            this._userName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this._userName.Name = "_userName";
            this._userName.Size = new System.Drawing.Size(211, 26);
            this._userName.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(14, 129);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 25);
            this.label7.TabIndex = 52;
            this.label7.Text = "Options:";
            // 
            // _treatUnstableAsSuccess
            // 
            this._treatUnstableAsSuccess.AutoSize = true;
            this._treatUnstableAsSuccess.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._treatUnstableAsSuccess.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._treatUnstableAsSuccess.ForeColor = System.Drawing.Color.White;
            this._treatUnstableAsSuccess.Location = new System.Drawing.Point(114, 129);
            this._treatUnstableAsSuccess.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this._treatUnstableAsSuccess.Name = "_treatUnstableAsSuccess";
            this._treatUnstableAsSuccess.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this._treatUnstableAsSuccess.Size = new System.Drawing.Size(233, 24);
            this._treatUnstableAsSuccess.TabIndex = 51;
            this._treatUnstableAsSuccess.Text = "Treat unstable as success";
            this._treatUnstableAsSuccess.UseVisualStyleBackColor = true;
            this._treatUnstableAsSuccess.CheckedChanged += new System.EventHandler(this._treatUnstableAsSuccess_CheckedChanged);
            // 
            // _filter
            // 
            this._filter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._filter.Location = new System.Drawing.Point(4, 170);
            this._filter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this._filter.Name = "_filter";
            this._filter.Size = new System.Drawing.Size(505, 26);
            this._filter.TabIndex = 53;
            this._filter.TextChanged += new System.EventHandler(this._filter_TextChanged);
            // 
            // _search
            // 
            this._search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._search.Image = ((System.Drawing.Image)(resources.GetObject("_search.Image")));
            this._search.Location = new System.Drawing.Point(514, 170);
            this._search.Name = "_search";
            this._search.Size = new System.Drawing.Size(24, 24);
            this._search.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._search.TabIndex = 54;
            this._search.TabStop = false;
            this._search.Click += new System.EventHandler(this._search_Click);
            // 
            // _checkAll
            // 
            this._checkAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._checkAll.Image = ((System.Drawing.Image)(resources.GetObject("_checkAll.Image")));
            this._checkAll.Location = new System.Drawing.Point(549, 171);
            this._checkAll.Name = "_checkAll";
            this._checkAll.Size = new System.Drawing.Size(24, 24);
            this._checkAll.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._checkAll.TabIndex = 55;
            this._checkAll.TabStop = false;
            this._checkAll.Click += new System.EventHandler(this._checkAll_Click);
            // 
            // ConfigureHudson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this.Controls.Add(this._checkAll);
            this.Controls.Add(this._search);
            this.Controls.Add(this._filter);
            this.Controls.Add(this.label7);
            this.Controls.Add(this._treatUnstableAsSuccess);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._password);
            this.Controls.Add(this._userName);
            this.Controls.Add(this._projects);
            this.Controls.Add(this._connect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._url);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ConfigureHudson";
            this.Size = new System.Drawing.Size(580, 431);
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
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox _password;
        private System.Windows.Forms.TextBox _userName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox _treatUnstableAsSuccess;
        private System.Windows.Forms.TextBox _filter;
        private System.Windows.Forms.PictureBox _search;
        private System.Windows.Forms.PictureBox _checkAll;
    }
}
