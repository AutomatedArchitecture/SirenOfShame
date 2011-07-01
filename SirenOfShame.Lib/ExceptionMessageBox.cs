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

        public static void Show(IWin32Window owner, string title, string message, Exception exception)
        {
            var dlg = new ExceptionMessageBox
            {
                Text = title,
                _message = { Text = message },
                _exception = { Text = exception.ToString() }
            };
            dlg._exception.Visible = false;
            dlg.Show(owner);
        }

        private void _ok_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void _showMore_Click(object sender, EventArgs e)
        {
            _exception.Visible = true;
            _showMore.Visible = false;
            ClientSize = new Size(ClientSize.Width, _exception.Bottom + 10);
        }
    }
}
