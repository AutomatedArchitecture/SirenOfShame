using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame
{
    public partial class NewsFeed : UserControl
    {
        public event UserClicked OnUserClicked;
        private bool _doAnimations = true;

        public new void SuspendLayout()
        {
            _doAnimations = false;
            base.SuspendLayout();
        }

        public new void ResumeLayout()
        {
            _doAnimations = true;
            base.ResumeLayout();
        }

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

        public NewsFeed()
        {
            InitializeComponent();
            _newsItemHeightAnimator.Interval = HeightAnimator.HALF_SECOND_ANIMATION_SPEED;
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
                    newsItem.Height = HeightAnimator.IncreaseWithEase(newsItem.Height, idealHeight);
                }
            }
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
            if (args == null || args.Person == null) return;
            if (args.ShouldUpdateOldInProgressNewsItem)
                TryToFindAndUpdateOldInProgressNewsItem(args);
            else
                AddNewsItemToPanel(args);
        }

        private void TryToFindAndUpdateOldInProgressNewsItem(NewNewsItemEventArgs args)
        {
            var oldBuild = GetNewsItemControls().FirstOrDefault(i => i.BuildId == args.BuildId);
            if (oldBuild != null)
                oldBuild.UpdateState(args);
            else
                AddNewsItemToPanel(args);
        }

        private void AddNewsItemToPanel(NewNewsItemEventArgs args)
        {
            _noNews.Visible = false;
            var newsItem = new NewsItem(args)
            {
                Dock = DockStyle.Top,
            };
            newsItem.OnUserClicked += NewsItemOnOnUserClicked;
            newsItem.MouseEnter += NewsItemOnMouseEnter;
            _newsItemsPanel.Controls.Add(newsItem);
            _newsItemsPanel.Controls.SetChildIndex(newsItem, 0);
            newsItem.Height = _doAnimations ? 1 : newsItem.GetIdealHeight();
            _newsItemsToOpen.Add(newsItem);
            _newsItemHeightAnimator.Start();
        }

        private void NewsItemOnMouseEnter(object sender, EventArgs eventArgs)
        {
            EnableMouseScrollWheel();
        }

        private void EnableMouseScrollWheel()
        {
            if (Parent.ContainsFocus) 
                _newsItemsPanel.Focus();
        }

        private void NewsItemOnOnUserClicked(object sender, UserClickedArgs args)
        {
            InvokeOnOnUserClicked(args);
        }

        private void ClearNewsClick(object sender, EventArgs e)
        {
            _newsItemsPanel.ClearAndDispose();
            _noNews.Visible = true;
        }

        private void NewsFeedMouseEnter(object sender, EventArgs e)
        {
            EnableMouseScrollWheel();
        }

        private void NewsItemsPanelMouseEnter(object sender, EventArgs e)
        {
            EnableMouseScrollWheel();
        }

        public void RefreshDisplayNames(UserDisplayNameChangedArgs args)
        {
            GetNewsItemControls()
                .Where(i => i.RawName == args.RawUserName)
                .ToList()
                .ForEach(i => i.DisplayName = args.NewDisplayName);
        }
    }
}
