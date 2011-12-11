namespace SirenOfShame
{
    partial class FullScreenBuildStatus
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FullScreenBuildStatus));
            this._buildDefinitions = new SirenOfShame.BuildStatusListView();
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.date = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.duration2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.checkedInBy = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.comment = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.balls = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // _buildDefinitions
            // 
            this._buildDefinitions.BackColor = System.Drawing.Color.Black;
            this._buildDefinitions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._buildDefinitions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name,
            this.date,
            this.duration2,
            this.checkedInBy,
            this.comment});
            this._buildDefinitions.Dock = System.Windows.Forms.DockStyle.Fill;
            this._buildDefinitions.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._buildDefinitions.ForeColor = System.Drawing.Color.White;
            this._buildDefinitions.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this._buildDefinitions.Location = new System.Drawing.Point(0, 0);
            this._buildDefinitions.Name = "_buildDefinitions";
            this._buildDefinitions.Scrollable = false;
            this._buildDefinitions.Size = new System.Drawing.Size(1347, 372);
            this._buildDefinitions.SmallImageList = this.balls;
            this._buildDefinitions.TabIndex = 37;
            this._buildDefinitions.UseCompatibleStateImageBehavior = false;
            this._buildDefinitions.View = System.Windows.Forms.View.Details;
            this._buildDefinitions.KeyDown += new System.Windows.Forms.KeyEventHandler(this._buildDefinitions_KeyDown);
            this._buildDefinitions.MouseDown += new System.Windows.Forms.MouseEventHandler(this._buildDefinitions_MouseDown);
            // 
            // name
            // 
            this.name.Text = "Name";
            this.name.Width = 317;
            // 
            // date
            // 
            this.date.Text = "Date";
            this.date.Width = 250;
            // 
            // duration2
            // 
            this.duration2.Text = "Duration";
            this.duration2.Width = 322;
            // 
            // checkedInBy
            // 
            this.checkedInBy.Text = "Checked In By";
            this.checkedInBy.Width = 412;
            // 
            // comment
            // 
            this.comment.Text = "Comment";
            this.comment.Width = 250;
            // 
            // balls
            // 
            this.balls.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("balls.ImageStream")));
            this.balls.TransparentColor = System.Drawing.Color.Transparent;
            this.balls.Images.SetKeyName(0, "ball_red.png");
            this.balls.Images.SetKeyName(1, "ball_green.png");
            this.balls.Images.SetKeyName(2, "ball_gray.png");
            this.balls.Images.SetKeyName(3, "ConfigureRules.png");
            this.balls.Images.SetKeyName(4, "ConfigureServer.png");
            this.balls.Images.SetKeyName(5, "TestSiren.png");
            this.balls.Images.SetKeyName(6, "ConfigureSiren.png");
            this.balls.Images.SetKeyName(7, "question.png");
            this.balls.Images.SetKeyName(8, "Tools.png");
            // 
            // FullScreenBuildStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1347, 372);
            this.Controls.Add(this._buildDefinitions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FullScreenBuildStatus";
            this.ShowIcon = false;
            this.Text = "FullScreenBuildStatus";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FullScreenBuildStatus_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private BuildStatusListView _buildDefinitions;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.ColumnHeader date;
        private System.Windows.Forms.ColumnHeader duration2;
        private System.Windows.Forms.ColumnHeader checkedInBy;
        private System.Windows.Forms.ColumnHeader comment;
        private System.Windows.Forms.ImageList balls;
    }
}