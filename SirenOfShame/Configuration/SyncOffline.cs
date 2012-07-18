using System;
using System.Windows.Forms;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame.Configuration
{
    public partial class SyncOffline : UserControl
    {
        private readonly SirenOfShameSettings _settings;

        public SyncOffline(SirenOfShameSettings settings)
        {
            _settings = settings;
            InitializeComponent();
            _settings.InitializeUserIAm(_userIAm);
        }

        private void RefreshOfflinePanel()
        {
            _exportedBuilds.Text = "";
            _exportedAchievements.Text = "";
            var sosDb = new SosDb();
            var exportedBuilds = sosDb.ExportNewBuilds(_settings);
            if (exportedBuilds == null)
            {
                return;
            }
            string exportedAchievements = _settings.ExportNewAchievements();
            _exportedBuilds.Text = exportedBuilds;
            _exportedAchievements.Text = exportedAchievements;
        }

        private void SaveUserIAm()
        {
            string myRawName = Settings.UserIamIsUnselected(_userIAm) ? null : ((PersonSetting)_userIAm.SelectedItem).RawName;
            _settings.MyRawName = myRawName;
        }

        private void SaveResultsClick(object sender, EventArgs e)
        {
            var newHighWaterMarkStr = _result.Text;
            long newHighwaterMarkInt;
            if (long.TryParse(newHighWaterMarkStr, out newHighwaterMarkInt))
            {
                var dateTime = new DateTime(newHighwaterMarkInt);
                if (dateTime.Year < 2012 || dateTime.Year > 3000)
                {
                    InvalidHighWaterMark();
                }
                else
                {
                    _settings.SosOnlineHighWaterMark = dateTime.Ticks;
                    _settings.Save();
                    if (ParentForm == null) throw new Exception("ParentForm was null");
                    ParentForm.Close();
                }
            } else
            {
                InvalidHighWaterMark();
            }
        }

        private static void InvalidHighWaterMark()
        {
            SosMessageBox.Show("Invalid Highwater Mark", "The highwater mark should be an 18 digit number that was returned as part of the xml results when you hit submit.", "Now I get it");
        }

        private void CopyBuildsToClipboardLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _exportedBuilds.SelectAll();
            _exportedBuilds.Focus();
            Clipboard.SetText(_exportedBuilds.Text);
        }

        private void CopyAchievementsToClipboardLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _exportedAchievements.SelectAll();
            _exportedAchievements.Focus();
            var text = _exportedAchievements.Text;
            if (!string.IsNullOrEmpty(text))
                Clipboard.SetText(text);
        }

        private void UserIAmSelectedIndexChanged(object sender, EventArgs e)
        {
            SaveUserIAm();
            RefreshOfflinePanel();
        }
    }
}
