using SirenOfShame;
using SirenOfShame.Lib;

namespace TravisCiServices.ServerConfiguration
{
    partial class ConfigureTravisCi
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
            this.label1 = new System.Windows.Forms.Label();
            this._ownerName = new System.Windows.Forms.TextBox();
            this._add = new SosButton();
            this._projects = new SirenOfShame.Lib.Helpers.ThreeStateTreeView();
            this.label3 = new System.Windows.Forms.Label();
            this._projectName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 43;
            this.label1.Text = "Owner Name:";
            // 
            // _ownerName
            // 
            this._ownerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._ownerName.Location = new System.Drawing.Point(83, 3);
            this._ownerName.Name = "_ownerName";
            this._ownerName.Size = new System.Drawing.Size(301, 20);
            this._ownerName.TabIndex = 0;
            // 
            // _add
            // 
            this._add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._add.Location = new System.Drawing.Point(309, 55);
            this._add.Name = "_add";
            this._add.Size = new System.Drawing.Size(75, 23);
            this._add.TabIndex = 2;
            this._add.Text = "Add";
            this._add.UseVisualStyleBackColor = true;
            this._add.Click += new System.EventHandler(this._add_Click);
            // 
            // _projects
            // 
            this._projects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._projects.CheckBoxes = true;
            this._projects.Location = new System.Drawing.Point(3, 84);
            this._projects.Name = "_projects";
            this._projects.Size = new System.Drawing.Size(381, 193);
            this._projects.TabIndex = 4;
            this._projects.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.ProjectsAfterCheck);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 49;
            this.label3.Text = "Project Name:";
            // 
            // _projectName
            // 
            this._projectName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._projectName.Location = new System.Drawing.Point(83, 29);
            this._projectName.Name = "_projectName";
            this._projectName.Size = new System.Drawing.Size(301, 20);
            this._projectName.TabIndex = 1;
            // 
            // ConfigureTravisCi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._projectName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._projects);
            this.Controls.Add(this._add);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._ownerName);
            this.Name = "ConfigureTravisCi";
            this.Size = new System.Drawing.Size(387, 280);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _ownerName;
        private SosButton _add;
        private SirenOfShame.Lib.Helpers.ThreeStateTreeView _projects;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox _projectName;
    }
}
