using System.Windows.Forms;

namespace SirenOfShame
{
    public partial class ViewAllNews : UserControl
    {
        public event CloseScreen OnClose;

        private void InvokeOnClose()
        {
            CloseScreen handler = OnClose;
            if (handler != null) handler(this, new CloseScreenArgs());
        }

        public ViewAllNews()
        {
            InitializeComponent();
        }

        private void BackClick(object sender, System.EventArgs e)
        {
            InvokeOnClose();
        }

    }
}
