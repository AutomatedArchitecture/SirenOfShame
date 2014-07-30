namespace SirenOfShame.Extruder
{
    partial class SettingsPage
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
            this._testSiren = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this._myname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._password = new System.Windows.Forms.TextBox();
            this._username = new System.Windows.Forms.TextBox();
            this._connectButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _testSiren
            // 
            this._testSiren.Location = new System.Drawing.Point(180, 85);
            this._testSiren.Name = "_testSiren";
            this._testSiren.Size = new System.Drawing.Size(92, 26);
            this._testSiren.TabIndex = 17;
            this._testSiren.Text = "Test Siren";
            this._testSiren.UseVisualStyleBackColor = true;
            this._testSiren.Click += new System.EventHandler(this.TestSiren_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "My Name";
            // 
            // _myname
            // 
            this._myname.Location = new System.Drawing.Point(94, 59);
            this._myname.Name = "_myname";
            this._myname.Size = new System.Drawing.Size(178, 20);
            this._myname.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Username";
            // 
            // _password
            // 
            this._password.Location = new System.Drawing.Point(94, 33);
            this._password.Name = "_password";
            this._password.Size = new System.Drawing.Size(178, 20);
            this._password.TabIndex = 12;
            // 
            // _username
            // 
            this._username.Location = new System.Drawing.Point(94, 7);
            this._username.Name = "_username";
            this._username.Size = new System.Drawing.Size(178, 20);
            this._username.TabIndex = 11;
            // 
            // _connectButton
            // 
            this._connectButton.Location = new System.Drawing.Point(94, 85);
            this._connectButton.Name = "_connectButton";
            this._connectButton.Size = new System.Drawing.Size(80, 26);
            this._connectButton.TabIndex = 10;
            this._connectButton.Text = "Connect";
            this._connectButton.UseVisualStyleBackColor = true;
            this._connectButton.Click += new System.EventHandler(this.Connect_Click);
            // 
            // SettingsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._testSiren);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._myname);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._password);
            this.Controls.Add(this._username);
            this.Controls.Add(this._connectButton);
            this.Name = "SettingsPage";
            this.Size = new System.Drawing.Size(283, 121);
            this.Load += new System.EventHandler(this.Settings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _testSiren;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox _myname;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _password;
        private System.Windows.Forms.TextBox _username;
        private System.Windows.Forms.Button _connectButton;

    }
}
