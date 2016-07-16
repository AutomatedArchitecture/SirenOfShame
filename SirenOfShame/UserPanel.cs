using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using SirenOfShame.Lib;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame
{
    public partial class UserPanel : UserControlBase
    {
        private float _baseFontSize;
        private float _baseFontSizeReputation;
        private float _baseFontSizeRank;
        private float _baseFontSizeDisplayName;
        private float _baseFontSizeSuccessfulBuildsInARow;

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
            FontChanged += OnFontChanged;
            StoreInitialFontSizes();
        }

        private void OnFontChanged(object sender, EventArgs e)
        {
            var newBaseFontSize = Font.Size;
            var fontSizeMultiplier = newBaseFontSize / _baseFontSize;
            ResetFontSize(fontSizeMultiplier, _reputation, _baseFontSizeReputation);
            ResetFontSize(fontSizeMultiplier, _displayName, _baseFontSizeDisplayName);
            ResetFontSize(fontSizeMultiplier, _rank, _baseFontSizeRank);
            ResetFontSize(fontSizeMultiplier, _successfulBuildsInARow, _baseFontSizeSuccessfulBuildsInARow);
            ResetFontSize(fontSizeMultiplier, _fixedSomeoneElsesBuild, _baseFontSizeSuccessfulBuildsInARow);
            ResetFontSize(fontSizeMultiplier, _totalBuilds, _baseFontSizeSuccessfulBuildsInARow);
            ResetFontSize(fontSizeMultiplier, label2, _baseFontSizeSuccessfulBuildsInARow);
            ResetFontSize(fontSizeMultiplier, label3, _baseFontSizeSuccessfulBuildsInARow);
            ResetFontSize(fontSizeMultiplier, label5, _baseFontSizeSuccessfulBuildsInARow);

        }

        private void ResetFontSize(float fontSizeMultiplier, Label label, float baseSize)
        {
            var newProjectNameSize = baseSize * fontSizeMultiplier;
            label.Font = new Font(label.Font.FontFamily, newProjectNameSize, label.Font.Style);
        }

        private void StoreInitialFontSizes()
        {
            _baseFontSize = Font.Size;
            _baseFontSizeReputation = _reputation.Font.Size;
            _baseFontSizeDisplayName = _displayName.Font.Size;
            _baseFontSizeRank = _rank.Font.Size;
            _baseFontSizeSuccessfulBuildsInARow = _successfulBuildsInARow.Font.Size;
        }

        public string RawName { get; private set; }

        public void RefreshAvatar(PersonSetting personSetting, ImageList imageList)
        {
            avatar1.SetImage(personSetting, imageList);
        }
        
        public string DisplayName
        {
            get { return _displayName.Text; }
            set { _displayName.Text = value; }
        }

        public int Rank
        {
            set { _rank.Text = $"#{value}"; }
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
