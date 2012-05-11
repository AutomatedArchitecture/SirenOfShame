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
            this._buildConfigurations = new System.Windows.Forms.TreeView();
            this._url = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.password = new System.Windows.Forms.TextBox();
            this.username = new System.Windows.Forms.TextBox();
            this.someoneElse = new System.Windows.Forms.RadioButton();
            this.windowsCredentials = new System.Windows.Forms.RadioButton();
            this._go = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _buildConfigurations
            // 
            this._buildConfigurations.CheckBoxes = true;
            this._buildConfigurations.Dock = System.Windows.Forms.DockStyle.Fill;
            this._buildConfigurations.Location = new System.Drawing.Point(0, 83);
            this._buildConfigurations.Name = "_buildConfigurations";
            this._buildConfigurations.Size = new System.Drawing.Size(591, 144);
            this._buildConfigurations.TabIndex = 0;
            this._buildConfigurations.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.BuildConfigurationsAfterCheck);
            // 
            // _url
            // 
            this._url.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._url.Location = new System.Drawing.Point(97, 5);
            this._url.Name = "_url";
            this._url.Size = new System.Drawing.Size(434, 20);
            this._url.TabIndex = 0;
            this._url.TextChanged += new System.EventHandler(this.UrlTextChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
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
            this.panel1.Size = new System.Drawing.Size(591, 83);
            this.panel1.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(256, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Password:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(99, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Username:";
            // 
            // password
            // 
            this.password.Enabled = false;
            this.password.Location = new System.Drawing.Point(318, 57);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(75, 20);
            this.password.TabIndex = 8;
            this.password.UseSystemPasswordChar = true;
            // 
            // username
            // 
            this.username.Enabled = false;
            this.username.Location = new System.Drawing.Point(163, 57);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(87, 20);
            this.username.TabIndex = 7;
            // 
            // someoneElse
            // 
            this.someoneElse.AutoSize = true;
            this.someoneElse.Location = new System.Drawing.Point(249, 34);
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
            this.windowsCredentials.Location = new System.Drawing.Point(97, 34);
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
            this.label1.Location = new System.Drawing.Point(5, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Url";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Authenciation:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(399, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "(stored encrypted)";
            // 
            // ConfigureTfs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._buildConfigurations);
            this.Controls.Add(this.panel1);
            this.Name = "ConfigureTfs";
            this.Size = new System.Drawing.Size(591, 227);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.TreeView _buildConfigurations;
		private System.Windows.Forms.TextBox _url;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _go;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.TextBox username;
        private System.Windows.Forms.RadioButton someoneElse;
        private System.Windows.Forms.RadioButton windowsCredentials;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
	}
}
