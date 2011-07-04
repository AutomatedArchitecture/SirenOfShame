using System;

namespace SirenOfShame
{
    public partial class TimeboxEnforcer : FormBase
    {
        public TimeboxEnforcer()
        {
            InitializeComponent();
        }

        private void CloseClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
