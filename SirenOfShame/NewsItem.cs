using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.Watcher;
using SirenOfShame.Helpers;

namespace SirenOfShame
{
    public partial class NewsItem : UserControl
    {
        private readonly NewsItemTypeEnum _newsItemEventType;
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
            get { return _userName.Text; }
            set { _userName.Text = value; }
        }

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
            _newsItemEventType = args.NewsItemType;
            _rawUserName = args.Person.RawName;
            _lastPrettyDate = args.EventDate.PrettyDate();
            
            InitializeComponent();

            _userName.Text = args.Person.DisplayName;
            _userName.BackColor = GetBackgroundColorForEventType(args.NewsItemType);

            SetReputationChange(args.ReputationChange, args.NewsItemType);

            richTextBox1.Clear();
            richTextBox1.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Italic, GraphicsUnit.Point, 0);
            richTextBox1.SelectedText = "\r\n" + args.Project;
            richTextBox1.SelectionFont = _regularFont;
            richTextBox1.SelectedText = "\r\n" + args.Title;
            richTextBox1.SelectionColor = Color.Gray;
            richTextBox1.SelectedText = "\r\n\r\n" + _lastPrettyDate;

            avatar1.SetImage(args.Person, args.AvatarImageList);
            avatar1.Cursor = args.Person.Clickable ? Cursors.Hand : Cursors.Default;

            EventDate = args.EventDate;
        }

        private void SetReputationChange(int? reputationChange, NewsItemTypeEnum newsItemType)
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
                int mainContentHeight = richTextBoxHeight + _userName.Height;

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

        private void Panel1Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.FillRoundedRectangle(new SolidBrush(GetBackgroundColorForEventType(_newsItemEventType)), panel1.ClientRectangle, 5);
            graphics.DrawRoundedRectangle(new Pen(GetBorderColorForEventType(_newsItemEventType)), 0, 0, panel1.Width - 1, panel1.Height - 1, 5);
        }

        private static Color GetColorForEventType(Dictionary<NewsItemTypeEnum, Color> dictionary, NewsItemTypeEnum newsItemEventType, Color defaultColor)
        {
            Color color;
            if (dictionary.TryGetValue(newsItemEventType, out color))
                return color;
            return defaultColor;
        }
        
        private Color GetBorderColorForEventType(NewsItemTypeEnum newsItemEventType)
        {
            return GetColorForEventType(_newsTypeToBorderColorMap, newsItemEventType, Color.LightGray);
        }

        private static readonly Dictionary<NewsItemTypeEnum, Color> _newsTypeToBorderColorMap = new Dictionary<NewsItemTypeEnum, Color>
        {
            { NewsItemTypeEnum.BuildSuccess, Color.FromArgb(255, 50, 175, 82) },
            { NewsItemTypeEnum.SosOnlineComment, Color.FromArgb(255, 88, 135, 182) },
            { NewsItemTypeEnum.BuildFailed, Color.FromArgb(255, 222, 64, 82) },
        };
        
        private static readonly Dictionary<NewsItemTypeEnum, Color> _newsTypeToBackgroundColorMap = new Dictionary<NewsItemTypeEnum, Color>
        {
            { NewsItemTypeEnum.BuildSuccess, Color.FromArgb(255, 219, 255, 228) },
            { NewsItemTypeEnum.SosOnlineComment, Color.FromArgb(255, 235, 245, 251) },
            { NewsItemTypeEnum.BuildFailed, Color.FromArgb(255, 255, 234, 226) },
        };
        
        private static Color GetBackgroundColorForEventType(NewsItemTypeEnum newsItemEventType)
        {
            return GetColorForEventType(_newsTypeToBackgroundColorMap, newsItemEventType, Color.FromArgb(255, 245, 245, 245));
        }

        private void NewsItemResize(object sender, EventArgs e)
        {
            // this fixes a drawing issue on the panel where it wouldn't always redraw the rounded corners background
            panel1.Invalidate();
        }
    }

    public delegate void UserClicked(object sender, UserClickedArgs args);

    public class UserClickedArgs
    {
        public string RawUserName { get; set; }
    }
}
