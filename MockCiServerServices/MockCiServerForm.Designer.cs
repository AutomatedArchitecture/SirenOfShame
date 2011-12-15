namespace MockCiServerServices
{
    partial class MockCiServerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MockCiServerForm));
            this._project3 = new MockCiServerServices.MockProject();
            this._project2 = new MockCiServerServices.MockProject();
            this._project1 = new MockCiServerServices.MockProject();
            this.SuspendLayout();
            // 
            // _project3
            // 
            this._project3.Location = new System.Drawing.Point(12, 348);
            this._project3.MaximumSize = new System.Drawing.Size(380, 162);
            this._project3.MinimumSize = new System.Drawing.Size(380, 162);
            this._project3.Name = "_project3";
            this._project3.ProjectName = "Project Name";
            this._project3.Size = new System.Drawing.Size(380, 162);
            this._project3.TabIndex = 2;
            // 
            // _project2
            // 
            this._project2.Location = new System.Drawing.Point(12, 180);
            this._project2.MaximumSize = new System.Drawing.Size(380, 162);
            this._project2.MinimumSize = new System.Drawing.Size(380, 162);
            this._project2.Name = "_project2";
            this._project2.ProjectName = "Project Name";
            this._project2.Size = new System.Drawing.Size(380, 162);
            this._project2.TabIndex = 1;
            // 
            // _project1
            // 
            this._project1.Location = new System.Drawing.Point(12, 12);
            this._project1.MaximumSize = new System.Drawing.Size(380, 162);
            this._project1.MinimumSize = new System.Drawing.Size(380, 162);
            this._project1.Name = "_project1";
            this._project1.ProjectName = "Project Name";
            this._project1.Size = new System.Drawing.Size(380, 162);
            this._project1.TabIndex = 0;
            // 
            // MockCiServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 560);
            this.Controls.Add(this._project3);
            this.Controls.Add(this._project2);
            this.Controls.Add(this._project1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MockCiServerForm";
            this.Text = "Mock CI Server";
            this.ResumeLayout(false);

        }

        #endregion

        private MockProject _project1;
        private MockProject _project2;
        private MockProject _project3;
    }
}