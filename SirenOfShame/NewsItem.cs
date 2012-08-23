using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame
{
    public partial class NewsItem : UserControl
    {
        private DateTime EventDate { get; set; }
        private readonly string _rawUserName;
        public event UserClicked OnUserClicked;

        public string RawName {
            get { return _rawUserName; }
        }

        public string DisplayName
        {
            set
            {
                _userName.Text = value;
                using (Graphics g = CreateGraphics())
                {
                    _userName.Width = (int)g.MeasureString(_userName.Text, _userName.Font).Width;
                }
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

        public NewsItem(NewNewsItemEventArgs args)
        {
            _rawUserName = args.Person.RawName;
            BuildId = args.BuildId;
            EventDate = args.EventDate;
            
            InitializeComponent();

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
            _project.Visible = !string.IsNullOrEmpty(args.Project);
            if (args.Project == null) return;
            _project.Text = args.Project.ToUpperInvariant();
        }

        private void InitializeReputationChangeLabel(int? reputationChange)
        {
            _reputationChange.Visible = reputationChange != null;
            if (reputationChange == null) return;
            _reputationChange.Text = GetNumericAsDelta(reputationChange.Value);
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
            //Invalidate();
            InitializeMetroColors(args);
        }

        private void Avatar1MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void TitleMouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }
    }

    public delegate void UserClicked(object sender, UserClickedArgs args);

    public class UserClickedArgs
    {
        public string RawUserName { get; set; }
    }
}
