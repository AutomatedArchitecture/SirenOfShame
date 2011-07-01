using System;
using System.Diagnostics;
using SirenOfShame.Lib;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Configuration
{
    public partial class Settings : FormBase
    {
        private readonly SirenOfShameSettings _settings;
        private string _logFilename;

        public Settings(SirenOfShameSettings settings)
        {
            _settings = settings;
            InitializeComponent();

            _pollInterval.Value = _settings.PollInterval;
            RefreshDurationText();

            try
            {
                _logFilename = MyLogManager.GetLogFilename();
                _viewLog.Enabled = true;
            }
            catch (Exception)
            {
                _viewLog.Enabled = false;
            }
        }

        private void OkClick(object sender, System.EventArgs e)
        {
            _settings.PollInterval = _pollInterval.Value;
            _settings.Save();
            Close();
        }

        private void CancelClick(object sender, System.EventArgs e)
        {
            Close();
        }

        private void PollIntervalValueChanged(object sender, System.EventArgs e)
        {
            RefreshDurationText();
        }

        private void RefreshDurationText()
        {
            string snideComment = ".";
            if (_pollInterval.Value <= 5)
                snideComment = ". (yes, I hate my sys admin that much)";
            if (_pollInterval.Value <= 1)
                snideComment = ". (yes, I really hate my co-workers that much, but s'alright, will blame the authors of this app)";
            _duration.Text = string.Format("{0} seconds{1}", _pollInterval.Value, snideComment);
        }

        private void _viewLog_Click(object sender, EventArgs e)
        {
            Process.Start(_logFilename);
        }
    }
}
