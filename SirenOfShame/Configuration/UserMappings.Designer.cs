namespace SirenOfShame.Configuration
{
    partial class UserMappings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserMappings));
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("sdf");
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._done = new SirenOfShame.Lib.SosButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this._mappingList = new System.Windows.Forms.ListView();
            this.Person = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._add = new SirenOfShame.Lib.SosButton();
            this._delete = new SirenOfShame.Lib.SosButton();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(88, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(176, 13);
            this.label3.TabIndex = 44;
            this.label3.Text = "Solving multiple personality disorder.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(82, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(158, 26);
            this.label2.TabIndex = 43;
            this.label2.Text = "User Mappings";
            // 
            // _done
            // 
            this._done.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._done.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this._done.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._done.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this._done.FlatAppearance.BorderSize = 0;
            this._done.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._done.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(227)))), ((int)(((byte)(136)))));
            this._done.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._done.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this._done.ForeColor = System.Drawing.Color.White;
            this._done.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._done.ImageIndex = 0;
            this._done.ImageList = this.imageList1;
            this._done.Location = new System.Drawing.Point(361, 222);
            this._done.Name = "_done";
            this._done.Size = new System.Drawing.Size(72, 25);
            this._done.TabIndex = 46;
            this._done.Text = "Done";
            this._done.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._done.UseVisualStyleBackColor = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList1.Images.SetKeyName(0, "check.bmp");
            this.imageList1.Images.SetKeyName(1, "add.bmp");
            this.imageList1.Images.SetKeyName(2, "delete.bmp");
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Image = global::SirenOfShame.Properties.Resources.id_cards;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 64);
            this.label1.TabIndex = 47;
            // 
            // _mappingList
            // 
            this._mappingList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._mappingList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Person,
            this.columnHeader1});
            this._mappingList.FullRowSelect = true;
            this._mappingList.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem2});
            this._mappingList.LabelWrap = false;
            this._mappingList.Location = new System.Drawing.Point(12, 76);
            this._mappingList.Name = "_mappingList";
            this._mappingList.Size = new System.Drawing.Size(421, 140);
            this._mappingList.TabIndex = 48;
            this._mappingList.UseCompatibleStateImageBehavior = false;
            this._mappingList.View = System.Windows.Forms.View.Details;
            this._mappingList.SelectedIndexChanged += new System.EventHandler(this.MappingListSelectedIndexChanged);
            // 
            // Person
            // 
            this.Person.Text = "When I See";
            this.Person.Width = 180;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Pretend It\'s Actually";
            this.columnHeader1.Width = 180;
            // 
            // _add
            // 
            this._add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._add.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this._add.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this._add.FlatAppearance.BorderSize = 0;
            this._add.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(55)))), ((int)(((byte)(0)))));
            this._add.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(65)))), ((int)(((byte)(0)))));
            this._add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._add.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this._add.ForeColor = System.Drawing.Color.White;
            this._add.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._add.ImageKey = "add.bmp";
            this._add.ImageList = this.imageList1;
            this._add.Location = new System.Drawing.Point(17, 222);
            this._add.Name = "_add";
            this._add.Size = new System.Drawing.Size(105, 25);
            this._add.TabIndex = 49;
            this._add.Text = "       Add Mapping";
            this._add.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._add.UseVisualStyleBackColor = true;
            this._add.Click += new System.EventHandler(this.AddClick);
            // 
            // _delete
            // 
            this._delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._delete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this._delete.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this._delete.FlatAppearance.BorderSize = 0;
            this._delete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(55)))), ((int)(((byte)(0)))));
            this._delete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(65)))), ((int)(((byte)(0)))));
            this._delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._delete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this._delete.ForeColor = System.Drawing.Color.White;
            this._delete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._delete.ImageKey = "delete.bmp";
            this._delete.ImageList = this.imageList1;
            this._delete.Location = new System.Drawing.Point(135, 222);
            this._delete.Name = "_delete";
            this._delete.Size = new System.Drawing.Size(71, 25);
            this._delete.TabIndex = 50;
            this._delete.Text = "       Delete";
            this._delete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._delete.UseVisualStyleBackColor = true;
            this._delete.Click += new System.EventHandler(this.DeleteClick);
            // 
            // UserMappings
            // 
            this.AcceptButton = this._done;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this.CancelButton = this._done;
            this.ClientSize = new System.Drawing.Size(445, 259);
            this.Controls.Add(this._delete);
            this.Controls.Add(this._add);
            this.Controls.Add(this._mappingList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._done);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Name = "UserMappings";
            this.Text = "UserMappings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private Lib.SosButton _done;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView _mappingList;
        private System.Windows.Forms.ColumnHeader Person;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private Lib.SosButton _add;
        private Lib.SosButton _delete;
    }
}