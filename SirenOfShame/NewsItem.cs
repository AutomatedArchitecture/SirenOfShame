using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using SirenOfShame.Lib;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame
{
    public partial class NewsItem : UserControlBase
    {
        private DateTime EventDate { get; set; }
        private string _rawUserName;
        public event UserClicked OnUserClicked;
        private float _baseFontSize;
        private float _baseFontSizeUserName;
        private float _baseFontSizeReputationChange;
        private float _baseFontSizeWhen;
        private float _baseFontSizeProject;

        public string RawName {
            get { return _rawUserName; }
        }

        private static void SetLabelTextAndWidth(Label label, UserControl parent, string text, int padding)
        {
                label.Text = text;
                using (Graphics g = parent.CreateGraphics())
                {
                    label.Width = ((int)g.MeasureString(label.Text, label.Font).Width) + padding;
                }
        }
        
        public string DisplayName
        {
            set
            {
                SetLabelTextAndWidth(_userName, this, value, 6);
            }
        }

        private string ReputationChange
        {
            set
            {
                SetLabelTextAndWidth(_reputationChange, this, value, 6);
            }
        }

        public string BuildId { get; private set; }

        public void ChangeImageIndex(int index)
        {
            avatar1.ImageIndex = index;
        }

        private void InvokeOnOnUserClicked()
        {
            UserClicked handler = OnUserClicked;
            if (handler != null) handler(this, new UserClickedArgs { RawUserName = _rawUserName});
        }

        public NewsItem()
        {
            InitializeComponent();
            FontChanged += OnFontChanged;
            StoreInitialFontSizes();
        }

        private void OnFontChanged(object sender, EventArgs e)
        {
            var newBaseFontSize = Font.Size;
            var fontSizeMultiplier = newBaseFontSize / _baseFontSize;
            ResetFontSize(fontSizeMultiplier, _userName, _baseFontSizeUserName);
            ResetFontSize(fontSizeMultiplier, _reputationChange, _baseFontSizeReputationChange);
            ResetFontSize(fontSizeMultiplier, _when, _baseFontSizeWhen);
            ResetFontSize(fontSizeMultiplier, _project, _baseFontSizeProject);
        }

        private void ResetFontSize(float fontSizeMultiplier, Label label, float baseSize)
        {
            var newProjectNameSize = baseSize * fontSizeMultiplier;
            label.Font = new Font(label.Font.FontFamily, newProjectNameSize, label.Font.Style);
        }

        private void StoreInitialFontSizes()
        {
            _baseFontSize = Font.Size;
            _baseFontSizeUserName = _userName.Font.Size;
            _baseFontSizeProject = _project.Font.Size;
            _baseFontSizeWhen = _when.Font.Size;
            _baseFontSizeReputationChange = _reputationChange.Font.Size;
        }

        public void Initialize(NewNewsItemEventArgs args)
        {
            _rawUserName = args.Person.RawName;
            BuildId = args.BuildId;
            EventDate = args.EventDate;

            InitializeUserNameLabel(args);
            InitializeReputationChangeLabel(args.ReputationChange);
            WriteProject(args);
            WriteTitle(args);
            WriteDate(args.EventDate);
            InitializeAvatar(args);
            InitializeMetroColors(args);
        }

        private void InitializeMetroColors(NewNewsItemEventArgs args)
        {
            Color backColor = GetBackgroundColorForEventType(args.NewsItemType);
            _leftPanel.BackColor = backColor;
            _project.BackColor = backColor;
            _userName.BackColor = backColor;
            _when.BackColor = backColor;
            _reputationChange.BackColor = backColor;
        }

        private void InitializeAvatar(NewNewsItemEventArgs args)
        {
            avatar1.SetImage(args.Person, args.AvatarImageList);
            avatar1.Cursor = args.Person.Clickable ? Cursors.Hand : Cursors.Default;
        }

        private void InitializeUserNameLabel(NewNewsItemEventArgs args)
        {
            DisplayName = args.Person.DisplayName;
        }

        private void WriteDate(DateTime eventDate)
        {
            _when.Text = eventDate.PrettyDate();
        }

        private void WriteTitle(NewNewsItemEventArgs args)
        {
            string title = args.NewsItemType == NewsItemTypeEnum.SosOnlineComment ? "\"" + args.Title + "\"" : args.Title;
            _title.Text = title;
        }

        private void WriteProject(NewNewsItemEventArgs args)
        {
            _project.Visible = !string.IsNullOrEmpty(args.BuildDefinitionId);
            if (args.BuildDefinitionId == null) return;
            _project.Text = args.BuildDefinitionId.ToUpperInvariant();
        }

        private void InitializeReputationChangeLabel(int? reputationChange)
        {
            _reputationChange.Visible = reputationChange != null;
            if (reputationChange == null) return;
            ReputationChange = GetNumericAsDelta(reputationChange.Value);
        }

        private string GetNumericAsDelta(int value)
        {
            return value > 0 ? "+" + value : value.ToString(CultureInfo.InvariantCulture);
        }

        public void RecalculatePrettyDate()
        {
            WriteDate(EventDate);
        }

        public int GetIdealHeight()
        {
            return 62;
        }

        public bool IsAtIdealHeight()
        {
            return Height >= GetIdealHeight();
        }

        private void Avatar1Click(object sender, EventArgs e)
        {
            bool isAvatarClickable = _rawUserName != null;
            if (isAvatarClickable)
                InvokeOnOnUserClicked();
        }

        private static Color GetColorForEventType(Dictionary<NewsItemTypeEnum, Color> dictionary, NewsItemTypeEnum newsItemEventType, Color defaultColor)
        {
            Color color;
            if (dictionary.TryGetValue(newsItemEventType, out color))
                return color;
            return defaultColor;
        }

        private static readonly Dictionary<NewsItemTypeEnum, Color> _newsTypeToBorderColorMap = new Dictionary<NewsItemTypeEnum, Color>
        {
            { NewsItemTypeEnum.BuildSuccess, ViewBuildBase.SuccessColor },
            { NewsItemTypeEnum.SosOnlineComment, Color.FromArgb(255, 0, 102, 221) },
            { NewsItemTypeEnum.SosOnlineMisc, Color.FromArgb(255, 73, 175, 205) },
            { NewsItemTypeEnum.SosOnlineNewAchievement, Color.FromArgb(255, 213, 160, 9) },
            { NewsItemTypeEnum.SosOnlineNewMember, Color.FromArgb(255, 73, 175, 205) },
            { NewsItemTypeEnum.SosOnlineReputationChange, Color.FromArgb(255, 73, 175, 205) },
            { NewsItemTypeEnum.BuildFailed, ViewBuildBase.FailColor },
            { NewsItemTypeEnum.NewAchievement, Color.FromArgb(255, 213, 160, 9) },
        };

        private static Color GetBackgroundColorForEventType(NewsItemTypeEnum newsItemEventType)
        {
            return GetColorForEventType(_newsTypeToBorderColorMap, newsItemEventType, ViewBuildBase.PrimaryColor);
        }

        private void NewsItemResize(object sender, EventArgs e)
        {
            // this fixes a drawing issue on the panel where it wouldn't always redraw the rounded corners background
        }

        public void UpdateState(NewNewsItemEventArgs args)
        {
            InitializeReputationChangeLabel(args.ReputationChange);
            InitializeMetroColors(args);
        }

        private void UserNameClick(object sender, EventArgs e)
        {
            bool isUserClickable = _rawUserName != null;
            if (isUserClickable)
                InvokeOnOnUserClicked();
        }
    }

    public delegate void UserClicked(object sender, UserClickedArgs args);

    public class UserClickedArgs
    {
        public string RawUserName { get; set; }
    }
}
