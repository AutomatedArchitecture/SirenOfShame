using System;
using System.Drawing;
using System.Windows.Forms;

namespace SirenOfShame.Lib
{
    public partial class ExceptionMessageBox : Form
    {
        public ExceptionMessageBox()
        {
            InitializeComponent();
            ClientSize = new Size(ClientSize.Width, _ok.Bottom + 10);
        }

        public static void Show(Form owner, string title, string message, Exception exception)
        {
            if (owner != null && owner.InvokeRequired)
            {
                owner.Invoke((Action)(() => DoShow(owner, title, message, exception)));
            }
            else
            {
                DoShow(owner, title, message, exception);
            }
        }

        private static void DoShow(Form owner, string title, string message, Exception exception)
        {
            var dlg = new ExceptionMessageBox
            {
                Text = title,
                _message = { Text = message },
                _exception = { Text = exception.ToString() }
            };
            dlg._exception.Visible = false;
            if (owner != null)
                dlg.ShowDialog(owner);
            else
                dlg.ShowDialog();
        }

        private void OkClick(object sender, EventArgs e)
        {
            Close();
        }

        private void ShowMoreClick(object sender, EventArgs e)
        {
            _exception.Visible = true;
            _showMore.Visible = false;
            ClientSize = new Size(ClientSize.Width, 270);
        }
    }
}
