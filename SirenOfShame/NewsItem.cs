using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SirenOfShame
{
    public partial class NewsItem : UserControl
    {
        readonly Font _regularFont = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);

        public NewsItem(string userName, string checkinComment)
        {
            InitializeComponent();

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
                int formPadding = Padding.Left + Padding.Right;
                int formWidth = Width;
                int margins = leftMargin.Width + rightMargin.Width;
                int renderWidth = formWidth - (formPadding + margins);
                SizeF size = g.MeasureString(richTextBox1.Text, _regularFont, renderWidth);
                int textBoxHeight = (int)Math.Ceiling(size.Height);

                return textBoxHeight + _eventDate.Height + bottomLine.Height;
            }
        }
    }
}
