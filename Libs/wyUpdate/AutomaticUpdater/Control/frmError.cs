﻿using System;
using System.Drawing;
using System.Windows.Forms;


namespace wyDay.Controls
{
    internal partial class frmError : Form
    {
        public bool TryAgainLater { get; private set; }

        public frmError(FailArgs failArgs, AUTranslation translation)
        {
            Font = SystemFonts.MessageBoxFont;

            InitializeComponent();

            richError.Text = failArgs.ErrorMessage;

            Text = translation.ErrorTitle;
            btnOK.Text = translation.CloseButton;
            btnTryAgainLater.Text = translation.TryAgainLater;
            lblTitle.Text = failArgs.ErrorTitle;

            // resize the buttons to fit their contents
            btnTryAgainLater.Left = btnOK.Left - btnTryAgainLater.Width - 6;

            MinimumSize = new Size(richError.Left + Right - btnTryAgainLater.Left, 250);
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);

            // resize the textbox to fill the form
            if (richError != null)
            {
                richError.Width = ClientRectangle.Width - 2 * richError.Left;
                richError.Height = ClientRectangle.Height - richError.Top - richError.Left - (ClientRectangle.Height - btnOK.Top);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnTryAgainLater_Click(object sender, EventArgs e)
        {
            TryAgainLater = true;
            Close();
        }
    }
}
