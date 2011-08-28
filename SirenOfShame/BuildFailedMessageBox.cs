using System.Windows.Forms;

namespace SirenOfShame
{
    public partial class BuildFailedMessageBox : FormBase
    {
        private static BuildFailedMessageBox _buildFailedMessageBox;

        public static void ShowOnce(string title, string body, string okMessage)
        {
            Program.Form.Invoke(() => {
                           if (_buildFailedMessageBox == null)
                           {
                               _buildFailedMessageBox = new BuildFailedMessageBox();
                           }
                           _buildFailedMessageBox.Text = title;
                           if (!string.IsNullOrEmpty(okMessage))           
                               _buildFailedMessageBox._ok.Text = okMessage;
                           _buildFailedMessageBox._body.Text = body;
                           _buildFailedMessageBox.Show();
                           _buildFailedMessageBox.Activate();
                       });
        }

        public BuildFailedMessageBox()
        {
            InitializeComponent();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
            base.OnClosing(e);
        }

        private void OkClick(object sender, System.EventArgs e)
        {
            Hide();
        }
    }
}
