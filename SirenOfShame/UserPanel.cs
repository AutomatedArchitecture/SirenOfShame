using System.Globalization;
using System.Windows.Forms;
using SirenOfShame.Lib;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame
{
    public partial class UserPanel : UserControlBase
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

        public string DisplayName
        {
            get { return _displayName.Text; }
            set { _displayName.Text = value; }
        }

        public void RefreshStats(PersonSetting person)
        {
            _reputation.Text = person.GetReputation().ToString(CultureInfo.InvariantCulture);
            _achievements.Text = person.Achievements.Count.ToString(CultureInfo.InvariantCulture);
            _failPercent.Text = string.Format("{0:p1}", person.CurrentBuildRatio).Replace(" ", "");
            _successfulBuildsInARow.Text = string.Format("{0}", person.CurrentSuccessInARow);
            _fixedSomeoneElsesBuild.Text = string.Format("{0}", person.NumberOfTimesFixedSomeoneElsesBuild);
            _totalBuilds.Text = string.Format("{0}", person.TotalBuilds);
        }
    }
}
