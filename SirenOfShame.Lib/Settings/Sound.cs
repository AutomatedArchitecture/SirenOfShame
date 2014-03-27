using System;
using System.Windows.Forms;
using System.Xml.Serialization;
using SirenOfShame.Lib.Services;

namespace SirenOfShame.Lib.Settings
{
    [Serializable]
    public class Sound
    {
        public Sound() {  }

        public Sound(string internalResourceName)
        {
            Location = internalResourceName;
            DisplayName = SoundService.InternalAudioLocationToDisplayName(internalResourceName);
        }

        public string Location { get; set; }
        public string DisplayName { get; set; }

        public ListViewItem AsListViewItem()
        {
            var listViewItem = new ListViewItem(DisplayName)
            {
                Tag = this
            };
            return listViewItem;
        }
    }
}