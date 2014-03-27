namespace SirenOfShame.Configuration
{
    partial class ConfigureSounds
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigureSounds));
            this._done = new SirenOfShame.Lib.SosButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._soundsList = new System.Windows.Forms.ListView();
            this.fileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label3 = new System.Windows.Forms.Label();
            this._add = new SirenOfShame.Lib.SosButton();
            this._delete = new SirenOfShame.Lib.SosButton();
            this.SuspendLayout();
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
            this._done.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._done.ForeColor = System.Drawing.Color.White;
            this._done.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._done.ImageIndex = 0;
            this._done.ImageList = this.imageList1;
            this._done.Location = new System.Drawing.Point(422, 272);
            this._done.Name = "_done";
            this._done.Size = new System.Drawing.Size(72, 25);
            this._done.TabIndex = 39;
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
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList2.Images.SetKeyName(0, "music.png");
            // 
            // label1
            // 
            this.label1.ImageKey = "music.png";
            this.label1.ImageList = this.imageList2;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 64);
            this.label1.TabIndex = 43;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(83, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(241, 26);
            this.label2.TabIndex = 44;
            this.label2.Text = "Configure Build Sounds";
            // 
            // _soundsList
            // 
            this._soundsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._soundsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.fileName});
            this._soundsList.FullRowSelect = true;
            this._soundsList.LabelWrap = false;
            this._soundsList.Location = new System.Drawing.Point(15, 90);
            this._soundsList.Name = "_soundsList";
            this._soundsList.Size = new System.Drawing.Size(479, 176);
            this._soundsList.TabIndex = 45;
            this._soundsList.UseCompatibleStateImageBehavior = false;
            this._soundsList.View = System.Windows.Forms.View.Details;
            this._soundsList.SelectedIndexChanged += new System.EventHandler(this.SoundsList_SelectedIndexChanged);
            // 
            // fileName
            // 
            this.fileName.Text = "File Name";
            this.fileName.Width = 460;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(88, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(357, 13);
            this.label3.TabIndex = 47;
            this.label3.Text = "Add taunts here, configure them by right-clicking builds in the main window";
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
            this._add.ForeColor = System.Drawing.Color.White;
            this._add.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._add.ImageKey = "add.bmp";
            this._add.ImageList = this.imageList1;
            this._add.Location = new System.Drawing.Point(15, 272);
            this._add.Name = "_add";
            this._add.Size = new System.Drawing.Size(76, 23);
            this._add.TabIndex = 48;
            this._add.Text = "    Add";
            this._add.UseVisualStyleBackColor = false;
            this._add.Click += new System.EventHandler(this.AddSound);
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
            this._delete.ForeColor = System.Drawing.Color.White;
            this._delete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._delete.ImageKey = "delete.bmp";
            this._delete.ImageList = this.imageList1;
            this._delete.Location = new System.Drawing.Point(97, 272);
            this._delete.Name = "_delete";
            this._delete.Size = new System.Drawing.Size(75, 23);
            this._delete.TabIndex = 49;
            this._delete.Text = "     Delete";
            this._delete.UseVisualStyleBackColor = false;
            this._delete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // ConfigureSounds
            // 
            this.AcceptButton = this._done;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(38)))), ((int)(((byte)(39)))));
            this.CancelButton = this._done;
            this.ClientSize = new System.Drawing.Size(506, 309);
            this.Controls.Add(this._delete);
            this.Controls.Add(this._add);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._soundsList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._done);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "ConfigureSounds";
            this.Text = "ConfigureSounds";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Lib.SosButton _done;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView _soundsList;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label3;
        private Lib.SosButton _add;
        private Lib.SosButton _delete;
        private System.Windows.Forms.ColumnHeader fileName;
    }
}