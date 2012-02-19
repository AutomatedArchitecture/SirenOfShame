namespace SirenOfShame.Configuration
{
    partial class ConfigureRules
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigureRules));
            this.panel1 = new System.Windows.Forms.Panel();
            this._reset = new System.Windows.Forms.Button();
            this._down = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this._up = new System.Windows.Forms.Button();
            this._delete = new System.Windows.Forms.Button();
            this._newRule = new System.Windows.Forms.Button();
            this._rulesList = new System.Windows.Forms.ListView();
            this.Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Build = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Person = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NotificationType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this._edit = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this._edit);
            this.panel1.Controls.Add(this._reset);
            this.panel1.Controls.Add(this._down);
            this.panel1.Controls.Add(this._up);
            this.panel1.Controls.Add(this._delete);
            this.panel1.Controls.Add(this._newRule);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(735, 30);
            this.panel1.TabIndex = 0;
            // 
            // _reset
            // 
            this._reset.FlatAppearance.BorderSize = 0;
            this._reset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._reset.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._reset.Location = new System.Drawing.Point(215, 4);
            this._reset.Name = "_reset";
            this._reset.Size = new System.Drawing.Size(44, 23);
            this._reset.TabIndex = 4;
            this._reset.Text = "Reset";
            this._reset.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._reset.UseVisualStyleBackColor = true;
            this._reset.Click += new System.EventHandler(this.ResetClick);
            // 
            // _down
            // 
            this._down.FlatAppearance.BorderSize = 0;
            this._down.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._down.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._down.ImageIndex = 1;
            this._down.ImageList = this.imageList1;
            this._down.Location = new System.Drawing.Point(296, 4);
            this._down.Name = "_down";
            this._down.Size = new System.Drawing.Size(28, 23);
            this._down.TabIndex = 3;
            this._down.UseVisualStyleBackColor = true;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Delete.png");
            this.imageList1.Images.SetKeyName(1, "Down.PNG");
            this.imageList1.Images.SetKeyName(2, "New.png");
            this.imageList1.Images.SetKeyName(3, "Up.png");
            this.imageList1.Images.SetKeyName(4, "Exclamation.png");
            this.imageList1.Images.SetKeyName(5, "Edit.png");
            // 
            // _up
            // 
            this._up.FlatAppearance.BorderSize = 0;
            this._up.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._up.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._up.ImageIndex = 3;
            this._up.ImageList = this.imageList1;
            this._up.Location = new System.Drawing.Point(262, 4);
            this._up.Name = "_up";
            this._up.Size = new System.Drawing.Size(28, 23);
            this._up.TabIndex = 2;
            this._up.UseVisualStyleBackColor = true;
            // 
            // _delete
            // 
            this._delete.FlatAppearance.BorderSize = 0;
            this._delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._delete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._delete.ImageIndex = 0;
            this._delete.ImageList = this.imageList1;
            this._delete.Location = new System.Drawing.Point(87, 4);
            this._delete.Name = "_delete";
            this._delete.Size = new System.Drawing.Size(65, 23);
            this._delete.TabIndex = 1;
            this._delete.Text = "Delete";
            this._delete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._delete.UseVisualStyleBackColor = true;
            this._delete.Click += new System.EventHandler(this.DeleteClick);
            // 
            // _newRule
            // 
            this._newRule.FlatAppearance.BorderSize = 0;
            this._newRule.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._newRule.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._newRule.ImageIndex = 2;
            this._newRule.ImageList = this.imageList1;
            this._newRule.Location = new System.Drawing.Point(4, 4);
            this._newRule.Name = "_newRule";
            this._newRule.Size = new System.Drawing.Size(83, 23);
            this._newRule.TabIndex = 0;
            this._newRule.Text = "New Rule";
            this._newRule.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._newRule.UseVisualStyleBackColor = true;
            this._newRule.Click += new System.EventHandler(this.NewRuleClick);
            // 
            // _rulesList
            // 
            this._rulesList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Type,
            this.Build,
            this.Person,
            this.NotificationType});
            this._rulesList.Dock = System.Windows.Forms.DockStyle.Fill;
            this._rulesList.Location = new System.Drawing.Point(0, 53);
            this._rulesList.Name = "_rulesList";
            this._rulesList.Size = new System.Drawing.Size(735, 158);
            this._rulesList.TabIndex = 4;
            this._rulesList.UseCompatibleStateImageBehavior = false;
            this._rulesList.View = System.Windows.Forms.View.Details;
            this._rulesList.SelectedIndexChanged += new System.EventHandler(this._rulesList_SelectedIndexChanged);
            this._rulesList.DoubleClick += new System.EventHandler(this.RulesListDoubleClick);
            // 
            // Type
            // 
            this.Type.Text = "Type";
            this.Type.Width = 200;
            // 
            // Build
            // 
            this.Build.Text = "Build";
            this.Build.Width = 99;
            // 
            // Person
            // 
            this.Person.Text = "Person";
            this.Person.Width = 116;
            // 
            // NotificationType
            // 
            this.NotificationType.Text = "Notification Type";
            this.NotificationType.Width = 180;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Window;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 30);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(735, 23);
            this.panel2.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.ImageIndex = 4;
            this.label1.ImageList = this.imageList1;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(670, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "     This page gives you the OCD way of controling the rules. Life really is easi" +
    "er when you right click on the builds on the main screen. #protip";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _edit
            // 
            this._edit.FlatAppearance.BorderSize = 0;
            this._edit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._edit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._edit.ImageIndex = 5;
            this._edit.ImageList = this.imageList1;
            this._edit.Location = new System.Drawing.Point(158, 4);
            this._edit.Name = "_edit";
            this._edit.Size = new System.Drawing.Size(55, 23);
            this._edit.TabIndex = 5;
            this._edit.Text = "Edit";
            this._edit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._edit.UseVisualStyleBackColor = true;
            this._edit.Click += new System.EventHandler(this.EditClick);
            // 
            // ConfigureRules
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(735, 211);
            this.Controls.Add(this._rulesList);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConfigureRules";
            this.Text = "Configure Rules";
            this.Load += new System.EventHandler(this.ConfigureRulesLoad);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button _newRule;
        private System.Windows.Forms.Button _down;
        private System.Windows.Forms.Button _up;
        private System.Windows.Forms.Button _delete;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ListView _rulesList;
        private System.Windows.Forms.ColumnHeader Type;
        private System.Windows.Forms.ColumnHeader Build;
        private System.Windows.Forms.ColumnHeader Person;
        private System.Windows.Forms.ColumnHeader NotificationType;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _reset;
        private System.Windows.Forms.Button _edit;
    }
}