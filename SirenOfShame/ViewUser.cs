using System;
using System.Windows.Forms;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame
{
    public partial class ViewUser : UserControl
    {
        private SirenOfShameSettings _settings;
        public event CloseViewUser OnClose;
        
        public ViewUser()
        {
            InitializeComponent();
        }

        public void Initilaize(SirenOfShameSettings settings)
        {
            _settings = settings;
        }

        private void CloseButtonClick(object sender, EventArgs e)
        {
            OnClose(this, new CloseViewUserArgs());
        }

        public void SetUser(PersonSetting personSetting)
        {
            _userName.Text = personSetting.DisplayName;
        }
    }

    public delegate void CloseViewUser(object sender, CloseViewUserArgs args);

    public class CloseViewUserArgs
    {
    }
}
