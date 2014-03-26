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
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(38, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 43;
            this.label1.Text = "URL:";
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
            this._connect.Location = new System.Drawing.Point(309, 81);
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
            this._projects.Location = new System.Drawing.Point(3, 110);
            this._projects.Name = "_projects";
            this._projects.Size = new System.Drawing.Size(381, 167);
            this._projects.TabIndex = 4;
            this._projects.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.ProjectsAfterCheck);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(224, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 13);
            this.label4.TabIndex = 50;
            this.label4.Text = "(stored encrypted)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(14, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 49;
            this.label3.Text = "Password:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(7, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 48;
            this.label2.Text = "User Name:";
            // 
            // _password
            // 
            this._password.Location = new System.Drawing.Point(76, 55);
            this._password.Name = "_password";
            this._password.PasswordChar = '*';
            this._password.Size = new System.Drawing.Size(142, 20);
            this._password.TabIndex = 2;
            // 
            // _userName
            // 
            this._userName.Location = new System.Drawing.Point(76, 29);
            this._userName.Name = "_userName";
            this._userName.Size = new System.Drawing.Size(142, 20);
            this._userName.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(9, 84);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 17);
            this.label7.TabIndex = 52;
            this.label7.Text = "Options:";
            // 
            // _treatUnstableAsSuccess
            // 
            this._treatUnstableAsSuccess.AutoSize = true;
            this._treatUnstableAsSuccess.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._treatUnstableAsSuccess.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._treatUnstableAsSuccess.ForeColor = System.Drawing.Color.White;
            this._treatUnstableAsSuccess.Location = new System.Drawing.Point(76, 84);
            this._treatUnstableAsSuccess.Name = "_treatUnstableAsSuccess";
            this._treatUnstableAsSuccess.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this._treatUnstableAsSuccess.Size = new System.Drawing.Size(150, 17);
            this._treatUnstableAsSuccess.TabIndex = 51;
            this._treatUnstableAsSuccess.Text = "Treat unstable as success";
            this._treatUnstableAsSuccess.UseVisualStyleBackColor = true;
            this._treatUnstableAsSuccess.CheckedChanged += new System.EventHandler(this._treatUnstableAsSuccess_CheckedChanged);
            // 
            // ConfigureHudson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
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
            this.Name = "ConfigureHudson";
            this.Size = new System.Drawing.Size(387, 280);
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
    }
}
