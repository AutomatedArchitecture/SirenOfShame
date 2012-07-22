using System;
using System.Drawing;
using System.Windows.Forms;

namespace SirenOfShame
{
    public partial class NewsItem : UserControl
    {
        readonly Font _regularFont = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);

        private const int MARGIN = 3;

        public NewsItem(string userName, string checkinComment)
        {
            InitializeComponent();

            richTextBox1.Top = 0;
            richTextBox1.Left = MARGIN;
            richTextBox1.Width = Width - (MARGIN*2);
            _eventDate.Padding = new Padding(MARGIN, 0, MARGIN, 0);
            
            richTextBox1.Clear();
            richTextBox1.SelectionFont = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            richTextBox1.SelectedText = userName;
            richTextBox1.SelectionFont = _regularFont;
            richTextBox1.SelectedText = checkinComment;
        }

        public NewsItem() : this("Joe Ferner", " started a build with a comment of \"Fixing Lee's bunk check-in from yesterday\"")
        {

        }

        public int GetIdealHeight()
        {
            using (Graphics g = CreateGraphics())
            {
                const int marginToAccountForBoldFont = 1;
                int renderWidth = richTextBox1.Width - marginToAccountForBoldFont;
                SizeF size = g.MeasureString(richTextBox1.Text, _regularFont, renderWidth);
                int textBoxHeight = (int)Math.Ceiling(size.Height);

                return Margin.Top + Margin.Bottom + textBoxHeight + _eventDate.Height + bottomLine.Height;
            }
        }

        public bool IsAtIdealHeight()
        {
            return Height >= GetIdealHeight();
        }
    }
}
