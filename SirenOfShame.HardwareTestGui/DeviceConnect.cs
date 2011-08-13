using System;
using SirenOfShame.Lib.Helpers;
using System.Windows.Forms;

namespace SirenOfShame.HardwareTestGui
{
    public partial class DeviceConnect : UserControl
    {
        public DeviceConnect()
        {
            InitializeComponent();
            if (!DesignMode && Program.SirenOfShameDevice != null)
            {
                Program.SirenOfShameDevice.Connected += SirenOfShameDevice_Connected;
                Program.SirenOfShameDevice.Disconnected += SirenOfShameDevice_Disconnected;
            }
        }

        private void SirenOfShameDevice_Disconnected(object sender, EventArgs e)
        {
            this.Invoke(() =>
            {
                _connect.Enabled = true;
                _disconnect.Enabled = false;
            });
        }

        private void SirenOfShameDevice_Connected(object sender, EventArgs e)
        {
            this.Invoke(() =>
            {
                _connect.Enabled = false;
                _disconnect.Enabled = true;
            });
        }

        private void _connect_Click(object sender, EventArgs e)
        {
            Program.SirenOfShameDevice.TryConnect();
        }

        private void _disconnect_Click(object sender, EventArgs e)
        {
            Program.SirenOfShameDevice.Disconnect();
        }
    }
}
