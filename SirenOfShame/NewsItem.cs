using System;
using System.Drawing;
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

        public void ChangeImageIndex(int index)
        {
            avatar1.ImageIndex = index;
        }

        private void InvokeOnOnUserClicked()
        {
            UserClicked handler = OnUserClicked;
            if (handler != null) handler(this, new UserClickedArgs { RawUserName = _rawUserName});
        }

        private NewsItem(PersonBase user, string checkinComment, DateTime date, ImageList avatarImageList)
        {
            InitializeComponent();

            richTextBox1.Clear();
            richTextBox1.SelectionFont = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            richTextBox1.SelectedText = user.DisplayName;
            richTextBox1.SelectionFont = _regularFont;
            richTextBox1.SelectedText = "\r\n" + checkinComment;
            _lastPrettyDate = date.PrettyDate();
            richTextBox1.SelectionColor = Color.Gray;
            richTextBox1.SelectedText = "\r\n\r\n" + _lastPrettyDate;

            avatar1.SetImage(user, avatarImageList);
            _rawUserName = user.RawName;
            avatar1.Cursor = user.Clickable ? Cursors.Hand : Cursors.Default;

            EventDate = date;
        }

        public NewsItem(NewNewsItemEventArgs args)
            : this(args.Person, args.Title, args.EventDate, args.AvatarImageList)
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
                const int marginToAccountForBoldFont = 1;
                int renderWidth = richTextBox1.Width - marginToAccountForBoldFont;
                SizeF size = g.MeasureString(richTextBox1.Text, _regularFont, renderWidth);
                int textBoxHeight = (int)Math.Ceiling(size.Height);

                return Margin.Top + Margin.Bottom + textBoxHeight + panel1.Height;
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
    }

    public delegate void UserClicked(object sender, UserClickedArgs args);

    public class UserClickedArgs
    {
        public string RawUserName { get; set; }
    }
}
