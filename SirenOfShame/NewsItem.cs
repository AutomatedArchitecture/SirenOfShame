using System;
using System.Drawing;
using System.Windows.Forms;
using SirenOfShame.Lib.Helpers;

namespace SirenOfShame
{
    public partial class NewsItem : UserControl
    {
        readonly Font _regularFont = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);

        private DateTime EventDate { get; set; }

        public NewsItem(string userName, string checkinComment, DateTime date)
        {
            InitializeComponent();

            richTextBox1.Clear();
            richTextBox1.SelectionFont = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            richTextBox1.SelectedText = userName;
            richTextBox1.SelectionFont = _regularFont;
            richTextBox1.SelectedText = "\r\n" + checkinComment;

            EventDate = date;
            _eventDate.Text = date.PrettyDate();
        }

        public NewsItem() : this("Joe Ferner", " started a build with a comment of \"Fixing Lee's bunk check-in from yesterday\"", DateTime.Now)
        {
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
    }
}
