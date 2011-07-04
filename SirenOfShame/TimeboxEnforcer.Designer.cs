namespace SirenOfShame
{
    partial class TimeboxEnforcer
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
            this._close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _close
            // 
            this._close.Location = new System.Drawing.Point(278, 164);
            this._close.Name = "_close";
            this._close.Size = new System.Drawing.Size(75, 23);
            this._close.TabIndex = 0;
            this._close.Text = "Close";
            this._close.UseVisualStyleBackColor = true;
            this._close.Click += new System.EventHandler(this.CloseClick);
            // 
            // TimeboxEnforcer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 199);
            this.Controls.Add(this._close);
            this.Name = "TimeboxEnforcer";
            this.Text = "Timebox Enforcer";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _close;
    }
}