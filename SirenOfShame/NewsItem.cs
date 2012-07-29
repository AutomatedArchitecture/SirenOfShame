using System;
using System.Collections.Generic;
using System.Drawing;
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

        private NewsItem(PersonBase user, string checkinComment, DateTime date, ImageList avatarImageList, NewsItemTypeEnum newsItemEventType)
        {
            _newsItemEventType = newsItemEventType;
            _rawUserName = user.RawName;
            _lastPrettyDate = date.PrettyDate();
            
            InitializeComponent();

            _userName.Text = user.DisplayName;
            _userName.BackColor = GetBackgroundColorForEventType(newsItemEventType);

            richTextBox1.Clear();
            richTextBox1.SelectionFont = _regularFont;
            richTextBox1.SelectedText = "\r\n" + checkinComment;
            richTextBox1.SelectionColor = Color.Gray;
            richTextBox1.SelectedText = "\r\n\r\n" + _lastPrettyDate;

            avatar1.SetImage(user, avatarImageList);
            avatar1.Cursor = user.Clickable ? Cursors.Hand : Cursors.Default;

            EventDate = date;
        }

        public NewsItem(NewNewsItemEventArgs args)
            : this(args.Person, args.Title, args.EventDate, args.AvatarImageList, args.NewsItemType)
        {
            
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
            graphics.DrawRoundedRectangle(new Pen(GetPenColorForEventType(_newsItemEventType)), 0, 0, panel1.Width - 1, panel1.Height - 1, 5);
        }

        private static Color GetColorForEventType(Dictionary<NewsItemTypeEnum, Color> dictionary, NewsItemTypeEnum newsItemEventType, Color defaultColor)
        {
            Color color;
            if (dictionary.TryGetValue(newsItemEventType, out color))
                return color;
            return defaultColor;
        }
        
        private Color GetPenColorForEventType(NewsItemTypeEnum newsItemEventType)
        {
            return GetColorForEventType(borderColorToColorMapping, newsItemEventType, Color.LightGray);
        }

        private static readonly Dictionary<NewsItemTypeEnum, Color> borderColorToColorMapping = new Dictionary<NewsItemTypeEnum, Color>
        {
            { NewsItemTypeEnum.BuildSuccess, Color.FromArgb(255, 50, 175, 82) },
            { NewsItemTypeEnum.SosOnlineComment, Color.FromArgb(255, 88, 135, 182) },
            { NewsItemTypeEnum.BuildFailed, Color.FromArgb(255, 222, 64, 82) },
        };
        
        private static readonly Dictionary<NewsItemTypeEnum, Color> backgroundColorToColorMapping = new Dictionary<NewsItemTypeEnum, Color>
        {
            { NewsItemTypeEnum.BuildSuccess, Color.FromArgb(255, 219, 255, 228) },
            { NewsItemTypeEnum.SosOnlineComment, Color.FromArgb(255, 235, 245, 251) },
            { NewsItemTypeEnum.BuildFailed, Color.FromArgb(255, 255, 234, 226) },
        };
        
        private static Color GetBackgroundColorForEventType(NewsItemTypeEnum newsItemEventType)
        {
            return GetColorForEventType(backgroundColorToColorMapping, newsItemEventType, Color.FromArgb(255, 245, 245, 245));
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
