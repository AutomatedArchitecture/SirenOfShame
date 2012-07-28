namespace SirenOfShame
{
    partial class Avatar
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Avatar));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "american_eskimo_puppy.png");
            this.imageList1.Images.SetKeyName(1, "angry_dog.png");
            this.imageList1.Images.SetKeyName(2, "basset_hound.png");
            this.imageList1.Images.SetKeyName(3, "beagle_harrier.png");
            this.imageList1.Images.SetKeyName(4, "black_and_white_dog.png");
            this.imageList1.Images.SetKeyName(5, "black_lab.png");
            this.imageList1.Images.SetKeyName(6, "cat_angry.png");
            this.imageList1.Images.SetKeyName(7, "cat_black.png");
            this.imageList1.Images.SetKeyName(8, "cat_fat.png");
            this.imageList1.Images.SetKeyName(9, "cat_tabby.png");
            this.imageList1.Images.SetKeyName(10, "cat_tongue_out.png");
            this.imageList1.Images.SetKeyName(11, "cat_white.png");
            this.imageList1.Images.SetKeyName(12, "chocolate_lab.png");
            this.imageList1.Images.SetKeyName(13, "chow.png");
            this.imageList1.Images.SetKeyName(14, "german_shepherd.png");
            this.imageList1.Images.SetKeyName(15, "golden_retriever_puppy.png");
            this.imageList1.Images.SetKeyName(16, "growling_pup.png");
            this.imageList1.Images.SetKeyName(17, "jack_russell_terrier.png");
            this.imageList1.Images.SetKeyName(18, "kromfohrlander.png");
            this.imageList1.Images.SetKeyName(19, "poodle.png");
            this.imageList1.Images.SetKeyName(20, "retriever.png");
            this.imageList1.Images.SetKeyName(21, "cloud-title.png");
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.ImageIndex = 0;
            this.label1.ImageList = this.imageList1;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.MinimumSize = new System.Drawing.Size(48, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 50);
            this.label1.TabIndex = 1;
            this.label1.Click += new System.EventHandler(this.Label1Click);
            // 
            // Avatar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.label1);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "Avatar";
            this.Size = new System.Drawing.Size(50, 50);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.AvatarPaint);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label1;
    }
}
