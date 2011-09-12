using System;
using System.ComponentModel.Composition;
using System.Linq;
using log4net;
using SirenOfShame.Lib;
using SirenOfShame.Lib.Device;
using SirenOfShame.Lib.Helpers;

namespace SirenOfShame.SirenConfiguration
{
    public partial class TestSiren : FormBase
    {
        [Import(typeof(ISirenOfShameDevice))]
        public ISirenOfShameDevice SirenOfShameDevice { get; set; }

        private static readonly ILog Log = MyLogManager.GetLogger(typeof(TestSiren));
        private bool _testing = false;

        public TestSiren()
        {
            IocContainer.Instance.Compose(this);
            InitializeComponent();

            SirenOfShameDevice.Disconnected += SirenofShameDeviceDisconnected;

            _audio.DataSource = SirenOfShameDevice.AudioPatterns.ToList();
            _audio.DisplayMember = "Name";

            _lights.DataSource = SirenOfShameDevice.LedPatterns.ToList();
            _lights.DisplayMember = "Name";
        }

        private void SirenofShameDeviceDisconnected(object sender, EventArgs e)
        {
            Invoke(Close);
        }

        private void TestClick(object sender, EventArgs e)
        {
            if (_testing)
            {
                StopTesting();
            } else
            {
                StartTesting();
            }
        }

        private void StopTesting()
        {
            try
            {
                SirenOfShameDevice.PlayAudioPattern(null, null);
                SirenOfShameDevice.PlayLightPattern(null, null);
                _test.Text = "Start Test";
                _testing = false;
            }
            catch (Exception ex)
            {
                Log.Error("Stop testing", ex);
            }
        }

        private void StartTesting()
        {
            try
            {
                if (_audioAndLights.Checked || _audioOnly.Checked)
                    SirenOfShameDevice.PlayAudioPattern((AudioPattern)_audio.SelectedItem, null);
                if (_audioAndLights.Checked || _lightsOnly.Checked)
                    SirenOfShameDevice.PlayLightPattern((LedPattern)_lights.SelectedItem, null);
                _test.Text = "Stop Test";
                _testing = true;
            }
            catch (Exception ex)
            {
                Log.Error("Start testing", ex);
                _testing = false;
            }
        }

        private void TestSiren_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            StopTesting();
        }
    }
}
