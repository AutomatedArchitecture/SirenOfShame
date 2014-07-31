using SirenOfShame.Extruder.Controls;

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
            this.label3 = new System.Windows.Forms.Label();
            this._myname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._password = new System.Windows.Forms.TextBox();
            this._username = new System.Windows.Forms.TextBox();
            this._testSiren = new SirenOfShame.Extruder.Controls.ExtruderButton();
            this._connectButton = new SirenOfShame.Extruder.Controls.ExtruderButton();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(7, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 20);
            this.label3.TabIndex = 16;
            this.label3.Text = "My Name";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _myname
            // 
            this._myname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._myname.Location = new System.Drawing.Point(75, 55);
            this._myname.Name = "_myname";
            this._myname.Size = new System.Drawing.Size(178, 20);
            this._myname.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(7, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 20);
            this.label2.TabIndex = 14;
            this.label2.Text = "Password";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(7, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 20);
            this.label1.TabIndex = 13;
            this.label1.Text = "Username";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _password
            // 
            this._password.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._password.Location = new System.Drawing.Point(75, 29);
            this._password.Name = "_password";
            this._password.Size = new System.Drawing.Size(178, 20);
            this._password.TabIndex = 12;
            // 
            // _username
            // 
            this._username.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._username.Location = new System.Drawing.Point(75, 3);
            this._username.Name = "_username";
            this._username.Size = new System.Drawing.Size(178, 20);
            this._username.TabIndex = 11;
            // 
            // _testSiren
            // 
            this._testSiren.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._testSiren.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this._testSiren.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this._testSiren.FlatAppearance.BorderSize = 0;
            this._testSiren.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(55)))), ((int)(((byte)(0)))));
            this._testSiren.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(65)))), ((int)(((byte)(0)))));
            this._testSiren.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._testSiren.ForeColor = System.Drawing.Color.White;
            this._testSiren.Location = new System.Drawing.Point(161, 83);
            this._testSiren.Name = "_testSiren";
            this._testSiren.Size = new System.Drawing.Size(92, 23);
            this._testSiren.TabIndex = 17;
            this._testSiren.Text = "Test Siren";
            this._testSiren.UseVisualStyleBackColor = false;
            this._testSiren.Click += new System.EventHandler(this.TestSiren_Click);
            // 
            // _connectButton
            // 
            this._connectButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._connectButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this._connectButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this._connectButton.FlatAppearance.BorderSize = 0;
            this._connectButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(55)))), ((int)(((byte)(0)))));
            this._connectButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(65)))), ((int)(((byte)(0)))));
            this._connectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._connectButton.ForeColor = System.Drawing.Color.White;
            this._connectButton.Location = new System.Drawing.Point(75, 81);
            this._connectButton.Name = "_connectButton";
            this._connectButton.Size = new System.Drawing.Size(80, 26);
            this._connectButton.TabIndex = 10;
            this._connectButton.Text = "Connect";
            this._connectButton.UseVisualStyleBackColor = false;
            this._connectButton.Click += new System.EventHandler(this.Connect_Click);
            // 
            // SettingsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.Controls.Add(this._testSiren);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._myname);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._password);
            this.Controls.Add(this._username);
            this.Controls.Add(this._connectButton);
            this.Name = "SettingsPage";
            this.Size = new System.Drawing.Size(264, 155);
            this.Load += new System.EventHandler(this.Settings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox _myname;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _password;
        private System.Windows.Forms.TextBox _username;
        private ExtruderButton _connectButton;
        private Controls.ExtruderButton _testSiren;

    }
}
