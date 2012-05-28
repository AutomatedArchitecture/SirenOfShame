using System;
using System.Windows.Forms;

namespace SirenOfShame.Lib.Settings.Upgrades
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
            Close();
        }

        public static void TryFindOldAchievements(SirenOfShameSettings settings)
        {
            FindOldAchievements form = new FindOldAchievements(settings);
            form.Show();
        }
    }
}
