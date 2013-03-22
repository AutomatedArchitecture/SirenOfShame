namespace MockCiServerServices
{
    partial class ConfigureMock
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
            this._projects = new SirenOfShame.Lib.Helpers.ThreeStateTreeView();
            this.SuspendLayout();
            // 
            // _projects
            // 
            this._projects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._projects.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this._projects.CheckBoxes = true;
            this._projects.ForeColor = System.Drawing.Color.White;
            this._projects.Location = new System.Drawing.Point(3, 3);
            this._projects.Name = "_projects";
            this._projects.Size = new System.Drawing.Size(503, 342);
            this._projects.TabIndex = 5;
            this._projects.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.ProjectsAfterCheck);
            // 
            // ConfigureMock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this.Controls.Add(this._projects);
            this.Name = "ConfigureMock";
            this.Size = new System.Drawing.Size(509, 348);
            this.Load += new System.EventHandler(this.ConfigureMockLoad);
            this.ResumeLayout(false);

        }

        #endregion

        private SirenOfShame.Lib.Helpers.ThreeStateTreeView _projects;
    }
}
