using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SirenOfShame.Lib.Settings;
using SirenOfShame.Lib.Watcher;

namespace SirenOfShame
{
    public partial class Leaders : UserControl
    {
        public Leaders()
        {
            InitializeComponent();
        }

        public event UserSelected OnUserSelected;
        public SirenOfShameSettings Settings { private get; set; }

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
        
        public void RefreshUserStats(IList<BuildStatus> changedBuildStatuses)
        {
            var userPanelsAndTheirPerson = from panel in GetUserPanels()
                                           join person in Settings.People on panel.RawName equals person.RawName
                                           orderby person.GetReputation() descending
                                           select new {panel, person};

            int i = 0;
            foreach (var panelAndPerson in userPanelsAndTheirPerson)
            {
                panelAndPerson.panel.RefreshStats(panelAndPerson.person);
                _usersPanel.Controls.SetChildIndex(panelAndPerson.panel, i);
                i++;
            }
            
        }

        public void Initialize(SirenOfShameSettings settings, ImageList avatarImageList)
        {
            Settings = settings;

            var peopleByReputation = settings.People.OrderByDescending(i => i.GetReputation());
            foreach (var person in peopleByReputation)
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
                var person = Settings.FindPersonByRawName(_selectedRawName);
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

        public void ChangeUserAvatarId(string rawName, int newImageIndex)
        {
            var userPanel = GetUserPanel(rawName);
            if (userPanel == null) return;
            userPanel.AvatarId = newImageIndex;
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

        private void RefreshUserPanelVisibility()
        {
            foreach (var userPanel in GetUserPanels())
            {
                var rawName = userPanel.RawName;
                var person = Settings.FindPersonByRawName(rawName);
                userPanel.Visible = _showAllUsers || !person.Hidden;
            }
        }

        private void HiddenButtonClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedRawName)) return;
            var selectedPerson = Settings.FindPersonByRawName(_selectedRawName);
            selectedPerson.Hidden = !selectedPerson.Hidden;
            RefreshUserPanelVisibility();
        }
    }

    public delegate void UserSelected(object sender, UserSelectedArgs args);

    public class UserSelectedArgs
    {
        public string RawName { get; set; }
    }
}
