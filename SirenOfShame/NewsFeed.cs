using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame
{
    public partial class NewsFeed : UserControl
    {
        public event UserClicked OnUserClicked;

        private void InvokeOnOnUserClicked(UserClickedArgs args)
        {
            UserClicked handler = OnUserClicked;
            if (handler != null) handler(this, args);
        }

        public void ChangeUserAvatarId(string rawUserName, int newImageIndex)
        {
            IEnumerable<NewsItem> newsItemControls = NewsItemControlsForUser(rawUserName);
            newsItemControls.ToList().ForEach(i => i.ChangeImageIndex(newImageIndex));
        }

        private IEnumerable<NewsItem> NewsItemControlsForUser(string rawUserName)
        {
            return GetNewsItemControls().Where(newsItemControl => newsItemControl.RawName == rawUserName);
        }

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
            return _newsItemsPanel.Controls
                .Cast<Control>()
                .Select(i => i as NewsItem)
                .Where(i => i != null);
        }

        public void AddNewsItem(NewNewsItemEventArgs args)
        {
            var newsItem = new NewsItem(args) { Dock = DockStyle.Top };
            newsItem.OnUserClicked += NewsItemOnOnUserClicked;
            newsItem.MouseEnter += NewsItemOnMouseEnter;
            _newsItemsPanel.Controls.Add(newsItem);
            newsItem.Height = 2;
            _newsItemsToOpen.Add(newsItem);
            _newsItemHeightAnimator.Start();
        }

        private void NewsItemOnMouseEnter(object sender, EventArgs eventArgs)
        {
            EnableMouseScrollWheel();
        }

        private void EnableMouseScrollWheel()
        {
            _newsItemsPanel.Focus();
        }

        private void NewsItemOnOnUserClicked(object sender, UserClickedArgs args)
        {
            InvokeOnOnUserClicked(args);
        }

        private void ClearNewsClick(object sender, EventArgs e)
        {
            _newsItemsPanel.Controls.Clear();
        }

        private void NewsFeed_MouseEnter(object sender, EventArgs e)
        {
            EnableMouseScrollWheel();
        }

        private void _newsItemsPanel_MouseEnter(object sender, EventArgs e)
        {
            EnableMouseScrollWheel();
        }

        public void RefreshDisplayNames(SirenOfShameSettings settings, UserDisplayNameChangedArgs args)
        {
            GetNewsItemControls()
                .Where(i => i.RawName == args.RawUserName)
                .ToList()
                .ForEach(i => i.DisplayName = args.NewDisplayName);
        }
    }
}
