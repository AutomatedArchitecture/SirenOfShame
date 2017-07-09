namespace SirenOfShame.HardwareTestGui
{
    partial class DeviceConnect
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
            this._connect = new System.Windows.Forms.Button();
            this._disconnect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _connect
            // 
            this._connect.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._connect.Location = new System.Drawing.Point(3, 3);
            this._connect.Name = "_connect";
            this._connect.Size = new System.Drawing.Size(150, 57);
            this._connect.TabIndex = 0;
            this._connect.Text = "Connect";
            this._connect.UseVisualStyleBackColor = true;
            this._connect.Click += new System.EventHandler(this._connect_Click);
            // 
            // _disconnect
            // 
            this._disconnect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._disconnect.Enabled = false;
            this._disconnect.Location = new System.Drawing.Point(159, 4);
            this._disconnect.Name = "_disconnect";
            this._disconnect.Size = new System.Drawing.Size(130, 57);
            this._disconnect.TabIndex = 1;
            this._disconnect.Text = "Disconnect";
            this._disconnect.UseVisualStyleBackColor = true;
            this._disconnect.Click += new System.EventHandler(this._disconnect_Click);
            // 
            // DeviceConnect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._disconnect);
            this.Controls.Add(this._connect);
            this.MinimumSize = new System.Drawing.Size(162, 30);
            this.Name = "DeviceConnect";
            this.Size = new System.Drawing.Size(292, 64);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Button _connect;
        protected System.Windows.Forms.Button _disconnect;
    }
}
