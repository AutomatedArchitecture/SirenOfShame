namespace SirenOfShame
{
    partial class ViewUser
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
            this._userName = new System.Windows.Forms.Label();
            this._closeButton = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this._closeButton)).BeginInit();
            this.SuspendLayout();
            // 
            // _userName
            // 
            this._userName.AutoSize = true;
            this._userName.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._userName.Location = new System.Drawing.Point(3, 3);
            this._userName.Name = "_userName";
            this._userName.Size = new System.Drawing.Size(70, 26);
            this._userName.TabIndex = 0;
            this._userName.Text = "label1";
            // 
            // _closeButton
            // 
            this._closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._closeButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this._closeButton.Image = global::SirenOfShame.Properties.Resources.CloseButton2;
            this._closeButton.Location = new System.Drawing.Point(618, 3);
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(12, 12);
            this._closeButton.TabIndex = 1;
            this._closeButton.TabStop = false;
            this._closeButton.Click += new System.EventHandler(this.CloseButtonClick);
            // 
            // ViewUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this._closeButton);
            this.Controls.Add(this._userName);
            this.Name = "ViewUser";
            this.Size = new System.Drawing.Size(633, 281);
            ((System.ComponentModel.ISupportInitialize)(this._closeButton)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _userName;
        private System.Windows.Forms.PictureBox _closeButton;
    }
}
