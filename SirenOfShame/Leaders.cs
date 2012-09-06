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
                                          };
                userPanel.MouseEnter += UserPanelMouseEnter;
                userPanel.Click += UserPanelClick;
                _usersPanel.Controls.Add(userPanel);
            }
        }

        void UserPanelClick(object sender, EventArgs e)
        {
            InvokeOnUserSelected(((UserPanel)sender).RawName);
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
    }

    public delegate void UserSelected(object sender, UserSelectedArgs args);

    public class UserSelectedArgs
    {
        public string RawName { get; set; }
    }
}
