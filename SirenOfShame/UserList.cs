using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame
{
    public partial class UserList : UserControl
    {
        public UserList()
        {
            InitializeComponent();
            
            _flashListViewItemTimer.Interval = 100;
            _flashListViewItemTimer.Tick += FlashListViewItemTimerTick;
        }

        public event UserSelected OnUserSelected;
        public event UserDisplayNameChanged OnUserDisplayNameChanged;
        public SirenOfShameSettings Settings { get; set; }
        readonly Timer _flashListViewItemTimer = new Timer();
        private readonly List<ListViewItem.ListViewSubItem> _listViewItemsToFlash = new List<ListViewItem.ListViewSubItem>();

        private void InvokeOnUserDisplayNameChanged(string rawName, string displayName)
        {
            var onUserDisplayNameChanged = OnUserDisplayNameChanged;
            if (onUserDisplayNameChanged != null)
            {
                onUserDisplayNameChanged(this, new UserDisplayNameChangedArgs { RawUserName = rawName, NewDisplayName = displayName });
            }
        }

        private void InvokeOnUserSelected(string rawName)
        {
            var userSelected = OnUserSelected;
            if (userSelected != null)
            {
                userSelected(this, new UserSelectedArgs { RawName = rawName });
            }
        }

        public void SelectUser(PersonSetting person)
        {
            foreach (ListViewItem listItem in _users.Items)
            {
                if ((string)listItem.Tag == person.RawName)
                    listItem.Selected = true;
            }
        }

        private static string GetReputation(int reputation, BuildStatus newlyChangedBuildStatus)
        {
            string reputationStr = reputation.ToString(CultureInfo.InvariantCulture);
            if (newlyChangedBuildStatus == null) return reputationStr;
            string delta = newlyChangedBuildStatus.BuildStatusEnum == BuildStatusEnum.Broken ? "-4" : "+1";
            return string.Format("{0} ({1})", reputationStr, delta);
        }

        private void FlashListViewSubItem(ListViewItem.ListViewSubItem lvi)
        {
            lvi.ForeColor = Color.Red;
            _listViewItemsToFlash.Add(lvi);
            if (!_flashListViewItemTimer.Enabled)
            {
                _flashListViewItemTimer.Start();
            }
        }

        private static ListViewItem.ListViewSubItem AddSubItem(ListViewItem lvi, string name, string value)
        {
            var subItem = new ListViewItem.ListViewSubItem(lvi, value)
            {
                Name = name
            };
            lvi.SubItems.Add(subItem);
            return subItem;
        }

        public void RefreshUserStats(IList<BuildStatus> changedBuildStatuses)
        {
            _users.Items.Clear();
            var filteredPeople = _showAllPeople ? Settings.People : Settings.VisiblePeople;
            var personSettings = filteredPeople
                .Select(i => new { i.RawName, i.DisplayName, Reputation = i.GetReputation() })
                .OrderByDescending(i => i.Reputation);
            foreach (var person in personSettings)
            {
                BuildStatus newlyChangedBuildStatus = changedBuildStatuses == null ? null : changedBuildStatuses.FirstOrDefault(i => i.RequestedBy == person.RawName);
                bool newlyChanged = newlyChangedBuildStatus != null;

                ListViewItem lvi = new ListViewItem(person.DisplayName) { UseItemStyleForSubItems = false };
                string reputation = GetReputation(person.Reputation, newlyChangedBuildStatus);
                ListViewItem.ListViewSubItem subItem = AddSubItem(lvi, "Reputation", reputation);
                if (newlyChanged)
                    FlashListViewSubItem(subItem);
                lvi.Tag = person.RawName;
                _users.Items.Add(lvi);
            }
            _flashListViewItemTimer.Start();
        }

        public PersonSetting GetActivePerson()
        {
            var listViewItem = _users.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
            if (listViewItem == null) return null;

            var rawName = (string)listViewItem.Tag;
            return Settings.People.FirstOrDefault(u => u.RawName == rawName);
        }

        private void UsersAfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            var activePerson = GetActivePerson();
            if (activePerson == null) return;

            activePerson.DisplayName = e.Label;
            Settings.Save();

            InvokeOnUserDisplayNameChanged(activePerson.RawName, activePerson.DisplayName);
        }

        private void UsersMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            ListViewItem lvi = GetSelectedUser();
            var anyUserSelected = lvi != null;
            _editUserName.Visible = anyUserSelected;
            _hideUser.Visible = anyUserSelected;
            if (anyUserSelected)
            {
                var person = GetActivePerson();
                _hideUser.Text = person.Hidden ? "Show" : "Hide";
            }
            _userMenu.Show(_users, e.X, e.Y);
        }

        public ListViewItem GetSelectedUser()
        {
            return _users.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
        }

        public void DeselectAllUsers()
        {
            // sometimes the view user is displayed but the selected items gets unselected so here's a little hack to ensure we hide the user page
            if (_users.SelectedItems.Count == 0 && _users.Items.Count >= 1)
            {
                _users.Items[0].Selected = true;
            }
            _users.SelectedItems.Clear();
        }

        private void UsersSelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedUser = _users.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
            InvokeOnUserSelected(selectedUser == null ? null : (string) selectedUser.Tag);
        }

        private void EditUserNameClick(object sender, EventArgs e)
        {
            ListViewItem lvi = GetSelectedUser();
            if (lvi != null)
                lvi.BeginEdit();
        }

        private void HideUserClick(object sender, EventArgs e)
        {
            var activePerson = GetActivePerson();
            if (activePerson == null) return;
            activePerson.Hidden = !activePerson.Hidden;
            Settings.Save();
            RefreshUserStats(null);
        }

        private bool _showAllPeople = false;

        private void ShowAllUsersCheckedChanged(object sender, EventArgs e)
        {
            _showAllPeople = _showAllUsers.Checked;
            RefreshUserStats(null);
        }

        private void FlashListViewItemTimerTick(object sender, EventArgs e)
        {
            if (!_listViewItemsToFlash.Any() || _listViewItemsToFlash.All(i => i.ForeColor.R == 0))
            {
                _listViewItemsToFlash.Clear();
                _flashListViewItemTimer.Stop();
            }

            foreach (var listViewSubItem in _listViewItemsToFlash)
            {
                var existingColor = listViewSubItem.ForeColor;
                const int amountToDecrement = 3;
                var newRed = Math.Max(0, existingColor.R - amountToDecrement);
                var newGreen = Math.Max(0, existingColor.G - amountToDecrement);
                var newBlue = Math.Max(0, existingColor.B - amountToDecrement);
                listViewSubItem.ForeColor = Color.FromArgb(newRed, newGreen, newBlue);
                bool isBlack = newRed == 0 && newGreen == 0 && newBlue == 0;
                bool containsDelta = listViewSubItem.Text.Contains(" ");
                if (isBlack && containsDelta)
                {
                    listViewSubItem.Text = listViewSubItem.Text.Split(' ').First();
                }
            }
        }
        

    }

    public delegate void UserDisplayNameChanged(object sender, UserDisplayNameChangedArgs args);

    public class UserDisplayNameChangedArgs
    {
        public string RawUserName { get; set; }
        public string NewDisplayName { get; set; }
    }

    public delegate void UserSelected(object sender, UserSelectedArgs args);

    public class UserSelectedArgs
    {
        public string RawName { get; set; }
    }
}
