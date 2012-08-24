using System;
using System.Globalization;
using System.Windows.Forms;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame
{
    public partial class UserPanel : UserControl
    {
        public UserPanel()
        {
            
        }

        public UserPanel(PersonSetting person, ImageList avatarImageList)
        {
            RawName = person.RawName;

            InitializeComponent();

            avatar1.SetImage(person, avatarImageList);
            _displayName.Text = person.DisplayName;
            RefreshStats(person);
        }

        public string RawName { get; private set; }

        public int AvatarId
        {
            set { if (avatar1 != null) avatar1.ImageIndex = value; }
        }

        private void Avatar1Click(object sender, EventArgs e)
        {
            OnClick(new EventArgs());
        }

        private void DisplayNameClick(object sender, EventArgs e)
        {
            OnClick(new EventArgs());
        }

        private void ReputationClick(object sender, EventArgs e)
        {
            OnClick(new EventArgs());
        }

        private void AchievementsClick(object sender, EventArgs e)
        {
            OnClick(new EventArgs());
        }

        private void PictureBox1Click(object sender, EventArgs e)
        {
            OnClick(new EventArgs());
        }

        private void Avatar1MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(new EventArgs());
        }

        public void RefreshStats(PersonSetting person)
        {
            _reputation.Text = person.GetReputation().ToString(CultureInfo.InvariantCulture);
            _achievements.Text = person.Achievements.Count.ToString(CultureInfo.InvariantCulture);
        }

        private void Panel1Click(object sender, EventArgs e)
        {
            OnClick(new EventArgs());
        }

        private void FlowLayoutPanel1Click(object sender, EventArgs e)
        {
            OnClick(new EventArgs());
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(new EventArgs());
        }
    }
}
