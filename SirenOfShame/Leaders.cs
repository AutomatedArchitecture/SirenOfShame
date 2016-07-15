using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SirenOfShame.Configuration;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Properties;

namespace SirenOfShame
{
    public partial class Leaders : UserControl
    {
        public Leaders()
        {
            InitializeComponent();
        }

        public event UserSelected OnUserSelected;
        private SirenOfShameSettings _settings;

        private void InvokeOnUserSelected(string rawName)
        {
            var userSelected = OnUserSelected;
            if (userSelected != null)
            {
                userSelected(this, new UserSelectedArgs { RawName = rawName });
            }
        }

        private IEnumerable<UserPanel> GetUserPanels()
        {
            return _usersPanel.Controls.Cast<UserPanel>();
        }
        
        public void RefreshUserStats()
        {
            var userPanelsAndTheirPerson = from panel in GetUserPanels()
                                           join person in _settings.People on panel.RawName equals person.RawName
                                           orderby person.GetReputation() descending
                                           select new {panel, person};

            int i = 0;
            foreach (var panelAndPerson in userPanelsAndTheirPerson)
            {
                panelAndPerson.panel.RefreshStats(panelAndPerson.person);
                _usersPanel.Controls.SetChildIndex(panelAndPerson.panel, i);
                i++;
            }
            UpdateRanks();
        }

        public void Initialize(SirenOfShameSettings settings, ImageList avatarImageList)
        {
            _settings = settings;

            var peopleByReputation = settings.People.OrderByDescending(i => i.GetReputation());
            foreach (var person in peopleByReputation)
            {
                AddUserPanel(avatarImageList, person);
            }
            UpdateRanks(true);
        }

        private void AddUserPanel(ImageList avatarImageList, PersonSetting person)
        {
            UserPanel userPanel = new UserPanel(person, avatarImageList)
            {
                Cursor = Cursors.Hand,
                Visible = !person.Hidden
            };

            userPanel.AddMouseUpToAllControls(UserPanelMouseUp);
            userPanel.AddMouseEnterToAllControls(UserPanelMouseEnter);

            _usersPanel.Controls.Add(userPanel);
        }

        private void UpdateRanks(bool all = false)
        {
            var rank = 1;
            foreach (var userPanel in GetUserPanels().Where(x => all || x.Visible))
            {
                userPanel.Rank = rank++;
            }
        }

        private string _selectedRawName;

        private void UserPanelMouseUp(object sender, MouseEventArgs e)
        {
            UserPanel userPanel = TraverseParentsUntilUserPanel((Control) sender);
            if (userPanel == null) return;
            
            if (e.Button == MouseButtons.Right)
            {
                _selectedRawName = userPanel.RawName;
                _userMenu.Show((Control)sender, e.Location);
                var person = _settings.FindPersonByRawName(_selectedRawName);
                _hiddenButton.Checked = person.Hidden;
            }
            
            if (e.Button == MouseButtons.Left)
            {
                InvokeOnUserSelected(userPanel.RawName);
            }
        }

        private UserPanel TraverseParentsUntilUserPanel(Control sender)
        {
            var userPanel = sender as UserPanel;
            if (userPanel != null) return userPanel;
            if (sender.Parent == null) return null;
            return TraverseParentsUntilUserPanel(sender.Parent);
        }

        void UserPanelMouseEnter(object sender, EventArgs e)
        {
            EnableMouseScrollWheel();
        }

        private void EnableMouseScrollWheel()
        {
            if (Parent.ContainsFocus) 
                _usersPanel.Focus();
        }

        public void RefreshAvatar(PersonSetting personSetting, ImageList imageList)
        {
            var userPanel = GetUserPanel(personSetting.RawName);
            if (userPanel == null) return;
            userPanel.RefreshAvatar(personSetting, imageList);
        }

        private UserPanel GetUserPanel(string rawName)
        {
            return GetUserPanels().FirstOrDefault(i => i.RawName == rawName);
        }

        private void UsersPanelMouseEnter(object sender, EventArgs e)
        {
            EnableMouseScrollWheel();
        }

        public void RefreshDisplayNames(UserDisplayNameChangedArgs args)
        {
            var userPanel = GetUserPanel(args.RawUserName);
            if (userPanel == null) return;
            userPanel.DisplayName = args.NewDisplayName;
        }

        private bool _showAllUsers = false;
        
        public void SetShowAllUsers(bool showAllUsers)
        {
            _showAllUsers = showAllUsers;
            RefreshUserPanelVisibility();
        }

        public void RefreshUserPanelVisibility()
        {
            foreach (var userPanel in GetUserPanels())
            {
                var rawName = userPanel.RawName;
                var person = _settings.FindPersonByRawName(rawName);
                userPanel.Visible = _showAllUsers || !person.Hidden;
            }
            UpdateRanks();
        }

        private void WithSelectedUser(Action<PersonSetting> action)
        {
            if (string.IsNullOrEmpty(_selectedRawName)) return;
            PersonSetting selectedPerson = _settings.FindPersonByRawName(_selectedRawName);
            if (selectedPerson == null) return;
            action(selectedPerson);
        }
        
        private void HiddenButtonClick(object sender, EventArgs e)
        {
            WithSelectedUser(selectedPerson =>
            {
                selectedPerson.Hidden = !selectedPerson.Hidden;
                RefreshUserPanelVisibility();
            });
        }

        private void UserMenuOpening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            WithSelectedUser(selectedPerson =>
            {
                _isADuplicate.Text = selectedPerson.RawName + Resources.Is_A_Duplicate;
            });
        }

        private void IsADuplicateClick(object sender, EventArgs e)
        {
            WithSelectedUser(selectedPerson =>
            {
                AddMapping addMapping = new AddMapping(_settings, selectedPerson.RawName);
                addMapping.ShowDialog(this);
                var userMappings = new UserMappings(_settings);
                userMappings.ShowDialog(this);
                RefreshUserPanelVisibility();
            });
        }

        public void NewUser(ImageList avatarImageList, string rawName)
        {
            var person = _settings.FindAddPerson(rawName);
            AddUserPanel(avatarImageList, person);
            UpdateRanks();
        }
    }

    public delegate void UserSelected(object sender, UserSelectedArgs args);

    public class UserSelectedArgs
    {
        public string RawName { get; set; }
    }
}
