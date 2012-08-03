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
        readonly Font _regularFont = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);

        private DateTime EventDate { get; set; }
        private readonly string _rawUserName;
        public event UserClicked OnUserClicked;
        string _lastPrettyDate;

        public string RawName {
            get { return _rawUserName; }
        }

        public string DisplayName
        {
            set { _userName.Text = value; }
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
            _lastPrettyDate = args.EventDate.PrettyDate();
            BuildId = args.BuildId;
            EventDate = args.EventDate;
            
            InitializeComponent();

            InitializeUserNameLabel(args);
            InitializeReputationChangeLabel(args.ReputationChange);
            InitializeRichTextBox(args);
            InitializeAvatar(args);
            InitializeMetroColors(args);
        }

        private void InitializeMetroColors(NewNewsItemEventArgs args)
        {
            Color backColor = GetBackgroundColorForEventType(args.NewsItemType);
            _metroPanel.BackColor = backColor;
            _userName.BackColor = backColor;
            richTextBox1.BackColor = backColor;
        }

        private void InitializeAvatar(NewNewsItemEventArgs args)
        {
            avatar1.SetImage(args.Person, args.AvatarImageList);
            avatar1.Cursor = args.Person.Clickable ? Cursors.Hand : Cursors.Default;
        }

        private void InitializeRichTextBox(NewNewsItemEventArgs args)
        {
            richTextBox1.Clear();
            WriteProject(args);
            WriteTitle(args);
            WriteDate();
        }

        private void InitializeUserNameLabel(NewNewsItemEventArgs args)
        {
            _userName.Text = args.Person.DisplayName;
        }

        private void WriteDate()
        {
            richTextBox1.SelectionColor = Color.Gray;
            richTextBox1.SelectedText = "\r\n\r\n" + _lastPrettyDate;
        }

        private void WriteTitle(NewNewsItemEventArgs args)
        {
            string title = args.NewsItemType == NewsItemTypeEnum.SosOnlineComment ? "\"" + args.Title + "\"" : args.Title;
            richTextBox1.SelectionFont = _regularFont;
            richTextBox1.SelectedText = "\r\n" + title;
        }

        private void WriteProject(NewNewsItemEventArgs args)
        {
            if (string.IsNullOrEmpty(args.Project)) return;
            richTextBox1.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Italic, GraphicsUnit.Point, 0);
            richTextBox1.SelectedText = "\r\n" + args.Project;
        }

        private void InitializeReputationChangeLabel(int? reputationChange)
        {
            _reputationChange.Visible = reputationChange != null;
            if (reputationChange == null) return;
            _reputationChange.BackColor = Color.Transparent;
            _reputationChange.PillColor = reputationChange.Value < 0
                                              ? Color.FromArgb(255, 203, 59, 75)
                                              : Color.FromArgb(255, 43, 151, 71);
            _reputationChange.Text = GetNumericAsDelta(reputationChange.Value);
        }

        private string GetNumericAsDelta(int value)
        {
            return value > 0 ? "+" + value : value.ToString(CultureInfo.InvariantCulture);
        }

        public void RecalculatePrettyDate()
        {
            var lastPrettyDate = _lastPrettyDate;
            var newPrettyDate = EventDate.PrettyDate();
            richTextBox1.Find(lastPrettyDate);
            richTextBox1.SelectedText = newPrettyDate;
            _lastPrettyDate = EventDate.PrettyDate();
        }

        public int GetIdealHeight()
        {
            using (Graphics g = CreateGraphics())
            {
                int renderWidth = richTextBox1.Width;
                SizeF size = g.MeasureString(richTextBox1.Text, _regularFont, renderWidth);
                int richTextBoxHeight = (int)Math.Ceiling(size.Height);
                int margins = (panel1.Location.Y + _metroPanel.Location.Y) * 2;
                int mainContentHeight = richTextBoxHeight + _userName.Height + margins;

                return Math.Max(mainContentHeight, avatar1.Height);
            }
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
            { NewsItemTypeEnum.BuildSuccess, Color.FromArgb(255, 50, 175, 82) },
            { NewsItemTypeEnum.SosOnlineComment, Color.FromArgb(255, 88, 135, 182) },
            { NewsItemTypeEnum.SosOnlineMisc, Color.FromArgb(255, 88, 135, 182) },
            { NewsItemTypeEnum.SosOnlineNewAchievement, Color.FromArgb(255, 213, 160, 9) },
            { NewsItemTypeEnum.SosOnlineNewMember, Color.FromArgb(255, 88, 135, 182) },
            { NewsItemTypeEnum.SosOnlineReputationChange, Color.FromArgb(255, 88, 135, 182) },
            { NewsItemTypeEnum.BuildFailed, Color.FromArgb(255, 222, 64, 82) },
            { NewsItemTypeEnum.NewAchievement, Color.FromArgb(255, 213, 160, 9) },
        };
        
        private static Color GetBackgroundColorForEventType(NewsItemTypeEnum newsItemEventType)
        {
            return GetColorForEventType(_newsTypeToBorderColorMap, newsItemEventType, Color.FromArgb(255, 40, 95, 152));
        }

        private void NewsItemResize(object sender, EventArgs e)
        {
            // this fixes a drawing issue on the panel where it wouldn't always redraw the rounded corners background
            panel1.Invalidate();
        }

        public void UpdateState(NewNewsItemEventArgs args)
        {
            InitializeReputationChangeLabel(args.ReputationChange);
            Invalidate();
            richTextBox1.Invalidate();
            InitializeMetroColors(args);
        }

        private void RichTextBox1MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void Avatar1MouseEnter(object sender, EventArgs e)
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
