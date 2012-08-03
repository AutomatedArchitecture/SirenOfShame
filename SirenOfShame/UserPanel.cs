using System;
using System.Globalization;
using System.Windows.Forms;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame
{
    public partial class UserPanel : UserControl
    {
        public UserPanel(PersonSetting person, ImageList avatarImageList)
        {
            RawName = person.RawName;

            InitializeComponent();

            avatar1.SetImage(person, avatarImageList);
            _displayName.Text = person.DisplayName;
            RefreshStats(person);
        }

        public string RawName { get; set; }

        public int AvatarId
        {
            get { return avatar1 == null ? 0 : avatar1.ImageIndex; }
            set { if (avatar1 != null) avatar1.ImageIndex = value; }
        }

        //private void UserPanelPaint(object sender, PaintEventArgs e)
        //{
        //    var borderColor = Color.LightGray;
        //    var backgroundColor = Color.FromArgb(255, 245, 245, 245);

        //    e.Graphics.FillRoundedRectangle(new SolidBrush(backgroundColor), ClientRectangle, 5);
        //    e.Graphics.DrawRoundedRectangle(new Pen(borderColor), 0, 0, Width - 1, Height - 1, 5);
        //}

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

        private void flowLayoutPanel1_Click(object sender, EventArgs e)
        {
            OnClick(new EventArgs());
        }
    }
}
