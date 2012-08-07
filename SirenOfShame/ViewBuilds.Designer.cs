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
            this._overflowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // _mainFlowLayoutPanel
            // 
            this._mainFlowLayoutPanel.BackColor = System.Drawing.Color.Transparent;
            this._mainFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainFlowLayoutPanel.Location = new System.Drawing.Point(38, 32);
            this._mainFlowLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this._mainFlowLayoutPanel.Name = "_mainFlowLayoutPanel";
            this._mainFlowLayoutPanel.Size = new System.Drawing.Size(478, 235);
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
            this.label1.Size = new System.Drawing.Size(478, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "Builds";
            // 
            // _overflowLayoutPanel
            // 
            this._overflowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._overflowLayoutPanel.Location = new System.Drawing.Point(38, 267);
            this._overflowLayoutPanel.Name = "_overflowLayoutPanel";
            this._overflowLayoutPanel.Size = new System.Drawing.Size(478, 100);
            this._overflowLayoutPanel.TabIndex = 0;
            // 
            // ViewBuilds
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this.Controls.Add(this._mainFlowLayoutPanel);
            this.Controls.Add(this._overflowLayoutPanel);
            this.Controls.Add(this.label1);
            this.Name = "ViewBuilds";
            this.Padding = new System.Windows.Forms.Padding(38, 0, 0, 0);
            this.Size = new System.Drawing.Size(516, 367);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel _mainFlowLayoutPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel _overflowLayoutPanel;
    }
}
