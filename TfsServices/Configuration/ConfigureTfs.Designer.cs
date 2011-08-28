namespace TfsServices.Configuration {
	sealed partial class ConfigureTfs {
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this._buildConfigurations = new System.Windows.Forms.TreeView();
            this._url = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this._go = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _buildConfigurations
            // 
            this._buildConfigurations.CheckBoxes = true;
            this._buildConfigurations.Dock = System.Windows.Forms.DockStyle.Fill;
            this._buildConfigurations.Location = new System.Drawing.Point(0, 32);
            this._buildConfigurations.Name = "_buildConfigurations";
            this._buildConfigurations.Size = new System.Drawing.Size(460, 195);
            this._buildConfigurations.TabIndex = 0;
            this._buildConfigurations.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.BuildConfigurationsAfterCheck);
            // 
            // _url
            // 
            this._url.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._url.Location = new System.Drawing.Point(31, 5);
            this._url.Name = "_url";
            this._url.Size = new System.Drawing.Size(384, 20);
            this._url.TabIndex = 0;
            this._url.TextChanged += new System.EventHandler(this.UrlTextChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._go);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this._url);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(460, 32);
            this.panel1.TabIndex = 4;
            // 
            // _go
            // 
            this._go.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._go.Location = new System.Drawing.Point(421, 3);
            this._go.Name = "_go";
            this._go.Size = new System.Drawing.Size(36, 23);
            this._go.TabIndex = 1;
            this._go.Text = "Go";
            this._go.UseVisualStyleBackColor = true;
            this._go.Click += new System.EventHandler(this.GoClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Url";
            // 
            // ConfigureTfs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._buildConfigurations);
            this.Controls.Add(this.panel1);
            this.Name = "ConfigureTfs";
            this.Size = new System.Drawing.Size(460, 227);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.TreeView _buildConfigurations;
		private System.Windows.Forms.TextBox _url;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _go;
	}
}
