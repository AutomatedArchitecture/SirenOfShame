using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace SirenOfShame
{
    public partial class NewsFeed : UserControl
    {
        private int IncreaseWithEase(int oldValue, int destination)
        {
            int newValue = (int)(Math.Pow(oldValue, 1.6)) + 2;
            return Math.Min(newValue, destination);
        }

        public NewsFeed()
        {
            InitializeComponent();
            newsItemHeightAnimator.Interval = 50;
            newsItemHeightAnimator.Tick += NewsItemHeightAnimatorOnTick;
            prettyDateCalculator.Interval = 10000;
            prettyDateCalculator.Tick += PrettyDateCalculatorOnTick;
            prettyDateCalculator.Start();
        }

        private void PrettyDateCalculatorOnTick(object sender, EventArgs eventArgs)
        {
            GetNewsItemControls().ToList().ForEach(i => i.RecalculatePrettyDate());
        }

        Timer newsItemHeightAnimator = new Timer();
        Timer prettyDateCalculator = new Timer();
        private List<NewsItem> newsItemsToOpen = new List<NewsItem>();

        private void NewsItemHeightAnimatorOnTick(object sender, EventArgs eventArgs)
        {
            if (newsItemsToOpen.Count == 0 || newsItemsToOpen.All(i => i.IsAtIdealHeight()))
            {
                newsItemHeightAnimator.Stop();
                newsItemsToOpen.Clear();
                return;
            }
            
            foreach (var newsItem in newsItemsToOpen.Where(i => !i.IsAtIdealHeight()))
            {
                int idealHeight = newsItem.GetIdealHeight();
                if (newsItem.Height < idealHeight)
                {
                    newsItem.Height = IncreaseWithEase(newsItem.Height, idealHeight);
                }
            }
        }

        private int _newsItemCount = 0;

        private void Button1Click(object sender, EventArgs e)
        {
            string userName = "Joe Ferner " + _newsItemCount;
            const string description = " broke the build with a comment of \"Fixing Lee's bunk check-in from yesterday\"";
            var newsItem = new NewsItem(userName, description, DateTime.Now) {Dock = DockStyle.Top};
            Controls.Add(newsItem);
            newsItem.Height = 2;
            _newsItemCount++;
            newsItemsToOpen.Add(newsItem);
            newsItemHeightAnimator.Start();
        }

        private void NewsFeedResize(object sender, EventArgs e)
        {
            GetNewsItemControls()
                .ToList()
                .ForEach(i => i.Height = i.GetIdealHeight());
        }

        private IEnumerable<NewsItem> GetNewsItemControls()
        {
            return Controls
                .Cast<Control>()
                .Select(i => i as NewsItem)
                .Where(i => i != null);
        }
    }
}
