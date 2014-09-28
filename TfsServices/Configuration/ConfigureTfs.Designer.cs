using SirenOfShame;
using SirenOfShame.Lib;

namespace TfsServices.Configuration {
	sealed partial class ConfigureTfs {
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigureTfs));
            this._buildConfigurations = new System.Windows.Forms.TreeView();
            this._url = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this._checkAll = new System.Windows.Forms.PictureBox();
            this._search = new System.Windows.Forms.PictureBox();
            this._filter = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this._applyBuildQuality = new System.Windows.Forms.CheckBox();
            this.passwordHintLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.password = new System.Windows.Forms.TextBox();
            this.username = new System.Windows.Forms.TextBox();
            this.someoneElse = new System.Windows.Forms.RadioButton();
            this.windowsCredentials = new System.Windows.Forms.RadioButton();
            this._go = new SirenOfShame.Lib.SosButton();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._checkAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._search)).BeginInit();
            this.SuspendLayout();
            // 
            // _buildConfigurations
            // 
            this._buildConfigurations.CheckBoxes = true;
            this._buildConfigurations.Dock = System.Windows.Forms.DockStyle.Fill;
            this._buildConfigurations.Location = new System.Drawing.Point(0, 138);
            this._buildConfigurations.Name = "_buildConfigurations";
            this._buildConfigurations.Size = new System.Drawing.Size(591, 187);
            this._buildConfigurations.TabIndex = 0;
            this._buildConfigurations.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.BuildConfigurationsAfterCheck);
            // 
            // _url
            // 
            this._url.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._url.Location = new System.Drawing.Point(104, 5);
            this._url.Name = "_url";
            this._url.Size = new System.Drawing.Size(408, 20);
            this._url.TabIndex = 0;
            this._url.TextChanged += new System.EventHandler(this.UrlTextChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this.panel1.Controls.Add(this._checkAll);
            this.panel1.Controls.Add(this._search);
            this.panel1.Controls.Add(this._filter);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this._applyBuildQuality);
            this.panel1.Controls.Add(this.passwordHintLabel);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.passwordLabel);
            this.panel1.Controls.Add(this.usernameLabel);
            this.panel1.Controls.Add(this.password);
            this.panel1.Controls.Add(this.username);
            this.panel1.Controls.Add(this.someoneElse);
            this.panel1.Controls.Add(this.windowsCredentials);
            this.panel1.Controls.Add(this._go);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this._url);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(591, 138);
            this.panel1.TabIndex = 4;
            // 
            // _checkAll
            // 
            this._checkAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._checkAll.Image = ((System.Drawing.Image)(resources.GetObject("_checkAll.Image")));
            this._checkAll.Location = new System.Drawing.Point(572, 120);
            this._checkAll.Margin = new System.Windows.Forms.Padding(2);
            this._checkAll.Name = "_checkAll";
            this._checkAll.Size = new System.Drawing.Size(16, 16);
            this._checkAll.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._checkAll.TabIndex = 58;
            this._checkAll.TabStop = false;
            // 
            // _search
            // 
            this._search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._search.Image = ((System.Drawing.Image)(resources.GetObject("_search.Image")));
            this._search.Location = new System.Drawing.Point(549, 119);
            this._search.Margin = new System.Windows.Forms.Padding(2);
            this._search.Name = "_search";
            this._search.Size = new System.Drawing.Size(16, 16);
            this._search.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._search.TabIndex = 57;
            this._search.TabStop = false;
            this._search.Click += new System.EventHandler(this.Search_Click);
            // 
            // _filter
            // 
            this._filter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._filter.Location = new System.Drawing.Point(0, 118);
            this._filter.Name = "_filter";
            this._filter.Size = new System.Drawing.Size(544, 20);
            this._filter.TabIndex = 56;
            this._filter.TextChanged += new System.EventHandler(this.Filter_TextChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Firebrick;
            this.label2.Location = new System.Drawing.Point(513, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "?";
            this.toolTip1.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(5, 89);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 17);
            this.label7.TabIndex = 16;
            this.label7.Text = "TFS Options:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Firebrick;
            this.label6.Location = new System.Drawing.Point(208, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(13, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "?";
            this.toolTip1.SetToolTip(this.label6, resources.GetString("label6.ToolTip"));
            // 
            // _applyBuildQuality
            // 
            this._applyBuildQuality.AutoSize = true;
            this._applyBuildQuality.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._applyBuildQuality.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._applyBuildQuality.ForeColor = System.Drawing.Color.White;
            this._applyBuildQuality.Location = new System.Drawing.Point(106, 89);
            this._applyBuildQuality.Name = "_applyBuildQuality";
            this._applyBuildQuality.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this._applyBuildQuality.Size = new System.Drawing.Size(106, 17);
            this._applyBuildQuality.TabIndex = 14;
            this._applyBuildQuality.Text = "Use Build Quality";
            this._applyBuildQuality.UseVisualStyleBackColor = true;
            this._applyBuildQuality.CheckedChanged += new System.EventHandler(this._applyBuildQuality_CheckedChanged);
            // 
            // passwordHintLabel
            // 
            this.passwordHintLabel.AutoSize = true;
            this.passwordHintLabel.ForeColor = System.Drawing.Color.White;
            this.passwordHintLabel.Location = new System.Drawing.Point(395, 59);
            this.passwordHintLabel.Name = "passwordHintLabel";
            this.passwordHintLabel.Size = new System.Drawing.Size(92, 13);
            this.passwordHintLabel.TabIndex = 12;
            this.passwordHintLabel.Text = "(stored encrypted)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(5, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "Authentication:";
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.ForeColor = System.Drawing.Color.White;
            this.passwordLabel.Location = new System.Drawing.Point(260, 57);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(56, 13);
            this.passwordLabel.TabIndex = 10;
            this.passwordLabel.Text = "Password:";
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.ForeColor = System.Drawing.Color.White;
            this.usernameLabel.Location = new System.Drawing.Point(103, 59);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(58, 13);
            this.usernameLabel.TabIndex = 9;
            this.usernameLabel.Text = "Username:";
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(318, 54);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(75, 20);
            this.password.TabIndex = 8;
            this.password.UseSystemPasswordChar = true;
            // 
            // username
            // 
            this.username.Location = new System.Drawing.Point(164, 54);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(87, 20);
            this.username.TabIndex = 7;
            // 
            // someoneElse
            // 
            this.someoneElse.AutoSize = true;
            this.someoneElse.ForeColor = System.Drawing.Color.White;
            this.someoneElse.Location = new System.Drawing.Point(256, 34);
            this.someoneElse.Name = "someoneElse";
            this.someoneElse.Size = new System.Drawing.Size(109, 17);
            this.someoneElse.TabIndex = 6;
            this.someoneElse.TabStop = true;
            this.someoneElse.Text = "Be Someone Else";
            this.someoneElse.UseVisualStyleBackColor = true;
            this.someoneElse.CheckedChanged += new System.EventHandler(this.SomeoneElseCheckedChanged);
            // 
            // windowsCredentials
            // 
            this.windowsCredentials.AutoSize = true;
            this.windowsCredentials.Checked = true;
            this.windowsCredentials.ForeColor = System.Drawing.Color.White;
            this.windowsCredentials.Location = new System.Drawing.Point(104, 34);
            this.windowsCredentials.Name = "windowsCredentials";
            this.windowsCredentials.Size = new System.Drawing.Size(146, 17);
            this.windowsCredentials.TabIndex = 5;
            this.windowsCredentials.TabStop = true;
            this.windowsCredentials.Text = "Use Windows Credentials";
            this.windowsCredentials.UseVisualStyleBackColor = true;
            this.windowsCredentials.CheckedChanged += new System.EventHandler(this.WindowsCredentialsCheckedChanged);
            // 
            // _go
            // 
            this._go.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._go.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this._go.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._go.ForeColor = System.Drawing.Color.White;
            this._go.Location = new System.Drawing.Point(537, 3);
            this._go.Name = "_go";
            this._go.Size = new System.Drawing.Size(51, 48);
            this._go.TabIndex = 1;
            this._go.Text = "Go";
            this._go.UseVisualStyleBackColor = true;
            this._go.Click += new System.EventHandler(this.GoClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(5, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Url";
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 15000;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.ReshowDelay = 100;
            // 
            // ConfigureTfs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._buildConfigurations);
            this.Controls.Add(this.panel1);
            this.Name = "ConfigureTfs";
            this.Size = new System.Drawing.Size(591, 325);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._checkAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._search)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.TreeView _buildConfigurations;
		private System.Windows.Forms.TextBox _url;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
        private SosButton _go;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.TextBox username;
        private System.Windows.Forms.RadioButton someoneElse;
        private System.Windows.Forms.RadioButton windowsCredentials;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label passwordHintLabel;
        private System.Windows.Forms.CheckBox _applyBuildQuality;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox _checkAll;
        private System.Windows.Forms.PictureBox _search;
        private System.Windows.Forms.TextBox _filter;
	}
}
