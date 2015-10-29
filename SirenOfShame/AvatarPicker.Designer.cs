namespace SirenOfShame
{
    partial class AvatarPicker
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this._gravatar = new SirenOfShame.Avatar();
            this.previewButton = new SirenOfShame.Lib.SosButton();
            this.saveButton = new SirenOfShame.Lib.SosButton();
            this.label2 = new System.Windows.Forms.Label();
            this.emailTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this._getFromUrl = new SirenOfShame.Lib.SosButton();
            this._url = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this._errorMessage = new System.Windows.Forms.Label();
            this._adDomain = new System.Windows.Forms.TextBox();
            this._adUser = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this._importFromAd = new SirenOfShame.Lib.SosButton();
            this._saveCustomImage = new SirenOfShame.Lib.SosButton();
            this._croppedCustom = new System.Windows.Forms.PictureBox();
            this._selectImage = new SirenOfShame.Lib.SosButton();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._croppedCustom)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(392, 200);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.flowLayoutPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(384, 174);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Dogs and Cats";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(378, 168);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this.tabPage2.Controls.Add(this._gravatar);
            this.tabPage2.Controls.Add(this.previewButton);
            this.tabPage2.Controls.Add(this.saveButton);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.emailTextbox);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(384, 174);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Gravatar";
            // 
            // _gravatar
            // 
            this._gravatar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this._gravatar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._gravatar.Cursor = System.Windows.Forms.Cursors.Hand;
            this._gravatar.ImageIndex = -1;
            this._gravatar.Location = new System.Drawing.Point(8, 9);
            this._gravatar.Name = "_gravatar";
            this._gravatar.Size = new System.Drawing.Size(48, 48);
            this._gravatar.TabIndex = 8;
            // 
            // previewButton
            // 
            this.previewButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.previewButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this.previewButton.FlatAppearance.BorderSize = 0;
            this.previewButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(55)))), ((int)(((byte)(0)))));
            this.previewButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(65)))), ((int)(((byte)(0)))));
            this.previewButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.previewButton.ForeColor = System.Drawing.Color.White;
            this.previewButton.Location = new System.Drawing.Point(200, 137);
            this.previewButton.Name = "previewButton";
            this.previewButton.Size = new System.Drawing.Size(85, 23);
            this.previewButton.TabIndex = 7;
            this.previewButton.Text = "Preview";
            this.previewButton.UseVisualStyleBackColor = false;
            this.previewButton.Click += new System.EventHandler(this.PreviewButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.saveButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this.saveButton.FlatAppearance.BorderSize = 0;
            this.saveButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(55)))), ((int)(((byte)(0)))));
            this.saveButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(65)))), ((int)(((byte)(0)))));
            this.saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveButton.ForeColor = System.Drawing.Color.White;
            this.saveButton.Location = new System.Drawing.Point(291, 137);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(85, 23);
            this.saveButton.TabIndex = 6;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = false;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(118, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(258, 50);
            this.label2.TabIndex = 4;
            this.label2.Text = "* After selling this address to the great and powerful spambots we will DDOS it t" +
    "o ensure you truly hate us.  No really, we just use it to calculate a hash.";
            // 
            // emailTextbox
            // 
            this.emailTextbox.Location = new System.Drawing.Point(121, 6);
            this.emailTextbox.Name = "emailTextbox";
            this.emailTextbox.Size = new System.Drawing.Size(255, 20);
            this.emailTextbox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(76, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "E-mail*";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this.tabPage3.Controls.Add(this._getFromUrl);
            this.tabPage3.Controls.Add(this._url);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this._errorMessage);
            this.tabPage3.Controls.Add(this._adDomain);
            this.tabPage3.Controls.Add(this._adUser);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this._importFromAd);
            this.tabPage3.Controls.Add(this._saveCustomImage);
            this.tabPage3.Controls.Add(this._croppedCustom);
            this.tabPage3.Controls.Add(this._selectImage);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(384, 174);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Custom";
            // 
            // _getFromUrl
            // 
            this._getFromUrl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this._getFromUrl.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this._getFromUrl.FlatAppearance.BorderSize = 0;
            this._getFromUrl.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(55)))), ((int)(((byte)(0)))));
            this._getFromUrl.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(65)))), ((int)(((byte)(0)))));
            this._getFromUrl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._getFromUrl.ForeColor = System.Drawing.Color.White;
            this._getFromUrl.Location = new System.Drawing.Point(343, 5);
            this._getFromUrl.Name = "_getFromUrl";
            this._getFromUrl.Size = new System.Drawing.Size(33, 23);
            this._getFromUrl.TabIndex = 13;
            this._getFromUrl.Text = "Get";
            this._getFromUrl.UseVisualStyleBackColor = false;
            this._getFromUrl.Click += new System.EventHandler(this.GetFromUrl_Click);
            // 
            // _url
            // 
            this._url.Location = new System.Drawing.Point(223, 7);
            this._url.Name = "_url";
            this._url.Size = new System.Drawing.Size(114, 20);
            this._url.TabIndex = 12;
            this._url.Text = "http://";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(148, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Or from URL:";
            // 
            // _errorMessage
            // 
            this._errorMessage.ForeColor = System.Drawing.Color.Red;
            this._errorMessage.Location = new System.Drawing.Point(8, 62);
            this._errorMessage.Name = "_errorMessage";
            this._errorMessage.Size = new System.Drawing.Size(134, 102);
            this._errorMessage.TabIndex = 10;
            this._errorMessage.Text = "[error message]";
            this._errorMessage.Visible = false;
            // 
            // _adDomain
            // 
            this._adDomain.Location = new System.Drawing.Point(200, 57);
            this._adDomain.Name = "_adDomain";
            this._adDomain.Size = new System.Drawing.Size(173, 20);
            this._adDomain.TabIndex = 9;
            // 
            // _adUser
            // 
            this._adUser.Location = new System.Drawing.Point(200, 81);
            this._adUser.Name = "_adUser";
            this._adUser.Size = new System.Drawing.Size(173, 20);
            this._adUser.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(148, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(225, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Or, Import From Active Directory (experimental)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(148, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Domain:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(148, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "User:";
            // 
            // _importFromAd
            // 
            this._importFromAd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this._importFromAd.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this._importFromAd.FlatAppearance.BorderSize = 0;
            this._importFromAd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(55)))), ((int)(((byte)(0)))));
            this._importFromAd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(65)))), ((int)(((byte)(0)))));
            this._importFromAd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._importFromAd.ForeColor = System.Drawing.Color.White;
            this._importFromAd.Location = new System.Drawing.Point(151, 107);
            this._importFromAd.Name = "_importFromAd";
            this._importFromAd.Size = new System.Drawing.Size(222, 21);
            this._importFromAd.TabIndex = 4;
            this._importFromAd.Text = "Import From AD";
            this._importFromAd.UseVisualStyleBackColor = false;
            this._importFromAd.Click += new System.EventHandler(this.ImportFromAd_Click);
            // 
            // _saveCustomImage
            // 
            this._saveCustomImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this._saveCustomImage.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this._saveCustomImage.FlatAppearance.BorderSize = 0;
            this._saveCustomImage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(55)))), ((int)(((byte)(0)))));
            this._saveCustomImage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(65)))), ((int)(((byte)(0)))));
            this._saveCustomImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._saveCustomImage.ForeColor = System.Drawing.Color.White;
            this._saveCustomImage.Location = new System.Drawing.Point(293, 141);
            this._saveCustomImage.Name = "_saveCustomImage";
            this._saveCustomImage.Size = new System.Drawing.Size(83, 23);
            this._saveCustomImage.TabIndex = 3;
            this._saveCustomImage.Text = "Save";
            this._saveCustomImage.UseVisualStyleBackColor = false;
            this._saveCustomImage.Click += new System.EventHandler(this.SaveCustomImage_Click);
            // 
            // _croppedCustom
            // 
            this._croppedCustom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._croppedCustom.Cursor = System.Windows.Forms.Cursors.Hand;
            this._croppedCustom.Location = new System.Drawing.Point(8, 6);
            this._croppedCustom.Name = "_croppedCustom";
            this._croppedCustom.Size = new System.Drawing.Size(48, 48);
            this._croppedCustom.TabIndex = 2;
            this._croppedCustom.TabStop = false;
            this._croppedCustom.Click += new System.EventHandler(this.CroppedCustom_Click);
            // 
            // _selectImage
            // 
            this._selectImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this._selectImage.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this._selectImage.FlatAppearance.BorderSize = 0;
            this._selectImage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(55)))), ((int)(((byte)(0)))));
            this._selectImage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(65)))), ((int)(((byte)(0)))));
            this._selectImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._selectImage.ForeColor = System.Drawing.Color.White;
            this._selectImage.Location = new System.Drawing.Point(62, 6);
            this._selectImage.Name = "_selectImage";
            this._selectImage.Size = new System.Drawing.Size(64, 48);
            this._selectImage.TabIndex = 0;
            this._selectImage.Text = "Select File On Disk";
            this._selectImage.UseVisualStyleBackColor = false;
            this._selectImage.Click += new System.EventHandler(this.SelectImage_Click);
            // 
            // AvatarPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(392, 200);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AvatarPicker";
            this.ShowInTaskbar = false;
            this.Text = "AvatarPicker";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._croppedCustom)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox emailTextbox;
        private System.Windows.Forms.Label label1;
        private Lib.SosButton previewButton;
        private Lib.SosButton saveButton;
        private Avatar _gravatar;
        private System.Windows.Forms.TabPage tabPage3;
        private Lib.SosButton _selectImage;
        private System.Windows.Forms.PictureBox _croppedCustom;
        private Lib.SosButton _saveCustomImage;
        private System.Windows.Forms.TextBox _adDomain;
        private System.Windows.Forms.TextBox _adUser;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private Lib.SosButton _importFromAd;
        private System.Windows.Forms.Label _errorMessage;
        private Lib.SosButton _getFromUrl;
        private System.Windows.Forms.TextBox _url;
        private System.Windows.Forms.Label label6;

    }
}