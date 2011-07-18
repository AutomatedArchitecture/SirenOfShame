namespace SirenOfShame
{
    partial class BuildFailedMessageBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BuildFailedMessageBox));
            this._body = new System.Windows.Forms.Label();
            this._ok = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _body
            // 
            this._body.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._body.Location = new System.Drawing.Point(12, 9);
            this._body.Name = "_body";
            this._body.Size = new System.Drawing.Size(320, 95);
            this._body.TabIndex = 0;
            this._body.Text = "label1";
            // 
            // _ok
            // 
            this._ok.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._ok.Location = new System.Drawing.Point(15, 114);
            this._ok.Name = "_ok";
            this._ok.Size = new System.Drawing.Size(317, 23);
            this._ok.TabIndex = 1;
            this._ok.Text = "OK, whatever";
            this._ok.UseVisualStyleBackColor = true;
            this._ok.Click += new System.EventHandler(this.OkClick);
            // 
            // BuildFailedMessageBox
            // 
            this.AcceptButton = this._ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 149);
            this.Controls.Add(this._ok);
            this.Controls.Add(this._body);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BuildFailedMessageBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BuildFailedMessageBox";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label _body;
        private System.Windows.Forms.Button _ok;
    }
}