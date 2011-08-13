using System;
using System.Windows.Forms;
using SirenOfShame.Lib.Device;
using SirenOfShame.Lib.Helpers;

namespace SirenOfShame.HardwareTestGui
{
    public partial class ManualControl : UserControl
    {
        public ManualControl()
        {
            InitializeComponent();
            if (Program.SirenOfShameDevice != null)
            {
                Program.SirenOfShameDevice.Connected += SirenOfShameDevice_Connected;
                Program.SirenOfShameDevice.Disconnected += SirenOfShameDevice_Disconnected;
            }
        }

        private void SirenOfShameDevice_Disconnected(object sender, EventArgs e)
        {
            EnableControls(false);
        }

        private void SirenOfShameDevice_Connected(object sender, EventArgs e)
        {
            EnableControls(true);
        }

        private void EnableControls(bool enable)
        {
            this.Invoke(() =>
            {
                _led1.Enabled = enable;
                _led2.Enabled = enable;
                _led3.Enabled = enable;
                _led4.Enabled = enable;
                _led5.Enabled = enable;
            });
        }

        private void _led_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDevice();
        }

        private void UpdateDevice()
        {
            Program.SirenOfShameDevice.ManualControl(new ManualControlData
            {
                Led1 = _led1.Checked,
                Led2 = _led2.Checked,
                Led3 = _led3.Checked,
                Led4 = _led4.Checked,
                Led5 = _led5.Checked,
                Siren = _siren.Checked
            });
        }

        private void _siren_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDevice();
        }

        private void _allOn_Click(object sender, EventArgs e)
        {
            _led1.Checked = true;
            _led2.Checked = true;
            _led3.Checked = true;
            _led4.Checked = true;
            _led5.Checked = true;
        }

        private void _allOff_Click(object sender, EventArgs e)
        {
            _led1.Checked = false;
            _led2.Checked = false;
            _led3.Checked = false;
            _led4.Checked = false;
            _led5.Checked = false;
        }
    }
}
