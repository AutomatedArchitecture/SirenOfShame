namespace SirenOfShame
{
    partial class ViewBuilds
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
            this._mainFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _mainFlowLayoutPanel
            // 
            this._mainFlowLayoutPanel.BackColor = System.Drawing.Color.Transparent;
            this._mainFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainFlowLayoutPanel.Location = new System.Drawing.Point(38, 32);
            this._mainFlowLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this._mainFlowLayoutPanel.Name = "_mainFlowLayoutPanel";
            this._mainFlowLayoutPanel.Size = new System.Drawing.Size(530, 335);
            this._mainFlowLayoutPanel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(38, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(530, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "Builds";
            // 
            // ViewBuilds
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this.Controls.Add(this._mainFlowLayoutPanel);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ViewBuilds";
            this.Padding = new System.Windows.Forms.Padding(38, 0, 0, 0);
            this.Size = new System.Drawing.Size(568, 367);
            this.Resize += new System.EventHandler(this.ViewBuildsResize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel _mainFlowLayoutPanel;
        private System.Windows.Forms.Label label1;
    }
}
