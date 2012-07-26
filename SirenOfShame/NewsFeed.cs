using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using SirenOfShame.Lib.Watcher;

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
            _newsItemHeightAnimator.Interval = 50;
            _newsItemHeightAnimator.Tick += NewsItemHeightAnimatorOnTick;
            _prettyDateCalculator.Interval = 10000;
            _prettyDateCalculator.Tick += PrettyDateCalculatorOnTick;
            _prettyDateCalculator.Start();
        }

        private void PrettyDateCalculatorOnTick(object sender, EventArgs eventArgs)
        {
            GetNewsItemControls().ToList().ForEach(i => i.RecalculatePrettyDate());
        }

        readonly Timer _newsItemHeightAnimator = new Timer();
        readonly Timer _prettyDateCalculator = new Timer();
        private readonly List<NewsItem> _newsItemsToOpen = new List<NewsItem>();

        private void NewsItemHeightAnimatorOnTick(object sender, EventArgs eventArgs)
        {
            if (_newsItemsToOpen.Count == 0 || _newsItemsToOpen.All(i => i.IsAtIdealHeight()))
            {
                _newsItemHeightAnimator.Stop();
                _newsItemsToOpen.Clear();
                return;
            }
            
            foreach (var newsItem in _newsItemsToOpen.Where(i => !i.IsAtIdealHeight()))
            {
                int idealHeight = newsItem.GetIdealHeight();
                if (newsItem.Height < idealHeight)
                {
                    newsItem.Height = IncreaseWithEase(newsItem.Height, idealHeight);
                }
            }
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

        public void AddNewsItem(NewNewsItemEventArgs args)
        {
            var newsItem = new NewsItem(args.Person, args.Title, args.EventDate) { Dock = DockStyle.Top };
            Controls.Add(newsItem);
            newsItem.Height = 2;
            _newsItemsToOpen.Add(newsItem);
            _newsItemHeightAnimator.Start();
        }
    }
}
