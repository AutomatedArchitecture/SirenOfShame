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
            this._connect.Location = new System.Drawing.Point(3, 3);
            this._connect.Name = "_connect";
            this._connect.Size = new System.Drawing.Size(75, 23);
            this._connect.TabIndex = 0;
            this._connect.Text = "Connect";
            this._connect.UseVisualStyleBackColor = true;
            this._connect.Click += new System.EventHandler(this._connect_Click);
            // 
            // _disconnect
            // 
            this._disconnect.Enabled = false;
            this._disconnect.Location = new System.Drawing.Point(84, 3);
            this._disconnect.Name = "_disconnect";
            this._disconnect.Size = new System.Drawing.Size(75, 23);
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
            this.MaximumSize = new System.Drawing.Size(162, 30);
            this.MinimumSize = new System.Drawing.Size(162, 30);
            this.Name = "DeviceConnect";
            this.Size = new System.Drawing.Size(162, 30);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _connect;
        private System.Windows.Forms.Button _disconnect;
    }
}
