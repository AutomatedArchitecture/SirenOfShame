using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using SirenOfShame.Lib;
using SirenOfShame.Lib.Helpers;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame
{
    public partial class NewsFeed : UserControlBase
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

        public NewsFeed()
        {
            InitializeComponent();
            ResetFunnelVisibility();
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
            if (!IncludeInFilter(args)) return;
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
            var newsItem = AddNewsItemToPanel();
            newsItem.Initialize(args);
            _newsItemsPanel.Controls.SetChildIndex(newsItem, 0);
            newsItem.Height = 1;
            _newsItemsToOpen.Add(newsItem);
            _newsItemHeightAnimator.Start();
        }

        private NewsItem AddNewsItemToPanel()
        {
            _noNews.Visible = false;
            var newsItem = new NewsItem
            {
                Dock = DockStyle.Top,
            };
            newsItem.OnUserClicked += NewsItemOnOnUserClicked;
            newsItem.MouseEnter += NewsItemOnMouseEnter;
            _newsItemsPanel.Controls.Add(newsItem);
            return newsItem;
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

        private string _filterPerson = null;
        private string _filterBuildId = null;

        public void AddBuildFilter(SirenOfShameSettings settings, string buildId, ImageList avatarImageList)
        {
            ClearAllFilters();
            _filterBuildId = buildId;
            ApplyFilters(settings, avatarImageList);
        }

        public void AddUserFilter(SirenOfShameSettings settings, PersonSetting selectedPerson, ImageList avatarImageList)
        {
            ClearAllFilters();
            _filterPerson = selectedPerson.RawName;
            ApplyFilters(settings, avatarImageList);
        }

        private void ApplyFilters(SirenOfShameSettings settings, ImageList avatarImageList)
        {
            _loading.Visible = true;
            _filterButton.Visible = false;

            new SosDb().GetMostRecentNewsItems(settings,
                recentEvent => Invoke(() =>
                {
                    _loading.Visible = false;
                    ResetFunnelVisibility();
                    _noNews.Visible = false;
                    var recentEventsByPerson = recentEvent
                        .Where(IncludeInFilter)
                        .GroupBy(i => i.BuildId)
                        .Take(RulesEngine.NEWS_ITEMS_TO_GET_ON_STARTUP)
                        .ToList();
                    ControlHelpers.SuspendLayout(this, () => ReinitializeNewsItems(recentEventsByPerson, avatarImageList));
                }));
        }

        private bool IncludeInFilter(NewNewsItemEventArgs i)
        {
            return IncludeInFilter(i, _filterPerson, _filterBuildId);
        }

        public static bool IncludeInFilter(NewNewsItemEventArgs newsItem, string filterPerson, string filterBuildId)
        {
            if (filterPerson == null && filterBuildId == null) return true;
            if (filterPerson != null) return newsItem.Person.RawName == filterPerson;
            return newsItem.IsSosOnlineEvent || newsItem.Project == filterBuildId;
        }

        private void ResetFunnelVisibility()
        {
            _filterButton.Visible = _filterPerson != null;
        }

        public void ClearFilter(SirenOfShameSettings settings, ImageList avatarImageList)
        {
            ClearAllFilters();
            ApplyFilters(settings, avatarImageList);
        }

        private void ClearAllFilters()
        {
            _filterPerson = null;
            _filterBuildId = null;
        }

        private void ReinitializeNewsItems(List<IGrouping<string, NewNewsItemEventArgs>> newsItems, ImageList avatarImageList)
        {
            EnsureWeHaveXNewsItemControls(newsItems.Count);
            var newsItemControls = GetNewsItemControls().ToList();
            for (int i = 0; i < newsItems.Count; i++)
            {
                var newsItemGrouping = newsItems[i];
                var newsItemsReversed = newsItemGrouping.Reverse().ToList();
                var firstNewsItem = newsItemsReversed.First();
                firstNewsItem.AvatarImageList = avatarImageList;
                var firstNewsItemControl = newsItemControls[i];
                firstNewsItemControl.Initialize(firstNewsItem);
                var subsequentNewsItems = newsItemsReversed.Skip(1);
                foreach (var newsItem in subsequentNewsItems)
                {
                    firstNewsItemControl.UpdateState(newsItem);
                }
            }
        }

        private void EnsureWeHaveXNewsItemControls(int newCount)
        {
            List<NewsItem> newsItems = GetNewsItemControls().ToList();
            var oldCount = newsItems.Count();
            bool weNeedToDeleteNewsItemControls = newCount < oldCount;
            if (weNeedToDeleteNewsItemControls)
            {
                int numberOfNewsItemsToDelete = oldCount - newCount;
                DeleteXNewsItemControls(newsItems, numberOfNewsItemsToDelete);
            }
            bool weNeedToAddNewsItemControls = newCount > oldCount;
            if (weNeedToAddNewsItemControls)
            {
                int numberOfNewsItemsToAdd = newCount - oldCount;
                for (int i = 0; i < numberOfNewsItemsToAdd; i++)
                {
                    AddNewsItemToPanel();
                }
            }
        }

        private void DeleteXNewsItemControls(IEnumerable<NewsItem> newsItems, int numberOfNewsItemsToDelete)
        {
            IEnumerable<NewsItem> newsItemsToDelete = newsItems.Take(numberOfNewsItemsToDelete);
            foreach (var newsItem in newsItemsToDelete)
            {
                _newsItemsPanel.Controls.Remove(newsItem);
                newsItem.Dispose();
            }
        }
    }
}
