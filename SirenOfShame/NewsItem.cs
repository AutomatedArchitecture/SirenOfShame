using System;
using System.Drawing;
using System.Windows.Forms;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame
{
    public partial class NewsItem : UserControl
    {
        readonly Font _regularFont = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);

        private DateTime EventDate { get; set; }
        private readonly string _rawUserName;
        public event UserClicked OnUserClicked;

        private void InvokeOnOnUserClicked()
        {
            UserClicked handler = OnUserClicked;
            if (handler != null) handler(this, new UserClickedArgs { RawUserName = _rawUserName});
        }

        public NewsItem(PersonSetting user, string checkinComment, DateTime date)
        {
            InitializeComponent();

            richTextBox1.Clear();
            richTextBox1.SelectionFont = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            richTextBox1.SelectedText = user.DisplayName;
            richTextBox1.SelectionFont = _regularFont;
            richTextBox1.SelectedText = "\r\n" + checkinComment;

            avatar1.SetPerson(user);
            _rawUserName = user.RawName;

            EventDate = date;
            _eventDate.Text = date.PrettyDate();
        }

        public void RecalculatePrettyDate()
        {
            _eventDate.Text = EventDate.PrettyDate();
        }

        public int GetIdealHeight()
        {
            using (Graphics g = CreateGraphics())
            {
                const int marginToAccountForBoldFont = 1;
                int renderWidth = richTextBox1.Width - marginToAccountForBoldFont;
                SizeF size = g.MeasureString(richTextBox1.Text, _regularFont, renderWidth);
                int textBoxHeight = (int)Math.Ceiling(size.Height);

                return Margin.Top + Margin.Bottom + textBoxHeight + _eventDate.Height;
            }
        }

        public bool IsAtIdealHeight()
        {
            return Height >= GetIdealHeight();
        }

        private void Avatar1Click(object sender, EventArgs e)
        {
            InvokeOnOnUserClicked();
        }
    }

    public delegate void UserClicked(object sender, UserClickedArgs args);

    public class UserClickedArgs
    {
        public string RawUserName { get; set; }
    }
}
