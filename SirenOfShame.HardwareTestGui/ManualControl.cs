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
            if (!DesignMode && Program.SirenOfShameDevice != null)
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

        private void UpdateDevice()
        {
            Program.SirenOfShameDevice.ManualControl(new ManualControlData
            {
                Led0 = (byte)(_led1.Checked ? _led1Value.Value : 0x00),
                Led1 = (byte)(_led2.Checked ? _led2Value.Value : 0x00),
                Led2 = (byte)(_led3.Checked ? _led3Value.Value : 0x00),
                Led3 = (byte)(_led4.Checked ? _led4Value.Value : 0x00),
                Led4 = (byte)(_led5.Checked ? _led5Value.Value : 0x00),
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

        private void _led1_CheckedChanged(object sender, EventArgs e)
        {
            ProcessCheckedChanged(_led1, _led1Value);
        }

        private void _led2_CheckedChanged(object sender, EventArgs e)
        {
            ProcessCheckedChanged(_led2, _led2Value);
        }

        private void _led3_CheckedChanged(object sender, EventArgs e)
        {
            ProcessCheckedChanged(_led3, _led3Value);
        }

        private void _led4_CheckedChanged(object sender, EventArgs e)
        {
            ProcessCheckedChanged(_led4, _led4Value);
        }

        private void _led5_CheckedChanged(object sender, EventArgs e)
        {
            ProcessCheckedChanged(_led5, _led5Value);
        }

        private void ProcessCheckedChanged(CheckBox check, TrackBar val)
        {
            if (check.Checked)
            {
                val.Enabled = true;
                val.Value = 0xfe;
            }
            else
            {
                val.Enabled = false;
                val.Value = 0;
            }
            UpdateDevice();
        }

        private void _ledValue_Scroll(object sender, EventArgs e)
        {
            UpdateDevice();
        }
    }
}
