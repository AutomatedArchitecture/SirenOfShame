using SirenOfShame;
using SirenOfShame.Lib;

namespace TravisCiServices.ServerConfiguration
{
    partial class ConfigureTravisCi
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
            this._ownerName = new System.Windows.Forms.TextBox();
            this._add = new SirenOfShame.Lib.SosButton();
            this._projects = new SirenOfShame.Lib.Helpers.ThreeStateTreeView();
            this.label3 = new System.Windows.Forms.Label();
            this._projectName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this._typePublic = new System.Windows.Forms.RadioButton();
            this._typePrivate = new System.Windows.Forms.RadioButton();
            this._typeEnterprise = new System.Windows.Forms.RadioButton();
            this._travisUrl = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.authTokenLabel = new System.Windows.Forms.Label();
            this._travisApiAccessToken = new System.Windows.Forms.TextBox();
            this._generateAuthToken = new SirenOfShame.Lib.SosButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 43;
            this.label1.Text = "Owner:";
            // 
            // _ownerName
            // 
            this._ownerName.Location = new System.Drawing.Point(52, 3);
            this._ownerName.Name = "_ownerName";
            this._ownerName.Size = new System.Drawing.Size(183, 20);
            this._ownerName.TabIndex = 0;
            // 
            // _add
            // 
            this._add.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._add.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this._add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._add.ForeColor = System.Drawing.Color.White;
            this._add.Location = new System.Drawing.Point(403, 98);
            this._add.Name = "_add";
            this._add.Size = new System.Drawing.Size(85, 23);
            this._add.TabIndex = 2;
            this._add.Text = "Add";
            this._add.UseVisualStyleBackColor = true;
            this._add.Click += new System.EventHandler(this.Add_Click);
            // 
            // _projects
            // 
            this._projects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._projects.CheckBoxes = true;
            this._projects.Location = new System.Drawing.Point(3, 127);
            this._projects.Name = "_projects";
            this._projects.Size = new System.Drawing.Size(485, 150);
            this._projects.TabIndex = 4;
            this._projects.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.ProjectsAfterCheck);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(241, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 49;
            this.label3.Text = "Project:";
            // 
            // _projectName
            // 
            this._projectName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._projectName.Location = new System.Drawing.Point(290, 3);
            this._projectName.Name = "_projectName";
            this._projectName.Size = new System.Drawing.Size(198, 20);
            this._projectName.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 55;
            this.label4.Text = "Type:";
            // 
            // _typePublic
            // 
            this._typePublic.AutoSize = true;
            this._typePublic.Checked = true;
            this._typePublic.Location = new System.Drawing.Point(54, 30);
            this._typePublic.Name = "_typePublic";
            this._typePublic.Size = new System.Drawing.Size(54, 17);
            this._typePublic.TabIndex = 50;
            this._typePublic.TabStop = true;
            this._typePublic.Text = "Public";
            this._typePublic.UseVisualStyleBackColor = true;
            this._typePublic.CheckedChanged += new System.EventHandler(this._typePublic_CheckedChanged);
            // 
            // _typePrivate
            // 
            this._typePrivate.AutoSize = true;
            this._typePrivate.Location = new System.Drawing.Point(108, 30);
            this._typePrivate.Name = "_typePrivate";
            this._typePrivate.Size = new System.Drawing.Size(58, 17);
            this._typePrivate.TabIndex = 51;
            this._typePrivate.Text = "Private";
            this._typePrivate.UseVisualStyleBackColor = true;
            this._typePrivate.CheckedChanged += new System.EventHandler(this._typePrivate_CheckedChanged);
            // 
            // _typeEnterprise
            // 
            this._typeEnterprise.AutoSize = true;
            this._typeEnterprise.Location = new System.Drawing.Point(166, 30);
            this._typeEnterprise.Name = "_typeEnterprise";
            this._typeEnterprise.Size = new System.Drawing.Size(72, 17);
            this._typeEnterprise.TabIndex = 52;
            this._typeEnterprise.Text = "Enterprise";
            this._typeEnterprise.UseVisualStyleBackColor = true;
            this._typeEnterprise.CheckedChanged += new System.EventHandler(this._typeEnterprise_CheckedChanged);
            // 
            // _travisUrl
            // 
            this._travisUrl.Location = new System.Drawing.Point(290, 30);
            this._travisUrl.Name = "_travisUrl";
            this._travisUrl.Size = new System.Drawing.Size(198, 20);
            this._travisUrl.TabIndex = 53;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(261, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 54;
            this.label2.Text = "Url:";
            // 
            // authTokenLabel
            // 
            this.authTokenLabel.AutoSize = true;
            this.authTokenLabel.Location = new System.Drawing.Point(105, 61);
            this.authTokenLabel.Name = "authTokenLabel";
            this.authTokenLabel.Size = new System.Drawing.Size(41, 13);
            this.authTokenLabel.TabIndex = 56;
            this.authTokenLabel.Text = "Token:";
            // 
            // _travisApiAccessToken
            // 
            this._travisApiAccessToken.Location = new System.Drawing.Point(152, 58);
            this._travisApiAccessToken.Name = "_travisApiAccessToken";
            this._travisApiAccessToken.Size = new System.Drawing.Size(83, 20);
            this._travisApiAccessToken.TabIndex = 57;
            // 
            // _generateAuthToken
            // 
            this._generateAuthToken.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._generateAuthToken.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this._generateAuthToken.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._generateAuthToken.ForeColor = System.Drawing.Color.White;
            this._generateAuthToken.Location = new System.Drawing.Point(244, 56);
            this._generateAuthToken.Name = "_generateAuthToken";
            this._generateAuthToken.Size = new System.Drawing.Size(244, 23);
            this._generateAuthToken.TabIndex = 58;
            this._generateAuthToken.Text = "Generate New Auth Token";
            this._generateAuthToken.UseVisualStyleBackColor = true;
            this._generateAuthToken.Click += new System.EventHandler(this.GenerateAuthToken_Click);
            // 
            // ConfigureTravisCi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._generateAuthToken);
            this.Controls.Add(this._travisApiAccessToken);
            this.Controls.Add(this.authTokenLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._travisUrl);
            this.Controls.Add(this._typeEnterprise);
            this.Controls.Add(this._typePrivate);
            this.Controls.Add(this._typePublic);
            this.Controls.Add(this._projectName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._projects);
            this.Controls.Add(this._add);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._ownerName);
            this.Name = "ConfigureTravisCi";
            this.Size = new System.Drawing.Size(491, 280);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _ownerName;
        private SosButton _add;
        private SirenOfShame.Lib.Helpers.ThreeStateTreeView _projects;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox _projectName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton _typePublic;
        private System.Windows.Forms.RadioButton _typePrivate;
        private System.Windows.Forms.RadioButton _typeEnterprise;
        private System.Windows.Forms.TextBox _travisUrl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label authTokenLabel;
        private System.Windows.Forms.TextBox _travisApiAccessToken;
        private SosButton _generateAuthToken;
    }
}
