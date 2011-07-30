namespace SirenOfShame.Lib
{
    partial class ExceptionMessageBox
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
            this._message = new System.Windows.Forms.Label();
            this._ok = new System.Windows.Forms.Button();
            this._showMore = new System.Windows.Forms.Button();
            this._exception = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // _message
            // 
            this._message.Location = new System.Drawing.Point(12, 9);
            this._message.Name = "_message";
            this._message.Size = new System.Drawing.Size(376, 41);
            this._message.TabIndex = 0;
            this._message.Text = "Error Message";
            // 
            // _ok
            // 
            this._ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._ok.Location = new System.Drawing.Point(313, 60);
            this._ok.Name = "_ok";
            this._ok.Size = new System.Drawing.Size(75, 23);
            this._ok.TabIndex = 1;
            this._ok.Text = "OK";
            this._ok.UseVisualStyleBackColor = true;
            this._ok.Click += new System.EventHandler(this.OkClick);
            // 
            // _showMore
            // 
            this._showMore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._showMore.Location = new System.Drawing.Point(232, 59);
            this._showMore.Name = "_showMore";
            this._showMore.Size = new System.Drawing.Size(75, 23);
            this._showMore.TabIndex = 2;
            this._showMore.Text = "More...";
            this._showMore.UseVisualStyleBackColor = true;
            this._showMore.Click += new System.EventHandler(this.ShowMoreClick);
            // 
            // _exception
            // 
            this._exception.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._exception.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._exception.Location = new System.Drawing.Point(12, 53);
            this._exception.Multiline = true;
            this._exception.Name = "_exception";
            this._exception.ReadOnly = true;
            this._exception.Size = new System.Drawing.Size(376, 0);
            this._exception.TabIndex = 3;
            this._exception.WordWrap = false;
            // 
            // ExceptionMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 95);
            this.Controls.Add(this._exception);
            this.Controls.Add(this._showMore);
            this.Controls.Add(this._ok);
            this.Controls.Add(this._message);
            this.Name = "ExceptionMessageBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ExceptionMessageBox";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _message;
        private System.Windows.Forms.Button _ok;
        private System.Windows.Forms.Button _showMore;
        private System.Windows.Forms.TextBox _exception;
    }
}