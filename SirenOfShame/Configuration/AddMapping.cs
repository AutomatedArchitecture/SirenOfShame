﻿using System.Linq;
using System.Windows.Forms;
using SirenOfShame.Lib.Settings;

namespace SirenOfShame.Configuration
{
    public partial class AddMapping : Form
    {
        private readonly SirenOfShameSettings _settings;

        public AddMapping(SirenOfShameSettings settings, string whenISeeDefaultRawName = null)
        {
            _settings = settings;
            InitializeComponent();

            var validPeople = _settings.People.Where(i => !string.IsNullOrEmpty(i.RawName));
            foreach (var personSetting in validPeople)
            {
                _whenISee.Items.Add(personSetting.RawName);
                _pretendItsActually.Items.Add(personSetting.RawName);
            }
            foreach (var mapping in _settings.UserMappings)
            {
                _whenISee.Items.Remove(mapping.WhenISee);
                _pretendItsActually.Items.Remove(mapping.WhenISee);
            }

            if (!string.IsNullOrEmpty(whenISeeDefaultRawName))
            {
                _whenISee.SelectedItem = whenISeeDefaultRawName;
            }
        }

        private void AddClick(object sender, System.EventArgs e)
        {
            var pretendItsActually = (string)_pretendItsActually.SelectedItem;
            var whenISee = (string)_whenISee.SelectedItem;
            if (!string.IsNullOrEmpty(whenISee) && !string.IsNullOrEmpty(pretendItsActually) && whenISee != pretendItsActually)
            {
                var whenISeePerson = _settings.FindPersonByRawName(whenISee);
                whenISeePerson.Hidden = true;
                _settings.UserMappings.Add(new UserMapping
                {
                    WhenISee = whenISee,
                    PretendItsActually = pretendItsActually
                });
                _whenISee.Items.Remove(whenISee);
                _pretendItsActually.Items.Remove(whenISee);
                _settings.Save();
            }
            Close();
            Dispose();
        }
    }
}
