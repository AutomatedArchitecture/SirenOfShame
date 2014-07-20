namespace SirenOfShame.Extruder
{
    partial class Form1
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
            this._connect = new System.Windows.Forms.Button();
            this._disconnect = new System.Windows.Forms.Button();
            this._username = new System.Windows.Forms.TextBox();
            this._password = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this._myname = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // _connect
            // 
            this._connect.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._connect.Location = new System.Drawing.Point(12, 90);
            this._connect.Name = "_connect";
            this._connect.Size = new System.Drawing.Size(260, 34);
            this._connect.TabIndex = 0;
            this._connect.Text = "Connect";
            this._connect.UseVisualStyleBackColor = true;
            this._connect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // _disconnect
            // 
            this._disconnect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._disconnect.Location = new System.Drawing.Point(12, 130);
            this._disconnect.Name = "_disconnect";
            this._disconnect.Size = new System.Drawing.Size(260, 31);
            this._disconnect.TabIndex = 1;
            this._disconnect.Text = "Disconnect";
            this._disconnect.UseVisualStyleBackColor = true;
            // 
            // _username
            // 
            this._username.Location = new System.Drawing.Point(94, 12);
            this._username.Name = "_username";
            this._username.Size = new System.Drawing.Size(178, 20);
            this._username.TabIndex = 2;
            // 
            // _password
            // 
            this._password.Location = new System.Drawing.Point(94, 38);
            this._password.Name = "_password";
            this._password.Size = new System.Drawing.Size(178, 20);
            this._password.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "My Name";
            // 
            // _myname
            // 
            this._myname.Location = new System.Drawing.Point(94, 64);
            this._myname.Name = "_myname";
            this._myname.Size = new System.Drawing.Size(178, 20);
            this._myname.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 173);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._myname);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._password);
            this.Controls.Add(this._username);
            this.Controls.Add(this._disconnect);
            this.Controls.Add(this._connect);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _connect;
        private System.Windows.Forms.Button _disconnect;
        private System.Windows.Forms.TextBox _username;
        private System.Windows.Forms.TextBox _password;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox _myname;
    }
}

