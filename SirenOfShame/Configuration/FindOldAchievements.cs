using System;
using System.Diagnostics;
using System.Windows.Forms;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Configuration
{
    public partial class FindOldAchievements : Form
    {
        private readonly SirenOfShameSettings _settings;

        public FindOldAchievements(SirenOfShameSettings settings)
        {
            _settings = settings;
            InitializeComponent();
        }

        private void OkClick(object sender, EventArgs e)
        {
            Close();
            if (_configureSosOnline.Checked)
                ShowConfigureSosOnline();
        }

        private void ShowConfigureSosOnline()
        {
            var dialog = new ConfigureSosOnline(_settings);
            dialog.ShowDialog();
        }

        public static void TryFindOldAchievements(SirenOfShameSettings settings)
        {
            FindOldAchievements form = new FindOldAchievements(settings);
            form.Show();
        }

        private void _details_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://sirenofshame.com/MyCi/Overview");
        }
    }
}
