using System;
using System.Linq;
using System.Windows.Forms;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

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

        private void Button1Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OkClick(object sender, EventArgs e)
        {
            var sosDb = new SosDb();
            var allSettings = sosDb
                .ReadAll(_settings.GetAllActiveBuildDefinitions())
                .OrderBy(i => i.StartedTime);
        }

        public static void TryFindOldAchievements(SirenOfShameSettings settings)
        {
            FindOldAchievements form = new FindOldAchievements(settings);
            form.Show();
        }
    }
}
