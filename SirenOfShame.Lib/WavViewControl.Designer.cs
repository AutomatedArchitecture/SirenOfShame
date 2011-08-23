namespace SirenOfShame.Lib
{
    partial class WavViewControl
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
            this._hscroll = new System.Windows.Forms.HScrollBar();
            this._image = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this._image)).BeginInit();
            this.SuspendLayout();
            // 
            // _hscroll
            // 
            this._hscroll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._hscroll.Location = new System.Drawing.Point(0, 137);
            this._hscroll.Name = "_hscroll";
            this._hscroll.Size = new System.Drawing.Size(564, 17);
            this._hscroll.TabIndex = 0;
            this._hscroll.Scroll += new System.Windows.Forms.ScrollEventHandler(this._hscroll_Scroll);
            // 
            // _image
            // 
            this._image.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._image.BackColor = System.Drawing.Color.Black;
            this._image.Location = new System.Drawing.Point(0, 0);
            this._image.Margin = new System.Windows.Forms.Padding(0);
            this._image.Name = "_image";
            this._image.Size = new System.Drawing.Size(564, 137);
            this._image.TabIndex = 1;
            this._image.TabStop = false;
            this._image.Paint += new System.Windows.Forms.PaintEventHandler(this._image_Paint);
            this._image.MouseDown += new System.Windows.Forms.MouseEventHandler(this._image_MouseDown);
            this._image.MouseMove += new System.Windows.Forms.MouseEventHandler(this._image_MouseMove);
            this._image.MouseUp += new System.Windows.Forms.MouseEventHandler(this._image_MouseUp);
            // 
            // WavViewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._image);
            this.Controls.Add(this._hscroll);
            this.Name = "WavViewControl";
            this.Size = new System.Drawing.Size(564, 154);
            ((System.ComponentModel.ISupportInitialize)(this._image)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.HScrollBar _hscroll;
        private System.Windows.Forms.PictureBox _image;
    }
}
