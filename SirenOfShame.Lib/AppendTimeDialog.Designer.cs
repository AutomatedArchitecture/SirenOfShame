namespace SirenOfShame.Lib
{
    partial class AppendTimeDialog
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
            this._ok = new System.Windows.Forms.Button();
            this._cancel = new System.Windows.Forms.Button();
            this._seconds = new System.Windows.Forms.NumericUpDown();
            this._minutes = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this._millis = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._seconds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._minutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._millis)).BeginInit();
            this.SuspendLayout();
            // 
            // _ok
            // 
            this._ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._ok.Location = new System.Drawing.Point(157, 51);
            this._ok.Name = "_ok";
            this._ok.Size = new System.Drawing.Size(75, 23);
            this._ok.TabIndex = 0;
            this._ok.Text = "OK";
            this._ok.UseVisualStyleBackColor = true;
            // 
            // _cancel
            // 
            this._cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancel.Location = new System.Drawing.Point(76, 51);
            this._cancel.Name = "_cancel";
            this._cancel.Size = new System.Drawing.Size(75, 23);
            this._cancel.TabIndex = 1;
            this._cancel.Text = "Cancel";
            this._cancel.UseVisualStyleBackColor = true;
            this._cancel.Visible = false;
            // 
            // _seconds
            // 
            this._seconds.Location = new System.Drawing.Point(94, 25);
            this._seconds.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this._seconds.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this._seconds.Name = "_seconds";
            this._seconds.Size = new System.Drawing.Size(57, 20);
            this._seconds.TabIndex = 2;
            this._seconds.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._seconds.ValueChanged += new System.EventHandler(this._seconds_ValueChanged);
            // 
            // _minutes
            // 
            this._minutes.Location = new System.Drawing.Point(15, 25);
            this._minutes.Name = "_minutes";
            this._minutes.Size = new System.Drawing.Size(57, 20);
            this._minutes.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Time to append";
            // 
            // _millis
            // 
            this._millis.Location = new System.Drawing.Point(173, 25);
            this._millis.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this._millis.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this._millis.Name = "_millis";
            this._millis.Size = new System.Drawing.Size(57, 20);
            this._millis.TabIndex = 5;
            this._millis.ValueChanged += new System.EventHandler(this._millis_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(78, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(10, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = ":";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(157, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(10, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = ".";
            // 
            // AppendTimeDialog
            // 
            this.AcceptButton = this._ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cancel;
            this.ClientSize = new System.Drawing.Size(244, 83);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._millis);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._minutes);
            this.Controls.Add(this._seconds);
            this.Controls.Add(this._cancel);
            this.Controls.Add(this._ok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AppendTimeDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Append Time";
            ((System.ComponentModel.ISupportInitialize)(this._seconds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._minutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._millis)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _ok;
        private System.Windows.Forms.Button _cancel;
        private System.Windows.Forms.NumericUpDown _seconds;
        private System.Windows.Forms.NumericUpDown _minutes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown _millis;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}